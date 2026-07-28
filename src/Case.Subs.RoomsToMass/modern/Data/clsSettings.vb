Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Windows.Forms
Imports Autodesk.Revit.Creation
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.DB.Architecture
Imports Autodesk.Revit.UI
Imports Newtonsoft.Json

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData

#Region "Public Properties - Application, Document, and Versioning"

    ''' <summary>
    ''' Active Document
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Doc As Autodesk.Revit.DB.Document
      Get
        Try
          Return _cmd.Application.ActiveUIDocument.Document
        Catch
        End Try
        Return Nothing
      End Get
    End Property

    ''' <summary>
    ''' Document Path
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DocName As String
      Get
        Try
          If Doc.IsWorkshared = True Then
            If Not String.IsNullOrEmpty(Doc.GetWorksharingCentralModelPath.CentralServerPath) Then
              Return Doc.GetWorksharingCentralModelPath.CentralServerPath
            Else
              Return "Detached Model"
            End If
          Else
            Return Doc.PathName
          End If
        Catch
        End Try
        Return ""
      End Get
    End Property

    ''' <summary>
    ''' UIApp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UiApp As UIApplication
      Get
        Try
          Return _cmd.Application
        Catch
        End Try
        Return Nothing
      End Get
    End Property

    ''' <summary>
    ''' Family Template Path
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FamilyTemplatePath As String
      Get
        Try
          Return Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location) & "\Mass.rft"
        Catch
        End Try
        Return ""
      End Get
    End Property

#End Region

#Region "Public Properties - Elements and Data"

    ''' <summary>
    ''' Family Document
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FamilyDoc As Autodesk.Revit.DB.Document

    ''' <summary>
    ''' Mass Parameters
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MassParameters As SortedDictionary(Of String, clsParameterDescription)

    ''' <summary>
    ''' Room Parameters
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RoomParameters As SortedDictionary(Of String, clsParameterDescription)

    ''' <summary>
    ''' Rooms
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Rooms As SortedDictionary(Of String, clsRoomFamily)

    ''' <summary>
    ''' Room ElementID's
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MassRooms As SortedDictionary(Of String, Element)

    ''' <summary>
    ''' Room Types
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RoomTypes As SortedDictionary(Of String, ElementId)

    ''' <summary>
    ''' Family Factory
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FamFactory As FamilyItemFactory

    ''' <summary>
    ''' Family Category
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Familycategory As Category
      Get
        Try
          Return FamilyDoc.Settings.Categories.Item(BuiltInCategory.OST_Mass)
        Catch ex As Exception
          Return Nothing
        End Try
      End Get
    End Property

#End Region

#Region "Public Properties - Saved Configurations"

    ''' <summary>
    ''' Saved Settings Path
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SavedSettingsPath As String
      Get
        Try
          Dim m_path As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                              "CASE",
                                              "Revit Rooms to Mass")
          If Not Directory.Exists(m_path) Then
            Directory.CreateDirectory(m_path)
          End If
          If Not Directory.Exists(m_path) Then m_path = ""
          Return Path.Combine(m_path, "Settings.ini")
        Catch
        End Try
        Return ""
      End Get
    End Property

    ''' <summary>
    ''' Saved Settings
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ModelConfig As List(Of clsConfig)

#End Region

    ''' <summary>
    ''' Settings Master
    ''' </summary>
    ''' <param name="cmdData"></param>
    ''' <remarks></remarks>
    Public Sub New(cmdData As ExternalCommandData)

      ' Widen Scope
      _cmd = cmdData

      ' Setup
      DoSetup()

    End Sub

#Region "Private Members"

    ''' <summary>
    ''' General Setup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoSetup()

      ' Fresh Lists
      ModelConfig = New List(Of clsConfig)
      RoomParameters = New SortedDictionary(Of String, clsParameterDescription)
      Rooms = New SortedDictionary(Of String, clsRoomFamily)
      RoomTypes = New SortedDictionary(Of String, ElementId)

      Try

        ' Settings File Data
        If File.Exists(SavedSettingsPath) Then

          ' Read it
          Using r As New StreamReader(SavedSettingsPath)
            Dim m_json As String = r.ReadLine
            If Not String.IsNullOrEmpty(m_json) Then
              ModelConfig = JsonConvert.DeserializeObject(Of List(Of clsConfig))(m_json)
            End If
          End Using

        End If

      Catch
      End Try

      Try

        ' LINQ Query - Rooms
        Dim m_rms = From e In New FilteredElementCollector(Doc) _
              .OfCategory(BuiltInCategory.OST_Rooms)
              Let r = TryCast(e, Room)
              Where r.Area > 1
              Where r.Location IsNot Nothing
              Select r

        ' To Helpers
        If Not m_rms Is Nothing Then
          For Each r In m_rms
            Rooms.Add(r.UniqueId.ToString.ToLower, New clsRoomFamily(r))
          Next
        End If

      Catch
      End Try

      ' Room Parameters
      GetRoomParameters()

      ' Mass Rooms
      GetRoomMasses()

    End Sub

    ''' <summary>
    ''' Update the List of Mass Parameters
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetMassParameters()

      ' Fresh List
      MassParameters = New SortedDictionary(Of String, clsParameterDescription)

      Try

        ' Get the Room
        For Each x In MassRooms.Values

          ' Get All Qualifying Parameters
          For Each p As Parameter In x.Parameters

            Try
              If p.IsReadOnly = True Then Continue For
              If p.StorageType = StorageType.None Then Continue For
              If p.StorageType = StorageType.ElementId Then Continue For

              If Not MassParameters.ContainsKey(p.Definition.Name) Then
                MassParameters.Add(p.Definition.Name, New clsParameterDescription(p.StorageType, p.Definition.Name))
              End If

            Catch
            End Try

          Next

          ' Only Need One
          Exit For

        Next

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Update the List of Room Parameters
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetRoomParameters()

      ' Fresh List
      RoomParameters = New SortedDictionary(Of String, clsParameterDescription)

      Try

        ' Get the Room
        For Each x As clsRoomFamily In Rooms.Values

          ' Get All Qualifying Parameters
          For Each p As Parameter In x.RoomElement.Parameters

            Try
              If p.StorageType = StorageType.None Then Continue For
              If p.StorageType = StorageType.ElementId Then Continue For

              If Not RoomParameters.ContainsKey(p.Definition.Name) Then
                RoomParameters.Add(p.Definition.Name, New clsParameterDescription(p.StorageType, p.Definition.Name))
              End If

            Catch
            End Try

          Next

          ' Only Need One
          Exit For

        Next

      Catch
      End Try

    End Sub

#End Region

#Region "Public Members"

    ''' <summary>
    ''' Update Masses Parameters with Associated Rooms
    ''' </summary>
    ''' <param name="m"></param>
    ''' <remarks></remarks>
    Public Sub UpdateMassParameters(m As List(Of clsMapping), p As ProgressBar)

      ' Progress
      With p
        .Minimum = 0
        .Maximum = MassRooms.Count
        .Value = 0
      End With

      ' Process Each Mass
      For Each x In MassRooms.Values

        Try
          p.Increment(1)
        Catch
        End Try

        ' The Room
        If Not Rooms.ContainsKey(x.Name.ToString) Then Continue For
        Dim m_rm As clsRoomFamily = Rooms(x.Name.ToString)
        If Not m_rm Is Nothing Then

          ' Start a New Transaction
          Using t As New Transaction(Doc, "Mass Parameter Updates")
            If t.Start = TransactionStatus.Started Then

              Try

                ' Update the Parameters
                For Each map As clsMapping In m
                  If map.DestinationName.ToLower.Contains("ignore") Then Continue For

                  Try

                    ' Get the Source Parameter
                    Dim m_pSource As Parameter = m_rm.RoomElement.LookupParameter(map.SourceName)
                    If Not m_pSource Is Nothing Then

                      ' Get the Destination Parameter
                      Dim m_pDest As Parameter = x.LookupParameter(map.DestinationName)
                      If Not m_pDest Is Nothing Then

                        ' As String?
                        Select Case m_pSource.StorageType

                          Case StorageType.Double
                            ' From Double
                            Select Case m_pDest.StorageType
                              Case StorageType.Double
                                m_pDest.Set(m_pSource.AsDouble)
                              Case StorageType.String
                                m_pDest.Set(m_pSource.AsValueString)
                            End Select

                          Case StorageType.Integer
                            ' From Integer
                            Select Case m_pDest.StorageType
                              Case StorageType.Integer
                                m_pDest.Set(m_pSource.AsInteger)
                              Case StorageType.String
                                m_pDest.Set(m_pSource.AsValueString)
                            End Select

                          Case StorageType.String
                            ' From String
                            Select Case m_pDest.StorageType
                              Case StorageType.String
                                m_pDest.Set(m_pSource.AsString)
                            End Select

                          Case StorageType.ElementId
                            ' Not supported

                        End Select

                      End If

                    End If

                  Catch
                  End Try

                Next

                ' Success
                t.Commit()

              Catch
              End Try

            End If
          End Using

        End If

      Next

    End Sub

    ''' <summary>
    ''' Get Room Mass Elements
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetRoomMasses()

      ' Fresh List
      MassRooms = New SortedDictionary(Of String, Element)

      Try

        ' LINQ Query
        Dim m_f = From e In New FilteredElementCollector(Doc) _
              .OfClass(GetType(FamilyInstance)) _
              .OfCategory(BuiltInCategory.OST_Mass)

        ' Verify Associations
        If Not m_f Is Nothing Then
          For Each x In m_f

            ' Room Masses are named the same as the room GUID
            If Rooms.ContainsKey(x.Name.ToString.ToLower) Then
              Try
                Dim m_t As FamilySymbol = TryCast(Doc.GetElement(x.GetTypeId), FamilySymbol)
                If Not m_t Is Nothing Then
                  RoomTypes.Add(m_t.Family.Id.ToString, m_t.Family.Id)
                End If
              Catch
              End Try
              MassRooms.Add(x.Id.ToString, x)
            End If

          Next
        End If

      Catch
      End Try

      ' Parameters
      GetMassParameters()

    End Sub

    ''' <summary>
    ''' Get a Category By Name
    ''' </summary>
    ''' <param name="catName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFamilyCategory(catName As String) As Category

      ' Categories
      For Each c As Category In Doc.Settings.Categories
        If c.Name.ToLower = catName.ToLower Then
          Return c
        End If
      Next

      ' Failure
      Return Nothing

    End Function

    ''' <summary>
    ''' Delete the Old Families
    ''' </summary>
    ''' <remarks></remarks>
    Public Function DeleteOldMassInstances() As Boolean

      ' Start a New Transaction
      Using t As New Transaction(Doc, "Deleting Mass Room Instances")
        If t.Start = TransactionStatus.Started Then

          Try

            ' List of ElementID
            Dim m_eids As List(Of ElementId) = (From x In MassRooms.Values Select x.Id).ToList()

            ' Delete them
            Doc.Delete(m_eids)
            t.Commit()

            ' Success
            Return True

          Catch
          End Try

        End If
      End Using

      ' Failure
      Return False

    End Function

    ''' <summary>
    ''' Delete the Old Families
    ''' </summary>
    ''' <remarks></remarks>
    Public Function DeleteOldMassTypes() As Boolean

      ' Start a New Transaction
      Using t As New Transaction(Doc, "Deleting Mass Room Types")
        If t.Start = TransactionStatus.Started Then

          Try

            ' Delete them
            Doc.Delete(RoomTypes.Values)
            t.Commit()

            ' Success
            Return True

          Catch
          End Try

        End If
      End Using

      ' Failure
      Return False

    End Function

    ''' <summary>
    ''' Close all Matching Document Families
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CloseAllFamilyDocuments()

      ' close Families
      For Each d As Autodesk.Revit.DB.Document In _cmd.Application.Application.Documents
        Try
          If d.PathName.ToLower.EndsWith("rfa") Then
            d.Close(True)
          End If
        Catch
        End Try
      Next

    End Sub

#End Region

  End Class
End Namespace
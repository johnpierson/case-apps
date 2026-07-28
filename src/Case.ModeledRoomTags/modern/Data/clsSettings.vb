Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports System.Reflection
Imports Autodesk.Revit.DB.Architecture
Imports [Case].ModeledRoomTags.Entry

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData
    Private _eSet As ElementSet

#Region "Public Properties"

    ''' <summary>
    ''' Tag Family File Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TagFamilyName As String
      Get
        Return "3D_RoomTag"
      End Get
    End Property

    ''' <summary>
    ''' UI Application
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UiApp As Autodesk.Revit.UI.UIApplication
      Get
        Try
          Return _cmd.Application
        Catch ex As Exception
          Return Nothing
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Active Document
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Doc As Document
      Get
        Try
          Return _cmd.Application.ActiveUIDocument.Document
        Catch ex As Exception
          Return Nothing
        End Try
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
        Catch ex As Exception
          Return ""
        End Try
      End Get
    End Property

    ''' <summary>
    ''' App Version
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AppVersion As String
      Get
        Return Assembly.GetExecutingAssembly.GetName.Version.ToString
      End Get
    End Property

    Public Property TagSymbol As FamilySymbol
    Public Property MatchingInstances As List(Of ElementId)
    Public Property RoomTags As List(Of clsRoomTagInfo)

#End Region

    ''' <summary>
    ''' Constructor 
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <param name="eSet"></param>
    ''' <remarks></remarks>
    Public Sub New(cmd As ExternalCommandData, eSet As ElementSet)

      ' Widen Scope
      _cmd = cmd
      _eSet = eSet

    End Sub

#Region "Private Members"

    ''' <summary>
    ''' Get the Family Instances
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetFamilyInstances()

      Try

        ' Make sure we have the Family and then also get the instances
        MatchingInstances = New List(Of ElementId)

        ' Instance Elements
        Using col As New FilteredElementCollector(Doc)
          col.OfCategory(BuiltInCategory.OST_GenericModel)
          col.WhereElementIsNotElementType()

          ' Get Each Instance
          For Each e As Element In col.ToElements

            Try

              ' Cast as Instance
              Dim x As FamilyInstance = TryCast(e, FamilyInstance)
              Dim m_eid As ElementId = x.GetTypeId

              ' Get the Symbol
              Dim m_symb As FamilySymbol = Doc.GetElement(m_eid)
              If m_symb.Family.Name.ToLower = TagFamilyName.ToLower Then

                ' Name Matches
                MatchingInstances.Add(x.Id)

              End If

            Catch
            End Try

          Next

        End Using

      Catch
      End Try

      Try

        ' Get Rooms
        GetRooms()

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Collect all Rooms into List
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetRooms()

      Dim m_dict As New SortedDictionary(Of Integer, clsRoomTagInfo)

      Try

        ' Fresh List
        RoomTags = New List(Of clsRoomTagInfo)
        Dim m_r As Room = Nothing

        ' Instance Elements
        Using col As New FilteredElementCollector(Doc)
          col.OfCategory(BuiltInCategory.OST_Rooms)
          For Each x In col.ToElements

            Try

              ' Cast as a Room Element
              m_r = TryCast(x, Room)

              ' Verify
              If m_r Is Nothing Then Continue For

              ' Is it Placed?
              If m_r.Area > 0 Then

                ' Cast to Helper
                Dim m_ri As New clsRoomTagInfo(m_r)

                Try
                  m_dict.Add(m_r.Id.Value, m_ri)
                Catch
                End Try

                ' Add to Main List
                RoomTags.Add(m_ri)

              End If

              m_r = Nothing

            Catch
            End Try

          Next

        End Using

      Catch
      End Try

    End Sub

#End Region

#Region "Public Members"

    ''' <summary>
    ''' Verify the Existence of a Family in the Model
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetFamilySymbol()

      Try

        ' Default
        TagSymbol = Nothing

        ' Type Elements
        Using col As New FilteredElementCollector(Doc)
          col.OfCategory(BuiltInCategory.OST_GenericModel)
          col.WhereElementIsElementType()

          ' Do we have the Family?
          For Each e In col.ToElements

            Try

              ' Cast it as a FamilySymbol
              Dim x As FamilySymbol = TryCast(e, FamilySymbol)
              If x.Family.Name.ToLower = TagFamilyName.ToLower Then

                ' Found It
                TagSymbol = x
                GetFamilyInstances()

                ' Exit
                Exit For

              End If

            Catch
            End Try

          Next

        End Using

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Load a Family into a Document
    ''' </summary>
    ''' <param name="FilePath">Full path to family being loaded</param>
    ''' <param name="TargetDoc">Document to load family into</param>
    ''' <param name="isOverwrite">Overwrite if existing found</param>
    ''' <returns>Family element loaded into the model</returns>
    ''' <remarks>Returns Nothing on failure</remarks>
    Public Function LoadFamily(FilePath As String,
                               TargetDoc As Document,
                               Optional isOverwrite As Boolean = False) As Family

      ' Resulting Family
      Dim m_family As Family = Nothing

      ' Start a new Transaction
      Using t As New Transaction(Doc, "Family Load")
        If t.Start = TransactionStatus.Started Then

          Try

            ' Overwrite Existing?
            If isOverwrite = True Then

              ' Overwrite
              TargetDoc.LoadFamily(FilePath, New clsFamLoadOptions, m_family)

            Else

              ' Don't Orverwrite Existing - Default
              TargetDoc.LoadFamily(FilePath, m_family)

            End If

            ' Success
            t.Commit()

          Catch
          End Try

        End If
      End Using

      ' Result
      Return m_family

    End Function

#End Region

  End Class
End Namespace
Imports System.IO
Imports System.Linq
Imports Autodesk.Revit.DB.Architecture
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings
    
    Private _cmd As ExternalCommandData
    Private _docLinks As Dictionary(Of String, RevitLinkInstance)

#Region "Public Properties"

    Public Property RoomSet As ElementSet
    Public Property Rooms As List(Of clsRoom)
    Public Property Levels As List(Of Level)
    Public Property Parameters As SortedDictionary(Of String, String)
    Public Property AvailableModels As SortedDictionary(Of String, Document)
    Public ReadOnly Property ActiveDoc As Document
      Get
        Try
          Return _cmd.Application.ActiveUIDocument.Document
        Catch
        End Try
        Return Nothing
      End Get
    End Property

    Public ReadOnly Property LinkedDocs As Dictionary(Of String, RevitLinkInstance)
      Get
        Try
          Return _docLinks
        Catch
        End Try
        Return Nothing
      End Get
    End Property

    Public ReadOnly Property DocPath As String
      Get
        Try
          If ActiveDoc.IsWorkshared = True Then
            If Not String.IsNullOrEmpty(ActiveDoc.GetWorksharingCentralModelPath.CentralServerPath) Then
              Return ActiveDoc.GetWorksharingCentralModelPath.CentralServerPath
            Else
              Return "Detached Model"
            End If
          Else
            Return ActiveDoc.PathName
          End If
        Catch ex As Exception
          Return ""
        End Try
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Settings Class
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks>Holds references and properties used throughout the project</remarks>
    Public Sub New(cmd As ExternalCommandData)

      ' Widen Scope
      _cmd = cmd
      Rooms = New List(Of clsRoom)
      Parameters = New SortedDictionary(Of String, String)

      ' Setup
      Setup()

    End Sub

#Region "Private Members"

    ''' <summary>
    ''' Basic Setup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Setup()

      Try

        ' Query
        _docLinks = New Dictionary(Of String, RevitLinkInstance)
        Dim m_items As IEnumerable(Of RevitLinkInstance) = From e In New FilteredElementCollector(ActiveDoc) _
                                                           .OfClass(GetType(RevitLinkInstance))
                                                           Let m_linkInst As RevitLinkInstance = TryCast(e, RevitLinkInstance)
                                                           Select m_linkInst

        ' Links by type name (model name)
        For Each x As RevitLinkInstance In m_items.ToList()
          Dim m_linkType As RevitLinkType = ActiveDoc.GetElement(x.GetTypeId())
          If Not m_linkType Is Nothing Then
            If Not _docLinks.ContainsKey(m_linkType.Name) Then
              _docLinks.Add(m_linkType.Name, x)
            End If
          End If
        Next
        
        ' Available Documents
        AvailableModels = New SortedDictionary(Of String, Document)
        For Each d As Document In _cmd.Application.Application.Documents
          Try
            If d.IsFamilyDocument = True Then Continue For
            If d.PathName.ToLower <> ActiveDoc.PathName.ToLower Then
              If _docLinks.ContainsKey(d.Title) Then
                AvailableModels.Add(d.Title, d)
              End If
            End If
          Catch
          End Try
        Next

      Catch
      End Try

      ' Fresh list
      Levels = New List(Of Level)
      Rooms = New List(Of clsRoom)
      RoomSet = New ElementSet

      ' Get Levels
      Dim m_colL As New FilteredElementCollector(ActiveDoc)
      m_colL.WhereElementIsNotElementType()
      m_colL.OfCategory(BuiltInCategory.OST_Levels)
      For Each x In m_colL.ToElements
        If TypeOf x Is Level Then
          Levels.Add(x)
        End If
      Next

      Try

        ' Local Rooms Filter
        Dim m_col As New FilteredElementCollector(ActiveDoc)
        m_col.OfCategory(BuiltInCategory.OST_Rooms)

        ' Update List
        For Each x As Element In m_col.ToElements

          Try

            Dim r As Room = TryCast(x, Room)

            If Not r Is Nothing Then
              If r.Area > 1 Then
                ' Add the room to the Dictionary
                Rooms.Add(New clsRoom(x))
                RoomSet.Insert(x)
              End If
            End If

          Catch
          End Try

        Next

      Catch
      End Try

    End Sub

#End Region

#Region "Friend Members"

    ''' <summary>
    ''' Add a parameter
    ''' </summary>
    ''' <param name="cat"></param>
    ''' <param name="paramName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function AddParameter(cat As Category, paramName As String) As Boolean

      Try


      Catch
      End Try

      Return False

    End Function

    ''' <summary>
    ''' Get properly placed rooms from a model document
    ''' </summary>
    ''' <param name="doc">Document to get rooms from</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetRoomsFromLinkedModel(doc As Document) As List(Of clsRoom)

      ' Get the Rooms
      Dim m_syncRooms As New List(Of clsRoom)

      Try

        ' Rooms
        If Not doc.IsFamilyDocument Then

          ' Query
          Dim m_items As IEnumerable(Of Element) = From e In New FilteredElementCollector(doc) _
                                                   .OfCategory(BuiltInCategory.OST_Rooms)
                                                   Let r As Room = TryCast(e, Room)
                                                   Where r.Area > 0
                                                   Where r.Location IsNot Nothing
                                                   Select e

          For Each x In m_items.ToList()
            m_syncRooms.Add(New clsRoom(x))
          Next

        End If

      Catch
      End Try

      ' Result
      Return m_syncRooms

    End Function

#End Region

#Region "Friend Members - Shared Parameter File and Parameters"

    #If Revit2020 Or Revit2021 Or Revit2022
        ''' <summary>
    ''' Add a parameter to a category binding
    ''' </summary>
    ''' <param name="bic">Builtin category</param>
    ''' <param name="pName">Parameter name</param>
    ''' <param name="grp">Group Name</param>
    ''' <param name="pType">Parameter Type</param>
    ''' <param name="isVisible">UI Visibility</param>
    ''' <returns></returns>
    
    Friend Function AddInstanceParameterToCategory(bic As BuiltInCategory, pName As String, grp As ForgeTypeId, pType As ParameterType, isVisible As Boolean) As Boolean

      ' Get the Shared Parameter File
      Dim m_defFile As DefinitionFile = SetSharedParameterFile()

      ' Fail if no file
      If m_defFile Is Nothing Then Return False

      ' Groups
      Dim m_groups As DefinitionGroups = m_defFile.Groups
      Dim m_group As DefinitionGroup = Nothing

      ' Get the Case Group
      m_group = m_groups.Item("CaseParameters")
      If m_group Is Nothing Then

        ' Create the Group if it is missing
        m_groups.Create("CaseParameters")
        m_group = m_groups.Item("CaseParameters")

      End If

      ' Does the param exist in file?
      Dim m_paramDef As Definition = m_group.Definitions.Item(pName)
      If m_paramDef Is Nothing Then

        ' Add it if missing
        m_group.Definitions.Create(new ExternalDefinitionCreationOptions(pName, pType))
        m_paramDef = m_group.Definitions.Item(pName)

      End If

      ' New category set for binding
      Dim m_cats As CategorySet = _cmd.Application.Application.Create.NewCategorySet()
      Dim m_cat As Category = Nothing

      ' Get the category from the builtin
      m_cat = ActiveDoc.Settings.Categories.Item(bic)
      m_cats.Insert(m_cat)

      ' Instance Binding
      Dim m_binding As Autodesk.Revit.DB.Binding = _cmd.Application.Application.Create.NewInstanceBinding(m_cats)

      ' Start a subtransaction
      Using t As New SubTransaction(ActiveDoc)
        If t.Start = TransactionStatus.Started Then
          Try

            ' Bind to category
            ActiveDoc.ParameterBindings.Insert(m_paramDef, m_binding, grp)

            ' Success
            t.Commit()
            Return True

          Catch
          End Try
        End If
      End Using

      ' Failure
      Return False

    End Function
#End If
        #If Revit2023 Or Revit2025 Or Revit2026 Or Revit2027
        ''' <summary>
    ''' Add a parameter to a category binding
    ''' </summary>
    ''' <param name="bic">Builtin category</param>
    ''' <param name="pName">Parameter name</param>
    ''' <param name="grp">Group Name</param>
    ''' <param name="pType">Parameter Type</param>
    ''' <param name="isVisible">UI Visibility</param>
    ''' <returns></returns>
    
    Friend Function AddInstanceParameterToCategory(bic As BuiltInCategory, pName As String, grp As ForgeTypeId, pType As ForgeTypeId, isVisible As Boolean) As Boolean

      ' Get the Shared Parameter File
      Dim m_defFile As DefinitionFile = SetSharedParameterFile()

      ' Fail if no file
      If m_defFile Is Nothing Then Return False

      ' Groups
      Dim m_groups As DefinitionGroups = m_defFile.Groups
      Dim m_group As DefinitionGroup = Nothing

      ' Get the Case Group
      m_group = m_groups.Item("CaseParameters")
      If m_group Is Nothing Then

        ' Create the Group if it is missing
        m_groups.Create("CaseParameters")
        m_group = m_groups.Item("CaseParameters")

      End If

      ' Does the param exist in file?
      Dim m_paramDef As Definition = m_group.Definitions.Item(pName)
      If m_paramDef Is Nothing Then

        ' Add it if missing
        m_group.Definitions.Create(new ExternalDefinitionCreationOptions(pName, pType))
        m_paramDef = m_group.Definitions.Item(pName)

      End If

      ' New category set for binding
      Dim m_cats As CategorySet = _cmd.Application.Application.Create.NewCategorySet()
      Dim m_cat As Category = Nothing

      ' Get the category from the builtin
      m_cat = ActiveDoc.Settings.Categories.Item(bic)
      m_cats.Insert(m_cat)

      ' Instance Binding
      Dim m_binding As Autodesk.Revit.DB.Binding = _cmd.Application.Application.Create.NewInstanceBinding(m_cats)

      ' Start a subtransaction
      Using t As New SubTransaction(ActiveDoc)
        If t.Start = TransactionStatus.Started Then
          Try

            ' Bind to category
            ActiveDoc.ParameterBindings.Insert(m_paramDef, m_binding, grp)

            ' Success
            t.Commit()
            Return True

          Catch
          End Try
        End If
      End Using

      ' Failure
      Return False

    End Function
#End If

    ''' <summary>
    ''' Open the Shared parameter File - Create if missing
    ''' </summary>
    ''' <returns></returns>
    Private Function SetSharedParameterFile() As DefinitionFile

      Try

        ' CASE Temp Path
        Dim m_filePath As String = GetCaseTempPath()
        If Not String.IsNullOrEmpty(m_filePath) Then

          ' Path to shared param file
          m_filePath = Path.Combine(m_filePath, "\CaseApps_Parameters.txt")

          ' Shared Param File
          Dim m_defFile As DefinitionFile

          ' Check if the file exists
          If Not File.Exists(m_filePath) Then

            ' Create the File
            Dim fileFlow As FileStream = File.Create(m_filePath)
            fileFlow.Close()

          End If

          ' Set the Shared Parameter File Open
          _cmd.Application.Application.SharedParametersFilename = m_filePath
          m_defFile = _cmd.Application.Application.OpenSharedParameterFile()

          ' Success
          Return m_defFile

        End If

      Catch
      End Try

      ' Failure
      Return Nothing

    End Function
        #If Revit2020 Or Revit2021 Or Revit2022
  ''' <summary>
    ''' Creates a new shared parameter
    ''' </summary>
    ''' <param name="paramName">Parameter Name</param>
    ''' <param name="paramType">Parameter data type</param>
    ''' <returns>True on Success</returns>
    Private Function CreateSharedParameter(paramName As String, paramType As ParameterType) As Boolean

      ' Open or Create Shared Param File
      Dim m_sharedParamFile As DefinitionFile = SetSharedParameterFile()

      ' Fail if no Shared Param File
      If m_sharedParamFile Is Nothing Then
        Return False
      End If

      ' Parameter Groups
      Dim m_defGroups As DefinitionGroups = m_sharedParamFile.Groups
      Dim m_group As DefinitionGroup = Nothing

      ' Find the Case Group
      m_group = m_defGroups.Item("CaseParameters")
      If m_group Is Nothing Then

        ' Create the Case Group if Missing
        m_defGroups.Create("CaseParameters")

        ' Verify Creation

        m_group = m_defGroups.Item("CaseParameters")
      End If

      ' Does the parameter exist in the file?
      Dim m_pDef As Definition = m_group.Definitions.Item(paramName)
      If m_pDef Is Nothing Then

        ' Create the Parameter if Missing
        m_group.Definitions.Create(new ExternalDefinitionCreationOptions(paramName, paramType))

        m_pDef = m_group.Definitions.Item(paramName)
      End If

      ' Category Binding
      Dim m_cats As CategorySet = _cmd.Application.Application.Create.NewCategorySet()
      Dim m_catProjInfo As Category = ActiveDoc.Settings.Categories.Item(BuiltInCategory.OST_ProjectInformation)
      m_cats.Insert(m_catProjInfo)

      ' New Instance Binding
      Dim m_bind As Autodesk.Revit.DB.Binding = _cmd.Application.Application.Create.NewInstanceBinding(m_cats)

      ' Start a Subtransaction
      Using t As New SubTransaction(ActiveDoc)

        Try
          t.Start()

          ' Add the binding to the category
          Dim m_boundResult As Boolean = ActiveDoc.ParameterBindings.Insert(m_pDef, m_bind, GroupTypeId.Data)
          t.Commit()

          ' Success
          Return m_boundResult

        Catch
          t.Dispose()

        End Try
      End Using

      ' Failed
      Return False

    End Function
#End If
    #If Revit2023 Or Revit2025 Or Revit2026 Or Revit2027
  ''' <summary>
    ''' Creates a new shared parameter
    ''' </summary>
    ''' <param name="paramName">Parameter Name</param>
    ''' <param name="paramType">Parameter data type</param>
    ''' <returns>True on Success</returns>
    Private Function CreateSharedParameter(paramName As String, paramType As ForgeTypeId) As Boolean

      ' Open or Create Shared Param File
      Dim m_sharedParamFile As DefinitionFile = SetSharedParameterFile()

      ' Fail if no Shared Param File
      If m_sharedParamFile Is Nothing Then
        Return False
      End If

      ' Parameter Groups
      Dim m_defGroups As DefinitionGroups = m_sharedParamFile.Groups
      Dim m_group As DefinitionGroup = Nothing

      ' Find the Case Group
      m_group = m_defGroups.Item("CaseParameters")
      If m_group Is Nothing Then

        ' Create the Case Group if Missing
        m_defGroups.Create("CaseParameters")

        ' Verify Creation

        m_group = m_defGroups.Item("CaseParameters")
      End If

      ' Does the parameter exist in the file?
      Dim m_pDef As Definition = m_group.Definitions.Item(paramName)
      If m_pDef Is Nothing Then

        ' Create the Parameter if Missing
        m_group.Definitions.Create(new ExternalDefinitionCreationOptions(paramName, paramType))

        m_pDef = m_group.Definitions.Item(paramName)
      End If

      ' Category Binding
      Dim m_cats As CategorySet = _cmd.Application.Application.Create.NewCategorySet()
      Dim m_catProjInfo As Category = ActiveDoc.Settings.Categories.Item(BuiltInCategory.OST_ProjectInformation)
      m_cats.Insert(m_catProjInfo)

      ' New Instance Binding
      Dim m_bind As Autodesk.Revit.DB.Binding = _cmd.Application.Application.Create.NewInstanceBinding(m_cats)

      ' Start a Subtransaction
      Using t As New SubTransaction(ActiveDoc)

        Try
          t.Start()

          ' Add the binding to the category
          Dim m_boundResult As Boolean = ActiveDoc.ParameterBindings.Insert(m_pDef, m_bind, GroupTypeId.Data)
          t.Commit()

          ' Success
          Return m_boundResult

        Catch
          t.Dispose()

        End Try
      End Using

      ' Failed
      Return False

    End Function
#End If

#End Region

  End Class
End Namespace
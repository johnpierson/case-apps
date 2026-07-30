Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports Microsoft.Win32

Namespace Data

#Region "Public Enums"

  ''' <summary>
  ''' Excel Mode for Startup
  ''' </summary>
  ''' <remarks></remarks>
  Public Enum EnumExcelSrartupMode
    IsExisting
    IsNewFile
    IsSmartSync
    IsSchedule
  End Enum

  ''' <summary>
  ''' Direction to Synchronize
  ''' </summary>
  ''' <remarks></remarks>
  Public Enum EnumSyncDir
    ToRevit
    ToExcel
    IsIgnore
  End Enum

  ''' <summary>
  ''' Revit Data Type (Normal, Read-Only, Complex)
  ''' </summary>
  ''' <remarks></remarks>
  Public Enum EnumCellDataType
    IsNormal
    IsReadOnly
    IsComplex
  End Enum

  ''' <summary>
  ''' Header Kind (Element, Type Instance)
  ''' </summary>
  ''' <remarks></remarks>
  Public Enum EnumExcelHeaderKind
    IsE
    IsT
    IsI
  End Enum

  ''' <summary>
  ''' The kind of table to generate
  ''' </summary>
  ''' <remarks></remarks>
  Public Enum EnumRevitElementTableType
    IsJustTypes
    IsJustInstances
    IsAllData
    IsFailure
  End Enum

  ''' <summary>
  ''' Version of Excel
  ''' </summary>
  ''' <remarks></remarks>
  Public Enum EnumOfficeVersion
    IsNotSupported
    IsNotInstalled
    Is2003
    Is2007
    Is2010
    Is2013
    Is17
  End Enum

#End Region

  Public Class clsSettings

    Private _officeInstallVersion As EnumOfficeVersion = EnumOfficeVersion.isNotInstalled
    Private _appPath As String = ""
    Private _cmd As ExternalCommandData
    Private _eSet As ElementSet

#Region "Public Properties - Configurations and Versioning"

    ''' <summary>
    ''' Directory where Stored Export Settings are Saved
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PathAppData As String
      Get
        Return _appPath
      End Get
    End Property

    ''' <summary>
    ''' App Version
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Version As String
      Get
        Try
          Return My.Application.Info.Version.ToString()
        Catch
        End Try
        Return ""
      End Get
    End Property

    ''' <summary>
    ''' Excel Installation Check
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property OfficeInstallVersion As EnumOfficeVersion
      Get
        Return _officeInstallVersion
      End Get
    End Property

    ''' <summary>
    ''' Configurations
    ''' </summary>
    ''' <remarks></remarks>
    Public Property Configurations As List(Of clsIoConfig)

    ''' <summary>
    ''' INI File
    ''' </summary>
    ''' <remarks></remarks>
    Public Property IniFile As clsIoIni

#End Region

#Region "Public Properties - Document and Elements"

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

          ' Workshared?
          If Doc.IsWorkshared Then

            ' Test for a valid file name
            If Not String.IsNullOrEmpty(Doc.GetWorksharingCentralModelPath.CentralServerPath) Then

              ' Only use central file name if the model has been saved
              Return Doc.GetWorksharingCentralModelPath.CentralServerPath

            Else

              ' Non Revit Server Path
              Dim m_centralFilePath As String = ModelPathUtils.ConvertModelPathToUserVisiblePath(Doc.GetWorksharingCentralModelPath)
              If Not String.IsNullOrEmpty(m_centralFilePath) Then
                Return m_centralFilePath
              Else
                Return ""
              End If
            End If

          Else

            ' Use the document title
            Return Doc.PathName

          End If

        Catch
        End Try

        ' Failure
        Return ""

      End Get
    End Property

    ''' <summary>
    ''' Categories
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AvailableCategories As SortedDictionary(Of String, Category)

    ''' <summary>
    ''' Category to Process
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExportCategories As List(Of clsRvtCategoryData)

    ''' <summary>
    ''' Category to Process
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExportSchedules As List(Of clsRvtScheduleData)

    ''' <summary>
    ''' User Kind Worksets
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserWorksets As SortedDictionary(Of Integer, clsRvtWorksets)

    ''' <summary>
    ''' Schedule Views
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Schedules As List(Of clsRvtSchedule)

    ''' <summary>
    ''' Phases
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Phases As Dictionary(Of String, Phase)

    ''' <summary>
    ''' Materials
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Materials As Dictionary(Of String, Material)

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

      ' Setup
      Setup()

    End Sub

#Region "Private Members"

    ''' <summary>
    ''' Class Setup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Setup()

      ' Fresh Lists
      ExportCategories = New List(Of clsRvtCategoryData)
      ExportSchedules = New List(Of clsRvtScheduleData)

      ' Excel Verification
      If ExcelVerification() = True Then

        ' Configurations
        ReadConfigurations()

        ' Categories
        GetCategories()

        ' Worksets
        GetWorksets()

        ' Materials
        GetMaterials()

        ' Phases
        GetPhases()

        ' Schedules
        GetSchedules()

      End If

    End Sub

    ''' <summary>
    ''' Verify the Installation of Proper Excel Version
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExcelVerification() As Boolean

      ' Excel.exe Path
      Dim m_installationPath As String = ""

      ' Excel Registry Key
      Dim m_keyPath As String = "Software\Microsoft\Windows\CurrentVersion\App Paths\excel.exe"

      Try

        ' Current User Registry
        Dim _mainKey As RegistryKey = Nothing

        Try

          ' Check Current User First
          _mainKey = Registry.CurrentUser
          _mainKey = _mainKey.OpenSubKey(m_keyPath, False)

          ' Get the Path from the Key
          If _mainKey IsNot Nothing Then
            m_installationPath = _mainKey.GetValue(String.Empty).ToString
          End If

        Catch
        End Try

        ' Check LOCAL_MACHINE if not found under user profile
        If String.IsNullOrEmpty(m_installationPath) Then

          Try

            ' LOCAL_MACHINE
            _mainKey = Registry.LocalMachine
            _mainKey = _mainKey.OpenSubKey(m_keyPath, False)

            ' Get the Path from the Key
            If _mainKey IsNot Nothing Then
              m_installationPath = _mainKey.GetValue(String.Empty).ToString
            End If

          Catch
          End Try

        End If

        ' Close the Key
        If _mainKey IsNot Nothing Then
          _mainKey.Close()
        End If

        ' Found Value?
        If Not String.IsNullOrEmpty(m_installationPath) Then

          ' Version Data Needs to be 14 or higher
          Dim m_verInt As Integer = GetFileMajorVersion(m_installationPath)
          Select Case m_verInt
            Case 16
              _officeInstallVersion = EnumOfficeVersion.Is17

            Case 15
              _officeInstallVersion = EnumOfficeVersion.is2013

            Case 14
              _officeInstallVersion = EnumOfficeVersion.is2010

            Case Else
              _officeInstallVersion = EnumOfficeVersion.isNotSupported
              Return False

          End Select

          ' Success
          Return True

        Else

          ' Not Installed
          _officeInstallVersion = EnumOfficeVersion.isNotInstalled
          MsgBox("Microsoft Excel was not found on this machine", MsgBoxStyle.Critical, "Error")

        End If

      Catch
      End Try

      ' Failure
      Return False

    End Function

    ''' <summary>
    ''' Read Configuration Data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadConfigurations()

      ' Default Configuration
      Configurations = New List(Of clsIoConfig)
      Configurations.Add(New clsIoConfig("<Unnamed Configuration>"))

      Try

        ' Path to LocalApplicationData \ Case Exceler8
        Dim m_appPath As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Case Exceler8"

        ' Create if Missing
        If (Not Directory.Exists(m_appPath)) Then
          Try
            Directory.CreateDirectory(m_appPath)
          Catch
          End Try
        End If

        ' Location Exists
        If Directory.Exists(m_appPath) Then
          _appPath = m_appPath

          ' Get and Read all Stored Files
          For Each x In Directory.GetFiles(_appPath, "*.exceler8", SearchOption.TopDirectoryOnly)
            Try

              ' Configuration Helper
              Dim m_config As New clsIoConfig(x)
              m_config.ReadData()

              ' Add to Master
              Configurations.Add(m_config)

            Catch
            End Try
          Next

          ' INI Helper
          iniFile = New clsIoIni(_appPath & "\Exceler8.ini")

        End If

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get all Categories
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetCategories()

      ' Categories
      AvailableCategories = New SortedDictionary(Of String, Category)
      For Each x As Category In Doc.Settings.Categories
        AvailableCategories.Add(x.Name.ToLower, x)
      Next

    End Sub

    ''' <summary>
    ''' Get all User Worksets
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetWorksets()

      ' Get Worksets
      UserWorksets = New SortedDictionary(Of Integer, clsRvtWorksets)
      Dim m_ws = New FilteredWorksetCollector(Doc).OfKind(WorksetKind.UserWorkset)

      ' Anything Returned?
      If Not m_ws Is Nothing Then
        For Each x In m_ws.ToList

          ' To Dictionary Sorted
          UserWorksets.Add(x.Id.IntegerValue, New clsRvtWorksets(x))

        Next
      End If

    End Sub

    ''' <summary>
    ''' Get Materials
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetMaterials()

      Try

        ' Get Materials
        Materials = New Dictionary(Of String, Material)
        Dim m_mat = From e In New FilteredElementCollector(Doc) _
              .OfClass(GetType(Material))
              Let m = TryCast(e, Material)
              Select m

        ' Anything Returned?
        If Not m_mat Is Nothing Then

          ' To Dictionary
          Materials = m_mat.ToDictionary(Function(m) m.Name)

        End If

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get Phases
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetPhases()

      Try

        ' Get Phases
        Phases = New Dictionary(Of String, Phase)
        Dim m_phases = From e In New FilteredElementCollector(Doc) _
              .OfClass(GetType(Phase))
              Let p = TryCast(e, Phase)
              Select p

        ' Anything Returned?
        If Not m_phases Is Nothing Then

          ' To Dictionary
          Phases = m_phases.ToDictionary(Function(p) p.Name)

        End If

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Schedules
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetSchedules()

      ' Get Schedules
      Schedules = New List(Of clsRvtSchedule)
      Dim m_sch = From e In New FilteredElementCollector(Doc) _
            .OfClass(GetType(ViewSchedule))
            Let sch = TryCast(e, ViewSchedule)
            Where sch.Definition.CategoryId.Value <> -1
            Select sch

      ' Anything Returned?
      If Not m_sch Is Nothing Then
        For Each x In m_sch.ToList

          ' Add the Schedule:
          Schedules.Add(New clsRvtSchedule(x))

        Next
      End If

    End Sub

    ''' <summary>
    ''' Set a Complex Value 
    ''' </summary>
    ''' <param name="p">Parameter Helper</param>
    ''' <param name="inputValue">Excel Value</param>
    ''' <returns>TRUE on Success</returns>
    ''' <remarks></remarks>
    Private Function SetComplexDataValue(p As clsRvtParameter,
                                         inputValue As String) As Boolean

      ' Value
      Dim m_val As Boolean = False

      Try

        ' Is it a Material?
          #If Revit2020 Or Revit2021 or Revit2022
                 If p.ParameterObject.Definition.ParameterType = ParameterType.Material Then
#End If
#If Revit2023 Or Revit2025 Or Revit2026 Or Revit2027
          If p.ParameterObject.Definition.GetDataType() = SpecTypeId.Reference.Material Then
#End If
          If Materials.ContainsKey(inputValue) Then

            Try

              ' Set this Value
              p.ParameterObject.Set(Materials(inputValue).Id)

              ' Done
              Return True

            Catch
            End Try

          End If
        End If

        ' Phase?
        Dim m_current = Doc.GetElement(CInt(p.Value))

        ' Phase?
        If TypeOf m_current Is Phase Then
          If Phases.ContainsKey(inputValue) Then

            Try

              ' Set this Value
              p.ParameterObject.Set(Phases(inputValue).Id)

              ' Done
              Return True

            Catch
            End Try

          Else
            ' Iterate as Lower
            For Each m In Phases
              If m.Key.ToLower = inputValue.ToLower Then

                ' Set this Value
                p.ParameterObject.Set(m.Value.Id)

                ' Done
                Return True

              End If
            Next

          End If

        End If

      Catch
      End Try

      ' Failure
      Return m_val

    End Function

    ''' <summary>
    ''' File version from path to File
    ''' </summary>
    ''' <param name="p">File Path to Check</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFileMajorVersion(p As String) As Integer

      ' Return Value
      Dim m_val As Integer = 0

      ' Check for file
      If File.Exists(p) Then
        Try
          m_val = FileVersionInfo.GetVersionInfo(p).FileMajorPart
        Catch
        End Try
      End If

      ' Final Value
      Return m_val

    End Function

#End Region

#Region "Public Members"

    ''' <summary>
    ''' Get Elements by Category
    ''' </summary>
    ''' <param name="isType">Collect Types when TRUE, Instances otherwise</param>
    ''' <param name="catId">Category ID</param>
    ''' <returns>List of elements matching search criteria</returns>
    ''' <remarks></remarks>
    Public Function GetElements(isType As Boolean,
                                catId As ElementId) As List(Of Element)

      ' Fresh List
      Dim m_elements As New List(Of Element)

      Try

        ' Collection
        Using col As New FilteredElementCollector(Doc)
          With col
            .OfCategoryId(catId)
            If isType = True Then
              .WhereElementIsElementType()
            Else
              .WhereElementIsNotElementType()
            End If
          End With

          ' Results
          Return col.ToElements

        End Using

      Catch
      End Try

      ' Return Value
      Return m_elements

    End Function

    ''' <summary>
    ''' Update Revit element and send CHANGED values back for Excel
    ''' </summary>
    ''' <param name="e">Element</param>
    ''' <param name="directParams">Parameters that are directly on this element</param>
    ''' <param name="parentParams">Parameters on this element's type</param>
    ''' <param name="asNumeric">TRUE will deal with numeric parameters as numbers</param>
    ''' <returns>List of Values for Excel Changes</returns>
    ''' <remarks></remarks>
    Public Function UpdateElementValues(e As Element,
                                        directParams As List(Of clsValue),
                                        parentParams As List(Of clsValue),
                                        asNumeric As Boolean) As Dictionary(Of String, List(Of clsValue))

      ' Fresh List ; d=Direct, p=Parent
      Dim m_values As New Dictionary(Of String, List(Of clsValue))
      m_values.Add("d", New List(Of clsValue))
      m_values.Add("p", New List(Of clsValue))

      ' Start a transaction
      Dim m_updatedElement As Boolean = False
      Using t As New Transaction(Doc, "Exceler8: " & e.Name)
        If t.Start = TransactionStatus.Started Then

          Try

            ' Parent Object?
            Dim m_typeElement As Element = Nothing
            If parentParams.Count > 0 Then
              m_typeElement = Doc.GetElement(e.GetTypeId)
            End If

            ' Direct Parameters
            For Each x In directParams

              ' Match the Name and Group
              For Each p As Parameter In e.Parameters
                If p.Definition.Name = x.Name Then
                  If LabelUtils.GetLabelForGroup(p.Definition.GetGroupTypeId()) = x.Group Then

                    Try

                      ' This is it
                      Dim m_p As New clsRvtParameter(p)

                      ' Direction
                      If x.Direction = EnumSyncDir.toRevit Then
                        m_updatedElement = True

                        ' Numeric?
                        If asNumeric = True Then
                          If Not m_p.Value = x.Value Then
                            m_p.Value = x.Value
                          End If
                        Else
                          If Not m_p.ValueString = x.Value Then
                            m_p.ValueString = x.Value
                          End If
                        End If

                      Else

                        ' Post Change to Output
                        If x.Direction = EnumSyncDir.toExcel Then

                          ' Numeric?
                          If asNumeric = True Then
                            If Not m_p.Value = x.Value Then
                              x.NewValue = m_p.Value
                              m_values("d").Add(x)
                            End If
                          Else
                            If Not m_p.ValueString = x.Value Then
                              x.NewValue = m_p.ValueString
                              m_values("d").Add(x)
                            End If
                          End If

                        End If

                      End If

                    Catch
                    End Try

                    ' Done
                    GoTo nextParamDirect

                  End If
                End If
              Next

              ' Next Param
nextParamDirect:

            Next ' Direct

            ' Parent
            If Not m_typeElement Is Nothing Then

              ' Parent Parameters
              For Each x In parentParams

                ' Match the Name and Group
                For Each p As Parameter In m_typeElement.Parameters
                  If p.Definition.Name = x.Name Then
                    If LabelUtils.GetLabelForGroup(p.Definition.GetGroupTypeId()) = x.Group Then

                      Try

                        ' This is it
                        Dim m_p As New clsRvtParameter(p)

                        ' Direction
                        If x.Direction = EnumSyncDir.toRevit Then
                          m_updatedElement = True

                          ' Numeric?
                          If asNumeric = True Then
                            If Not m_p.Value = x.Value Then
                              m_p.Value = x.Value
                            End If
                          Else
                            If Not m_p.ValueString = x.Value Then
                              m_p.ValueString = x.Value
                            End If
                          End If

                        Else

                          ' Post Change to Output
                          If x.Direction = EnumSyncDir.toExcel Then

                            ' Numeric?
                            If asNumeric = True Then
                              If Not m_p.Value = x.Value Then
                                x.NewValue = m_p.Value
                                m_values("p").Add(x)
                              End If
                            Else
                              If Not m_p.ValueString = x.Value Then
                                x.NewValue = m_p.ValueString
                                m_values("p").Add(x)
                              End If
                            End If

                          End If

                        End If

                      Catch
                      End Try

                      ' Done
                      GoTo nextParamParent

                    End If
                  End If
                Next

                ' Next Param
nextParamParent:

              Next ' Direct

            End If

            ' Success
            If m_updatedElement = True Then t.Commit()

          Catch
          End Try

        End If
      End Using

      ' Final Result
      Return m_values

    End Function

    '' '' ''' <summary>
    '' '' ''' Set Values for Sync Element
    '' '' ''' </summary>
    '' '' ''' <param name="e"></param>
    '' '' ''' <param name="dList"></param>
    '' '' ''' <param name="eList"></param>
    '' '' ''' <returns></returns>
    '' '' ''' <remarks></remarks>
    '' ''Public Function SetElementValues(e As Element,
    '' ''                                 dList As List(Of clsDirection),
    '' ''                                 ByRef eList As List(Of clsValue)) As Boolean

    '' ''  ' Stat a New Transaction
    '' ''  Using t As New Transaction(Doc, "Exceler8 Sync: " & e.Name)
    '' ''    If t.Start Then

    '' ''      Try

    '' ''        ' Check Direction
    '' ''        For Each d As clsDirection In dList
    '' ''          If d.Direction = syncDir.isIgnore Then Continue For

    '' ''          ' Process Instance Data
    '' ''          For Each i As clsValue In eList

    '' ''            ' Correct Parameter
    '' ''            Dim m_foundIt As Boolean = False

    '' ''            ' Name Match?
    '' ''            If Not d.NameAndGroup.ToLower = (i.Name & "|" & i.Group).ToLower Then Continue For

    '' ''            ' Does the Param Exist in the Model
    '' ''            Dim m_param As Parameter = Nothing

    '' ''            ' Name - Override for Materials?
    '' ''            If i.Group.ToLower = "identity data" And i.Name.ToLower = "name" Then
    '' ''              Try

    '' ''                ' Set the Value
    '' ''                If Not String.IsNullOrEmpty(i.Value) Then
    '' ''                  ' Not the Same
    '' ''                  If Not i.Value.ToLower = e.Name.ToLower Then
    '' ''                    If d.Direction = syncDir.toExcel Then
    '' ''                      i.NewValue = e.Name
    '' ''                    End If
    '' ''                    If d.Direction = syncDir.toRevit Then
    '' ''                      e.Name = i.Value
    '' ''                    End If
    '' ''                  End If
    '' ''                End If

    '' ''              Catch
    '' ''              End Try

    '' ''              ' Next Param
    '' ''              Exit For

    '' ''            End If

    '' ''            m_param = e.LookupParameter(i.Name)
    '' ''            If Not m_param Is Nothing Then

    '' ''              ' Correct Group?
    '' ''              Dim m_groupName As String = LabelUtils.GetLabelForGroup(m_param.Definition.GetGroupTypeId())
    '' ''              If Not String.IsNullOrEmpty(m_groupName) Then
    '' ''                If Not i.Group.ToLower = m_groupName.ToLower Then

    '' ''                  ' Get the Correct One:
    '' ''                  For Each pp As Parameter In e.Parameters
    '' ''                    If pp.Definition.Name.ToLower = i.Name.ToLower Then
    '' ''                      m_groupName = LabelUtils.GetLabelForGroup(m_param.Definition.GetGroupTypeId())
    '' ''                      If i.Group.ToLower = m_groupName.ToLower Then
    '' ''                        m_foundIt = True
    '' ''                        m_param = pp
    '' ''                        Exit For
    '' ''                      End If
    '' ''                    End If
    '' ''                  Next

    '' ''                Else

    '' ''                  ' Correct Parameter
    '' ''                  m_foundIt = True

    '' ''                End If
    '' ''              End If

    '' ''              ' Found it?
    '' ''              If m_foundIt = False Then Continue For

    '' ''              ' Cast to Helper
    '' ''              Dim m_para As New clsRvtParameter(m_param)

    '' ''              ' Ignore if Same
    '' ''              If m_para.ValueString = i.Value Then Continue For
    '' ''              If m_para.Value = i.Value Then Continue For

    '' ''              ' Data Direction
    '' ''              If d.Direction = syncDir.toExcel Then

    '' ''                ' Save Excel Value
    '' ''                i.NewValue = m_para.ValueString

    '' ''              Else

    '' ''                ' Write to Revit Element
    '' ''                If m_param.IsReadOnly = True Then Continue For
    '' ''                If m_para.ParameterObject.StorageType = StorageType.ElementId Then

    '' ''                  Try

    '' ''                    ' Set Complex Value
    '' ''                    SetComplexDataValue(m_para, i.Value)

    '' ''                  Catch
    '' ''                  End Try

    '' ''                Else

    '' ''                  ' Send to Revit
    '' ''                  m_para.ValueString = i.Value

    '' ''                End If

    '' ''              End If

    '' ''              ' Next Param - Done
    '' ''              Exit For

    '' ''            Else

    '' ''              ' Parameter not in Model
    '' ''              Dim m_todo1 As String = "Log Maybe?"

    '' ''            End If

    '' ''          Next ' Worksheet Data Parameter

    '' ''        Next

    '' ''        ' Success
    '' ''        t.Commit()
    '' ''        Return True

    '' ''      Catch
    '' ''      End Try

    '' ''    End If
    '' ''  End Using

    '' ''  ' Failure
    '' ''  Return False

    '' ''End Function

#End Region

  End Class
End Namespace
Imports System.Windows.Controls
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Class form_ExportPoints

  Private _s As clsSettings

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s

    ' Remove Unwanted Categories
    _s.ScanCategories()
    Dim m_names As New Dictionary(Of String, String)
    For Each x As Category In _s.Categories.Values

      Try

        If x.Name.ToLower.Contains("level") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("view") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("tags") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("grid") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("callout") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("baluster") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("ceiling") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("legend") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("conduit") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("construction") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("wire") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("duct") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("pipe") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("system") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("wall") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("detail") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("marks") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("floor") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("fluid") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("annotation") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("finish") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("roof") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("opening") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("pattern") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("profile") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("railing") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("reveal") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("revision") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("slab") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("stair") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("title") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("voltage") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("ramp") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("fascia") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("location") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("gutter") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("mass shade") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("mass wind") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("structural fou") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("structural fra") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("span dir") Then m_names.Add(x.Name, x.Name)
        If x.Name.ToLower.Contains("property line") Then m_names.Add(x.Name, x.Name)
      Catch
      End Try

    Next

    ' Remove Unwanted
    For Each x As String In m_names.Keys
      Try
        _s.Categories.Remove(x)
      Catch
      End Try
    Next

    ' Export All Parameters Only If Category or Specific Family Chosen
    Dim m_isCategoryOrFamily As Boolean = False

    ' Form Title
    Title = "CASE Export Point Data" & _s.AppVersion
    ComboBoxNamedLocations.IsEnabled = False

    ' Bind Control Data
    _s.GetLevels()
    _s.GetDesignOptions()
    _s.GetSymbolNames()
    _s.GetNamedLocations()
    _s.GetTags()
    BindData()

    ' Disable Base Buttons
    ButtonPreview.IsEnabled = False
    ButtonExport.IsEnabled = False

    ' Form Visibility
    SetFormViz(formViz.isStandBy)

  End Sub

  ''' <summary>
  ''' Check Minimum Selections
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub isMinimumSelected()
    If ComboBoxCategory.SelectedIndex + ComboBoxFamily.SelectedIndex > 0 Then
      ButtonPreview.IsEnabled = True
      ButtonExport.IsEnabled = True
    Else
      ButtonPreview.IsEnabled = False
      ButtonExport.IsEnabled = False
    End If
  End Sub

#Region "Form Visibility"

  ''' <summary>
  ''' Set the Form Visibility
  ''' </summary>
  ''' <param name="p_viz"></param>
  ''' <remarks></remarks>
  Private Sub SetFormViz(p_viz As formViz)
    Select Case p_viz
      Case formViz.isStandBy
        ProgressBar1.Visibility = Visibility.Hidden
        ButtonCancel.Visibility = Visibility.Visible
        ButtonExport.Visibility = Visibility.Visible
        ButtonPreview.Visibility = Visibility.Visible
        ButtonParameterMapping.Visibility = Visibility.Visible
      Case formViz.isProcessing
        ProgressBar1.Visibility = Visibility.Visible
        ButtonCancel.Visibility = Visibility.Hidden
        ButtonExport.Visibility = Visibility.Hidden
        ButtonPreview.Visibility = Visibility.Hidden
        ButtonParameterMapping.Visibility = Visibility.Hidden
    End Select
  End Sub

#End Region

#Region "Private Members"

  ''' <summary>
  ''' Populate the Datagrid with Data Being Exported
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub LoadPreviewData()

    ' Display the Progress Bar
    SetFormViz(formViz.isProcessing)
    With ProgressBar1
      .Minimum = 0
      .Maximum = 100
      .Value = 0
    End With

    ' Clear List
    DataGrid1.ItemsSource = Nothing

    ' New Collection of Reporting Data
    _s.PointsCollection = New List(Of clsReportingData)

    ' New Revit Element Collector
    Using col As New FilteredElementCollector(_s.Doc)
      col.WhereElementIsNotElementType()

      ' Active Design Option
      If ComboBoxDesignOption.SelectedIndex > 0 Then
        Dim m_d As clsDesignOption = TryCast(Me.ComboBoxDesignOption.SelectedItem, clsDesignOption)
        If Not m_d Is Nothing Then
          Try
            col.ContainedInDesignOption(m_d.DesignOption.Id)
          Catch
          End Try
        End If
      End If

      ' Active Family
      Dim m_ElementSymbol As FamilySymbol = Nothing
      If ComboBoxFamily.SelectedIndex > 0 Then
        Dim m_f As clsFamily = TryCast(Me.ComboBoxFamily.SelectedItem, clsFamily)
        If Not m_f Is Nothing Then
          Try
            m_ElementSymbol = m_f.ElementObject
          Catch
          End Try
        End If
      End If

      ' Active Category
      If ComboBoxCategory.SelectedIndex > 0 Then
        Dim m_c As Category = TryCast(Me.ComboBoxCategory.SelectedItem, Category)
        If Not m_c Is Nothing Then
          Try
            col.OfCategoryId(m_c.Id)
          Catch
          End Try
        End If
      End If

      ' Active Level
      Dim m_level As Level = Nothing
      If ComboBoxLevel.SelectedIndex > 0 Then
        Dim m_l As clsLevel = TryCast(Me.ComboBoxLevel.SelectedItem, clsLevel)
        If Not m_l Is Nothing Then
          m_level = m_l.LevelElement
        End If
      End If

      ' Final List of Elements
      Dim m_emts As New List(Of Element)
      m_emts = col.ToElements
      ProgressBar1.Maximum = m_emts.Count

      ' Do we have a Category entry for PNEZD?
      Dim m_map As clsParamMap = Nothing
      Dim m_map_try As clsParamMap = Nothing
      Try
        ' Category Element for Testing
        Dim m_test_catname As Category = ComboBoxCategory.SelectedItem
        ' Get by Key
        If _s.ConfigFile.DictParamMaps.TryGetValue(m_test_catname.Name, m_map_try) Then
          ' Map Holder
          m_map = m_map_try
        End If

      Catch
      End Try

      ' Cycle the Resultig List
      For Each e As Element In m_emts

        ' Step the Progress Bar
        IncrementProgress(Me.ProgressBar1, 1)

        ' Must have a Category
        If e.Category Is Nothing Then Continue For

        ' Family Filtering by Symbol
        If Not m_ElementSymbol Is Nothing Then
          Try
            Dim m_sym As Element = _s.Doc.GetElement(e.GetTypeId)
            If Not m_sym.Id = m_ElementSymbol.Id Then Continue For
          Catch
            Continue For
          End Try
        End If

        ' Level Filtering
        If Not m_level Is Nothing Then
          Try
            If Not e.LevelId = m_level.Id Then Continue For
          Catch
            Continue For
          End Try
        End If

        ' Active Location Setting
        Dim m_loc As LocationKind
        Dim m_locname As ProjectLocation = Nothing
        If RadioButtonNamed.IsChecked = True Then
          m_loc = LocationKind.isNamed
          m_locname = ComboBoxNamedLocations.SelectedItem
        End If
        If RadioButtonCoordinatesProject.IsChecked = True Then m_loc = LocationKind.isProject
        If RadioButtonCoordinatesShared.IsChecked = True Then
          m_loc = LocationKind.isShared

          ' This is WRONG - needs location from survey point
          Dim m_todo As String = ""

          m_locname = _s.LocationCurrent

        End If

        ' Add the Reporting Item
        _s.PointsCollection.Add(New clsReportingData(e, m_loc, _s.SharedBasePoint, m_locname, m_map))

      Next

    End Using

    ' Bind to the Datagrid
    DataGrid1.DataContext = _s.PointsCollection
    DataGrid1.ItemsSource = _s.PointsCollection

    ' Set the Form Back to Stand By
    SetFormViz(formViz.isStandBy)

  End Sub

  ''' <summary>
  ''' Load Controls 
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub BindData()
    ' Levels
    ComboBoxLevel.DisplayMemberPath = "LevelLongName" ' "LevelName"
    For Each x As clsLevel In _s.Levels
      ComboBoxLevel.Items.Add(x)
    Next
    ComboBoxLevel.SelectedIndex = 0
    ' Categories
    ComboBoxCategory.DisplayMemberPath = "Name"
    ComboBoxCategory.Items.Add(New clsNameHelper("<All>"))
    For Each x As Category In _s.Categories.Values
      ComboBoxCategory.Items.Add(x)
    Next
    ComboBoxCategory.SelectedIndex = 0
    ' Design Options
    ComboBoxDesignOption.DisplayMemberPath = "DisplayString"
    For Each x As clsDesignOption In _s.DesignOptionNames.Values
      ComboBoxDesignOption.Items.Add(x)
    Next
    ComboBoxDesignOption.SelectedIndex = 0
    ' Named Locations
    ComboBoxNamedLocations.DisplayMemberPath = "Name"
    For Each x As ProjectLocation In _s.NamedLocations.Values
      ComboBoxNamedLocations.Items.Add(x)
    Next
    Try
      ComboBoxNamedLocations.SelectedIndex = 0
    Catch
    End Try
    ' Family and Type
    LoadFamilyData()
  End Sub

  ''' <summary>
  ''' Load the Families into the Drop-Down
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub LoadFamilyData()
    ' Clean Combo List
    ComboBoxFamily.Items.Clear()
    ComboBoxFamily.DisplayMemberPath = "ElementFullName"
    ' Load from m_s
    For Each x As clsFamily In _s.FamilyTypeNames.Values
      If ComboBoxCategory.SelectedIndex > 0 Then
        Dim m_c As Category = TryCast(Me.ComboBoxCategory.SelectedItem, Category)
        If Not m_c Is Nothing Then
          Try
            If Not m_c.Name.ToLower = x.ElementObject.Category.Name.ToLower Then Continue For
          Catch
          End Try
        End If
      End If
      ComboBoxFamily.Items.Add(x)
    Next
    ComboBoxFamily.SelectedIndex = 0
  End Sub

#End Region

#Region "Form Controls & Events"

  ''' <summary>
  ''' Update Parameter Mapping
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonParameterMapping_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonParameterMapping.Click

    ' Construct and Display the Form
    Dim m_d As New form_ParameterConfigurationManager(_s)
    m_d.ShowDialog()

    ' Update Grid if Data Available
    If ButtonPreview.IsEnabled = True Then
      ButtonPreview_Click(Nothing, Nothing)
    End If

  End Sub

  ''' <summary>
  ''' Commit the Export of Data
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonExport_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonExport.Click

    ' Form Visibility
    SetFormViz(formViz.isProcessing)

    ' Update Preview Data
    LoadPreviewData()
    _s.UpdateDatatableFromPoints()

    ' Process the Export
    Dim m_sd As New Microsoft.Win32.SaveFileDialog()

    ' CSV
    If RadioButtonCSV.IsChecked Then
      m_sd.FileName = "ExportedPoints"
      m_sd.DefaultExt = ".csv"
      m_sd.Filter = "Comma Separated Values (.csv)|*.csv"
      Dim m_r As Boolean = m_sd.ShowDialog
      If m_r = True Then
        ' Do the Write Data
        Dim m_catName As String = ""
        Try
          Dim m_catObject As Category = ComboBoxCategory.SelectedItem
          m_catName = m_catObject.Name
        Catch
        End Try
        Dim m_f As New clsFile(m_sd.FileName, _s, eFileType.isCSV, True, m_catName)
      End If
    End If

    ' TXT Tab Delimited
    If RadioButtonTxtTab.IsChecked Then
      m_sd.FileName = "ExportedPoints"
      m_sd.DefaultExt = ".txt"
      m_sd.Filter = "Text Documents (Tab Delimited) (.txt)|*.txt"
      Dim m_r As Boolean = m_sd.ShowDialog
      If m_r = True Then
        ' Do the Write Data
        Dim m_catName As String = ""
        Try
          Dim m_catObject As Category = ComboBoxCategory.SelectedItem
          m_catName = m_catObject.Name
        Catch
        End Try
        Dim m_f As New clsFile(m_sd.FileName, _s, eFileType.isTXT, True, m_catName)
      End If
    End If

    ' Excel
    If RadioButtonExcel.IsChecked Then
      Dim m_cat As Category = ComboBoxCategory.SelectedItem
      If m_cat Is Nothing Then
        MsgBox("Error Getting Category Name", MsgBoxStyle.Critical, "Error")
        Exit Sub
      End If
      m_sd.FileName = "ExportedPoints"
      m_sd.DefaultExt = ".xlsx"
      m_sd.Filter = "Excel 2010 Documents (.xlsx)|*.xlsx"
      Dim m_r As Boolean = m_sd.ShowDialog
      If m_r = True Then
        ' Do the Write Data
        Dim m_f As New clsExcel(m_sd.FileName, _s)
        m_f.FillExcelWorksheetFromDataTable(m_cat.Name, True)
        m_f.ShutDownExcel()
      End If
    End If

    ' Report Items Statistics
    Dim m_td As New Autodesk.Revit.UI.TaskDialog("Reporting Statistics")
    With m_td
      .MainInstruction = _s.PointsCollection.Count.ToString & " Records Written"
      .MainContent = "File Exported As:" & vbCr & m_sd.FileName
    End With
    m_td.Show()

    ' Close and Finalize
    Close()

  End Sub

  ''' <summary>
  ''' Preview Data in the Main Viewer
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonPreview_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonPreview.Click
    MinHeight = 700
    ' Loca Preview Data
    LoadPreviewData()
  End Sub

  ''' <summary>
  ''' Cancel
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonCancel.Click
    Close()
  End Sub

  ''' <summary>
  ''' Filter the List of Families in the Family Drop Down
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ComboBoxCategory_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles ComboBoxCategory.SelectionChanged
    isMinimumSelected()
    LoadFamilyData()
  End Sub

  ''' <summary>
  ''' Enable Buttons if Minimum Selection Made
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ComboBoxFamily_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles ComboBoxFamily.SelectionChanged
    isMinimumSelected()
  End Sub

#End Region

#Region "Form Controls & Events - Point Location Options"

  ''' <summary>
  ''' Named Locations
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub RadioButtonNamed_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles RadioButtonNamed.Click
    DataGrid1.ItemsSource = Nothing
    If RadioButtonNamed.IsChecked = True Then
      ComboBoxNamedLocations.IsEnabled = True
    Else
      ComboBoxNamedLocations.IsEnabled = False
    End If
  End Sub

  ''' <summary>
  ''' Project Coordinates
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub RadioButtonCoordinatesProject_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles RadioButtonCoordinatesProject.Click
    DataGrid1.ItemsSource = Nothing
    If RadioButtonNamed.IsChecked = True Then
      ComboBoxNamedLocations.IsEnabled = True
    Else
      ComboBoxNamedLocations.IsEnabled = False
    End If
  End Sub

  ''' <summary>
  ''' Shared Coordinates
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub RadioButtonCoordinatesShared_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles RadioButtonCoordinatesShared.Click
    DataGrid1.ItemsSource = Nothing
    If RadioButtonNamed.IsChecked = True Then
      ComboBoxNamedLocations.IsEnabled = True
    Else
      ComboBoxNamedLocations.IsEnabled = False
    End If
  End Sub

#End Region

#Region "Delegates for UI Updates"

  ''' <summary>
  ''' Create a Delegate that matches the Signature of a value
  ''' </summary>
  ''' <param name="dp"></param>
  ''' <param name="value"></param>
  ''' <remarks></remarks>
  Private Delegate Sub UpdateDelegate(ByVal dp As System.Windows.DependencyProperty, ByVal value As Object)

  ''' <summary>
  ''' Step the Progressbar By Specified Unit
  ''' </summary>
  ''' <param name="p_progbar">The Progressbar Object to Update</param>
  ''' <param name="p_val">Optional, Default is 1</param>
  ''' <remarks></remarks>
  Public Sub IncrementProgress(p_progbar As ProgressBar, Optional p_val As Integer = 1)
    Try
      ' Step the Value
      Dim value As Double = p_progbar.Value + p_val
      'Create a new instance of our ProgressBar Delegate that points to the ProgressBar's SetValue method.
      Dim updatePbDelegate As New UpdateDelegate(AddressOf p_progbar.SetValue)
      ' Invoke the Update
      Dispatcher.Invoke(updatePbDelegate, System.Windows.Threading.DispatcherPriority.Background, New Object() {ProgressBar.ValueProperty, value})
    Catch
    End Try
  End Sub

#End Region

End Class
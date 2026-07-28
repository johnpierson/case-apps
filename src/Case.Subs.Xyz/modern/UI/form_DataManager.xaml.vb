Imports System.IO
Imports System.Data
Imports System.Windows.Controls
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Public Class form_DataManager

  Private _s As clsSettings
  Private _excel As clsExcel
  Private _file As clsFile
  Private _errorEid As New List(Of String)

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="p_s"></param>
  ''' <remarks></remarks>
  Public Sub New(p_s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = p_s

    ' Form Visibility
    Me.Title = "CASE Points Data Manager" & _s.AppVersion
    Me.ComboBoxExcelWorksheet.Visibility = Visibility.Hidden
    Me.LabelExcelWorksheet.Visibility = Visibility.Hidden
    Me.ButtonUpdate.IsEnabled = False
    SetFormVisibility(formViz.isStandBy)

  End Sub

  ''' <summary>
  ''' Commit the Data to the Points Elements
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdatePoints()

    ' Transaction
    Dim m_t As New Transaction(_s.Doc, "PNEZD Data Manager from External File")
    m_t.Start()

    Dim iCntSuccess As Integer = 0

    Try
      ' Process Each Row, ElementID is Required
      For Each cr As clsReportingData In Me.DataGridPointsData.ItemsSource

        Try

          ' Element from ID
          Dim i As Integer = cr.eID
          Dim m_e As Element = _s.Doc.GetElement(New ElementId(i))

          ' Did we get an element
          If m_e Is Nothing Then
            _errorEid.Add(cr.eID)
            Continue For
          End If

          ' Write the Parameter Data
          Dim m_param_p As New clsPara(cr.ParamMap.Param_P, cr.P, m_e)
          If Not String.IsNullOrEmpty(m_param_p.ErrorMessage) Then
            _errorEid.Add(cr.eID & ": " & m_param_p.ErrorMessage)
          Else
            If m_param_p.SameValue = False Then iCntSuccess += 1
          End If
          Dim m_param_n As New clsPara(cr.ParamMap.Param_N, cr.N, m_e)
          If Not String.IsNullOrEmpty(m_param_n.ErrorMessage) Then
            _errorEid.Add(cr.eID & ": " & m_param_n.ErrorMessage)
          Else
            If m_param_n.SameValue = False Then iCntSuccess += 1
          End If
          Dim m_param_e As New clsPara(cr.ParamMap.Param_E, cr.E, m_e)
          If Not String.IsNullOrEmpty(m_param_e.ErrorMessage) Then
            _errorEid.Add(cr.eID & ": " & m_param_e.ErrorMessage)
          Else
            If m_param_e.SameValue = False Then iCntSuccess += 1
          End If
          Dim m_param_z As New clsPara(cr.ParamMap.Param_Z, cr.Z, m_e)
          If Not String.IsNullOrEmpty(m_param_z.ErrorMessage) Then
            _errorEid.Add(cr.eID & ": " & m_param_z.ErrorMessage)
          Else
            If m_param_z.SameValue = False Then iCntSuccess += 1
          End If
          Dim m_param_d As New clsPara(cr.ParamMap.Param_D, cr.D, m_e)
          If Not String.IsNullOrEmpty(m_param_d.ErrorMessage) Then
            _errorEid.Add(cr.eID & ": " & m_param_d.ErrorMessage)
          Else
            If m_param_d.SameValue = False Then iCntSuccess += 1
          End If
          Dim m_param_1 As New clsPara(cr.ParamMap.Param_1, cr.Param1, m_e)
          If Not String.IsNullOrEmpty(m_param_1.ErrorMessage) Then
            _errorEid.Add(cr.eID & ": " & m_param_1.ErrorMessage)
          Else
            If m_param_1.SameValue = False Then iCntSuccess += 1
          End If
          Dim m_param_2 As New clsPara(cr.ParamMap.Param_2, cr.Param2, m_e)
          If Not String.IsNullOrEmpty(m_param_2.ErrorMessage) Then
            _errorEid.Add(cr.eID & ": " & m_param_2.ErrorMessage)
          Else
            If m_param_2.SameValue = False Then iCntSuccess += 1
          End If
          Dim m_param_3 As New clsPara(cr.ParamMap.Param_3, cr.Param3, m_e)
          If Not String.IsNullOrEmpty(m_param_3.ErrorMessage) Then
            _errorEid.Add(cr.eID & ": " & m_param_3.ErrorMessage)
          Else
            If m_param_3.SameValue = False Then iCntSuccess += 1
          End If
          Dim m_param_4 As New clsPara(cr.ParamMap.Param_4, cr.Param4, m_e)
          If Not String.IsNullOrEmpty(m_param_4.ErrorMessage) Then
            _errorEid.Add(cr.eID & ": " & m_param_4.ErrorMessage)
          Else
            If m_param_4.SameValue = False Then iCntSuccess += 1
          End If
          Dim m_param_5 As New clsPara(cr.ParamMap.Param_5, cr.Param5, m_e)
          If Not String.IsNullOrEmpty(m_param_5.ErrorMessage) Then
            _errorEid.Add(cr.eID & ": " & m_param_5.ErrorMessage)
          Else
            If m_param_5.SameValue = False Then iCntSuccess += 1
          End If

        Catch ex As Exception

          ' Quiet Fail on Simpleton
          _errorEid.Add(cr.eID & ": " & ex.Message)

        End Try

      Next

      ' Commit Transaction
      m_t.Commit()

    Catch

      ' Rollback on Error
      m_t.RollBack()

    End Try

    ' Report any errors
    If _errorEid.Count > 0 Then

      If iCntSuccess = 0 Then

        ' Big Problems
        Using td As New Autodesk.Revit.UI.TaskDialog("Nothing Went According to Plan :(")
          td.MainInstruction = "No Parameters Were Updated..."
          td.MainContent = "...and other problems:" & vbCr & vbCr
          For Each x As String In _errorEid
            td.MainContent += x & vbCr
          Next
          td.ExpandedContent = "It Happens... :("
          td.Show()
        End Using

      Else

        ' Failures
        Using td As New Autodesk.Revit.UI.TaskDialog("Detected Some Mishaps")
          td.MainInstruction = iCntSuccess.ToString & " Parameters Updated Succesfully..."
          td.MainContent = "...but there were problems:" & vbCr & vbCr
          For Each x As String In _errorEid
            td.MainContent += x & vbCr
          Next
          td.ExpandedContent = "It Happens... :("
          td.Show()
        End Using

      End If

    Else

      If iCntSuccess = 0 Then

        ' Nothing to do
        Using td As New Autodesk.Revit.UI.TaskDialog("Nothing Required Changing!")
          td.MainInstruction = "Nothing mismatched and nothing failed...."
          td.Show()
        End Using

      Else

        ' No Failures, Made Changes
        Using td As New Autodesk.Revit.UI.TaskDialog("Nothing Failed!")
          td.MainInstruction = "We made some changes to parameters..."
          td.MainContent = iCntSuccess.ToString & " parameters were updated in all!"
          td.Show()
        End Using

      End If

    End If

  End Sub

#Region "Private Members"

  ''' <summary>
  ''' Load the Points into the Form
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub LoadPointsDataIntoForm()

    ' Excel
    If Me.RadioButtonExcel.IsChecked = True Then

      ' Load the Active Worksheet Data
      _s.PointsCollectionDatatable = _excel.FillDataTableFromExcelWorksheet(Me.ComboBoxExcelWorksheet.SelectedItem.ToString)

      If _s.PointsCollectionDatatable Is Nothing Then Exit Sub

      ' Update Points
      _s.PointsCollection = New List(Of clsReportingData)
      For Each dr As DataRow In _s.PointsCollectionDatatable.Rows

        ' Add the Item
        _s.PointsCollection.Add(New clsReportingData(dr))

      Next

    Else

      ' Load the Active Data
      _s.PointsCollection = _file.ReadData

    End If

    ' Bind the Data to the Control
    Me.DataGridPointsData.DataContext = _s.PointsCollection
    Me.DataGridPointsData.ItemsSource = _s.PointsCollection

  End Sub

#End Region

#Region "Form Visibility"

  ''' <summary>
  ''' Set the Form Visibility Based on State
  ''' </summary>
  ''' <param name="p_v"></param>
  ''' <remarks></remarks>
  Private Sub SetFormVisibility(p_v As formViz)
    Select Case p_v
      Case formViz.isProcessing
        Me.ProgressBar1.Visibility = Visibility.Visible
        Me.ButtonUpdate.Visibility = Visibility.Hidden
        Me.ButtonCancel.Visibility = Visibility.Hidden
      Case formViz.isStandBy
        Me.ProgressBar1.Visibility = Visibility.Hidden
        Me.ButtonUpdate.Visibility = Visibility.Visible
        Me.ButtonCancel.Visibility = Visibility.Visible
    End Select
  End Sub

#End Region

#Region "Form Controls & Events"

  ''' <summary>
  ''' Change the Worksheet Name
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ComboBoxExcelWorksheet_SelectionChanged(sender As System.Object,
                                                      e As System.Windows.Controls.SelectionChangedEventArgs) Handles ComboBoxExcelWorksheet.SelectionChanged

    ' Load the Data
    LoadPointsDataIntoForm()

  End Sub

  ''' <summary>
  ''' Commit Changes
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonUpdate_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonUpdate.Click

    ' Form Visibility
    SetFormVisibility(formViz.isProcessing)

    ' Update the Points
    UpdatePoints()

    ' Close and Exit
    Me.Close()

  End Sub

  ''' <summary>
  ''' Browse and Load Data
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonBrowse_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonBrowse.Click

    ' Disable the Button
    Me.ButtonUpdate.IsEnabled = False

    ' Open File Dialog
    Dim m_od As New Microsoft.Win32.OpenFileDialog()

    ' Load the Data - Excel
    If Me.RadioButtonExcel.IsChecked = True Then

      ' Filter
      m_od.DefaultExt = ".xlsx"
      m_od.Filter = "Excel 2010 Documents (.xlsx)|*.xlsx"
      Me.LabelFileLoad.Visibility = Visibility.Hidden
      Me.ComboBoxExcelWorksheet.Visibility = Visibility.Visible
      Me.LabelExcelWorksheet.Visibility = Visibility.Visible

      If m_od.ShowDialog = True Then

        If String.IsNullOrEmpty(m_od.FileName) Then Exit Sub

        Me.LabelFileLoad.Content = m_od.FileName
        _excel = New clsExcel(m_od.FileName, _s, False)

        ' Load the Worksheet Names
        For Each x As String In _excel.WorkSheetNames
          Try
            Me.ComboBoxExcelWorksheet.Items.Add(x)
          Catch
          End Try
        Next

        ' Set the First Worksheet Active
        If Me.ComboBoxExcelWorksheet.Items.Count > 0 Then
          Me.ComboBoxExcelWorksheet.SelectedIndex = 0
        End If

        ' Enable the Button
        Me.ButtonUpdate.IsEnabled = True

      Else

        Exit Sub

      End If

    End If

    ' Load the Data - CSV
    If Me.RadioButtonCSV.IsChecked = True Then

      ' Filter
      m_od.DefaultExt = ".csv"
      m_od.Filter = "Comma Separated Values (.csv)|*.csv"
      Me.LabelFileLoad.Visibility = Visibility.Visible
      Me.ComboBoxExcelWorksheet.Visibility = Visibility.Hidden
      Me.LabelExcelWorksheet.Visibility = Visibility.Hidden

      If m_od.ShowDialog = True Then

        If String.IsNullOrEmpty(m_od.FileName) Then Exit Sub

        Me.LabelFileLoad.Content = m_od.FileName
        _file = New clsFile(m_od.FileName, _s, eFileType.isCSV, False)

        ' Enable the Button
        Me.ButtonUpdate.IsEnabled = True

      Else

        Exit Sub

      End If

    End If

    ' Load the Data - TXT
    If Me.RadioButtonTxt.IsChecked = True Then

      ' Filter
      m_od.DefaultExt = ".txt"
      m_od.Filter = "Text Documents (Tab Delimited) (.txt)|*.txt"
      Me.LabelFileLoad.Visibility = Visibility.Visible
      Me.ComboBoxExcelWorksheet.Visibility = Visibility.Hidden
      Me.LabelExcelWorksheet.Visibility = Visibility.Hidden

      If m_od.ShowDialog = True Then

        If String.IsNullOrEmpty(m_od.FileName) Then Exit Sub

        Me.LabelFileLoad.Content = m_od.FileName
        _file = New clsFile(m_od.FileName, _s, eFileType.isTXT, False)

        ' Enable the Button
        Me.ButtonUpdate.IsEnabled = True

      Else

        Exit Sub

      End If

    End If

    ' Load the Data
    LoadPointsDataIntoForm()

  End Sub

  ''' <summary>
  ''' Cancel
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonCancel.Click
    Me.Close()
  End Sub

  ''' <summary>
  ''' Form Closing - Kill Excel
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_DataManager_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
    If Not _excel Is Nothing Then
      _excel.ShutDownExcel()
    End If
  End Sub

  ''' <summary>
  ''' Excel Radio Click - Clear Results
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub RadioButtonExcel_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles RadioButtonExcel.Click
    Me.DataGridPointsData.ItemsSource = Nothing
    Me.LabelFileLoad.Content = "Select a File to Load Data From"
    Me.LabelFileLoad.Visibility = Visibility.Hidden
    Me.ComboBoxExcelWorksheet.Visibility = Visibility.Visible
    Me.LabelExcelWorksheet.Visibility = Visibility.Visible
  End Sub

  ''' <summary>
  ''' CSV Radio Click - Clear Results
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub RadioButtonCSV_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles RadioButtonCSV.Click
    Me.DataGridPointsData.ItemsSource = Nothing
    Me.LabelFileLoad.Content = "Select a File to Load Data From"
    Me.LabelFileLoad.Visibility = Visibility.Visible
    Me.ComboBoxExcelWorksheet.Visibility = Visibility.Hidden
    Me.LabelExcelWorksheet.Visibility = Visibility.Hidden
  End Sub

  ''' <summary>
  ''' TXT Radio Click - Clear Results
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub RadioButtonTxt_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles RadioButtonTxt.Click
    Me.DataGridPointsData.ItemsSource = Nothing
    Me.LabelFileLoad.Content = "Select a File to Load Data From"
    Me.LabelFileLoad.Visibility = Visibility.Visible
    Me.ComboBoxExcelWorksheet.Visibility = Visibility.Hidden
    Me.LabelExcelWorksheet.Visibility = Visibility.Hidden
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
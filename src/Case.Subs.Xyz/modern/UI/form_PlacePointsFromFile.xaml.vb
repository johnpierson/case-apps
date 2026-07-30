Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports System.Data
Imports System.Windows.Controls

Public Class form_PlacePointsFromFile

  Private _s As clsSettings
  Private _openDlg As New Microsoft.Win32.OpenFileDialog()
  Private _Excel As clsExcel
  Private _File As clsFile
  Private _Error As New List(Of String)
  Private _Success As New List(Of String)

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)

    InitializeComponent()

    ' Widen Scope
    _s = s

    ' List Available Families for Placement
    _s.GetSymbolNames(True)
    ComboBoxFamilyName.DisplayMemberPath = "ElementFullName"
    For Each x As clsFamily In _s.FamilyTypeNames.Values
      ComboBoxFamilyName.Items.Add(x)
    Next
    ComboBoxFamilyName.SelectedIndex = 0

    ' Update Form
    Title = "Place Points From File" & _s.AppVersion
    ComboBoxExcelWorksheet.IsEnabled = False
    ProgressBar1.Visibility = Visibility.Hidden
    AllowOk()

  End Sub

#Region "Private Members"

  ''' <summary>
  ''' Place the Objects
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub PlacePoints()

    ' 0 Origin
    Dim m_origin As New Autodesk.Revit.DB.XYZ(0, 0, 0)
    Dim m_up As New Autodesk.Revit.DB.XYZ(0, 0, 1)
    Dim m_rotLine As Line = Line.CreateUnbound(m_origin, m_up)

    ' Get the Symbol
    Dim m_symbolI As clsFamily = ComboBoxFamilyName.SelectedItem
    Dim m_symbol As FamilySymbol = TryCast(m_symbolI.ElementObject, FamilySymbol)
    If m_symbol Is Nothing Then
      MsgBox("Error Getting Family Symbol for Placement", MsgBoxStyle.Critical, "Cannot Continue!")
      Exit Sub
    End If

    ' Transaction
    Dim m_t As New Transaction(_s.Doc, "CASE Point Placement from File")
    m_t.Start()

    Try
      ' Iterate Items
      For Each x As clsReportingData In _s.PointsCollection

        ' Progress
        IncrementProgress(Me.ProgressBar1, 1)

        ' Rotation Angle
        Dim m_rotationAngle As Double = 0

        Try
          ' - Get XYZ
          Dim m_x As Double = x.E
          Dim m_y As Double = x.N
          Dim m_z As Double = x.Z

          x.ElementXyz = New Autodesk.Revit.DB.XYZ(m_x, m_y, m_z)
        Catch
        End Try

        ' Did we get a valid point?
        If x.ElementXyz Is Nothing Then

          ' Record as Failure
          _Error.Add("Failed to get XYZ from element")
          Continue For

        Else

          ' Rotation for Current location
          Dim m_pp As ProjectPosition = _s.Doc.ActiveProjectLocation.GetProjectPosition(m_origin)
          m_rotationAngle = -m_pp.Angle ' * 57.2957795

        End If

        ' Update Plain Numerals
        x.E = x.ElementXyz.X
        x.N = x.ElementXyz.Y
        x.Z = x.ElementXyz.Z

        ' Place Element
        Dim m_fi As FamilyInstance = Nothing
        Try
          m_fi = _s.Doc.Create.NewFamilyInstance(x.ElementXyz, m_symbol, [Structure].StructuralType.NonStructural)

          ' Rotate it if necessary
          If Not m_rotationAngle = 0 Then

            ' Rotate the Family
            ElementTransformUtils.RotateElement(_s.Doc, m_fi.Id, m_rotLine, m_rotationAngle)

          End If

        Catch
          ' Record and report errors
          _Error.Add("Failed to place family instance m_fi")
        End Try

        ' Update All Associated Parameters
        If m_fi Is Nothing Then

          ' Log Failure
          _Error.Add("Failed to get family instance m_fi object")

        Else

          ' Update All Parameters
          x.Elem = m_fi
          x.eID = m_fi.Id.ToString

          x.GetElementData(True)

          ' Update the Parameters
          If Not String.IsNullOrEmpty(x.P) Then
            Try
              Dim m_para As New clsPara(x.ParamMap.Param_P, x.P, m_fi)
            Catch
            End Try
          End If
          If Not String.IsNullOrEmpty(x.N) Then
            Try
              Dim m_para As New clsPara(x.ParamMap.Param_N, x.N, m_fi)
            Catch
            End Try
          End If
          If Not String.IsNullOrEmpty(x.E) Then
            Try
              Dim m_para As New clsPara(x.ParamMap.Param_E, x.E, m_fi)
            Catch
            End Try
          End If
          If Not String.IsNullOrEmpty(x.Z) Then
            Try
              Dim m_para As New clsPara(x.ParamMap.Param_Z, x.Z, m_fi)
            Catch
            End Try
          End If
          If Not String.IsNullOrEmpty(x.D) Then
            Try
              Dim m_para As New clsPara(x.ParamMap.Param_D, x.D, m_fi)
            Catch
            End Try
          End If
          If Not String.IsNullOrEmpty(x.Param1) Then
            Try
              Dim m_para As New clsPara(x.ParamMap.Param_1, x.Param1, m_fi)
            Catch
            End Try
          End If
          If Not String.IsNullOrEmpty(x.Param2) Then
            Try
              Dim m_para As New clsPara(x.ParamMap.Param_2, x.Param2, m_fi)
            Catch
            End Try
          End If
          If Not String.IsNullOrEmpty(x.Param3) Then
            Try
              Dim m_para As New clsPara(x.ParamMap.Param_3, x.Param3, m_fi)
            Catch
            End Try
          End If
          If Not String.IsNullOrEmpty(x.Param4) Then
            Try
              Dim m_para As New clsPara(x.ParamMap.Param_4, x.Param4, m_fi)
            Catch
            End Try
          End If
          If Not String.IsNullOrEmpty(x.Param5) Then
            Try
              Dim m_para As New clsPara(x.ParamMap.Param_5, x.Param5, m_fi)
            Catch
            End Try
          End If

          ' Record Success
          _Success.Add("Added: ")

        End If

      Next

      ' Commit
      m_t.Commit()

    Catch

      ' Rollback
      m_t.RollBack()

    End Try

    ' Report Status to User
    Using td As New Autodesk.Revit.UI.TaskDialog("Here's What Happened:")
      If _Error.Count = 0 Then

        ' No Errors
        If _Success.Count = 0 Then

          ' Nothing Failed, Nothing Updated
          td.MainInstruction = "Nothing Failed... but..."
          td.MainContent = "Nothing was placed either..."

        Else

          ' Perfect Scenario
          td.MainInstruction = "No Failures!"
          td.MainContent = _Success.Count.ToString & " 'point' families have been placed successfully!"

        End If

      Else

        ' Errors
        If _Success.Count = 0 Then

          ' Everything Failed
          td.MainInstruction = "I've Got Some BAD News"
          td.MainContent = _Error.Count.ToString & " 'Point' Families Failed (All of Them)!"
          td.MainContent += vbCr & vbCr & "No Success to Report this Time..."

        Else

          ' Some Failures and Some Success
          td.MainInstruction = "I've Got Some Good and Some Bad News"
          td.MainContent = _Error.Count.ToString & " 'Point' Families Failed..."
          td.MainContent += vbCr & vbCr & _Error.Count.ToString & " 'point' families have been placed successfully!"

        End If

      End If

      ' Show Dialog
      td.Show()

    End Using

  End Sub

#End Region

#Region "Form Controls & Events"

  ''' <summary>
  ''' Need Data to Export
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AllowOk()
    If DataGridPoints.Items.Count < 1 Then
      ButtonPlacePoints.IsEnabled = False
    Else
      ButtonPlacePoints.IsEnabled = True
    End If
  End Sub

  ''' <summary>
  ''' Load the Datagridview
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub LoadData()

    ' Fresh List
    DataGridPoints.ItemsSource = Nothing
    DataGridPoints.DataContext = _s.PointsCollection
    DataGridPoints.ItemsSource = _s.PointsCollection

  End Sub

  ''' <summary>
  ''' Place the Points
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonPlacePoints_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonPlacePoints.Click

    ' Prime the Progress Bar
    With ProgressBar1
      .Maximum = DataGridPoints.Items.Count
      .Minimum = 0
      .Value = 0
      .Visibility = Visibility.Visible
    End With

    ' Hide Buttons
    ButtonCancel.Visibility = Visibility.Hidden
    ButtonPlacePoints.Visibility = Visibility.Hidden

    ' Place Points
    PlacePoints()

    ' '' '' Update File with ElementID After
    '' ''Dim m_todo As String = ""

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Cancel
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonCancel.Click

    ' Close 
    Close()

  End Sub

  ''' <summary>
  ''' Update Datatable from Worksheet
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ComboBoxExcelWorksheet_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles ComboBoxExcelWorksheet.SelectionChanged

    Try

      ' Update the Datatable
      _s.PointsCollectionDatatable = _Excel.FillDataTableFromExcelWorksheet(Me.ComboBoxExcelWorksheet.SelectedItem.ToString, True)

      ' Update Datagridview
      LoadData()

    Catch

    End Try

  End Sub

  ''' <summary>
  ''' Browse for a File
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonBrowse_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonBrowse.Click

    ' Excel
    If RadioButtonExcel.IsChecked = True Then

      ' Setup
      ComboBoxExcelWorksheet.Visibility = Visibility.Visible
      _openDlg.FileName = "Excel file"
      _openDlg.DefaultExt = ".xlsx"
      _openDlg.Filter = "Microsoft Excel 2010 Workbooks (.xlsx)|*.xlsx"

      ' Show open file dialog box
      If _openDlg.ShowDialog() = True Then

        ' New File
        _Excel = New clsExcel(_openDlg.FileName, _s, False)
        _File = Nothing

        ' Load and Display the Worksheet Names
        ComboBoxExcelWorksheet.Items.Clear()
        For Each x As String In _Excel.WorkSheetNames
          ComboBoxExcelWorksheet.Items.Add(x)
        Next

        ' Update the Datatable
        _s.PointsCollectionDatatable = _Excel.FillDataTableFromExcelWorksheet(Me.ComboBoxExcelWorksheet.Items(0).ToString, True)

        ' Update Points
        _s.PointsCollection = New List(Of clsReportingData)
        For Each dr As DataRow In _s.PointsCollectionDatatable.Rows
          _s.PointsCollection.Add(New clsReportingData(dr))
        Next

        ' Update Index
        ComboBoxExcelWorksheet.IsEnabled = True
        ComboBoxExcelWorksheet.SelectedIndex = 0

      End If

    End If

    ' CSV
    If RadioButtonCSV.IsChecked = True Then

      ' Setup
      ComboBoxExcelWorksheet.Visibility = Visibility.Hidden
      _openDlg.FileName = "csv file"
      _openDlg.DefaultExt = ".csv"
      _openDlg.Filter = "Comma Separated Values (.csv)|*.csv"

      ' Show open file dialog box
      If _openDlg.ShowDialog() = True Then

        ' The File
        _File = New clsFile(_openDlg.FileName, _s, eFileType.isCSV, False)
        _Excel = Nothing

        ' Update Points Data
        _s.PointsCollection = _File.ReadData
        LoadData()

      End If

    End If

    ' TXT
    If RadioButtonTxtTab.IsChecked = True Then

      ' Setup
      ComboBoxExcelWorksheet.Visibility = Visibility.Hidden
      _openDlg.FileName = "txt file"
      _openDlg.DefaultExt = ".txt"
      _openDlg.Filter = "Text documents (.txt)|*.txt"

      ' Show open file dialog box
      If _openDlg.ShowDialog() = True Then

        ' The File
        _File = New clsFile(_openDlg.FileName, _s, eFileType.isTXT, False)
        _Excel = Nothing

        ' Update Points Data
        _s.PointsCollection = _File.ReadData
        LoadData()

      End If

    End If

    ' Update Form
    AllowOk()

  End Sub

  ''' <summary>
  ''' TXT Clicked
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub RadioButtonTxtTab_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles RadioButtonTxtTab.Click
    ComboBoxExcelWorksheet.IsEnabled = False
    DataGridPoints.ItemsSource = Nothing
  End Sub

  ''' <summary>
  ''' CSV Clicked
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub RadioButtonCSV_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles RadioButtonCSV.Click
    ComboBoxExcelWorksheet.IsEnabled = False
    DataGridPoints.ItemsSource = Nothing
  End Sub

  ''' <summary>
  ''' Excel Clicked
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub RadioButtonExcel_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles RadioButtonExcel.Click
    ComboBoxExcelWorksheet.IsEnabled = True
    DataGridPoints.ItemsSource = Nothing
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
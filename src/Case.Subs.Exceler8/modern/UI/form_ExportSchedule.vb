Imports System.Diagnostics
Imports System.Windows.Forms
Imports [Case].Subs.Exceler8.Data
Imports System.Data

Public Class form_ExportSchedule

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
    Text = "Exceler8 Export Schedule v" & My.Application.Info.Version.ToString()

  End Sub

#Region "Private Members"

  ''' <summary>
  ''' Load Datagridview
  ''' </summary>
  ''' <param name="isChecked"></param>
  ''' <remarks></remarks>
  Private Sub ShowSchedules(isChecked As Boolean)

    ' Fresh List
    Dim m_schedules As New SortableBindingList(Of clsRvtSchedule)

    Try

      ' Load them into the List
      For Each x As clsRvtSchedule In _s.Schedules
        If x.FieldCount < 1 Then Continue For
        x.IsChecked = isChecked
        m_schedules.Add(x)
      Next

      ' Bind to Grid
      DataGridViewSchedules.DataSource = m_schedules

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Export Data
  ''' </summary>
  ''' <param name="schedules"></param>
  ''' <remarks></remarks>
  Private Sub ExportSchedules(schedules As List(Of clsRvtSchedule))

    For Each x In schedules

      Try

        ' Schedule Helpers
        Dim m_schExporter As New clsRvtScheduleData(x.GetViewSchedule())

        ' Add the Helper
        _s.ExportSchedules.Add(m_schExporter)

      Catch
      End Try

    Next

    ' Progress
    With ProgressBar1
      .Minimum = 0
      .Maximum = _s.ExportSchedules.Count + 1
      .Value = 1
    End With

    Try

      Dim m_excel As clsExcel

      ' File Export Kind?
      If RadioButtonMulti.Checked = True Then

        ' One File Per Category
        For Each x As clsRvtScheduleData In _s.ExportSchedules

          Try
            ProgressBar1.Increment(1)
          Catch
          End Try

          Try

            ' File Name
            Dim m_fileName As String = Replace(SaveFileDialogExcel.FileName,
                                               ".xlsx",
                                               "_" & x.Schedule.Name & ".xlsx", , ,
                                               CompareMethod.Text)

            ' New Excel Application
            m_excel = New clsExcel(m_fileName,
                                   _s,
                                   EnumExcelSrartupMode.IsSchedule)
            Dim m_names As New List(Of String)
            m_names.Add(x.Schedule.Name)

            ' Rename First Tab
            m_excel.AddWorksheets(m_names)

            Try

              ' Property Values
              Dim m_dt As List(Of List(Of String)) = x.GetData()

              ' Export to Worksheet
              m_excel.WriteWorksheetFromListOfString(x.Schedule.Name, m_dt)

            Catch
            End Try

            ' Close Excel?
            m_excel.ExcelShutDown()

          Catch
          End Try

        Next

      Else

        ' Single File - Multiple Tabs: New Excel Application
        m_excel = New clsExcel(SaveFileDialogExcel.FileName,
                               _s,
                               EnumExcelSrartupMode.IsSchedule)

        ' Create Worksheet Tab for Each Category
        Dim m_names As New List(Of String)
        For Each x As clsRvtScheduleData In _s.ExportSchedules
          m_names.Add(x.Schedule.Name)
        Next
        m_excel.AddWorksheets(m_names)

        ' Each Category
        For Each x As clsRvtScheduleData In _s.ExportSchedules

          Try
            ProgressBar1.Increment(1)
          Catch
          End Try

          Try

            ' Property Values
            Dim m_dt As List(Of List(Of String)) = x.GetData()

            ' Export to Worksheet
            m_excel.WriteWorksheetFromListOfString(x.Schedule.Name, m_dt)

          Catch
          End Try

        Next

        ' Close Excel?
        m_excel.ExcelShutDown()

      End If

    Catch
    End Try

  End Sub

#End Region

#Region "Form Controls & Events"

#Region "Form Controls & Events - Viz"

  Private Enum EnumFormViz
    IsProcessing
    IsStandby
  End Enum

  ''' <summary>
  ''' Form Controlls
  ''' </summary>
  ''' <param name="f"></param>
  ''' <remarks></remarks>
  Private Sub SetFormViz(f As EnumFormViz)

    Select Case f

      Case EnumFormViz.IsProcessing
        ProgressBar1.Show()
        ButtonCancel.Hide()
        ButtonHelp.Hide()
        ButtonCheckAll.Hide()
        ButtonCheckNone.Hide()
        ButtonOk.Hide()

      Case EnumFormViz.IsStandby
        ProgressBar1.Hide()
        ButtonCancel.Show()
        ButtonHelp.Show()
        ButtonCheckAll.Show()
        ButtonCheckNone.Show()
        ButtonOk.Show()

    End Select
  End Sub

#End Region

  ''' <summary>
  ''' Load Form Data
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_ExportSchedule_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    Try

      ' Form Viz
      SetFormViz(EnumFormViz.IsStandby)

      ' Load Schedules
      ShowSchedules(False)

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Cancel
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click

    ' Form Viz
    SetFormViz(EnumFormViz.IsProcessing)

    ' Get a Filename
    If SaveFileDialogExcel.ShowDialog = System.Windows.Forms.DialogResult.OK Then
      If Not String.IsNullOrEmpty(SaveFileDialogExcel.FileName) Then

        ' Data
        Dim m_schedules As New List(Of clsRvtSchedule)

        ' Process Each Row
        For Each x As DataGridViewRow In DataGridViewSchedules.Rows

          ' Get the Bound Item
          Dim m_sch As clsRvtSchedule = x.DataBoundItem
          If m_sch Is Nothing Then Continue For

          ' Checked?
          If m_sch.IsChecked = True Then m_schedules.Add(m_sch)

        Next

        ' Export
        ExportSchedules(m_schedules)

      End If
    End If

    ' Closse
    Close()

  End Sub

  ''' <summary>
  ''' Help
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click

    ' Help
    Process.Start("http://apps.case-inc.com/content/subscription-exceler8-revit")

  End Sub

  ''' <summary>
  ''' Uncheck All
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCheckNone_Click(sender As Object, e As EventArgs) Handles ButtonCheckNone.Click
    ShowSchedules(False)
  End Sub

  ''' <summary>
  ''' Check All
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCheckAll_Click(sender As Object, e As EventArgs) Handles ButtonCheckAll.Click
    ShowSchedules(True)
  End Sub

#End Region

End Class
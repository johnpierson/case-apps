Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports System.Windows.Forms
Imports System.Reflection
Imports System.Diagnostics
Imports [Case].Directionality.API
Imports [Case].Directionality.Data

Public Class form_Main

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

    ' Load Combobox
    UpdateParameterList()

    ' Load the Results
    UpdateDatagrid()

    ' Form Setup
    ProgressBar1.Hide()
    Text = "External Wall Facings v" & Assembly.GetExecutingAssembly.GetName.Version.ToString

  End Sub

  ''' <summary>
  ''' List the Parameters
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateParameterList()

    Try
      ' Clean List 
      ComboBoxParameter.Items.Clear()

      If Me.RadioButtonText.Checked = True Then
        For Each x As String In _s.WallParametersString
          ComboBoxParameter.Items.Add(x)
        Next
      Else
        For Each x As String In _s.WallParametersNumber
          ComboBoxParameter.Items.Add(x)
        Next
      End If

      ' Set Default
      ComboBoxParameter.SelectedIndex = 0

    Catch ex As Exception

    End Try

  End Sub

  ''' <summary>
  ''' Update the Datagrid
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateDatagrid()

    ' Clean List of Rows
    DataGridView1.Rows.Clear()

    ' Load DataGrid
    Try
      For Each x As clsExternalWalls In _s.ExternalWalls

        ' Fresh Row
        Dim m_row As New DataGridViewRow

        ' ElementID
        Dim m_txt As New DataGridViewTextBoxCell
        m_txt.Value = x.WallObject.Id.Value
        m_row.Cells.Add(m_txt)

        ' Type
        m_txt = New DataGridViewTextBoxCell
        m_txt.Value = x.WallFamilyType
        m_row.Cells.Add(m_txt)

        If Me.RadioButtonAngle.Checked = True Then
          ' Facing
          m_txt = New DataGridViewTextBoxCell
          m_txt.Value = x.WallAngle
          m_row.Cells.Add(m_txt)
        Else
          ' Facing
          m_txt = New DataGridViewTextBoxCell
          m_txt.Value = x.WallFacing
          m_row.Cells.Add(m_txt)
        End If

        ' Length
        m_txt = New DataGridViewTextBoxCell
        m_txt.Value = x.WallLength
        m_row.Cells.Add(m_txt)

        ' Level
        m_txt = New DataGridViewTextBoxCell
        m_txt.Value = x.WallLevel
        m_row.Cells.Add(m_txt)

        ' Add the Row
        DataGridView1.Rows.Add(m_row)

      Next

    Catch ex As Exception
      ' Quiet Fail
    End Try

  End Sub

  ''' <summary>
  ''' Write the Directionality Data
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub SaveDirectionData()

    ' Failure Count
    Dim iFail As Integer = 0
    ' Success Count
    Dim iSuc As Integer = 0

    ' Progress Bar
    With Me.ProgressBar1
      .Show()
      .Minimum = 0
      .Maximum = Me.DataGridView1.Rows.Count
      .Value = 0
    End With
    ButtonCancel.Hide()
    ButtonOk.Hide()
    Application.DoEvents()

    ' Transaction
    Dim m_t As New Transaction(_s.Doc, "CASE - External Wall Directionality")
    m_t.Start()

    ' Iterate Each Row
    For Each x As DataGridViewRow In Me.DataGridView1.Rows

      ' Step the Progressbar
      ProgressBar1.Increment(1)

      Dim m_e As Element = Nothing
      Try

        ' Get the Element
        Dim m_i As Integer = x.Cells(0).Value
        m_e = _s.Doc.GetElement(New ElementId(m_i))

      Catch ex As Exception

        ' Record Failure?
        iFail += 1

        Continue For

      End Try

      ' Do we have an element to twork with
      If Not m_e Is Nothing Then

        Try

          ' Get the Parameter
          Dim m_p As New clsPara(m_e.LookupParameter(ComboBoxParameter.SelectedItem.ToString))
          If Not m_p Is Nothing Then

            ' Set the Parameter
            m_p.Value = x.Cells(2).Value

            iSuc += 1

          Else

            ' Record Failure?
            iFail += 1

          End If

        Catch ex As Exception

          ' Record Failure?
          iFail += 1

          Continue For

        End Try

      End If

    Next

    ' Commit
    m_t.Commit()

    ' Report to User
    Dim m_dlg As New Autodesk.Revit.UI.TaskDialog("Here's What Happened:")
    If iFail > 0 Then
      m_dlg.MainInstruction = iFail.ToString & " values failed to record!"
      m_dlg.MainContent = iSuc.ToString & " values recorded successfully!"
    Else
      m_dlg.MainInstruction = iSuc.ToString & " values recorded successfully!"
    End If
    m_dlg.Show()

  End Sub

#Region "Form Controls & Events"

  ''' <summary>
  ''' Help Documentation
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As System.Object, e As System.EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/free-external-wall-directions")
  End Sub

  ''' <summary>
  ''' CASE Site
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
    Process.Start("http://www.case-inc.com")
  End Sub

  ''' <summary>
  ''' Text Mode
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub RadioButtonText_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonText.CheckedChanged
    UpdateParameterList()
    UpdateDatagrid()
  End Sub

  ''' <summary>
  ''' Angle Mode
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub RadioButtonAngle_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonAngle.CheckedChanged
    UpdateParameterList()
    UpdateDatagrid()
  End Sub

  ''' <summary>
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOk.Click

    ' Process Items
    SaveDirectionData()

    ' Close 
    Close()

  End Sub

  ''' <summary>
  ''' Cancel
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCancel.Click

    ' Close
    Close()

  End Sub

#End Region

End Class
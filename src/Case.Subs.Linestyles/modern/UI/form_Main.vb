Imports System.Windows.Forms
Imports [Case].Subs.Linestyles.Data

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

    ' Form configurations
    Text = My.Application.Info.Title.ToString & " v" & My.Application.Info.Version.ToString
    ProgressBar1.Hide()

    ' If no Current Selection, DIM the Selection Option
    If _s.ActiveUiDoc.Selection.GetElementIds.Count > 0 Then
      RadioButtonScopeSelected.Enabled = True
      RadioButtonScopeSelected.Checked = True
      RadioButtonScopeAll.Enabled = True
    Else
      RadioButtonScopeSelected.Enabled = False
      RadioButtonScopeAll.Checked = True
      RadioButtonScopeAll.Enabled = False
    End If

    ' Bind to the Combo Boxes
    For Each x In _s.LineTypes

      Try

        ' Grid Row
        Dim m_r As New DataGridViewRow

        ' text Column
        Dim m_t As New DataGridViewTextBoxCell
        m_t.Value = x
        m_r.Cells.Add(m_t)

        ' Combo Cell
        Dim m_c As New DataGridViewComboBoxCell
        For Each g In _s.LineTypes
          m_c.Items.Add(g)
          If g = x Then
            m_c.Value = g
          End If
        Next
        m_r.Cells.Add(m_c)

        ' Add the Row to the Grid
        DataGridViewViews.Rows.Add(m_r)

      Catch ex As Exception

      End Try

    Next

  End Sub

#Region "Form Events and Controls"

  ''' <summary>
  ''' Help
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/subscription-change-and-replace-line-styles")
  End Sub

  ''' <summary>
  ''' Cancel
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(ByVal sender As System.Object,
                                 ByVal e As EventArgs) Handles ButtonCancel.Click
    Close()
  End Sub

  ''' <summary>
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonSwapStyles_Click(sender As System.Object,
                                     e As EventArgs) Handles ButtonSwapStyles.Click
    
    ' Iterate looking for Differences
    For Each x As DataGridViewRow In DataGridViewViews.Rows

      ' Are they Different?
      If x.Cells(0).Value.ToString <> x.Cells(1).Value.ToString Then

        Try

          ' Update the Styles
          _s.ReplaceStyles(x.Cells(0).Value.ToString,
                           x.Cells(1).Value.ToString,
                           RadioButtonScopeSelected.Checked)
          
        Catch
        End Try

      End If

    Next

    ' Close the Form
    Close()

  End Sub

#End Region

End Class
Imports System.Reflection
Imports [Case].LineChanger.Data

Public Class form_Main

  Private _s As clsSettings

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scoper
    _s = s

  End Sub

#Region "Form Controls & Events"

  ''' <summary>
  ''' Setup
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_Main_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    Try
      ' Form configurations
      Text = "Change and Replace Line Styles - " & "v" & Assembly.GetExecutingAssembly.GetName.Version.ToString

      ' Hide Progressbar
      ProgressBar1.Hide()

      ' If no Current Selection, DIM the Selection Option
      If _s.ActiveUiDoc.Selection.GetElementIds().Count > 0 Then
        RadioButtonScopeSelected.Enabled = True
        RadioButtonScopeSelected.Checked = True
        RadioButtonScopeAll.Enabled = True
      Else
        RadioButtonScopeSelected.Enabled = False
        RadioButtonScopeAll.Checked = True
        RadioButtonScopeAll.Enabled = False
      End If

      ' Bind to the Combo Boxes
      ComboBoxStyleFind.DataSource = _s.LineTypes
      ComboBoxStyleFind.DisplayMember = "Name"
      For Each x As String In _s.LineTypes
        ComboBoxStyleReplace.Items.Add(x)
      Next
      ComboBoxStyleReplace.SelectedIndex = 0

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Help Documentation
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As System.Object, e As System.EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/free-change-and-replace-line-styles")
  End Sub

  ''' <summary>
  ''' Cancel and do nothing
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(ByVal sender As System.Object,
                                 ByVal e As System.EventArgs) Handles ButtonCancel.Click
    Close()
  End Sub

  ''' <summary>
  ''' Commit Changes
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonSwapStyles_Click(sender As System.Object,
                                     e As System.EventArgs) Handles ButtonSwapStyles.Click

    ' Replace the Styles
    _s.ReplaceStyles(ComboBoxStyleFind.SelectedItem.ToString,
                             ComboBoxStyleReplace.SelectedItem.ToString,
                             RadioButtonScopeSelected.Checked)
    Close()
  End Sub

#End Region

End Class
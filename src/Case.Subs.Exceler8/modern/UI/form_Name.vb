Imports System.Windows.Forms

Public Class form_Name

  ''' <summary>
  ''' No Name
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As System.Object, e As EventArgs) Handles ButtonCancel.Click

    ' Set Name Blank
    TextBoxName.Text = ""

    ' Close 
    Close()

  End Sub

  ''' <summary>
  ''' OK
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As System.Object, e As EventArgs) Handles ButtonOk.Click

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Enter in Textbox
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub TextBoxName_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxName.KeyDown
    If e.KeyCode = System.Windows.Forms.Keys.Enter Then
      ButtonOk_Click(Nothing, Nothing)
    End If
  End Sub

End Class
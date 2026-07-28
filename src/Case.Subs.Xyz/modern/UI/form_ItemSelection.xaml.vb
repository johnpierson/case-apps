Public Class form_ItemSelection

  Public Property SelectionName As String
  Public Property SelectionFamily As clsFamily

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="p_c">List of Categories to Include</param>
  ''' <param name="p_t">Title for Form</param>
  ''' <remarks></remarks>
  Public Sub New(p_c As List(Of String), Optional p_t As String = "Select Item")
    InitializeComponent()
    ' Load the Categories
    For Each x As String In p_c
      Me.ComboBoxItems.Items.Add(x)
    Next
    ' Title
    Me.Title = p_t
    ' Select the First
    Me.ComboBoxItems.SelectedIndex = 0
  End Sub

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="p_c"></param>
  ''' <param name="p_t"></param>
  ''' <remarks></remarks>
  Public Sub New(p_c As List(Of clsFamily), Optional p_t As String = "Select Family Item")
    InitializeComponent()
    ' Load the Categories
    Me.ComboBoxItems.DisplayMemberPath = "ElementFullName"
    For Each x As clsFamily In p_c
      Me.ComboBoxItems.Items.Add(x)
    Next
    ' Title
    Me.Title = p_t
    ' Select the First
    Me.ComboBoxItems.SelectedIndex = 0
  End Sub

#Region "Form Controls & Events"

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
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonOk.Click
    SelectionName = Me.ComboBoxItems.SelectedItem.ToString
    Try
      SelectionFamily = Me.ComboBoxItems.SelectedItem
    Catch
    End Try
    Me.Close()
  End Sub

#End Region

End Class
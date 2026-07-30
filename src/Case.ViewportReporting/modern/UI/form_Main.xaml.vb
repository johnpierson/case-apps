Imports [Case].ViewportReporting.API
Imports [Case].ViewportReporting.Data

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

  End Sub

#Region "Form Controls & Events"

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
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonOk.Click

    ' To Do: Add Code

    ' Close the Form
    Close()

  End Sub

  ''' <summary>
  ''' Load the Data
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_Main_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated

    ' Get All Viewports
    _s.GetViewports()

    ' Bind to Control
    DataGridViewports.ItemsSource = _s.ViewPortInstances

  End Sub

#End Region

End Class

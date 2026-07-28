Imports System.Windows.Forms
Imports [Case].Subs.ViewSync.Data

Public Class form_Tag

  Private _s As clsSettings
  Private _viewports As New List(Of clsVP)

  Friend Fs As clsFam = Nothing
  Friend Vp As clsVp
  Friend IsCancel As Boolean = False

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

  ''' <summary>
  ''' Load the Viewport Data
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub LoadViewports()

    ' Fresh List
    _viewports = New List(Of clsVp)

    Try

      ' Viewports
      For Each x As clsVp In _s.ConfigData.ViewData.ViewPorts.Values

        ' Ignore Deleted
        If x.ViewState.ToLower = "deleted" Then Continue For

        ' Filter by Name
        If Not String.IsNullOrEmpty(TextBoxShtName.Text) Then
          If Not x.SheetName.ToLower.Contains(TextBoxShtName.Text.ToLower) Then Continue For
        End If
        If Not String.IsNullOrEmpty(TextBoxShtNo.Text) Then
          If Not x.SheetNumber.ToLower.Contains(TextBoxShtNo.Text.ToLower) Then Continue For
        End If
        If Not String.IsNullOrEmpty(TextBoxDetNo.Text) Then
          If Not x.DetailNumber.ToLower.Contains(TextBoxDetNo.Text.ToLower) Then Continue For
        End If
        If Not String.IsNullOrEmpty(TextBoxViewRef.Text) Then
          If Not x.ViewName.ToLower.Contains(TextBoxViewRef.Text.ToLower) Then Continue For
        End If

        ' Load the Item
        _viewports.Add(x)

      Next

      ' Bind to the Control
      DataGridViewFamilies.DataSource = _viewports

    Catch
    End Try

  End Sub

#Region "Form Controls & Events"

  ''' <summary>
  ''' Startup
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_Tag_Load(sender As Object, e As EventArgs) Handles Me.Load

    Try

      ' Title
      Text = "Subscription View Tag Sync, Place View Reference Tag" & _s.Version

      ' Load the Tags
      Dim _sym As New SortedDictionary(Of String, clsFam)
      For Each x As clsFam In _s.ConfigData.Families
        _sym.Add(x.DisplayName, x)
      Next
      For Each x As clsFam In _sym.Values
        ComboBoxTagFamily.Items.Add(x)
      Next
      ComboBoxTagFamily.DisplayMember = "DisplayName"
      ComboBoxTagFamily.SelectedIndex = 0

    Catch
    End Try

    ' Load the Viewports
    LoadViewports()

  End Sub

  ''' <summary>
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click

    ' Family Symbol
    If ComboBoxTagFamily.SelectedIndex > -1 Then
      Try
        Fs = ComboBoxTagFamily.SelectedItem
      Catch
        Fs = Nothing
      End Try
    End If

    ' Selected Viewport
    For Each x As DataGridViewRow In DataGridViewFamilies.SelectedRows
      Try
        Vp = x.DataBoundItem
      Catch
      End Try
    Next

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Close
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click

    ' Close
    IsCancel = True
    Close()

  End Sub

  Private Sub TextBoxShtNo_TextChanged(sender As Object, e As EventArgs) Handles TextBoxShtNo.TextChanged
    LoadViewports()
  End Sub

  Private Sub TextBoxShtName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxShtName.TextChanged
    LoadViewports()
  End Sub

  Private Sub TextBoxDetNo_TextChanged(sender As Object, e As EventArgs) Handles TextBoxDetNo.TextChanged
    LoadViewports()
  End Sub

  Private Sub TextBoxViewRef_TextChanged(sender As Object, e As EventArgs) Handles TextBoxViewRef.TextChanged
    LoadViewports()
  End Sub

#End Region

End Class
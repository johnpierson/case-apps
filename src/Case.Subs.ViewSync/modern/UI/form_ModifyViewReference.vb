Imports System.Windows.Forms
Imports [Case].Subs.ViewSync.Data

Public Class form_ModifyViewReference

  Private _s As clsSettings

  Friend Vp As clsVp = Nothing

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s

    ' Load Items
    FilterListings()

    Text = "Subscription View Tag Sync, Modify View Reference" & _s.Version

  End Sub

  ''' <summary>
  ''' Check if Filter Matches Item
  ''' </summary>
  ''' <param name="vp"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function CheckItemPasses(vp As clsVp) As Boolean

    Try

      ' Texy Filtering
      If Not String.IsNullOrEmpty(TextBoxDetNo.Text) Then
        If Not vp.DetailNumber.ToLower.Contains(TextBoxDetNo.Text.ToLower) Then Return False
      End If
      If Not String.IsNullOrEmpty(TextBoxShtName.Text) Then
        If Not vp.SheetName.ToLower.Contains(TextBoxShtName.Text.ToLower) Then Return False
      End If
      If Not String.IsNullOrEmpty(TextBoxShtNo.Text) Then
        If Not vp.SheetNumber.ToLower.Contains(TextBoxShtNo.Text.ToLower) Then Return False
      End If
      If Not String.IsNullOrEmpty(TextBoxViewRef.Text) Then
        If Not vp.ViewName.ToLower.Contains(TextBoxViewRef.Text.ToLower) Then Return False
      End If

      ' Passes
      Return True

    Catch
    End Try

    ' Failure
    Return False

  End Function

  ''' <summary>
  ''' Load the Items into the Grid 
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub FilterListings()

    ' Fresh Lists
    Dim _tagsFind As New clsSortableBindingList(Of clsVp)

    Try

      ' Find
      For Each x In _s.ConfigData.ViewData.ViewPorts.Values

        Try
          If CheckItemPasses(x) = True Then
            _tagsFind.Add(x)
          End If
        Catch
        End Try

      Next

    Catch
    End Try

    Try

      ' Bind to Controls
      DataGridViewViews.DataSource = _tagsFind

    Catch
    End Try

  End Sub

#Region "Form Controls & Events"

  ''' <summary>
  ''' Cancel
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click

    ' Cancel
    Close()

  End Sub

  ''' <summary>
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click

    Try
      ' Return the Selected View
      For Each x As DataGridViewRow In DataGridViewViews.SelectedRows
        Vp = x.DataBoundItem
      Next
    Catch
    End Try

    ' Close
    Close()

  End Sub

  Private Sub TextBoxShtNo_TextChanged(sender As Object, e As EventArgs) Handles TextBoxShtNo.TextChanged
    FilterListings()
  End Sub

  Private Sub TextBoxDetNo_TextChanged(sender As Object, e As EventArgs) Handles TextBoxDetNo.TextChanged
    FilterListings()
  End Sub

  Private Sub TextBoxShtName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxShtName.TextChanged
    FilterListings()
  End Sub

  Private Sub TextBoxViewRef_TextChanged(sender As Object, e As EventArgs) Handles TextBoxViewRef.TextChanged
    FilterListings()
  End Sub

#End Region

End Class
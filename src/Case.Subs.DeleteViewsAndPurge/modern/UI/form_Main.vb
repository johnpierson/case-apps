Imports Autodesk.Revit.DB
Imports System.Windows.Forms
Imports [Case].Subs.DeleteViewsAndPurge.Data

Public Class form_Main

  Private _s As clsSettings

  Private _viewTypes As New Dictionary(Of Integer, clsViewType)

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

#Region "Private Members"
  
  ''' <summary>
  ''' Update the List of Views
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateViewsList()

    Try
      ' Load Datagrid Views
      Dim m_views As New SortableBindingList(Of clsViews)
      For Each x In _s.Views
        Try
          If _viewTypes.ContainsKey(x.GetViewFamilyTypeId.Value) Then
            If _viewTypes(x.GetViewFamilyTypeId.Value).isChecked = True Then
              m_views.Add(x)
            End If
          End If
        Catch
        End Try
      Next
      DataGridViewViews.DataSource = m_views
    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Purge Selected Items
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub DeleteSelections()

    Dim m_iCntshts As Integer = 0
    Dim m_iCntview As Integer = 0
    Dim m_iCntLinks As Integer = 0

    ' Delete Sheets
    For Each y As DataGridViewRow In DataGridViewSheets.Rows

      Try
        ' Step the Progressbar
        ProgressBar1.Increment(1)
      Catch
      End Try

      ' Do Sheets?
      If CheckBoxSheets.Checked = False Then Continue For
      Dim x As clsSheets = y.DataBoundItem

      ' Checked?
      If x.IsChecked = False Then Continue For

      ' Start a New Transaction
      Using t As New Transaction(_s.Doc, "Delete Sheet: " & x.Number)
        If t.Start Then

          Try

            ' Delete It
            _s.Doc.Delete(x.GetId)

            ' Commit
            t.Commit()

            m_iCntshts += 1

          Catch
          End Try

        End If
      End Using

    Next

    ' Delete Views
    For Each y As DataGridViewRow In DataGridViewViews.Rows

      Try
        ' Step the Progressbar
        ProgressBar1.Increment(1)
      Catch
      End Try

      ' Do Views?
      If CheckBoxViews.Checked = False Then Continue For
      Dim x As clsViews = y.DataBoundItem

      ' Checked?
      If x.IsChecked = False Then Continue For

      ' Start a New Transaction
      Using t As New Transaction(_s.Doc, "Delete View: " & x.Name)
        If t.Start Then

          Try

            ' Delete It
            _s.Doc.Delete(x.GetId)

            ' Commit
            t.Commit()

            m_iCntview += 1

          Catch
          End Try

        End If
      End Using

    Next

    ' Delete Links
    For Each y As DataGridViewRow In DataGridViewLinks.Rows

      Try
        ' Step the Progressbar
        ProgressBar1.Increment(1)
      Catch
      End Try

      ' Do Views?
      If CheckBoxLinks.Checked = False Then Continue For
      Dim x As clsLinks = y.DataBoundItem

      ' Checked?
      If x.IsChecked = False Then Continue For

      ' Start a New Transaction
      Using t As New Transaction(_s.Doc, "Delete Link: " & x.Link)
        If t.Start Then

          Try

            ' Delete It
            _s.Doc.Delete(x.GetId)

            ' Commit
            t.Commit()

            m_iCntLinks += 1

          Catch
          End Try

        End If
      End Using

    Next

    ' Tell the User
    Using td As New Autodesk.Revit.UI.TaskDialog("Here's What Just Happened!:")
      td.MainInstruction = m_iCntshts.ToString & " Sheets were removed..." & vbCr &
          m_iCntview.ToString & " Views were removed..." & vbCr & vbCr &
          m_iCntLinks.ToString & " Revit Links succesfully removed..." & vbCr & vbCr &
          "Please use the Purge command (repeatedly) if you need to remove unused items from the model"
      td.MainContent = "If this makes you sad, CLOSE and DO NOT save the model!"
      td.TitleAutoPrefix = False
      td.Show()
    End Using

  End Sub

  ''' <summary>
  ''' Delete the Unused Types
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub DeleteUnusedTypes()

    ' Purge Count
    Dim m_catCount As Integer = 0
    For Each x In _s.Doc.Settings.Categories
      m_catCount += 0
    Next

    ' Progress Bar
    With ProgressBar1
      .Value = 0
      .Minimum = 0
      MaximizeBox = m_catCount
    End With

    ' Get Unused types
    For Each x As Category In _s.Doc.Settings.Categories

      Try
        ' Step the Progress
        ProgressBar1.Increment(1)
        Application.DoEvents()
      Catch
      End Try

      ' Get the Elements
      Dim m_ebc As New clsElementsByCategory(x, _s.Doc)
      If m_ebc.TypeElements.Count < 1 Then Continue For
      If m_ebc.PurgeItems.Values.Count < 1 Then Continue For

      ' Transaction
      Using m_t As New Transaction(_s.Doc, "Purging Unused Types - " & x.Name)
        If m_t.Start() Then

          Try

            ' Delete the Unused Types
            Dim m_purge As New List(Of ElementId)
            For Each t As ElementId In m_ebc.PurgeItems.Values
              m_purge.Add(t)
            Next

            ' Delete from Model
            _s.Doc.Delete(m_purge)

            ' Commit
            m_t.Commit()

          Catch
          End Try

        End If
      End Using

    Next

  End Sub

#End Region

#Region "Form Visibility"

  Private Enum EnumFormViz
    IsProcessing
    IsStandBy
  End Enum

  ''' <summary>
  ''' Set the Form Visibility Basedon State
  ''' </summary>
  ''' <param name="v"></param>
  ''' <remarks></remarks>
  Private Sub SetFormViz(v As EnumFormViz)

    Select Case v

      Case EnumFormViz.isProcessing
        CheckBoxSheets.Enabled = False
        CheckBoxViews.Enabled = False
        CheckBoxLinks.Enabled = False
        ProgressBar1.Show()
        ButtonCancel.Hide()
        ButtonOk.Hide()
        ButtonHelp.Hide()

      Case EnumFormViz.isStandBy
        ProgressBar1.Hide()
        ButtonCancel.Show()
        ButtonOk.Show()
        ButtonHelp.Show()

    End Select

  End Sub

#End Region

#Region "Form Controls & Events"

  ''' <summary>
  ''' Anything to Delete?
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_Main_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    ' Form Setup
    Text = "Subscription Delete Sheets, Views and Links v" & My.Application.Info.Version.ToString
    SetFormViz(EnumFormViz.IsStandBy)

    ' Get Data
    _s.GetLinks()
    _s.GetSheets()
    _s.GetViews()

    ' Any Data?
    If _s.Views.Count + _s.Sheets.Count + _s.Links.Count = 0 Then
      MsgBox("Good News:" & vbCr & "There's nothing left to delete!!", MsgBoxStyle.Exclamation, "All Gone!")
      Close()
    End If

    Try

      With CheckBoxTypes
        .Checked = False
        .Hide()
      End With

      Try
        ' Load Datagrid Viewtypes
        Dim m_viewTypes = New SortableBindingList(Of clsViewType)
        For Each x In _s.ViewTypes.Values
          If x.Count < 1 Then Continue For

          ' Add to Master Dictionary
          _viewTypes.Add(x.GetId, x)

          ' Add to Data View
          m_viewTypes.Add(x)

        Next
        DataGridViewViewTypes.DataSource = m_viewTypes
      Catch
      End Try

      Try
        ' Load Datagrid Views
        Dim m_views As New SortableBindingList(Of clsViews)
        For Each x In _s.Views
          m_views.Add(x)
        Next
        DataGridViewViews.DataSource = m_views
      Catch
      End Try

      Try
        ' Load Datagrid Sheets
        Dim m_sheets As New SortableBindingList(Of clsSheets)
        For Each x In _s.Sheets
          m_sheets.Add(x)
        Next
        DataGridViewSheets.DataSource = m_sheets
      Catch
      End Try

      Try
        ' Load Datagrid Links
        Dim m_links As New SortableBindingList(Of clsLinks)
        For Each x In _s.Links
          m_links.Add(x)
        Next
        DataGridViewLinks.DataSource = m_links
      Catch
      End Try

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As System.Object, e As EventArgs) Handles ButtonOk.Click

    ' Are they sure?
    If MsgBox("It is recommended that you only run this utility on detached models!!" & vbCr & vbCr &
              "Are You Sure You Want to Continue??" & vbCr & "Removing Links Cannot be Undone!",
              MsgBoxStyle.YesNoCancel,
              "Are You  SURE?!") = MsgBoxResult.Yes Then

      ' Set for Visibility
      SetFormViz(EnumFormViz.IsProcessing)

      ' Total Counts
      Dim m_total As Integer = 0
      If CheckBoxSheets.Checked = True Then m_total += DataGridViewSheets.Rows.Count
      If CheckBoxViews.Checked = True Then m_total += DataGridViewViews.Rows.Count
      If CheckBoxLinks.Checked = True Then m_total += DataGridViewLinks.Rows.Count

      ' Prep Progress Bar
      With ProgressBar1
        .Minimum = 0
        .Maximum = m_total
        .Value = 0
      End With

      ' Start Purging
      DeleteSelections()

      ' Types?
      If CheckBoxTypes.Checked = True Then
        DeleteUnusedTypes()
        DeleteUnusedTypes()
        DeleteUnusedTypes()
        DeleteUnusedTypes()
      End If

      ' Close
      Close()

    End If

  End Sub

  ''' <summary>
  ''' Close
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCancel.Click

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Launch Case Site
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
    Process.Start("http://www.case-inc.com")
  End Sub

  ''' <summary>
  ''' Select All Sheets
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonSheetsAll_Click(sender As System.Object, e As System.EventArgs) Handles ButtonSheetsAll.Click

    Try
      ' Load Datagrid Sheets
      Dim m_sheets As New SortableBindingList(Of clsSheets)
      For Each x In _s.Sheets
        x.IsChecked = True
        m_sheets.Add(x)
      Next
      DataGridViewSheets.DataSource = m_sheets
    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Unselect All Sheets
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonSheetsNone_Click(sender As System.Object, e As System.EventArgs) Handles ButtonSheetsNone.Click

    Try
      ' Load Datagrid Sheets
      Dim m_sheets As New SortableBindingList(Of clsSheets)
      For Each x In _s.Sheets
        x.IsChecked = False
        m_sheets.Add(x)
      Next
      DataGridViewSheets.DataSource = m_sheets
    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Select All Views
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonViewsAll_Click(sender As System.Object, e As System.EventArgs) Handles ButtonViewsAll.Click

    ' Check all
    For Each x As clsViews In _s.Views
      x.IsChecked = True
    Next

    Try
      ' Load Datagrid Views
      UpdateViewsList()
    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Unselect All Views
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonViewsNone_Click(sender As System.Object, e As System.EventArgs) Handles ButtonViewsNone.Click

    ' Check all
    For Each x As clsViews In _s.Views
      x.IsChecked = False
    Next

    Try
      ' Load Datagrid Views
      UpdateViewsList()
    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Select All Links
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonLinksAll_Click(sender As System.Object, e As System.EventArgs) Handles ButtonLinksAll.Click

    Try
      ' Load Datagrid Links
      Dim m_links As New SortableBindingList(Of clsLinks)
      For Each x In _s.Links
        x.IsChecked = True
        m_links.Add(x)
      Next
      DataGridViewLinks.DataSource = m_links
    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Unselect All Links
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonLinksNone_Click(sender As System.Object, e As System.EventArgs) Handles ButtonLinksNone.Click

    Try
      ' Load Datagrid Links
      Dim m_links As New SortableBindingList(Of clsLinks)
      For Each x In _s.Links
        x.IsChecked = False
        m_links.Add(x)
      Next
      DataGridViewLinks.DataSource = m_links
    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Sheets
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub CheckBoxSheets_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxSheets.CheckedChanged
    ButtonSheetsAll.Enabled = CheckBoxSheets.Checked
    ButtonSheetsNone.Enabled = CheckBoxSheets.Checked
    DataGridViewSheets.Enabled = CheckBoxSheets.Checked
  End Sub

  ''' <summary>
  ''' Views
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub CheckBoxViews_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxViews.CheckedChanged
    ButtonViewsAll.Enabled = CheckBoxViews.Checked
    ButtonViewsNone.Enabled = CheckBoxViews.Checked
    DataGridViewViews.Enabled = CheckBoxViews.Checked
  End Sub

  ''' <summary>
  ''' Links
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub CheckBoxLinks_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxLinks.CheckedChanged
    ButtonLinksAll.Enabled = CheckBoxLinks.Checked
    ButtonLinksNone.Enabled = CheckBoxLinks.Checked
    DataGridViewLinks.Enabled = CheckBoxLinks.Checked
  End Sub

  ''' <summary>
  ''' View Type Selection Change
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub DataGridViewViewTypes_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridViewViewTypes.CellMouseUp
    Try

      ' Get the Checkbox
      Dim m_ch As DataGridViewCheckBoxCell = DataGridViewViewTypes.Rows(e.RowIndex).Cells(0)
      Dim m_vt As clsViewType = DataGridViewViewTypes.Rows(e.RowIndex).DataBoundItem

      ' Is it Dirty?
      If m_ch.EditingCellValueChanged = True Then

        ' Add or Remove Items
        If _viewTypes.ContainsKey(m_vt.GetId) Then
          _viewTypes(m_vt.GetId).IsChecked = Not m_ch.Value
          UpdateViewsList()
          Exit Sub
        End If

      End If

    Catch
    End Try
  End Sub

  ''' <summary>
  ''' Help
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/subscription-delete-sheets-views-and-revit-links")
  End Sub

#End Region

End Class
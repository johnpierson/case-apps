Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports System.Windows.Forms
Imports [Case].Subs.ViewSync.Data

Public Class form_Find

  Private _s As clsSettings

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="s">Settings</param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s

    ' Setup
    DoSetup()

  End Sub

  ''' <summary>
  ''' Form Setup
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub DoSetup()

    ' Title
    Text = "Subscription View Tag Sync, Find Tags" & _s.Version
    If _s.FamTagHelpers(EnumTagState.IsOrphan).Count > 0 Then
      TabControl1.SelectedTab = TabControl1.TabPages(2)
    Else
      If _s.FamTagHelpers(EnumTagState.IsNull).Count > 0 Then
        TabControl1.SelectedTab = TabControl1.TabPages(1)
      End If
    End If

    ' Load Items
    FilterListings()

  End Sub

#Region "Private Members"

  ''' <summary>
  ''' Check if Filter Matches Item
  ''' </summary>
  ''' <param name="fi"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function CheckItemPasses(fi As clsTagInstance) As Boolean

    Try

      ' '' '' Comment Required?
      '' ''If CheckBoxComments.Checked = True Then
      '' ''  If String.IsNullOrEmpty(fi.Comments) Then Return False
      '' ''End If

      ' Texy Filtering
      If Not String.IsNullOrEmpty(TextBoxComments.Text) Then
        If Not fi.Comments.ToLower.Contains(TextBoxComments.Text.ToLower) Then Return False
      End If
      If Not String.IsNullOrEmpty(TextBoxDetNo.Text) Then
        If Not fi.DetailNumber.ToLower.Contains(TextBoxDetNo.Text.ToLower) Then Return False
      End If
      '' ''If Not String.IsNullOrEmpty(TextBoxFamType.Text) Then
      '' ''  If Not fi.FamType.ToLower.Contains(TextBoxFamType.Text.ToLower) Then Return False
      '' ''End If
      If Not String.IsNullOrEmpty(TextBoxShtName.Text) Then
        If Not fi.SheetName.ToLower.Contains(TextBoxShtName.Text.ToLower) Then Return False
      End If
      If Not String.IsNullOrEmpty(TextBoxShtNo.Text) Then
        If Not fi.SheetNumber.ToLower.Contains(TextBoxShtNo.Text.ToLower) Then Return False
      End If
      '' ''If Not String.IsNullOrEmpty(TextBoxViewPlaced.Text) Then
      '' ''  If Not fi.ViewPlaced.ToLower.Contains(TextBoxViewPlaced.Text.ToLower) Then Return False
      '' ''End If
      '' ''If Not String.IsNullOrEmpty(TextBoxViewRef.Text) Then
      '' ''  If Not fi.ViewName.ToLower.Contains(TextBoxViewPlaced.Text.ToLower) Then Return False
      '' ''End If

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
    Dim m_tagsFind As New clsSortableBindingList(Of clsTagInstance)
    Dim m_tagsNull As New clsSortableBindingList(Of clsTagInstance)
    Dim m_tagsOrphaned As New clsSortableBindingList(Of clsTagInstance)

    Try

      ' Find
      For Each x In _s.FamTagHelpers(EnumTagState.IsOk)

        Try
          If CheckItemPasses(x) = True Then
            m_tagsFind.Add(x)
          End If
        Catch
        End Try

      Next

      ' Null
      For Each x In _s.FamTagHelpers(EnumTagState.IsNull)
        Try
          If CheckItemPasses(x) = True Then
            m_tagsNull.Add(x)
          End If
        Catch
        End Try
      Next

      ' Orphaned
      For Each x In _s.FamTagHelpers(EnumTagState.IsOrphan)
        Try
          If CheckItemPasses(x) = True Then
            m_tagsOrphaned.Add(x)
          End If
        Catch
        End Try
      Next

    Catch
    End Try

    Try

      ' Set Tab Names
      If _s.FamTagHelpers(EnumTagState.IsOk).Count > 0 Then
        TabPage1.Text = "Tags in Good Standing (" & m_tagsFind.Count.ToString & " of " &
           _s.FamTagHelpers(EnumTagState.IsOk).Count.ToString & ")"
      Else
        With TabPage1
          .Text = "Tags in Good Standing (None Found)"
        End With
      End If
      If _s.FamTagHelpers(EnumTagState.IsNull).Count > 0 Then
        TabPage2.Text = "Tags without Reference Data (" & m_tagsNull.Count.ToString & " of " &
           _s.FamTagHelpers(EnumTagState.IsNull).Count.ToString & ")"
        TabPage2.ImageIndex = 0
      Else
        With TabPage2
          .Text = "Tags without Reference Data (None Found)"
          .ImageIndex = 2
        End With
      End If
      If _s.FamTagHelpers(EnumTagState.IsOrphan).Count > 0 Then
        TabPage3.Text = "Tags Referencing Missing Views (" & m_tagsOrphaned.Count.ToString & " of " &
           _s.FamTagHelpers(EnumTagState.IsOrphan).Count.ToString & ")"
        TabPage3.ImageIndex = 1
      Else
        With TabPage3
          .Text = "Tags Referencing Missing Views (None Found)"
          .ImageIndex = 2
        End With
      End If

      ' Bind to Controls
      DataGridViewFind.DataSource = m_tagsFind
      DataGridViewNulls.DataSource = m_tagsNull
      DataGridViewOrphaned.DataSource = m_tagsOrphaned

    Catch
    End Try

  End Sub

#End Region

#Region "Form Controls & Events"

  Private Sub TextBoxShtNo_TextChanged(sender As Object, e As EventArgs) Handles TextBoxShtNo.TextChanged
    FilterListings()
  End Sub

  Private Sub TextBoxShtName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxShtName.TextChanged
    FilterListings()
  End Sub

  Private Sub TextBoxDetNo_TextChanged(sender As Object, e As EventArgs) Handles TextBoxDetNo.TextChanged
    FilterListings()
  End Sub

  Private Sub TextBoxViewRef_TextChanged(sender As Object, e As EventArgs) Handles TextBoxViewRef.TextChanged
    FilterListings()
  End Sub

  '' ''Private Sub TextBoxFamType_TextChanged(sender As System.Object, e As System.EventArgs)
  '' ''  FilterListings()
  '' ''End Sub

  '' ''Private Sub TextBoxViewPlaced_TextChanged(sender As System.Object, e As System.EventArgs)
  '' ''  FilterListings()
  '' ''End Sub

  Private Sub TextBoxComments_TextChanged(sender As Object, e As EventArgs) Handles TextBoxComments.TextChanged
    FilterListings()
  End Sub

  '' ''Private Sub CheckBoxComments_CheckedChanged(sender As System.Object, e As System.EventArgs)
  '' ''  FilterListings()
  '' ''End Sub

#End Region

#Region "Form Controls & Events - Context Menus"

  Private Sub ZoomToTagFind_Click(sender As Object, e As EventArgs) Handles ZoomToTagFind.Click

    Try

      ' Clear Selection
      _s.SelectedElements.Clear()

      ' Get the Current Element
      For Each x As DataGridViewRow In DataGridViewFind.SelectedRows
        Dim m_e As Element = x.DataBoundItem.GetE

        ' Select the Element
        _s.SelectedElements.Insert(m_e)
        _s.UiDoc.Selection.SetElementIds(_s.SelectedElements)

        ' Zoom to the Element
        _s.ZoomToElement(m_e)

      Next

    Catch
    End Try

  End Sub

  Private Sub ChangeViewReferenceFind_Click(sender As Object, e As EventArgs) Handles ChangeViewReferenceFind.Click
    Try
      ' Get the Current Element
      For Each x As DataGridViewRow In DataGridViewFind.SelectedRows
        Dim m_tag As clsTagInstance = x.DataBoundItem
        If Not m_tag Is Nothing Then
          Using d As New form_ModifyViewReference(_s)
            d.ShowDialog()
            If Not d.Vp Is Nothing Then
              ' Update the Tag
              Using t As New Transaction(_s.Doc, "Modified Tag")
                If t.Start Then
                  Try
                    m_tag.SetValues(d.Vp)
                    t.Commit()
                    Using td As New Autodesk.Revit.UI.TaskDialog("Results")
                      td.MainInstruction = "Updated Tag!"
                      td.Show()
                    End Using
                    DoSetup()

                    ' Log
                    _s.ConfigData.WriteLogLine("Changed a (Good Standing Tag) reference", _s.DocName, EnumLogKind.IsTag)

                  Catch
                  End Try
                End If
              End Using
            End If
          End Using
        End If
      Next
    Catch
    End Try
  End Sub

  Private Sub ZoomToTagNull_Click(sender As Object, e As EventArgs) Handles ZoomToTagOther.Click

    Try

      ' Clear Selection
      _s.SelectedElements.Clear()

      ' Get the Current Element
      For Each x As DataGridViewRow In DataGridViewNulls.SelectedRows

        ' Get Bound Element
        Dim m_taginst As clsTagInstance = x.DataBoundItem

        ' Get Anything?
        If Not m_taginst Is Nothing Then
          Dim m_e As Element = m_taginst.GetFamilyInstance

          ' Select the Element
          _s.SelectedElements.Insert(m_e)
          _s.UiDoc.Selection.SetElementIds(_s.SelectedElements)

          ' Zoom to the Element
          _s.ZoomToElement(m_e)

        End If

      Next

    Catch
    End Try

  End Sub

  Private Sub ChooseViewReferenceNull_Click(sender As Object, e As EventArgs) Handles ChooseViewReferenceOther.Click
    Try
      ' Get the Current Element
      For Each x As DataGridViewRow In DataGridViewNulls.SelectedRows
        Dim m_tag As clsTagInstance = x.DataBoundItem
        If Not m_tag Is Nothing Then
          Using d As New form_ModifyViewReference(_s)
            d.ShowDialog()
            If Not d.Vp Is Nothing Then
              ' Update the Tag
              Using t As New Transaction(_s.Doc, "Modified Tag")
                If t.Start Then
                  Try
                    m_tag.SetValues(d.Vp)
                    t.Commit()
                    Using td As New Autodesk.Revit.UI.TaskDialog("Results")
                      td.MainInstruction = "Updated Tag!"
                      td.Show()
                    End Using
                    DoSetup()

                    ' Log
                    _s.ConfigData.WriteLogLine("Changed a (NULL Tag) reference", _s.DocName, EnumLogKind.IsTag)

                  Catch
                  End Try
                End If
              End Using
            End If
          End Using
        End If
      Next
    Catch
    End Try
  End Sub

  Private Sub DeleteTagNull_Click(sender As Object, e As EventArgs) Handles DeleteTagOther.Click
    Try
      If MsgBox("Are You Sure You Want to Delete the Selected Tag?", MsgBoxStyle.YesNo, "Question!") = MsgBoxResult.Yes Then
        Using t As New Transaction(_s.Doc, "Deleted NULL View Reference Tag")
          If t.Start Then
            ' Get the Current Element
            For Each x As DataGridViewRow In DataGridViewNulls.SelectedRows
              Dim m_e As Element = x.DataBoundItem.GetE
              Try
                ' Zoom to the Element
                _s.Doc.Delete(m_e.Id)
                t.Commit()

                ' Log
                _s.ConfigData.WriteLogLine("Deleted a (NULL Tag) reference", _s.DocName, EnumLogKind.IsTag)

              Catch
              End Try
            Next
          End If
        End Using
      End If
    Catch
    End Try
  End Sub

  Private Sub ToolStripOrphZoom_Click(sender As Object, e As EventArgs) Handles ToolStripOrphZoom.Click
    Try

      ' Clear Selection
      _s.SelectedElements.Clear()

      ' Get the Current Element
      For Each x As DataGridViewRow In DataGridViewOrphaned.SelectedRows
        Dim m_e As Element = x.DataBoundItem.GetE

        ' Select the Element
        _s.SelectedElements.Insert(m_e)
        _s.UiDoc.Selection.SetElementIds(_s.SelectedElements)

        ' Zoom to the Element
        _s.ZoomToElement(m_e)

      Next
    Catch
    End Try
  End Sub

  Private Sub ToolStripOrphModify_Click(sender As Object, e As EventArgs) Handles ToolStripOrphModify.Click
    Try
      ' Get the Current Element
      For Each x As DataGridViewRow In DataGridViewOrphaned.SelectedRows
        Dim m_tag As clsTagInstance = x.DataBoundItem
        If Not m_tag Is Nothing Then
          Using d As New form_ModifyViewReference(_s)
            d.ShowDialog()
            If Not d.Vp Is Nothing Then
              ' Update the Tag
              Using t As New Transaction(_s.Doc, "Modified Tag")
                If t.Start Then
                  Try
                    m_tag.SetValues(d.Vp)
                    t.Commit()
                    Using td As New Autodesk.Revit.UI.TaskDialog("Results")
                      td.MainInstruction = "Updated Tag!"
                      td.Show()
                    End Using
                    DoSetup()

                    ' Log
                    _s.ConfigData.WriteLogLine("Changed a (Orphaned Tag) reference", _s.DocName, EnumLogKind.IsTag)

                  Catch
                  End Try
                End If
              End Using
            End If
          End Using
        End If
      Next
    Catch
    End Try
  End Sub

  Private Sub ToolStripOrphDelete_Click(sender As Object, e As EventArgs) Handles ToolStripOrphDelete.Click
    Try
      If MsgBox("Are You Sure You Want to Delete the Selected Tag?", MsgBoxStyle.YesNo, "Question!") = MsgBoxResult.Yes Then
        Using t As New Transaction(_s.Doc, "Deleted Orphaned View Reference Tag")
          If t.Start Then
            ' Get the Current Element
            For Each x As DataGridViewRow In DataGridViewOrphaned.SelectedRows
              Dim m_e As Element = x.DataBoundItem.GetE
              Try
                ' Zoom to the Element
                _s.Doc.Delete(m_e.Id)
                t.Commit()

                ' Log
                _s.ConfigData.WriteLogLine("Deleted a (Orphaned Tag) reference", _s.DocName, EnumLogKind.IsTag)

              Catch
              End Try
            Next
          End If
        End Using
      End If
    Catch
    End Try
  End Sub

  '' '' ''' <summary>
  '' '' ''' Clear All
  '' '' ''' </summary>
  '' '' ''' <param name="sender"></param>
  '' '' ''' <param name="e"></param>
  '' '' ''' <remarks></remarks>
  '' ''Private Sub ButtonClear_Click(sender As System.Object, e As System.EventArgs)
  '' ''  Try
  '' ''    For Each c As System.Windows.Forms.Control In GroupBoxFilters.Controls
  '' ''      If TypeOf c Is System.Windows.Forms.TextBox Then c.Text = ""
  '' ''      If TypeOf c Is CheckBox Then
  '' ''        Try
  '' ''          Dim m_chb As CheckBox = TryCast(c, CheckBox)
  '' ''          m_chb.Checked = False
  '' ''        Catch
  '' ''        End Try
  '' ''      End If
  '' ''    Next
  '' ''  Catch
  '' ''  End Try
  '' ''End Sub

#End Region

End Class
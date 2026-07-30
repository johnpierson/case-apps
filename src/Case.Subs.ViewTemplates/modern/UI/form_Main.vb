Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.IO
Imports [Case].Subs.ViewTemplates.Data

Public Class form_Main

  Private _s As clsSettings
  Private _doEvents As Boolean = False
  Private _vtDict As New Dictionary(Of Integer, clsViewTemplate)
  Private _dictViewAssignments As New SortedDictionary(Of Integer, Integer)

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
  ''' Delete Checked
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub DeleteCheckedItems()

    ' Errors
    Dim m_errors As New List(Of String)
    Dim iCnt As Integer = 0

    ' Are They Sure?
    If MsgBox("Are you sure you want to permanently delete the checked templates?",
              MsgBoxStyle.YesNo, "Really?") = MsgBoxResult.Yes Then

      ' Progress
      With Me.ProgressBar1
        .Maximum = Me.DataGridViewViewTemplates.Rows.Count
        .Value = 0
        .Minimum = 0
      End With

      ' Start a New Transaction
      Using t As New Transaction(_s.Doc, "Delete View Templates")
        If t.Start = TransactionStatus.Started Then

          Try

            ' Process Each Item
            For Each x As DataGridViewRow In Me.DataGridViewViewTemplates.Rows

              Try
                ' Progress
                Me.ProgressBar1.Increment(1)
              Catch
              End Try

              ' Did we get a View Template
              Dim m_vt As clsViewTemplate = x.DataBoundItem
              If Not m_vt Is Nothing Then

                ' Checked?
                If m_vt.isChecked = True Then

                  ' Try to Delete it
                  If m_vt.DeleteVT(_s.Doc) = False Then

                    ' Failure?
                    m_errors.Add("(" & m_vt.ViewTemplateKind & ") " & m_vt.ViewTemplateName)

                  Else

                    ' Success
                    iCnt += 1

                  End If

                End If

              End If
            Next

            ' Success
            t.Commit()

            ' Update Listings from Settings
            _s.GetViews()

            ' Load Views
            LoadViews()

            ' Report to User
            Using td As New Autodesk.Revit.UI.TaskDialog("Here's What Happened:")
              With td
                .TitleAutoPrefix = False
                If m_errors.Count > 0 Then
                  .MainInstruction = "We Encountered " & m_errors.Count.ToString & " Errors!"
                  .MainContent = iCnt.ToString & " View Templates Deleted..." & vbCr & vbCr
                  .MainContent += "The following view templates failed to delete:"
                  For Each x In m_errors
                    .MainInstruction += x & vbCr
                  Next
                Else
                  .MainInstruction = "No Errors!"
                  .MainContent = iCnt.ToString & " View Templates Deleted!"
                End If
              End With

              ' Don't forget to show it
              td.Show()

            End Using

          Catch

            MsgBox("Major Error Deleting View Templates", MsgBoxStyle.Critical, "Halting!")

          End Try

        End If
      End Using

    End If

  End Sub

  ''' <summary>
  ''' To Help Maintain Accurate isChecked Properties
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateVtFromGrid()

    ' Default
    _vtDict = New Dictionary(Of Integer, clsViewTemplate)

    ' Events Enabled?
    If _doEvents = False Then Exit Sub

    Try

      ' Update Dictionary from Grid
      For Each x As DataGridViewRow In Me.DataGridViewViewTemplates.Rows
        Dim m_vt As clsViewTemplate = x.DataBoundItem

        ' Add to Dictionary
        If Not m_vt Is Nothing Then
          Try
            _vtDict.Add(m_vt.GetTemplate.Id.Value, m_vt)
          Catch
          End Try
        End If

      Next

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Load the Templates into the Grid
  ''' </summary>
  ''' <param name="SetSelected"></param>
  ''' <remarks></remarks>
  Private Sub LoadViewTemplates(Optional SetSelected As String = "")

    ' Events Enabled?
    If _doEvents = False Then Exit Sub

    ' Update from Grid
    UpdateVtFromGrid()

    ' Update Assignment Counts from Dictionary
    For Each x In _dictViewAssignments
      If _s.ViewTemplates.ContainsKey(x.Key) Then
        _s.ViewTemplates(x.Key).Assignments = x.Value
      End If
    Next

    ' Fresh List
    Dim m_vtList As New SortableBindingList(Of clsViewTemplate)

    ' Load the Items
    For Each x In _s.ViewTemplates.Values

      ' Selection Settings
      Select Case SetSelected

        Case "true"
          x.isChecked = True

        Case "false"
          x.isChecked = True = False

        Case Else
          If _vtDict.ContainsKey(x.GetTemplate.Id.Value) Then
            x.isChecked = _vtDict(x.GetTemplate.Id.Value).isChecked
          End If

      End Select

      ' Assignment Filtering
      If Me.CheckBoxShowUnusedOnly.Checked = True Then
        If x.Assignments > 0 Then Continue For
      End If

      ' Add to List
      m_vtList.Add(x)

    Next

    ' Bind to Control
    Me.DataGridViewViewTemplates.DataSource = m_vtList

  End Sub

  ''' <summary>
  ''' Update Treeview with View Data
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub LoadViews()

    ' Events Enabled?
    If _doEvents = False Then Exit Sub

    ' Fresh Dictionary
    _dictViewAssignments = New SortedDictionary(Of Integer, Integer)

    Try

      ' Fresh List
      Me.TreeViewViewTemplates.Nodes.Clear()

      ' Add the View Templates
      For Each x In _s.ViewTemplates.Values

        ' TemplateID
        Dim m_id As Integer = x.GetTemplate.Id.Value

        Try
          ' Assignment
          If Not _dictViewAssignments.ContainsKey(m_id) Then
            _dictViewAssignments.Add(m_id, 0)
          End If
        Catch
        End Try

        Try
          ' View Kind
          If Not Me.TreeViewViewTemplates.Nodes.ContainsKey(x.ViewTemplateKind) Then
            ' Add the Kind and Default None Template
            Me.TreeViewViewTemplates.Nodes.Add(x.ViewTemplateKind, x.ViewTemplateKind)
            Me.TreeViewViewTemplates.Nodes(x.ViewTemplateKind).Nodes.Add("-1", "<No Template>")
          End If
        Catch
        End Try

        Try
          ' Template
          Dim m_newNode As TreeNode = Me.TreeViewViewTemplates.Nodes(x.ViewTemplateKind).Nodes.Add(x.GetTemplate.Id.ToString, "Template: " & x.ViewTemplateName)
          m_newNode.Tag = x
        Catch
        End Try
      Next

      ' Add the Views
      For Each x As clsView In _s.ViewElements

        Try

          ' TemplateID
          Dim m_id As Integer = x.ViewElement.ViewTemplateId.Value

          Try
            ' Add to Dictionary
            If _dictViewAssignments.ContainsKey(m_id) Then
              _dictViewAssignments(m_id) += 1
            End If
          Catch
          End Try

          Try
            ' View Kind
            If Not Me.TreeViewViewTemplates.Nodes.ContainsKey(x.ViewKind) Then Continue For
          Catch
          End Try

          ' Hide it?
          If Not String.IsNullOrEmpty(TextBoxViewName.Text) Then
            If Not x.ViewName.ToLower.Contains(TextBoxViewName.Text.ToLower) Then

              ' Skip it
              Continue For

            End If
          End If

          Try

            ' Add to the Tree
            Dim m_text As String = ""
            If x.ViewLevel.ToLower <> "n/a" Then
              m_text = "(" & x.ViewLevel & ") " & x.ViewName
            Else
              m_text = x.ViewName
            End If
            Dim m_node As TreeNode = Me.TreeViewViewTemplates.Nodes(x.ViewKind).Nodes(m_id.ToString).Nodes.Add(x.ViewElement.Id.ToString, m_text)
            m_node.Tag = x

          Catch
          End Try

        Catch
        End Try

      Next

    Catch
    End Try

    ' Expand all when searching
    If Not String.IsNullOrEmpty(TextBoxViewName.Text) Then
      Me.TreeViewViewTemplates.ExpandAll()
    End If

    ' Load Templates
    LoadViewTemplates()

  End Sub

#Region "Form Controls & Events"

  ''' <summary>
  ''' Form Load
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_Main_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    ' Load
    SetFormViz(formViz.isStandby)

    ' Allow Events
    _doEvents = True
    LoadViews()

  End Sub

#Region "Form Controls & Events - Form Viz"

  Private Enum formViz
    isStandby
    isProcessing
  End Enum

  ''' <summary>
  ''' Set the Form Visibility
  ''' </summary>
  ''' <param name="fv"></param>
  ''' <remarks></remarks>
  Private Sub SetFormViz(fv As formViz)

    Me.Text = "Subscription View Template Manager v" & _s.Version

    Select Case fv
      Case formViz.isStandby
        Me.ProgressBar1.Hide()
        Me.ButtonCancel.Show()
        Me.ButtonHelp.Show()
        Me.ButtonAll.Enabled = True
        Me.ButtonNone.Enabled = True
        Me.ButtonCheckedDelete.Enabled = True
      Case formViz.isProcessing
        Me.ProgressBar1.Show()
        Me.ButtonCancel.Hide()
        Me.ButtonHelp.Hide()
        Me.ButtonAll.Enabled = False
        Me.ButtonNone.Enabled = False
        Me.ButtonCheckedDelete.Enabled = False
    End Select

  End Sub

#End Region

  ''' <summary>
  ''' Export Datagrid to File
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonExportCSV_Click(sender As Object, e As EventArgs) Handles ButtonExportCSV.Click

    Try

      ' Browse to a File
      Using fs As New SaveFileDialog
        With fs
          .Title = "Where so you want to save the export?"
          .DefaultExt = "csv"
          .Filter = "CSV Files | *.csv"
          .FileName = ""
        End With

        ' Did we get a File Path?
        If fs.ShowDialog = System.Windows.Forms.DialogResult.OK Then
          If Not String.IsNullOrEmpty(fs.FileName) Then

            ' Data Stream
            Using sw As New StreamWriter(fs.FileName, False)

              sw.WriteLine(Chr(34) & "Template Name" & Chr(34) & "," &
                           Chr(34) & "Template Kind" & Chr(34) & "," &
                           Chr(34) & "Assigned" & Chr(34))

              ' Write Each Row
              For Each dr As DataGridViewRow In Me.DataGridViewViewTemplates.Rows
                sw.WriteLine(Chr(34) & dr.Cells(1).Value.ToString & Chr(34) & "," &
                             Chr(34) & dr.Cells(2).Value.ToString & Chr(34) & "," &
                             dr.Cells(3).Value.ToString)
              Next

            End Using

          End If
        End If

      End Using

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' View Name Filter
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub TextBoxViewName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxViewName.TextChanged
    LoadViews()
  End Sub

  ''' <summary>
  ''' Filter to Unassigned View Templates
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub CheckBoxShowUnusedOnly_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShowUnusedOnly.CheckedChanged
    LoadViewTemplates()
  End Sub

  ''' <summary>
  ''' Set All Checked
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonAll_Click(sender As Object, e As EventArgs) Handles ButtonAll.Click
    LoadViewTemplates("true")
  End Sub

  ''' <summary>
  ''' Set all NOT Checked
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonNone_Click(sender As Object, e As EventArgs) Handles ButtonNone.Click
    LoadViewTemplates("false")
  End Sub

  ''' <summary>
  ''' Delete View templates
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCheckedDelete_Click(sender As Object, e As EventArgs) Handles ButtonCheckedDelete.Click

    ' Form Viz
    SetFormViz(formViz.isProcessing)

    ' Delete Template
    DeleteCheckedItems()

    ' Form Viz
    SetFormViz(formViz.isStandby)

  End Sub

  ''' <summary>
  ''' Close
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCancel.Click

    ' Close
    Me.Close()

  End Sub

  ''' <summary>
  ''' Documentation
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As System.Object, e As System.EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/subscription-view-template-manager-revit")
  End Sub

  ''' <summary>
  ''' Case Site
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
    Process.Start("http://www.case-inc.com")
  End Sub

#Region "Form Controls & Events - Drag Drop Tree"

  ''' <summary>
  ''' Drag Drop - Step #1 (Correct Node Depth)
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub TreeViewViewTemplates_ItemDrag(sender As Object, e As System.Windows.Forms.ItemDragEventArgs) Handles TreeViewViewTemplates.ItemDrag

    Try

      ' Disallow Non Views
      Dim m_t As TreeNode = TryCast(e.Item, TreeNode)
      If m_t.Level < 2 Then Exit Sub

    Catch
      Exit Sub
    End Try

    ' Set the drag node and initiate the DragDrop 
    DoDragDrop(e.Item, DragDropEffects.Move)

  End Sub

  ''' <summary>
  ''' Drag Drop - Step #2
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub TreeViewViewTemplates_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles TreeViewViewTemplates.DragEnter

    'See if there is a TreeNode being dragged
    If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then

      ' TreeNode found allow move effect
      e.Effect = DragDropEffects.Move

    Else

      ' No TreeNode found, prevent move
      e.Effect = DragDropEffects.None

    End If

  End Sub

  ''' <summary>
  ''' Drag Drop - Step #3 (Validate)
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub TreeViewViewTemplates_DragOver(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles TreeViewViewTemplates.DragOver

    ' Check that there is a TreeNode being dragged 
    If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then
      Exit Sub
    End If

    ' Get the TreeView raising the event (incase multiple on form)
    Dim selectedTreeview As TreeView = CType(sender, TreeView)

    ' As the mouse moves over nodes, provide feedback to 
    ' the user by highlighting the node that is the current drop target
    Dim pt As System.Drawing.Point = CType(sender, TreeView).PointToClient(New System.Drawing.Point(e.X, e.Y))
    Dim targetNode As TreeNode = selectedTreeview.GetNodeAt(pt)

    ' See if the targetNode is currently selected, if so no need to validate again
    If Not (selectedTreeview.SelectedNode Is targetNode) Then

      'Select the node currently under the cursor
      selectedTreeview.SelectedNode = targetNode

      ' Check that the selected node is not an invalid target
      Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

      ' Default Behavior
      e.Effect = DragDropEffects.None

      ' Validation only to Level 1
      If targetNode.Level = 1 Then e.Effect = DragDropEffects.Move

      '' ''Do Until targetNode Is Nothing
      '' ''  If targetNode.Level = 1 Then
      '' ''    e.Effect = DragDropEffects.None
      '' ''    Exit Sub
      '' ''  End If
      '' ''  If targetNode Is dropNode Then
      '' ''    e.Effect = DragDropEffects.None
      '' ''    Exit Sub
      '' ''  End If
      '' ''  targetNode = targetNode.Parent
      '' ''Loop

      ' '' ''Currently selected node is a suitable target
      '' ''e.Effect = DragDropEffects.Move

    End If

  End Sub

  ''' <summary>
  ''' Drag Drop - Step #4 (Complete)
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub TreeViewViewTemplates_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles TreeViewViewTemplates.DragDrop

    ' Get the TreeView raising the event (incase multiple on form)
    Dim m_treeView As TreeView = CType(sender, TreeView)

    ' Get the TreeNode being dragged
    Dim m_source As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

    ' The target node should be selected from the DragOver event
    Dim m_target As TreeNode = m_treeView.SelectedNode

    Try

      ' Get the Special Helper Objects
      Dim m_view As clsView = m_source.Tag

      ' Qualification
      If Not m_view Is Nothing Then
        If Not String.IsNullOrEmpty(m_target.Name) Then

          ' Update the ViewTemplate
          If m_view.SetTemplate(m_target.Name) = False Then
            MsgBox("Cannot Apply That Kind of Template to that Kind of View", MsgBoxStyle.Exclamation, "Not Updated")
          Else

            ' Remove the Node and Add to New Location
            m_source.Remove()

            ' If there is no targetNode add dropNode to the bottom of
            ' the TreeView root nodes, otherwise add it to the end of the dropNode child nodes
            If m_target Is Nothing Then
              m_treeView.Nodes.Add(m_source)
            Else
              m_target.Nodes.Add(m_source)
            End If

            ' Ensure the newley created node is visible to the user and select it
            m_source.EnsureVisible()
            m_treeView.SelectedNode = m_source

            ' Update Templates Listing
            LoadViewTemplates()

          End If

        End If
      End If

    Catch
    End Try

  End Sub

#End Region

#End Region

End Class
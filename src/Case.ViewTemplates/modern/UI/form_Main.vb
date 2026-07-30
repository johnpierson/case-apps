Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports System.Windows.Forms
Imports System.Diagnostics
Imports [Case].ViewTemplates.Data

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

    ' Load
    SetFormViz(formViz.isStandby)
    LoadViews()

  End Sub

#Region "Form Viz"

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

    Text = "View Template Controller v" & _s.AppVersion

    Select Case fv
      Case formViz.isStandby
        ProgressBar1.Hide()
        ButtonCancel.Show()
      Case formViz.isProcessing
        ProgressBar1.Show()
        ButtonCancel.Hide()
    End Select

  End Sub

#End Region

  ''' <summary>
  ''' Update Treeview with View Data
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub LoadViews()

    Try

      ' Fresh List
      TreeViewViewTemplates.Nodes.Clear()

      ' Add the View Templates
      For Each x In _s.ViewTemplates

        Try
          ' View Kind
          If Not Me.TreeViewViewTemplates.Nodes.ContainsKey(x.ViewTemplate.ViewType.ToString) Then
            ' Add the Kind and Default None Template
            TreeViewViewTemplates.Nodes.Add(x.ViewTemplate.ViewType.ToString, x.ViewTemplate.ViewType.ToString)
            TreeViewViewTemplates.Nodes(x.ViewTemplate.ViewType.ToString).Nodes.Add("-1", "<No Template>")
          End If
        Catch
        End Try

        Try
          ' Template
          TreeViewViewTemplates.Nodes(x.ViewTemplate.ViewType.ToString).Nodes.Add(x.ViewTemplate.Id.ToString, "Template: " & x.ViewTemplateName)
        Catch
        End Try
      Next

      ' Add the Views
      For Each x As clsViews In _s.ViewElements

        Try
          ' View Kind
          If Not Me.TreeViewViewTemplates.Nodes.ContainsKey(x.ViewKind) Then Continue For
        Catch
        End Try

        Try
          ' View
          Dim m_text As String = ""
          If x.ViewLevel.ToLower <> "n/a" Then
            m_text = "(" & x.ViewLevel & ") " & x.ViewName
          Else
            m_text = x.ViewName
          End If
          TreeViewViewTemplates.Nodes(x.ViewKind).Nodes(x.ViewElement.ViewTemplateId.Value.ToString).Nodes.Add(x.ViewElement.Id.ToString, m_text)
          TreeViewViewTemplates.Nodes(x.ViewKind).Nodes(x.ViewElement.ViewTemplateId.Value.ToString).Nodes(x.ViewElement.Id.ToString).Tag = x
        Catch
        End Try

      Next

    Catch
    End Try

  End Sub

#Region "Form Controls & Events"

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
  ''' Documentation
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As System.Object, e As System.EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/free-view-template-controller")
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
      Dim m_view As clsViews = m_source.Tag

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

          End If

        End If
      End If

    Catch
    End Try

  End Sub

#End Region

#End Region

End Class
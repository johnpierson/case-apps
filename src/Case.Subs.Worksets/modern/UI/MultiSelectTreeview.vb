Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Namespace UI

  Public Class MultiSelectTreeview
    Inherits TreeView

#Region "Selected Node(s) Properties"

    Private m_SelectedNodes As List(Of TreeNode) = Nothing
    Public Property SelectedNodes() As List(Of TreeNode)
      Get
        Return m_SelectedNodes
      End Get
      Set(value As List(Of TreeNode))
        ClearSelectedNodes()
        If value IsNot Nothing Then
          For Each node As TreeNode In value
            ToggleNode(node, True)
          Next
        End If
      End Set
    End Property

    ' Note we use the new keyword to Hide the native treeview's SelectedNode property.
    Private m_SelectedNode As TreeNode
    Public Shadows Property SelectedNode() As TreeNode
      Get
        Return m_SelectedNode
      End Get
      Set(value As TreeNode)
        ClearSelectedNodes()
        If value IsNot Nothing Then
          SelectNode(value)
        End If
      End Set
    End Property

#End Region

    Public Sub New()
      'InitializeComponent()
      m_SelectedNodes = New List(Of TreeNode)
      MyBase.SelectedNode = Nothing
    End Sub

#Region "Overridden Events"

    Protected Overrides Sub OnGotFocus(e As EventArgs)
      ' Make sure at least one node has a selection
      ' this way we can tab to the ctrl and use the 
      ' keyboard to select nodes
      Try
        If m_SelectedNode Is Nothing AndAlso Me.TopNode IsNot Nothing Then
          ToggleNode(TopNode, True)
        End If

        MyBase.OnGotFocus(e)
      Catch ex As Exception
        HandleException(ex)
      End Try
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
      ' If the user clicks on a node that was not
      ' previously selected, select it now.

      Try
        MyBase.SelectedNode = Nothing

        Dim node As TreeNode = Me.GetNodeAt(e.Location)
        If node IsNot Nothing Then
          Dim leftBound As Integer = node.Bounds.X
          ' - 20; // Allow user to click on image
          Dim rightBound As Integer = node.Bounds.Right + 10
          ' Give a little extra room
          If e.Location.X > leftBound AndAlso e.Location.X < rightBound Then
            ' Potential Drag Operation
            ' Let Mouse Up do select
            If ModifierKeys = Keys.None AndAlso (m_SelectedNodes.Contains(node)) Then
            Else
              SelectNode(node)
            End If
          End If
        End If

        MyBase.OnMouseDown(e)
      Catch ex As Exception
        HandleException(ex)
      End Try
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
      ' If the clicked on a node that WAS previously
      ' selected then, reselect it now. This will clear
      ' any other selected nodes. e.g. A B C D are selected
      ' the user clicks on B, now A C & D are no longer selected.
      Try
        ' Check to see if a node was clicked on 
        Dim node As TreeNode = Me.GetNodeAt(e.Location)
        If node IsNot Nothing Then
          If ModifierKeys = Keys.None AndAlso m_SelectedNodes.Contains(node) Then
            Dim leftBound As Integer = node.Bounds.X
            ' -20; // Allow user to click on image
            Dim rightBound As Integer = node.Bounds.Right + 10
            ' Give a little extra room
            If e.Location.X > leftBound AndAlso e.Location.X < rightBound Then

              SelectNode(node)
            End If
          End If
        End If

        MyBase.OnMouseUp(e)
      Catch ex As Exception
        HandleException(ex)
      End Try
    End Sub

    Protected Overrides Sub OnItemDrag(e As ItemDragEventArgs)
      ' If the user drags a node and the node being dragged is NOT
      ' selected, then clear the active selection, select the
      ' node being dragged and drag it. Otherwise if the node being
      ' dragged is selected, drag the entire selection.
      Try
        Dim node As TreeNode = TryCast(e.Item, TreeNode)

        If node IsNot Nothing Then
          If Not m_SelectedNodes.Contains(node) Then
            SelectSingleNode(node)
            ToggleNode(node, True)
          End If
        End If

        MyBase.OnItemDrag(e)
      Catch ex As Exception
        HandleException(ex)
      End Try
    End Sub

    Protected Overrides Sub OnBeforeSelect(e As TreeViewCancelEventArgs)
      ' Never allow base.SelectedNode to be set!
      Try
        MyBase.SelectedNode = Nothing
        e.Cancel = True

        MyBase.OnBeforeSelect(e)
      Catch ex As Exception
        HandleException(ex)
      End Try
    End Sub

    Protected Overrides Sub OnAfterSelect(e As TreeViewEventArgs)
      ' Never allow base.SelectedNode to be set!
      Try
        MyBase.OnAfterSelect(e)
        MyBase.SelectedNode = Nothing
      Catch ex As Exception
        HandleException(ex)
      End Try
    End Sub

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
      ' Handle all possible key strokes for the control.
      ' including navigation, selection, etc.

      MyBase.OnKeyDown(e)

      If e.KeyCode = Keys.ShiftKey Then
        Return
      End If

      'this.BeginUpdate();
      Dim bShift As Boolean = (ModifierKeys = Keys.Shift)

      Try
        ' Nothing is selected in the tree, this isn't a good state
        ' select the top node
        If m_SelectedNode Is Nothing AndAlso Me.TopNode IsNot Nothing Then
          ToggleNode(TopNode, True)
        End If

        ' Nothing is still selected in the tree, this isn't a good state, leave.
        If m_SelectedNode Is Nothing Then
          Return
        End If

        If e.KeyCode = Keys.Left Then
          If m_SelectedNode.IsExpanded AndAlso m_SelectedNode.Nodes.Count > 0 Then
            ' Collapse an expanded node that has children
            m_SelectedNode.Collapse()
          ElseIf m_SelectedNode.Parent IsNot Nothing Then
            ' Node is already collapsed, try to select its parent.
            SelectSingleNode(m_SelectedNode.Parent)
          End If
        ElseIf e.KeyCode = Keys.Right Then
          If Not m_SelectedNode.IsExpanded Then
            ' Expand a collpased node's children
            m_SelectedNode.Expand()
          Else
            ' Node was already expanded, select the first child
            SelectSingleNode(m_SelectedNode.FirstNode)
          End If
        ElseIf e.KeyCode = Keys.Up Then
          ' Select the previous node
          If m_SelectedNode.PrevVisibleNode IsNot Nothing Then
            SelectNode(m_SelectedNode.PrevVisibleNode)
          End If
        ElseIf e.KeyCode = Keys.Down Then
          ' Select the next node
          If m_SelectedNode.NextVisibleNode IsNot Nothing Then
            SelectNode(m_SelectedNode.NextVisibleNode)
          End If
        ElseIf e.KeyCode = Keys.Home Then
          If bShift Then
            If m_SelectedNode.Parent Is Nothing Then
              ' Select all of the root nodes up to this point 
              If Me.Nodes.Count > 0 Then
                SelectNode(Nodes(0))
              End If
            Else
              ' Select all of the nodes up to this point under this nodes parent
              SelectNode(m_SelectedNode.Parent.FirstNode)
            End If
          Else
            ' Select this first node in the tree
            If Me.Nodes.Count > 0 Then
              SelectSingleNode(Nodes(0))
            End If
          End If
        ElseIf e.KeyCode = Keys.[End] Then
          If bShift Then
            If m_SelectedNode.Parent Is Nothing Then
              ' Select the last ROOT node in the tree
              If Me.Nodes.Count > 0 Then
                SelectNode(Nodes(Nodes.Count - 1))
              End If
            Else
              ' Select the last node in this branch
              SelectNode(m_SelectedNode.Parent.LastNode)
            End If
          Else
            If Me.Nodes.Count > 0 Then
              ' Select the last node visible node in the tree.
              ' Don't expand branches incase the tree is virtual
              Dim ndLast As TreeNode = Me.Nodes(0).LastNode
              While ndLast.IsExpanded AndAlso (ndLast.LastNode IsNot Nothing)
                ndLast = ndLast.LastNode
              End While
              SelectSingleNode(ndLast)
            End If
          End If
        ElseIf e.KeyCode = Keys.PageUp Then
          ' Select the highest node in the display
          Dim nCount As Integer = Me.VisibleCount
          Dim ndCurrent As TreeNode = m_SelectedNode
          While (nCount) > 0 AndAlso (ndCurrent.PrevVisibleNode IsNot Nothing)
            ndCurrent = ndCurrent.PrevVisibleNode
            nCount -= 1
          End While
          SelectSingleNode(ndCurrent)
        ElseIf e.KeyCode = Keys.PageDown Then
          ' Select the lowest node in the display
          Dim nCount As Integer = Me.VisibleCount
          Dim ndCurrent As TreeNode = m_SelectedNode
          While (nCount) > 0 AndAlso (ndCurrent.NextVisibleNode IsNot Nothing)
            ndCurrent = ndCurrent.NextVisibleNode
            nCount -= 1
          End While
          SelectSingleNode(ndCurrent)
        Else
          ' Assume this is a search character a-z, A-Z, 0-9, etc.
          ' Select the first node after the current node that 
          ' starts with this character
          Dim sSearch As String = ChrW(e.KeyValue).ToString()

          Dim ndCurrent As TreeNode = m_SelectedNode
          While (ndCurrent.NextVisibleNode IsNot Nothing)
            ndCurrent = ndCurrent.NextVisibleNode
            If ndCurrent.Text.StartsWith(sSearch) Then
              SelectSingleNode(ndCurrent)
              Exit While
            End If
          End While
        End If
      Catch ex As Exception
        HandleException(ex)
      Finally
        Me.EndUpdate()
      End Try
    End Sub

#End Region

#Region "Helper Methods"

    Private Sub SelectNode(node As TreeNode)
      Try
        Me.BeginUpdate()

        If m_SelectedNode Is Nothing OrElse ModifierKeys = Keys.Control Then
          ' Ctrl+Click selects an unselected node, or unselects a selected node.
          Dim bIsSelected As Boolean = m_SelectedNodes.Contains(node)
          ToggleNode(node, Not bIsSelected)
        ElseIf ModifierKeys = Keys.Shift Then
          ' Shift+Click selects nodes between the selected node and here.
          Dim ndStart As TreeNode = m_SelectedNode
          Dim ndEnd As TreeNode = node

          If ndStart.Parent.Equals(ndEnd.Parent) Then
            ' Selected node and clicked node have same parent, easy case.
            If ndStart.Index < ndEnd.Index Then
              ' If the selected node is beneath the clicked node walk down
              ' selecting each Visible node until we reach the end.
              While Not ndStart.Equals(ndEnd)
                ndStart = ndStart.NextVisibleNode
                If ndStart Is Nothing Then
                  Exit While
                End If
                ToggleNode(ndStart, True)
              End While
              ' Clicked same node, do nothing
            ElseIf ndStart.Index = ndEnd.Index Then
            Else
              ' If the selected node is above the clicked node walk up
              ' selecting each Visible node until we reach the end.
              While Not ndStart.Equals(ndEnd)
                ndStart = ndStart.PrevVisibleNode
                If ndStart Is Nothing Then
                  Exit While
                End If
                ToggleNode(ndStart, True)
              End While
            End If
          Else
            ' Selected node and clicked node have same parent, hard case.
            ' We need to find a common parent to determine if we need
            ' to walk down selecting, or walk up selecting.

            Dim ndStartP As TreeNode = ndStart
            Dim ndEndP As TreeNode = ndEnd
            Dim startDepth As Integer = Math.Min(ndStartP.Level, ndEndP.Level)

            ' Bring lower node up to common depth
            While ndStartP.Level > startDepth
              ndStartP = ndStartP.Parent
            End While

            ' Bring lower node up to common depth
            While ndEndP.Level > startDepth
              ndEndP = ndEndP.Parent
            End While

            ' Walk up the tree until we find the common parent
            While Not ndStartP.Parent.Equals(ndEndP.Parent)
              ndStartP = ndStartP.Parent
              ndEndP = ndEndP.Parent
            End While

            ' Select the node
            If ndStartP.Index < ndEndP.Index Then
              ' If the selected node is beneath the clicked node walk down
              ' selecting each Visible node until we reach the end.
              While Not ndStart.Equals(ndEnd)
                ndStart = ndStart.NextVisibleNode
                If ndStart Is Nothing Then
                  Exit While
                End If
                ToggleNode(ndStart, True)
              End While
            ElseIf ndStartP.Index = ndEndP.Index Then
              If ndStart.Level < ndEnd.Level Then
                While Not ndStart.Equals(ndEnd)
                  ndStart = ndStart.NextVisibleNode
                  If ndStart Is Nothing Then
                    Exit While
                  End If
                  ToggleNode(ndStart, True)
                End While
              Else
                While Not ndStart.Equals(ndEnd)
                  ndStart = ndStart.PrevVisibleNode
                  If ndStart Is Nothing Then
                    Exit While
                  End If
                  ToggleNode(ndStart, True)
                End While
              End If
            Else
              ' If the selected node is above the clicked node walk up
              ' selecting each Visible node until we reach the end.
              While Not ndStart.Equals(ndEnd)
                ndStart = ndStart.PrevVisibleNode
                If ndStart Is Nothing Then
                  Exit While
                End If
                ToggleNode(ndStart, True)
              End While
            End If
          End If
        Else
          ' Just clicked a node, select it
          SelectSingleNode(node)
        End If

        OnAfterSelect(New TreeViewEventArgs(m_SelectedNode))
      Finally
        Me.EndUpdate()
      End Try
    End Sub

    Private Sub ClearSelectedNodes()
      Try
        For Each node As TreeNode In m_SelectedNodes
          node.BackColor = Me.BackColor
          node.ForeColor = Me.ForeColor
        Next
      Finally
        m_SelectedNodes.Clear()
        m_SelectedNode = Nothing
      End Try
    End Sub

    Private Sub SelectSingleNode(node As TreeNode)
      If node Is Nothing Then
        Return
      End If

      ClearSelectedNodes()
      ToggleNode(node, True)
      node.EnsureVisible()
    End Sub

    Private Sub ToggleNode(node As TreeNode, bSelectNode As Boolean)
      If bSelectNode Then
        m_SelectedNode = node
        If Not m_SelectedNodes.Contains(node) Then
          m_SelectedNodes.Add(node)
        End If
        node.BackColor = SystemColors.Highlight
        node.ForeColor = SystemColors.HighlightText
      Else
        m_SelectedNodes.Remove(node)
        node.BackColor = Me.BackColor
        node.ForeColor = Me.ForeColor
      End If
    End Sub

    Private Sub HandleException(ex As Exception)
      ' Perform some error handling here.
      ' We don't want to bubble errors to the CLR. 
      MessageBox.Show(ex.Message)
    End Sub

#End Region

  End Class
End Namespace
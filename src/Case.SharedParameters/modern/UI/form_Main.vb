Imports Autodesk.Revit.DB
Imports System.Windows.Forms
Imports System.Linq
Imports System.Diagnostics

Imports [Case].SharedParameters.Data

Public Class form_Main

  Private _s As clsSettings
  Private _params As clsSharedParameters
  Private _eventsEnabled = True

  ''' <summary>
  ''' Constructor 
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s

    ' Setup
    doSetup()

  End Sub

#Region "Private Members"

  ''' <summary>
  ''' Form Setup
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub doSetup()

    ' Form
    Text = "Super Shared Parameter Loader v" & _s.Version

    ' Load Category List
    LoadCategories()

    ' Parameters
    _params = New clsSharedParameters(_s.uiApp)
    LabelParameterFilePath.Text = _params.FileName
    LoadSharedParameters()

    ' Groupings
    Dim m_g As New SortedDictionary(Of String, clsBuiltInParameterGroup)
    For Each x In ParameterUtils.GetAllBuiltInGroups()
      Dim m_helper As New clsBuiltInParameterGroup(x)
      Try
        m_g.Add(m_helper.DisplayName, m_helper)
      Catch
      End Try
    Next
    Dim m_groups As List(Of clsBuiltInParameterGroup) = m_g.Values.ToList

    ' Bind to Control
    ComboBoxGroup.DataSource = m_groups
    ComboBoxGroup.DisplayMember = "DisplayName"
    ComboBoxGroup.SelectedIndex = 0

    ' Subscription Disabled
    LabelFilterParam.Enabled = False
    TextBoxFilterParams.Enabled = False
    LabelFilterCat.Enabled = False
    TextBoxFilterCategories.Enabled = False

  End Sub

  ''' <summary>
  ''' Load the Categories
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub LoadCategories()

    ' Fresh List
    TreeViewCategories.Nodes.Clear()

    ' Parameter Bindable Only
    For Each c As Category In _s.ActiveDoc.Settings.Categories
      If c.AllowsBoundParameters = True Then
        Dim m_n As TreeNode = Me.TreeViewCategories.Nodes.Add(c.Name, c.Name)
        m_n.Tag = c
      End If
    Next

    ' Sort the List
    TreeViewCategories.Sort()

  End Sub

  ''' <summary>
  ''' Node Check Helper
  ''' </summary>
  ''' <param name="n"></param>
  ''' <param name="v"></param>
  ''' <remarks></remarks>
  Private Sub CheckNode(n As TreeNode, v As Boolean)

    ' No Events
    _eventsEnabled = False

    n.Checked = v

    ' Events
    _eventsEnabled = True

  End Sub

  ''' <summary>
  ''' Load the Parameter Names
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub LoadSharedParameters()

    ' Fresh List
    TreeViewParameters.Nodes.Clear()

    Try

      ' Shared Parameters
      If Me.RadioButtonByGroup.Checked = True Then

        ' Load by Group
        For Each x In _params.DefinitionsByGroup
          Dim m_defs As List(Of Definition) = x.Value

          ' Each Definition
          For Each d In m_defs

            ' Parent Node
            If Not Me.TreeViewParameters.Nodes.ContainsKey(x.Key.ToString) Then

              Try
                ' Add Group Name
                TreeViewParameters.Nodes.Add(x.Key.ToString, x.Key.ToString)
              Catch
              End Try

            End If

            Try
              ' Child Only
              Dim m_tn As TreeNode = Me.TreeViewParameters.Nodes(x.Key.ToString).Nodes.Add(d.Name, d.Name)
              m_tn.Tag = d
            Catch
            End Try

          Next

        Next

      Else

        ' Load by Type
        For Each x In _params.DefinitionsByType
          Dim m_defs As List(Of Definition) = x.Value

          ' Each Definition
          For Each d In m_defs

            ' Parent Node
            If Not Me.TreeViewParameters.Nodes.ContainsKey(x.Key.ToString) Then

              Try
                ' Add Type Name
                TreeViewParameters.Nodes.Add(x.Key.ToString, x.Key.ToString)
              Catch
              End Try

            End If

            Try
              ' Child Only
              Dim m_tn As TreeNode = Me.TreeViewParameters.Nodes(x.Key.ToString).Nodes.Add(d.Name, d.Name)
              m_tn.Tag = d
            Catch
            End Try

          Next

        Next

      End If

    Catch ex As Exception

    End Try

  End Sub

#End Region

#Region "Form Controls & Events"

  ''' <summary>
  ''' Load All Checked Parameters
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonLoad_Click(sender As System.Object, e As System.EventArgs) Handles ButtonLoad.Click

    ' Get all Checked Parameters
    Dim m_defs As New List(Of Definition)
    For Each n1 As TreeNode In Me.TreeViewParameters.Nodes
      For Each n2 As TreeNode In n1.Nodes
        If n2.Checked = True Then
          m_defs.Add(n2.Tag)
        End If
      Next
    Next

    ' Get all Checked Categories
    Dim m_categories As New List(Of Category)
    For Each x As TreeNode In Me.TreeViewCategories.Nodes
      If x.Checked = True Then
        m_categories.Add(x.Tag)
      End If
    Next

    Dim m_group As clsBuiltInParameterGroup = Me.ComboBoxGroup.SelectedItem

    ' Bind the Definitions
    _params.BindDefinitionsToCategories(m_defs,
                                        m_categories,
                                        m_group.ParameterGroup,
                                        RadioButtonParamInst.Checked)

    MsgBox("Parameters Added!")

  End Sub

  ''' <summary>
  ''' Check All Parameters 
  ''' </summary>
  ''' <param name="sender"></param>,
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonAll_Click(sender As System.Object, e As System.EventArgs) Handles ButtonAll.Click

    ' Check All
    For Each x As TreeNode In Me.TreeViewParameters.Nodes

      ' Uncheck
      If x.Checked = False Then x.Checked = True

      ' Children
      For Each y As TreeNode In x.Nodes

        If y.Checked = False Then y.Checked = True

      Next

    Next

  End Sub

  ''' <summary>
  ''' Uncheck All Parameters
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonNone_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNone.Click

    ' Uncheck All
    For Each x As TreeNode In Me.TreeViewParameters.Nodes

      ' Uncheck
      If x.Checked = True Then x.Checked = False

      ' Children
      For Each y As TreeNode In x.Nodes

        If y.Checked = True Then y.Checked = False

      Next

    Next

  End Sub

  ''' <summary>
  ''' Check All Categories
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCatsAll_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCatsAll.Click

    ' Uncheck All
    For Each x As TreeNode In Me.TreeViewCategories.Nodes

      ' Uncheck
      If x.Checked = False Then x.Checked = True

      ' Children
      For Each y As TreeNode In x.Nodes

        If y.Checked = False Then y.Checked = True

      Next

    Next

  End Sub

  ''' <summary>
  ''' Uncheck All Categories
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCatsNone_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCatsNone.Click

    ' Uncheck All
    For Each x As TreeNode In Me.TreeViewCategories.Nodes

      ' Uncheck
      If x.Checked = True Then x.Checked = False

      ' Children
      For Each y As TreeNode In x.Nodes

        If y.Checked = True Then y.Checked = False

      Next

    Next

  End Sub

  ''' <summary>
  ''' Browse for a Shared Parameter File
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonBrowseShared_Click(sender As System.Object, e As System.EventArgs) Handles ButtonBrowseShared.Click

    ' Browse for File
    If Me.OpenFileDialog1.ShowDialog = DialogResult.OK Then
      If Not String.IsNullOrEmpty(OpenFileDialog1.FileName) Then
        LabelParameterFilePath.Text = Me.OpenFileDialog1.FileName

        ' Update the Parameters Listing
        _params.LoadSharedParameterFile(OpenFileDialog1.FileName)
        LoadSharedParameters()

      End If
    End If

  End Sub

  ''' <summary>
  ''' Help
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/free-super-shared-parameter-loader")
  End Sub

  ''' <summary>
  ''' Filter Parameters List
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub TextBoxFilter_TextChanged(sender As System.Object, e As System.EventArgs)

    ' Update the Parameters Listing
    LoadSharedParameters()

  End Sub

  ''' <summary>
  ''' For Parents - Check all or None to Match
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub TreeViewParameters_AfterCheck(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles TreeViewParameters.AfterCheck

    ' Events Enabled?
    If _eventsEnabled = False Then Exit Sub

    ' Is it a parent?
    If e.Node.Level = 0 Then

      ' Match Children Checking
      For Each x As TreeNode In e.Node.Nodes
        CheckNode(x, e.Node.Checked)
      Next

    Else

      ' Check the Parent, if a child is checked, the parent must be checked
      If e.Node.Checked = True Then

        CheckNode(e.Node.Parent, True)

      Else

        ' If last child checked, uncheck parent
        For Each x As TreeNode In e.Node.Parent.Nodes
          If x.Checked = True Then Exit Sub
        Next

        ' Uncheck Parent
        CheckNode(e.Node.Parent, False)

      End If

    End If

  End Sub

  ''' <summary>
  ''' Update Parameter Nodes
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub RadioButtonByGroup_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonByGroup.CheckedChanged
    LoadSharedParameters()
  End Sub

  ''' <summary>
  ''' Update Parameter Nodes
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub RadioButtonByFormat_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonByFormat.CheckedChanged
    LoadSharedParameters()
  End Sub

  ''' <summary>
  ''' Launch CASE Site
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click

    ' Launch Site
    Process.Start("http://www.case-inc.com")

  End Sub

#End Region

End Class
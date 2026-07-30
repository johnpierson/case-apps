Imports System.Windows.Forms
Imports System.Linq
Imports [Case].Subs.Worksets.Charts
Imports [Case].Subs.Worksets.Data
Imports System.Windows.Forms.DataVisualization.Charting
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI.Selection
Imports Autodesk.Revit.UI

Namespace UI
  Public Class form_Data

    Private _s As clsSettings
    Private _enableEvents As Boolean = False

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

#Region "Private Members - TreeNodes"

    ''' <summary>
    ''' Only Show Category Names - Load on Demand
    ''' </summary>
    ''' <param name="n">Top Node</param>
    ''' <remarks></remarks>
    Private Sub ShowAsCategoryBasic(Optional n As TreeNode = Nothing)

      ' Dynamic Load?
      If Not n Is Nothing Then

        ' Category Override
        Dim m_catName As String = " n/a"
        If n.Name <> " n/a" Then m_catName = n.Name

        Try

          ' Clear Children Nodes
          n.Nodes.Clear()

        Catch
        End Try

        Try

          ' Type Categories
          Dim m_masterType As IEnumerable(Of clsRvtElemType) = From e In _s.ElementsType.Values
                                                               Where e.CategoryName = m_catName

          ' Instance Categories
          Dim m_masterInst As IEnumerable(Of clsRvtElemInst) = From e In _s.ElementsInst.Values
                                                               Where e.CategoryName = m_catName

          ' Add Types to the Tree
          For Each x In m_masterType.ToList

            Try
              If String.IsNullOrEmpty(x.FamilyName) Then
                Dim m_node As TreeNode = n.Nodes.Add(x.UniqueId, "(" & x.CategoryName & ") " & x.TypeName)
                m_node.Tag = x
              Else
                Dim m_node As TreeNode = n.Nodes.Add(x.UniqueId, "(" & x.FamilyName & ") " & x.TypeName)
                m_node.Tag = x
              End If
            Catch
            End Try

          Next

          ' Add Instances to the Tree
          For Each x In m_masterInst.ToList

            Try

              ' Does it point to a type?
              If Not String.IsNullOrEmpty(x.TypeEid) Then

                Try

                  ' Add under Type
                  Dim m_node As TreeNode = n.Nodes(x.TypeEid).Nodes.Add(x.UniqueId, "(" & x.Eid & ") " & x.ObjectClass)
                  m_node.Tag = x

                Catch
                End Try

              Else

                ' Add Directly
                Dim m_node As TreeNode = n.Nodes.Add(x.UniqueId, "(" & x.Eid & ") " & x.ObjectClass)
                m_node.Tag = x

              End If

            Catch
            End Try

          Next

        Catch
        End Try

      Else

        ' Loading From Scratch - Top Level Only
        MultiSelectTreeview1.Nodes.Clear()
        GroupBoxBrowser.Text = "Building Data Browser: " & "Category -> (Category / Family) Type"

        Try

          ' Type Categories
          Dim m_masterType As IEnumerable(Of String) = From e In _s.ElementsType.Values
                                                       Select e.CategoryName Distinct
                                                       Order By CategoryName

          ' Instance Categories
          Dim m_masterInst As IEnumerable(Of String) = From e In _s.ElementsInst.Values
                                                       Select e.CategoryName Distinct
                                                       Order By CategoryName

          ' Combine to List
          Dim m_cats As List(Of String) = m_masterType.ToList
          For Each x In m_masterInst
            If Not m_cats.Contains(x) Then m_cats.Add(x)
          Next
          m_cats.Sort()

          ' Add to the Tree
          For Each x In m_cats
            Dim m_key As String = x
            If m_key.ToLower = " n/a" Then m_key = " " & x
            Try
              MultiSelectTreeview1.Nodes.Add(x, m_key)
            Catch
            End Try
            Try
              MultiSelectTreeview1.Nodes(x).Nodes.Add("X", "X")
            Catch
            End Try
          Next

        Catch
        End Try

      End If

    End Sub

    ''' <summary>
    ''' Show Data as Worksets
    ''' </summary>
    ''' <param name="n">Top Node</param>
    ''' <remarks></remarks>
    Public Sub ShowAsWorksetsBasic(Optional n As TreeNode = Nothing)

      ' Query Value?
      If Not n Is Nothing Then

        Try

          ' Clear Nested Nodes
          n.Nodes.Clear()

        Catch
        End Try

        Dim m_parent As String = n.Parent.Name
        If m_parent = " n/a" Then
          m_parent = ""
        End If

        Try

          ' Instance Categories
          Dim m_masterInst As IEnumerable(Of clsRvtElemInst) = From e In _s.ElementsInst.Values
                                                               Where e.WorksetName = m_parent And
                                                               e.CategoryName = n.Name

          ' Add Instances to the Tree
          For Each x In m_masterInst.ToList

            Try
              ' Item Node
              Dim m_node As TreeNode = n.Nodes.Add(x.UniqueId, "(" & x.Eid & ") " & x.ObjectClass)
              m_node.Tag = x
            Catch
            End Try

          Next

        Catch
        End Try

      Else

        ' Loading
        MultiSelectTreeview1.Nodes.Clear()
        GroupBoxBrowser.Text = "Building Data Browser: " & "Workset -> Category -> (ElementID) Name"

        Dim m_masterInst As IEnumerable(Of String) = From e In _s.ElementsInst.Values
                                                     Select e.WorksetName Distinct
                                                     Order By WorksetName

        ' List
        Dim m_ws As List(Of String) = m_masterInst.ToList

        ' Process the Workset Names
        For Each x In m_ws

          ' Workset Name
          Dim m_text As String = x
          If String.IsNullOrEmpty(m_text) Then m_text = " n/a"
          MultiSelectTreeview1.Nodes.Add(m_text, m_text)

          ' Query Instances:
          Dim m_qryInst As IEnumerable(Of String) = From e In _s.ElementsInst.Values
                                                    Where e.WorksetName = m_text
                                                    Select e.CategoryName Distinct
                                                    Order By CategoryName

          If String.IsNullOrEmpty(x) Then

            ' Query Instances:
            m_qryInst = From e In _s.ElementsInst.Values
                                  Where e.WorksetName = ""
                                  Select e.CategoryName Distinct
                                  Order By CategoryName

          End If

          ' Add the Instances
          For Each e In m_qryInst

            ' Category
            Dim m_catname As String = e
            If String.IsNullOrEmpty(m_catname) Then m_catname = " n/a"

            Try
              ' Category Node
              If Not MultiSelectTreeview1.Nodes(m_text).Nodes.ContainsKey(m_catname) Then
                MultiSelectTreeview1.Nodes(m_text).Nodes.Add(m_catname, m_catname)
                MultiSelectTreeview1.Nodes(m_text).Nodes(m_catname).Nodes.Add("X", "X")
              End If
            Catch
            End Try

          Next

        Next

      End If

    End Sub

    ''' <summary>
    ''' Show Data as Class Types
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowAsKindsBasic(Optional n As TreeNode = Nothing)

      ' Query Value?
      If Not n Is Nothing Then

        Try

          ' Clear Nested Nodes
          n.Nodes.Clear()

        Catch
        End Try

        ' Types
        Dim m_qryType As IEnumerable(Of clsRvtElemType) = From e In _s.ElementsType.Values
                                                       Where e.ObjectClass = n.Name
                                                       Select e

        ' Instances
        Dim m_qryInst As IEnumerable(Of clsRvtElemInst) = From e In _s.ElementsInst.Values
                                                       Where e.ObjectClass = n.Name
                                                       Select e

        Try

          ' Add the Types
          For Each x As clsRvtElemType In m_qryType
            Try
              Dim m_node As TreeNode = n.Nodes.Add(x.UniqueId, "(" & x.CategoryName & ") " & "(" & x.FamilyName & ") " & x.TypeName)
              m_node.Tag = x
            Catch
            End Try
          Next

        Catch
        End Try

        Try

          ' Add the Instances
          For Each x As clsRvtElemInst In m_qryInst
            Try
              Dim m_node As TreeNode = n.Nodes.Add(x.UniqueId, "(" & x.CategoryName & ") " & x.ObjectClass)
              m_node.Tag = x
            Catch
            End Try
          Next

        Catch
        End Try

      Else

        ' Loading
        MultiSelectTreeview1.Nodes.Clear()
        GroupBoxBrowser.Text = "Building Data Browser: " & "Class -> (Category) (Family) Type"

        ' Types
        Dim m_masterType As IEnumerable(Of String) = From e In _s.ElementsType.Values
                                                     Select e.ObjectClass Distinct
                                                     Order By ObjectClass

        ' Instances
        Dim m_masterInst As IEnumerable(Of String) = From e In _s.ElementsInst.Values
                                                     Select e.ObjectClass Distinct
                                                     Order By ObjectClass

        ' Add the Types
        For Each x In m_masterType.ToList
          If Not MultiSelectTreeview1.Nodes.ContainsKey(x) Then
            MultiSelectTreeview1.Nodes.Add(x, x)
            MultiSelectTreeview1.Nodes(x).Nodes.Add("X", "X")
          End If
        Next

        ' Add the Instances
        For Each x In m_masterInst.ToList
          If Not MultiSelectTreeview1.Nodes.ContainsKey(x) Then
            MultiSelectTreeview1.Nodes.Add(x, x)
            MultiSelectTreeview1.Nodes(x).Nodes.Add("X", "X")
          End If
        Next

      End If

    End Sub

#End Region

#Region "Private Members - Charts"

    ''' <summary>
    ''' Summary - Workset
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadSummaryWorkset()

      ' Sortable Rows
      Dim m_worksets As New SortableBindingList(Of clsChartWorksets)

      Try

        ' Query Values
        m_worksets = QueryWorksets(_s.ElementsInst.Values.ToList())

        ' Bind to Control
        DataGridViewWorksets.DataSource = m_worksets

      Catch
      End Try

      Try

        ' Chart
        With ChartWorksets.ChartAreas(0)
          .Area3DStyle.Enable3D = True
          .Area3DStyle.Perspective = 20
          .Area3DStyle.Inclination = 10
          .Area3DStyle.Rotation = 10
        End With

        ' Add the Points
        Dim m_series As Series = ChartWorksets.Series(0)
        m_series.Points.Clear()
        Dim m_iCnt As Integer = 1
        For Each x In m_worksets

          ' Point
          Dim m_pt As New DataPoint(m_iCnt, x.Instances)
          m_pt.AxisLabel = x.Workset
          m_series.Points.Add(m_pt)

          ' Step the Counter
          m_iCnt += 1

          Try
            If m_iCnt > CInt(ComboBoxWorksetCount.SelectedItem) Then Exit For
          Catch
            If m_iCnt > 5 Then Exit For
          End Try

        Next

      Catch
      End Try

      Try
        ChartWorksets.Update()
      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Summary - Category
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadSummaryCategories()

      ' Sortable Rows
      Dim m_cats As New SortableBindingList(Of clsChartCategory)

      Try

        ' Query Values
        m_cats = QueryCategories(_s.ElementsInst.Values.ToList(),
                                 _s.ElementsType.Values.ToList(),
                                 RadioButtonCatsByInstances.Checked)

        ' Bind to Control
        DataGridViewCategory.DataSource = m_cats

      Catch
      End Try

      Try

        ' Chart
        With ChartCategory.ChartAreas(0)
          .Area3DStyle.Enable3D = True
          .Area3DStyle.Perspective = 20
          .Area3DStyle.Inclination = 10
          .Area3DStyle.Rotation = 10
        End With

        ' Add the Points
        Dim m_series As Series = ChartCategory.Series(0)
        m_series.Points.Clear()
        Dim m_iCnt As Integer = 1
        For Each x In m_cats

          ' Point
          Dim m_pt As DataPoint
          If RadioButtonCatsByInstances.Checked = True Then
            m_pt = New DataPoint(m_iCnt, x.Instances)
          Else
            m_pt = New DataPoint(m_iCnt, x.Types)
          End If
          m_pt.AxisLabel = x.Category
          m_series.Points.Add(m_pt)

          ' Step the Counter
          m_iCnt += 1

          Try
            If m_iCnt > CInt(ComboBoxCategoryItemCount.SelectedItem) Then Exit For
          Catch
            If m_iCnt > 5 Then Exit For
          End Try

        Next

      Catch
      End Try

      Try
        ChartCategory.Update()
      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Summary - Kinds
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadSummaryKinds()

      ' Sortable Rows
      Dim m_kinds As New SortableBindingList(Of clsChartKind)

      Try

        ' Query Values
        m_kinds = QueryKinds(_s.ElementsInst.Values.ToList(),
                             _s.ElementsType.Values.ToList(),
                             RadioButtonChartClassInst.Checked)

        ' Bind to Control
        DataGridViewSummaryClass.DataSource = m_kinds

      Catch
      End Try

      Try

        ' Chart
        With ChartClass.ChartAreas(0)
          .Area3DStyle.Enable3D = True
          .Area3DStyle.Perspective = 20
          .Area3DStyle.Inclination = 10
          .Area3DStyle.Rotation = 10
        End With

        ' Add the Points
        Dim m_series As Series = ChartClass.Series(0)
        m_series.Points.Clear()
        Dim m_iCnt As Integer = 1
        For Each x In m_kinds

          ' Point
          Dim m_pt As DataPoint
          If RadioButtonCatsByInstances.Checked = True Then
            m_pt = New DataPoint(m_iCnt, x.Quantity)
          Else
            m_pt = New DataPoint(m_iCnt, x.Quantity)
          End If
          m_pt.AxisLabel = x.Kind
          m_series.Points.Add(m_pt)

          ' Step the Counter
          m_iCnt += 1

          Try
            If m_iCnt > CInt(ComboBoxChartClassCount.SelectedItem) Then Exit For
          Catch
            If m_iCnt > 5 Then Exit For
          End Try

        Next

      Catch
      End Try

      Try
        ChartClass.Update()
      Catch
      End Try

    End Sub

#End Region

#Region "Private Members - Form Controls & Events"

    ''' <summary>
    ''' Startup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub form_Data_Load(sender As Object, e As EventArgs) Handles Me.Load

      Try

        ' Title
        Text = "Workset Browser v" & _s.Version

        ' Tree sorting
        MultiSelectTreeview1.TreeViewNodeSorter = New clsUtilNodeSorter

        ' Category Combo Selection
        For i = 5 To 20
          ComboBoxCategoryItemCount.Items.Add(i)
          ComboBoxChartClassCount.Items.Add(i)
          ComboBoxWorksetCount.Items.Add(i)
        Next
        ComboBoxCategoryItemCount.SelectedIndex = 0
        ComboBoxChartClassCount.SelectedIndex = 0
        ComboBoxWorksetCount.SelectedIndex = 0

        ' Charts
        LoadSummaryWorkset()
        LoadSummaryCategories()
        LoadSummaryKinds()

        ' Show Data
        ShowAsWorksetsBasic()

      Catch
      End Try

      ' Events Enabled
      _enableEvents = True

    End Sub

    ''' <summary>
    ''' Select in Model
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonSelect_Click(sender As Object, e As EventArgs) Handles ButtonSelect.Click

      ' Selected Instances
      Dim m_selected As New SortedDictionary(Of String, Element)

      Try

        ' Expand All Selected Nodes
        For Each x As TreeNode In MultiSelectTreeview1.SelectedNodes
          x.ExpandAll()
        Next

      Catch
      End Try

      Try

        ' Get Instance Elements
        For Each x As TreeNode In MultiSelectTreeview1.SelectedNodes

          Dim m_instMaster As clsRvtElemInst = TryCast(x.Tag, clsRvtElemInst)
          If Not m_instMaster Is Nothing Then
            If Not m_selected.ContainsKey(m_instMaster.Eid) Then

              ' Base Element
              Dim m_element = _s.Doc.GetElement(m_instMaster.UniqueId)
              If Not m_element Is Nothing Then
                m_selected.Add(m_instMaster.Eid, m_element)
              End If

            End If
          End If

          For Each node1 As TreeNode In x.Nodes
            Dim m_inst As clsRvtElemInst = TryCast(node1.Tag, clsRvtElemInst)
            If Not m_inst Is Nothing Then
              If Not m_selected.ContainsKey(m_inst.Eid) Then
                Dim m_element = _s.Doc.GetElement(m_inst.UniqueId)
                If Not m_element Is Nothing Then
                  m_selected.Add(m_inst.Eid, m_element)
                End If
              End If
            End If

            For Each node2 As TreeNode In node1.Nodes
              Dim m_inst2 As clsRvtElemInst = TryCast(node2.Tag, clsRvtElemInst)
              If Not m_inst2 Is Nothing Then
                If Not m_selected.ContainsKey(m_inst2.Eid) Then
                  Dim m_element = _s.Doc.GetElement(m_inst2.UniqueId)
                  If Not m_element Is Nothing Then
                    m_selected.Add(m_inst2.Eid, m_element)
                  End If
                End If
              End If
            Next

          Next

        Next

        ' Clear Selection
        _s.SelectedElements.Clear()
        Dim m_iCnt As Integer = 0

        ' Final Selection
        For Each x In m_selected.Values

          Try

            ' Insert
            _s.SelectedElements.Add(x.Id)
            m_iCnt += 1

          Catch
          End Try

        Next

        ' Update Selection
        _s.UiDoc.Selection.SetElementIds(_s.SelectedElements)

        ' Tell User
        Using td As New Autodesk.Revit.UI.TaskDialog("Selection Applied")
          With td
            .TitleAutoPrefix = False
            .MainInstruction = m_iCnt.ToString() & " Elements Selected"
            .Show()
            End With
        End Using

        Close()

      Catch

        ' Tell User
        Using td As New Autodesk.Revit.UI.TaskDialog("Uh Oh")
          With td
            .TitleAutoPrefix = False
            .MainInstruction = "Failed to Apply Selection"
            .MainContent = "An unknown error prevented the selection of elements in the model"
            .Show()
          End With
        End Using

      End Try

    End Sub

    ''' <summary>
    ''' Tree Node Expand
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MultiSelectTreeview1_AfterExpand(sender As Object, e As TreeViewEventArgs) Handles MultiSelectTreeview1.AfterExpand

      ' Category
      If RadioButtonCategory.Checked = True Then
        If e.Node.Level = 0 Then
          If e.Node.Nodes.ContainsKey("X") Then
            ShowAsCategoryBasic(e.Node)
            Return
          End If
        End If
      End If

      ' Workset
      If RadioButtonWorkset.Checked = True Then
        If e.Node.Level = 1 Then
          If e.Node.Nodes.ContainsKey("X") Then
            ShowAsWorksetsBasic(e.Node)
            Return
          End If
        End If
      End If

      ' Class Kinds
      If RadioButtonClass.Checked = True Then
        If e.Node.Level = 0 Then
          If e.Node.Nodes.ContainsKey("X") Then
            ShowAsKindsBasic(e.Node)
            Return
          End If
        End If
      End If

    End Sub

    ''' <summary>
    ''' Update Workset Counts
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ComboBoxWorksetCount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxWorksetCount.SelectedIndexChanged
      If _enableEvents = True Then LoadSummaryWorkset()
    End Sub

    Private Sub ComboBoxChartClassCount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxChartClassCount.SelectedIndexChanged
      If _enableEvents = True Then LoadSummaryKinds()
    End Sub

    Private Sub RadioButtonChartClassType_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonChartClassType.CheckedChanged
      If _enableEvents = True Then LoadSummaryKinds()
    End Sub

    Private Sub RadioButtonChartClassInst_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonChartClassInst.CheckedChanged
      If _enableEvents = True Then LoadSummaryKinds()
    End Sub

    Private Sub RadioButtonWorkset_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonWorkset.CheckedChanged
      If _enableEvents = True And RadioButtonWorkset.Checked = True Then ShowAsWorksetsBasic()
    End Sub

    Private Sub RadioButtonCategory_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonCategory.CheckedChanged
      If _enableEvents = True And RadioButtonCategory.Checked = True Then ShowAsCategoryBasic()
    End Sub

    Private Sub RadioButtonClass_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonClass.CheckedChanged
      If _enableEvents = True And RadioButtonClass.Checked = True Then ShowAsKindsBasic()
    End Sub

    Private Sub ComboBoxCategoryItemCount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCategoryItemCount.SelectedIndexChanged
      If _enableEvents = True Then LoadSummaryCategories()
    End Sub

    Private Sub RadioButtonCatsByTypes_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonCatsByTypes.CheckedChanged
      If _enableEvents = True Then LoadSummaryCategories()
    End Sub

    Private Sub RadioButtonCatsByInstances_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonCatsByInstances.CheckedChanged
      If _enableEvents = True Then LoadSummaryCategories()
    End Sub

#End Region

  End Class
End Namespace
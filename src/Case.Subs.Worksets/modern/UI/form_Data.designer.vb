Namespace UI
  <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
  Partial Class form_Data
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
      Try
        If disposing AndAlso components IsNot Nothing Then
          components.Dispose()
        End If
      Finally
        MyBase.Dispose(disposing)
      End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
      Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
      Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
      Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
      Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
      Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
      Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_Data))
      Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
      Me.TabPage1 = New System.Windows.Forms.TabPage()
      Me.SplitContainerChartCategory = New System.Windows.Forms.SplitContainer()
      Me.ComboBoxCategoryItemCount = New System.Windows.Forms.ComboBox()
      Me.RadioButtonCatsByTypes = New System.Windows.Forms.RadioButton()
      Me.RadioButtonCatsByInstances = New System.Windows.Forms.RadioButton()
      Me.ChartCategory = New System.Windows.Forms.DataVisualization.Charting.Chart()
      Me.DataGridViewCategory = New System.Windows.Forms.DataGridView()
      Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
      Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
      Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
      Me.TabPage3 = New System.Windows.Forms.TabPage()
      Me.SplitContainerChartWs = New System.Windows.Forms.SplitContainer()
      Me.ComboBoxWorksetCount = New System.Windows.Forms.ComboBox()
      Me.ChartWorksets = New System.Windows.Forms.DataVisualization.Charting.Chart()
      Me.DataGridViewWorksets = New System.Windows.Forms.DataGridView()
      Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
      Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
      Me.TabPage4 = New System.Windows.Forms.TabPage()
      Me.SplitContainerChartKind = New System.Windows.Forms.SplitContainer()
      Me.ComboBoxChartClassCount = New System.Windows.Forms.ComboBox()
      Me.RadioButtonChartClassType = New System.Windows.Forms.RadioButton()
      Me.RadioButtonChartClassInst = New System.Windows.Forms.RadioButton()
      Me.ChartClass = New System.Windows.Forms.DataVisualization.Charting.Chart()
      Me.DataGridViewSummaryClass = New System.Windows.Forms.DataGridView()
      Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
      Me.TabControl1 = New System.Windows.Forms.TabControl()
      Me.SplitContainerMaster = New System.Windows.Forms.SplitContainer()
      Me.GroupBoxBrowserOptions = New System.Windows.Forms.GroupBox()
      Me.RadioButtonClass = New System.Windows.Forms.RadioButton()
      Me.RadioButtonWorkset = New System.Windows.Forms.RadioButton()
      Me.RadioButtonCategory = New System.Windows.Forms.RadioButton()
      Me.GroupBoxBrowser = New System.Windows.Forms.GroupBox()
      Me.ButtonSelect = New System.Windows.Forms.Button()
      Me.MultiSelectTreeview1 = New [Case].Subs.Worksets.UI.MultiSelectTreeview()
      Me.TabPage1.SuspendLayout()
      CType(SplitContainerChartCategory, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SplitContainerChartCategory.Panel1.SuspendLayout()
      Me.SplitContainerChartCategory.Panel2.SuspendLayout()
      Me.SplitContainerChartCategory.SuspendLayout()
      CType(ChartCategory, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(DataGridViewCategory, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.TabPage3.SuspendLayout()
      CType(SplitContainerChartWs, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SplitContainerChartWs.Panel1.SuspendLayout()
      Me.SplitContainerChartWs.Panel2.SuspendLayout()
      Me.SplitContainerChartWs.SuspendLayout()
      CType(ChartWorksets, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(DataGridViewWorksets, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.TabPage4.SuspendLayout()
      CType(SplitContainerChartKind, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SplitContainerChartKind.Panel1.SuspendLayout()
      Me.SplitContainerChartKind.Panel2.SuspendLayout()
      Me.SplitContainerChartKind.SuspendLayout()
      CType(ChartClass, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(DataGridViewSummaryClass, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.TabControl1.SuspendLayout()
      CType(SplitContainerMaster, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SplitContainerMaster.Panel1.SuspendLayout()
      Me.SplitContainerMaster.Panel2.SuspendLayout()
      Me.SplitContainerMaster.SuspendLayout()
      Me.GroupBoxBrowserOptions.SuspendLayout()
      Me.GroupBoxBrowser.SuspendLayout()
      Me.SuspendLayout()
      '
      'DataGridViewTextBoxColumn2
      '
      Me.DataGridViewTextBoxColumn2.DataPropertyName = "Quantity"
      Me.DataGridViewTextBoxColumn2.HeaderText = "Quantity"
      Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
      Me.DataGridViewTextBoxColumn2.ReadOnly = True
      '
      'TabPage1
      '
      Me.TabPage1.Controls.Add(SplitContainerChartCategory)
      Me.TabPage1.Location = New System.Drawing.Point(4, 22)
      Me.TabPage1.Name = "TabPage1"
      Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
      Me.TabPage1.Size = New System.Drawing.Size(610, 836)
      Me.TabPage1.TabIndex = 0
      Me.TabPage1.Text = "Category Summary"
      Me.TabPage1.UseVisualStyleBackColor = True
      '
      'SplitContainerChartCategory
      '
      Me.SplitContainerChartCategory.Dock = System.Windows.Forms.DockStyle.Fill
      Me.SplitContainerChartCategory.Location = New System.Drawing.Point(3, 3)
      Me.SplitContainerChartCategory.Name = "SplitContainerChartCategory"
      Me.SplitContainerChartCategory.Orientation = System.Windows.Forms.Orientation.Horizontal
      '
      'SplitContainerChartCategory.Panel1
      '
      Me.SplitContainerChartCategory.Panel1.Controls.Add(ComboBoxCategoryItemCount)
      Me.SplitContainerChartCategory.Panel1.Controls.Add(RadioButtonCatsByTypes)
      Me.SplitContainerChartCategory.Panel1.Controls.Add(RadioButtonCatsByInstances)
      Me.SplitContainerChartCategory.Panel1.Controls.Add(ChartCategory)
      '
      'SplitContainerChartCategory.Panel2
      '
      Me.SplitContainerChartCategory.Panel2.Controls.Add(DataGridViewCategory)
      Me.SplitContainerChartCategory.Size = New System.Drawing.Size(604, 830)
      Me.SplitContainerChartCategory.SplitterDistance = 324
      Me.SplitContainerChartCategory.TabIndex = 0
      '
      'ComboBoxCategoryItemCount
      '
      Me.ComboBoxCategoryItemCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.ComboBoxCategoryItemCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.ComboBoxCategoryItemCount.FormattingEnabled = True
      Me.ComboBoxCategoryItemCount.Location = New System.Drawing.Point(388, 297)
      Me.ComboBoxCategoryItemCount.Name = "ComboBoxCategoryItemCount"
      Me.ComboBoxCategoryItemCount.Size = New System.Drawing.Size(81, 21)
      Me.ComboBoxCategoryItemCount.TabIndex = 13
      '
      'RadioButtonCatsByTypes
      '
      Me.RadioButtonCatsByTypes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.RadioButtonCatsByTypes.AutoSize = True
      Me.RadioButtonCatsByTypes.Location = New System.Drawing.Point(209, 301)
      Me.RadioButtonCatsByTypes.Name = "RadioButtonCatsByTypes"
      Me.RadioButtonCatsByTypes.Size = New System.Drawing.Size(120, 17)
      Me.RadioButtonCatsByTypes.TabIndex = 12
      Me.RadioButtonCatsByTypes.Text = "Show Top by Types"
      Me.RadioButtonCatsByTypes.UseVisualStyleBackColor = True
      '
      'RadioButtonCatsByInstances
      '
      Me.RadioButtonCatsByInstances.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.RadioButtonCatsByInstances.AutoSize = True
      Me.RadioButtonCatsByInstances.Checked = True
      Me.RadioButtonCatsByInstances.Location = New System.Drawing.Point(15, 301)
      Me.RadioButtonCatsByInstances.Name = "RadioButtonCatsByInstances"
      Me.RadioButtonCatsByInstances.Size = New System.Drawing.Size(137, 17)
      Me.RadioButtonCatsByInstances.TabIndex = 11
      Me.RadioButtonCatsByInstances.TabStop = True
      Me.RadioButtonCatsByInstances.Text = "Show Top by Instances"
      Me.RadioButtonCatsByInstances.UseVisualStyleBackColor = True
      '
      'ChartCategory
      '
      Me.ChartCategory.BackColor = System.Drawing.Color.Transparent
      ChartArea1.Name = "ChartArea1"
      Me.ChartCategory.ChartAreas.Add(ChartArea1)
      Me.ChartCategory.Dock = System.Windows.Forms.DockStyle.Fill
      Me.ChartCategory.Location = New System.Drawing.Point(0, 0)
      Me.ChartCategory.Name = "ChartCategory"
      Me.ChartCategory.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel
      Series1.ChartArea = "ChartArea1"
      Series1.Name = "Types"
      Me.ChartCategory.Series.Add(Series1)
      Me.ChartCategory.Size = New System.Drawing.Size(604, 324)
      Me.ChartCategory.TabIndex = 14
      Me.ChartCategory.Text = "Chart1"
      '
      'DataGridViewCategory
      '
      Me.DataGridViewCategory.AllowUserToAddRows = False
      Me.DataGridViewCategory.AllowUserToDeleteRows = False
      Me.DataGridViewCategory.AllowUserToResizeRows = False
      Me.DataGridViewCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
      Me.DataGridViewCategory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column3, Me.Column4, Me.Column5})
      Me.DataGridViewCategory.Dock = System.Windows.Forms.DockStyle.Fill
      Me.DataGridViewCategory.Location = New System.Drawing.Point(0, 0)
      Me.DataGridViewCategory.Name = "DataGridViewCategory"
      Me.DataGridViewCategory.ReadOnly = True
      Me.DataGridViewCategory.RowHeadersVisible = False
      Me.DataGridViewCategory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
      Me.DataGridViewCategory.Size = New System.Drawing.Size(604, 502)
      Me.DataGridViewCategory.TabIndex = 2
      '
      'Column3
      '
      Me.Column3.DataPropertyName = "Category"
      Me.Column3.HeaderText = "Category"
      Me.Column3.Name = "Column3"
      Me.Column3.ReadOnly = True
      Me.Column3.Width = 200
      '
      'Column4
      '
      Me.Column4.DataPropertyName = "Types"
      Me.Column4.HeaderText = "Types"
      Me.Column4.Name = "Column4"
      Me.Column4.ReadOnly = True
      '
      'Column5
      '
      Me.Column5.DataPropertyName = "Instances"
      Me.Column5.HeaderText = "Instances"
      Me.Column5.Name = "Column5"
      Me.Column5.ReadOnly = True
      '
      'TabPage3
      '
      Me.TabPage3.Controls.Add(SplitContainerChartWs)
      Me.TabPage3.Location = New System.Drawing.Point(4, 22)
      Me.TabPage3.Name = "TabPage3"
      Me.TabPage3.Size = New System.Drawing.Size(610, 836)
      Me.TabPage3.TabIndex = 2
      Me.TabPage3.Text = "Workset Summary"
      Me.TabPage3.UseVisualStyleBackColor = True
      '
      'SplitContainerChartWs
      '
      Me.SplitContainerChartWs.Dock = System.Windows.Forms.DockStyle.Fill
      Me.SplitContainerChartWs.Location = New System.Drawing.Point(0, 0)
      Me.SplitContainerChartWs.Name = "SplitContainerChartWs"
      Me.SplitContainerChartWs.Orientation = System.Windows.Forms.Orientation.Horizontal
      '
      'SplitContainerChartWs.Panel1
      '
      Me.SplitContainerChartWs.Panel1.Controls.Add(ComboBoxWorksetCount)
      Me.SplitContainerChartWs.Panel1.Controls.Add(ChartWorksets)
      '
      'SplitContainerChartWs.Panel2
      '
      Me.SplitContainerChartWs.Panel2.Controls.Add(DataGridViewWorksets)
      Me.SplitContainerChartWs.Size = New System.Drawing.Size(610, 836)
      Me.SplitContainerChartWs.SplitterDistance = 323
      Me.SplitContainerChartWs.TabIndex = 0
      '
      'ComboBoxWorksetCount
      '
      Me.ComboBoxWorksetCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.ComboBoxWorksetCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.ComboBoxWorksetCount.FormattingEnabled = True
      Me.ComboBoxWorksetCount.Location = New System.Drawing.Point(10, 294)
      Me.ComboBoxWorksetCount.Name = "ComboBoxWorksetCount"
      Me.ComboBoxWorksetCount.Size = New System.Drawing.Size(81, 21)
      Me.ComboBoxWorksetCount.TabIndex = 14
      '
      'ChartWorksets
      '
      Me.ChartWorksets.BackColor = System.Drawing.Color.Silver
      Me.ChartWorksets.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.HorizontalCenter
      ChartArea2.Name = "ChartArea1"
      Me.ChartWorksets.ChartAreas.Add(ChartArea2)
      Me.ChartWorksets.Dock = System.Windows.Forms.DockStyle.Fill
      Me.ChartWorksets.Location = New System.Drawing.Point(0, 0)
      Me.ChartWorksets.Name = "ChartWorksets"
      Me.ChartWorksets.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones
      Series2.ChartArea = "ChartArea1"
      Series2.Name = "Worksets"
      Me.ChartWorksets.Series.Add(Series2)
      Me.ChartWorksets.Size = New System.Drawing.Size(610, 323)
      Me.ChartWorksets.TabIndex = 15
      Me.ChartWorksets.Text = "Chart1"
      '
      'DataGridViewWorksets
      '
      Me.DataGridViewWorksets.AllowUserToAddRows = False
      Me.DataGridViewWorksets.AllowUserToDeleteRows = False
      Me.DataGridViewWorksets.AllowUserToResizeRows = False
      Me.DataGridViewWorksets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
      Me.DataGridViewWorksets.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
      Me.DataGridViewWorksets.Dock = System.Windows.Forms.DockStyle.Fill
      Me.DataGridViewWorksets.Location = New System.Drawing.Point(0, 0)
      Me.DataGridViewWorksets.Name = "DataGridViewWorksets"
      Me.DataGridViewWorksets.ReadOnly = True
      Me.DataGridViewWorksets.RowHeadersVisible = False
      Me.DataGridViewWorksets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
      Me.DataGridViewWorksets.Size = New System.Drawing.Size(610, 509)
      Me.DataGridViewWorksets.TabIndex = 3
      '
      'Column1
      '
      Me.Column1.DataPropertyName = "Workset"
      Me.Column1.HeaderText = "Workset"
      Me.Column1.Name = "Column1"
      Me.Column1.ReadOnly = True
      Me.Column1.Width = 200
      '
      'Column2
      '
      Me.Column2.DataPropertyName = "Instances"
      Me.Column2.HeaderText = "Instances"
      Me.Column2.Name = "Column2"
      Me.Column2.ReadOnly = True
      '
      'TabPage4
      '
      Me.TabPage4.Controls.Add(SplitContainerChartKind)
      Me.TabPage4.Location = New System.Drawing.Point(4, 22)
      Me.TabPage4.Name = "TabPage4"
      Me.TabPage4.Size = New System.Drawing.Size(610, 836)
      Me.TabPage4.TabIndex = 3
      Me.TabPage4.Text = "Element Class Summary"
      Me.TabPage4.UseVisualStyleBackColor = True
      '
      'SplitContainerChartKind
      '
      Me.SplitContainerChartKind.Dock = System.Windows.Forms.DockStyle.Fill
      Me.SplitContainerChartKind.Location = New System.Drawing.Point(0, 0)
      Me.SplitContainerChartKind.Name = "SplitContainerChartKind"
      Me.SplitContainerChartKind.Orientation = System.Windows.Forms.Orientation.Horizontal
      '
      'SplitContainerChartKind.Panel1
      '
      Me.SplitContainerChartKind.Panel1.Controls.Add(ComboBoxChartClassCount)
      Me.SplitContainerChartKind.Panel1.Controls.Add(RadioButtonChartClassType)
      Me.SplitContainerChartKind.Panel1.Controls.Add(RadioButtonChartClassInst)
      Me.SplitContainerChartKind.Panel1.Controls.Add(ChartClass)
      '
      'SplitContainerChartKind.Panel2
      '
      Me.SplitContainerChartKind.Panel2.Controls.Add(DataGridViewSummaryClass)
      Me.SplitContainerChartKind.Size = New System.Drawing.Size(610, 836)
      Me.SplitContainerChartKind.SplitterDistance = 323
      Me.SplitContainerChartKind.TabIndex = 0
      '
      'ComboBoxChartClassCount
      '
      Me.ComboBoxChartClassCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.ComboBoxChartClassCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.ComboBoxChartClassCount.FormattingEnabled = True
      Me.ComboBoxChartClassCount.Location = New System.Drawing.Point(335, 300)
      Me.ComboBoxChartClassCount.Name = "ComboBoxChartClassCount"
      Me.ComboBoxChartClassCount.Size = New System.Drawing.Size(81, 21)
      Me.ComboBoxChartClassCount.TabIndex = 14
      '
      'RadioButtonChartClassType
      '
      Me.RadioButtonChartClassType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.RadioButtonChartClassType.AutoSize = True
      Me.RadioButtonChartClassType.Location = New System.Drawing.Point(177, 301)
      Me.RadioButtonChartClassType.Name = "RadioButtonChartClassType"
      Me.RadioButtonChartClassType.Size = New System.Drawing.Size(120, 17)
      Me.RadioButtonChartClassType.TabIndex = 13
      Me.RadioButtonChartClassType.Text = "Show Top by Types"
      Me.RadioButtonChartClassType.UseVisualStyleBackColor = True
      '
      'RadioButtonChartClassInst
      '
      Me.RadioButtonChartClassInst.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.RadioButtonChartClassInst.AutoSize = True
      Me.RadioButtonChartClassInst.Checked = True
      Me.RadioButtonChartClassInst.Location = New System.Drawing.Point(15, 301)
      Me.RadioButtonChartClassInst.Name = "RadioButtonChartClassInst"
      Me.RadioButtonChartClassInst.Size = New System.Drawing.Size(137, 17)
      Me.RadioButtonChartClassInst.TabIndex = 12
      Me.RadioButtonChartClassInst.TabStop = True
      Me.RadioButtonChartClassInst.Text = "Show Top by Instances"
      Me.RadioButtonChartClassInst.UseVisualStyleBackColor = True
      '
      'ChartClass
      '
      Me.ChartClass.BackColor = System.Drawing.Color.Transparent
      ChartArea3.Name = "ChartArea1"
      Me.ChartClass.ChartAreas.Add(ChartArea3)
      Me.ChartClass.Dock = System.Windows.Forms.DockStyle.Fill
      Me.ChartClass.Location = New System.Drawing.Point(0, 0)
      Me.ChartClass.Name = "ChartClass"
      Me.ChartClass.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Fire
      Series3.BorderColor = System.Drawing.Color.White
      Series3.ChartArea = "ChartArea1"
      Series3.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
      Series3.Name = "Types"
      Me.ChartClass.Series.Add(Series3)
      Me.ChartClass.Size = New System.Drawing.Size(610, 323)
      Me.ChartClass.TabIndex = 16
      Me.ChartClass.Text = "Chart1"
      '
      'DataGridViewSummaryClass
      '
      Me.DataGridViewSummaryClass.AllowUserToAddRows = False
      Me.DataGridViewSummaryClass.AllowUserToDeleteRows = False
      Me.DataGridViewSummaryClass.AllowUserToResizeRows = False
      Me.DataGridViewSummaryClass.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
      Me.DataGridViewSummaryClass.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2})
      Me.DataGridViewSummaryClass.Dock = System.Windows.Forms.DockStyle.Fill
      Me.DataGridViewSummaryClass.Location = New System.Drawing.Point(0, 0)
      Me.DataGridViewSummaryClass.Name = "DataGridViewSummaryClass"
      Me.DataGridViewSummaryClass.ReadOnly = True
      Me.DataGridViewSummaryClass.RowHeadersVisible = False
      Me.DataGridViewSummaryClass.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
      Me.DataGridViewSummaryClass.Size = New System.Drawing.Size(610, 509)
      Me.DataGridViewSummaryClass.TabIndex = 4
      '
      'DataGridViewTextBoxColumn1
      '
      Me.DataGridViewTextBoxColumn1.DataPropertyName = "Kind"
      Me.DataGridViewTextBoxColumn1.HeaderText = "Element Class Kind"
      Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
      Me.DataGridViewTextBoxColumn1.ReadOnly = True
      Me.DataGridViewTextBoxColumn1.Width = 200
      '
      'TabControl1
      '
      Me.TabControl1.Controls.Add(TabPage3)
      Me.TabControl1.Controls.Add(TabPage1)
      Me.TabControl1.Controls.Add(TabPage4)
      Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
      Me.TabControl1.Location = New System.Drawing.Point(0, 0)
      Me.TabControl1.Name = "TabControl1"
      Me.TabControl1.SelectedIndex = 0
      Me.TabControl1.Size = New System.Drawing.Size(618, 862)
      Me.TabControl1.TabIndex = 1
      '
      'SplitContainerMaster
      '
      Me.SplitContainerMaster.Dock = System.Windows.Forms.DockStyle.Fill
      Me.SplitContainerMaster.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
      Me.SplitContainerMaster.Location = New System.Drawing.Point(0, 0)
      Me.SplitContainerMaster.Name = "SplitContainerMaster"
      '
      'SplitContainerMaster.Panel1
      '
      Me.SplitContainerMaster.Panel1.Controls.Add(GroupBoxBrowserOptions)
      Me.SplitContainerMaster.Panel1.Controls.Add(GroupBoxBrowser)
      '
      'SplitContainerMaster.Panel2
      '
      Me.SplitContainerMaster.Panel2.Controls.Add(TabControl1)
      Me.SplitContainerMaster.Size = New System.Drawing.Size(990, 862)
      Me.SplitContainerMaster.SplitterDistance = 368
      Me.SplitContainerMaster.TabIndex = 1
      '
      'GroupBoxBrowserOptions
      '
      Me.GroupBoxBrowserOptions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
              Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.GroupBoxBrowserOptions.Controls.Add(RadioButtonClass)
      Me.GroupBoxBrowserOptions.Controls.Add(RadioButtonWorkset)
      Me.GroupBoxBrowserOptions.Controls.Add(RadioButtonCategory)
      Me.GroupBoxBrowserOptions.Location = New System.Drawing.Point(12, 6)
      Me.GroupBoxBrowserOptions.Name = "GroupBoxBrowserOptions"
      Me.GroupBoxBrowserOptions.Size = New System.Drawing.Size(353, 78)
      Me.GroupBoxBrowserOptions.TabIndex = 3
      Me.GroupBoxBrowserOptions.TabStop = False
      Me.GroupBoxBrowserOptions.Text = "Data Browsing Options"
      '
      'RadioButtonClass
      '
      Me.RadioButtonClass.AutoSize = True
      Me.RadioButtonClass.Location = New System.Drawing.Point(228, 31)
      Me.RadioButtonClass.Name = "RadioButtonClass"
      Me.RadioButtonClass.Size = New System.Drawing.Size(106, 17)
      Me.RadioButtonClass.TabIndex = 2
      Me.RadioButtonClass.Text = "By Element Class"
      Me.RadioButtonClass.UseVisualStyleBackColor = True
      '
      'RadioButtonWorkset
      '
      Me.RadioButtonWorkset.AutoSize = True
      Me.RadioButtonWorkset.Checked = True
      Me.RadioButtonWorkset.Location = New System.Drawing.Point(20, 31)
      Me.RadioButtonWorkset.Name = "RadioButtonWorkset"
      Me.RadioButtonWorkset.Size = New System.Drawing.Size(80, 17)
      Me.RadioButtonWorkset.TabIndex = 1
      Me.RadioButtonWorkset.TabStop = True
      Me.RadioButtonWorkset.Text = "By Workset"
      Me.RadioButtonWorkset.UseVisualStyleBackColor = True
      '
      'RadioButtonCategory
      '
      Me.RadioButtonCategory.AutoSize = True
      Me.RadioButtonCategory.Location = New System.Drawing.Point(121, 31)
      Me.RadioButtonCategory.Name = "RadioButtonCategory"
      Me.RadioButtonCategory.Size = New System.Drawing.Size(82, 17)
      Me.RadioButtonCategory.TabIndex = 0
      Me.RadioButtonCategory.Text = "By Category"
      Me.RadioButtonCategory.UseVisualStyleBackColor = True
      '
      'GroupBoxBrowser
      '
      Me.GroupBoxBrowser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
              Or System.Windows.Forms.AnchorStyles.Left) _
              Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.GroupBoxBrowser.Controls.Add(MultiSelectTreeview1)
      Me.GroupBoxBrowser.Controls.Add(ButtonSelect)
      Me.GroupBoxBrowser.Location = New System.Drawing.Point(12, 90)
      Me.GroupBoxBrowser.Name = "GroupBoxBrowser"
      Me.GroupBoxBrowser.Size = New System.Drawing.Size(353, 760)
      Me.GroupBoxBrowser.TabIndex = 0
      Me.GroupBoxBrowser.TabStop = False
      Me.GroupBoxBrowser.Text = "Building Data Browser"
      '
      'ButtonSelect
      '
      Me.ButtonSelect.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
              Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.ButtonSelect.Location = New System.Drawing.Point(12, 19)
      Me.ButtonSelect.Name = "ButtonSelect"
      Me.ButtonSelect.Size = New System.Drawing.Size(335, 40)
      Me.ButtonSelect.TabIndex = 1
      Me.ButtonSelect.Text = "Select Elements in Model"
      Me.ButtonSelect.UseVisualStyleBackColor = True
      '
      'MultiSelectTreeview1
      '
      Me.MultiSelectTreeview1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
              Or System.Windows.Forms.AnchorStyles.Left) _
              Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.MultiSelectTreeview1.Location = New System.Drawing.Point(12, 65)
      Me.MultiSelectTreeview1.Name = "MultiSelectTreeview1"
      Me.MultiSelectTreeview1.SelectedNodes = CType(resources.GetObject("MultiSelectTreeview1.SelectedNodes"), System.Collections.Generic.List(Of System.Windows.Forms.TreeNode))
      Me.MultiSelectTreeview1.Size = New System.Drawing.Size(335, 689)
      Me.MultiSelectTreeview1.TabIndex = 2
      '
      'form_Data
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(990, 862)
      Me.Controls.Add(SplitContainerMaster)
      Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
      Me.MinimizeBox = False
      Me.Name = "form_Data"
      Me.Text = "Workset Viewer"
      Me.TabPage1.ResumeLayout(False)
      Me.SplitContainerChartCategory.Panel1.ResumeLayout(False)
      Me.SplitContainerChartCategory.Panel1.PerformLayout()
      Me.SplitContainerChartCategory.Panel2.ResumeLayout(False)
      CType(SplitContainerChartCategory, System.ComponentModel.ISupportInitialize).EndInit()
      Me.SplitContainerChartCategory.ResumeLayout(False)
      CType(ChartCategory, System.ComponentModel.ISupportInitialize).EndInit()
      CType(DataGridViewCategory, System.ComponentModel.ISupportInitialize).EndInit()
      Me.TabPage3.ResumeLayout(False)
      Me.SplitContainerChartWs.Panel1.ResumeLayout(False)
      Me.SplitContainerChartWs.Panel2.ResumeLayout(False)
      CType(SplitContainerChartWs, System.ComponentModel.ISupportInitialize).EndInit()
      Me.SplitContainerChartWs.ResumeLayout(False)
      CType(ChartWorksets, System.ComponentModel.ISupportInitialize).EndInit()
      CType(DataGridViewWorksets, System.ComponentModel.ISupportInitialize).EndInit()
      Me.TabPage4.ResumeLayout(False)
      Me.SplitContainerChartKind.Panel1.ResumeLayout(False)
      Me.SplitContainerChartKind.Panel1.PerformLayout()
      Me.SplitContainerChartKind.Panel2.ResumeLayout(False)
      CType(SplitContainerChartKind, System.ComponentModel.ISupportInitialize).EndInit()
      Me.SplitContainerChartKind.ResumeLayout(False)
      CType(ChartClass, System.ComponentModel.ISupportInitialize).EndInit()
      CType(DataGridViewSummaryClass, System.ComponentModel.ISupportInitialize).EndInit()
      Me.TabControl1.ResumeLayout(False)
      Me.SplitContainerMaster.Panel1.ResumeLayout(False)
      Me.SplitContainerMaster.Panel2.ResumeLayout(False)
      CType(SplitContainerMaster, System.ComponentModel.ISupportInitialize).EndInit()
      Me.SplitContainerMaster.ResumeLayout(False)
      Me.GroupBoxBrowserOptions.ResumeLayout(False)
      Me.GroupBoxBrowserOptions.PerformLayout()
      Me.GroupBoxBrowser.ResumeLayout(False)
      Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainerChartCategory As System.Windows.Forms.SplitContainer
    Friend WithEvents ComboBoxCategoryItemCount As System.Windows.Forms.ComboBox
    Friend WithEvents RadioButtonCatsByTypes As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonCatsByInstances As System.Windows.Forms.RadioButton
    Friend WithEvents DataGridViewCategory As System.Windows.Forms.DataGridView
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainerChartWs As System.Windows.Forms.SplitContainer
    Friend WithEvents ComboBoxWorksetCount As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridViewWorksets As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainerChartKind As System.Windows.Forms.SplitContainer
    Friend WithEvents ComboBoxChartClassCount As System.Windows.Forms.ComboBox
    Friend WithEvents RadioButtonChartClassType As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonChartClassInst As System.Windows.Forms.RadioButton
    Friend WithEvents DataGridViewSummaryClass As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents SplitContainerMaster As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBoxBrowserOptions As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonClass As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonWorkset As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonCategory As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBoxBrowser As System.Windows.Forms.GroupBox
    Friend WithEvents ChartCategory As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents ChartWorksets As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents ChartClass As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents ButtonSelect As System.Windows.Forms.Button
    Friend WithEvents MultiSelectTreeview1 As [Case].Subs.Worksets.UI.MultiSelectTreeview
  End Class
End Namespace
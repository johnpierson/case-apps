<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_Main
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_Main))
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.ButtonOk = New System.Windows.Forms.Button()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.CheckBoxSheets = New System.Windows.Forms.CheckBox()
    Me.CheckBoxViews = New System.Windows.Forms.CheckBox()
    Me.CheckBoxLinks = New System.Windows.Forms.CheckBox()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.TabControl1 = New System.Windows.Forms.TabControl()
    Me.TabPage1 = New System.Windows.Forms.TabPage()
    Me.DataGridViewSheets = New System.Windows.Forms.DataGridView()
    Me.DataGridViewCheckBoxColumnSheets = New System.Windows.Forms.DataGridViewCheckBoxColumn()
    Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.ButtonSheetsNone = New System.Windows.Forms.Button()
    Me.ButtonSheetsAll = New System.Windows.Forms.Button()
    Me.TabPage2 = New System.Windows.Forms.TabPage()
    Me.GroupBoxViews = New System.Windows.Forms.GroupBox()
    Me.DataGridViewViews = New System.Windows.Forms.DataGridView()
    Me.DataGridViewCheckBoxColumnViews = New System.Windows.Forms.DataGridViewCheckBoxColumn()
    Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.GroupBoxViewTypes = New System.Windows.Forms.GroupBox()
    Me.DataGridViewViewTypes = New System.Windows.Forms.DataGridView()
    Me.DataGridViewCheckBoxColumnViewFamilyTypes = New System.Windows.Forms.DataGridViewCheckBoxColumn()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.DataGridViewTextBoxColumnVT = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.ButtonViewsNone = New System.Windows.Forms.Button()
    Me.ButtonViewsAll = New System.Windows.Forms.Button()
    Me.TabPage3 = New System.Windows.Forms.TabPage()
    Me.DataGridViewLinks = New System.Windows.Forms.DataGridView()
    Me.DataGridViewCheckBoxLinks = New System.Windows.Forms.DataGridViewCheckBoxColumn()
    Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.ButtonLinksNone = New System.Windows.Forms.Button()
    Me.ButtonLinksAll = New System.Windows.Forms.Button()
    Me.CheckBoxTypes = New System.Windows.Forms.CheckBox()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    CType(PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox1.SuspendLayout()
    Me.TabControl1.SuspendLayout()
    Me.TabPage1.SuspendLayout()
    CType(DataGridViewSheets, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage2.SuspendLayout()
    Me.GroupBoxViews.SuspendLayout()
    CType(DataGridViewViews, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBoxViewTypes.SuspendLayout()
    CType(DataGridViewViewTypes, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage3.SuspendLayout()
    CType(DataGridViewLinks, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(581, 660)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(91, 40)
    Me.ButtonCancel.TabIndex = 0
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ButtonOk
    '
    Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonOk.Location = New System.Drawing.Point(434, 660)
    Me.ButtonOk.Name = "ButtonOk"
    Me.ButtonOk.Size = New System.Drawing.Size(141, 40)
    Me.ButtonOk.TabIndex = 1
    Me.ButtonOk.Text = "Remove Selections"
    Me.ButtonOk.UseVisualStyleBackColor = True
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(177, 660)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(495, 40)
    Me.ProgressBar1.TabIndex = 3
    '
    'CheckBoxSheets
    '
    Me.CheckBoxSheets.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.CheckBoxSheets.AutoSize = True
    Me.CheckBoxSheets.Checked = True
    Me.CheckBoxSheets.CheckState = System.Windows.Forms.CheckState.Checked
    Me.CheckBoxSheets.Location = New System.Drawing.Point(22, 628)
    Me.CheckBoxSheets.Name = "CheckBoxSheets"
    Me.CheckBoxSheets.Size = New System.Drawing.Size(93, 17)
    Me.CheckBoxSheets.TabIndex = 4
    Me.CheckBoxSheets.Text = "Delete Sheets"
    Me.CheckBoxSheets.UseVisualStyleBackColor = True
    '
    'CheckBoxViews
    '
    Me.CheckBoxViews.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.CheckBoxViews.AutoSize = True
    Me.CheckBoxViews.Checked = True
    Me.CheckBoxViews.CheckState = System.Windows.Forms.CheckState.Checked
    Me.CheckBoxViews.Location = New System.Drawing.Point(177, 628)
    Me.CheckBoxViews.Name = "CheckBoxViews"
    Me.CheckBoxViews.Size = New System.Drawing.Size(88, 17)
    Me.CheckBoxViews.TabIndex = 5
    Me.CheckBoxViews.Text = "Delete Views"
    Me.CheckBoxViews.UseVisualStyleBackColor = True
    '
    'CheckBoxLinks
    '
    Me.CheckBoxLinks.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.CheckBoxLinks.AutoSize = True
    Me.CheckBoxLinks.Checked = True
    Me.CheckBoxLinks.CheckState = System.Windows.Forms.CheckState.Checked
    Me.CheckBoxLinks.Location = New System.Drawing.Point(330, 628)
    Me.CheckBoxLinks.Name = "CheckBoxLinks"
    Me.CheckBoxLinks.Size = New System.Drawing.Size(122, 17)
    Me.CheckBoxLinks.TabIndex = 6
    Me.CheckBoxLinks.Text = "Remove Revit Links"
    Me.CheckBoxLinks.UseVisualStyleBackColor = True
    '
    'PictureBox1
    '
    Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.PictureBox1.Image = Global.[Case].Subs.DeleteViewsAndPurge.My.Resources.Resources.Case_Clearly
    Me.PictureBox1.Location = New System.Drawing.Point(12, 660)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(159, 40)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 7
    Me.PictureBox1.TabStop = False
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(TabControl1)
    Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(660, 610)
    Me.GroupBox1.TabIndex = 8
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Remove Selected Items"
    '
    'TabControl1
    '
    Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TabControl1.Controls.Add(TabPage1)
    Me.TabControl1.Controls.Add(TabPage2)
    Me.TabControl1.Controls.Add(TabPage3)
    Me.TabControl1.Location = New System.Drawing.Point(6, 19)
    Me.TabControl1.Name = "TabControl1"
    Me.TabControl1.SelectedIndex = 0
    Me.TabControl1.Size = New System.Drawing.Size(648, 585)
    Me.TabControl1.TabIndex = 0
    '
    'TabPage1
    '
    Me.TabPage1.Controls.Add(DataGridViewSheets)
    Me.TabPage1.Controls.Add(ButtonSheetsNone)
    Me.TabPage1.Controls.Add(ButtonSheetsAll)
    Me.TabPage1.Location = New System.Drawing.Point(4, 22)
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage1.Size = New System.Drawing.Size(640, 559)
    Me.TabPage1.TabIndex = 0
    Me.TabPage1.Text = "Sheets"
    Me.TabPage1.UseVisualStyleBackColor = True
    '
    'DataGridViewSheets
    '
    Me.DataGridViewSheets.AllowUserToAddRows = False
    Me.DataGridViewSheets.AllowUserToDeleteRows = False
    Me.DataGridViewSheets.AllowUserToResizeRows = False
    Me.DataGridViewSheets.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.DataGridViewSheets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewSheets.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewCheckBoxColumnSheets, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn2})
    Me.DataGridViewSheets.Location = New System.Drawing.Point(3, 6)
    Me.DataGridViewSheets.MultiSelect = False
    Me.DataGridViewSheets.Name = "DataGridViewSheets"
    Me.DataGridViewSheets.RowHeadersVisible = False
    Me.DataGridViewSheets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewSheets.Size = New System.Drawing.Size(550, 553)
    Me.DataGridViewSheets.TabIndex = 7
    '
    'DataGridViewCheckBoxColumnSheets
    '
    Me.DataGridViewCheckBoxColumnSheets.DataPropertyName = "isChecked"
    Me.DataGridViewCheckBoxColumnSheets.HeaderText = ""
    Me.DataGridViewCheckBoxColumnSheets.MinimumWidth = 25
    Me.DataGridViewCheckBoxColumnSheets.Name = "DataGridViewCheckBoxColumnSheets"
    Me.DataGridViewCheckBoxColumnSheets.Width = 25
    '
    'DataGridViewTextBoxColumn3
    '
    Me.DataGridViewTextBoxColumn3.DataPropertyName = "Number"
    Me.DataGridViewTextBoxColumn3.HeaderText = "Number"
    Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
    '
    'DataGridViewTextBoxColumn2
    '
    Me.DataGridViewTextBoxColumn2.DataPropertyName = "Name"
    Me.DataGridViewTextBoxColumn2.HeaderText = "Name"
    Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
    Me.DataGridViewTextBoxColumn2.ReadOnly = True
    Me.DataGridViewTextBoxColumn2.Width = 400
    '
    'ButtonSheetsNone
    '
    Me.ButtonSheetsNone.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonSheetsNone.Location = New System.Drawing.Point(559, 52)
    Me.ButtonSheetsNone.Name = "ButtonSheetsNone"
    Me.ButtonSheetsNone.Size = New System.Drawing.Size(75, 40)
    Me.ButtonSheetsNone.TabIndex = 2
    Me.ButtonSheetsNone.Text = "Select None"
    Me.ButtonSheetsNone.UseVisualStyleBackColor = True
    '
    'ButtonSheetsAll
    '
    Me.ButtonSheetsAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonSheetsAll.Location = New System.Drawing.Point(559, 6)
    Me.ButtonSheetsAll.Name = "ButtonSheetsAll"
    Me.ButtonSheetsAll.Size = New System.Drawing.Size(75, 40)
    Me.ButtonSheetsAll.TabIndex = 1
    Me.ButtonSheetsAll.Text = "Select All"
    Me.ButtonSheetsAll.UseVisualStyleBackColor = True
    '
    'TabPage2
    '
    Me.TabPage2.Controls.Add(GroupBoxViews)
    Me.TabPage2.Controls.Add(GroupBoxViewTypes)
    Me.TabPage2.Controls.Add(ButtonViewsNone)
    Me.TabPage2.Controls.Add(ButtonViewsAll)
    Me.TabPage2.Location = New System.Drawing.Point(4, 22)
    Me.TabPage2.Name = "TabPage2"
    Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage2.Size = New System.Drawing.Size(640, 559)
    Me.TabPage2.TabIndex = 1
    Me.TabPage2.Text = "Views"
    Me.TabPage2.UseVisualStyleBackColor = True
    '
    'GroupBoxViews
    '
    Me.GroupBoxViews.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxViews.Controls.Add(DataGridViewViews)
    Me.GroupBoxViews.Location = New System.Drawing.Point(6, 154)
    Me.GroupBoxViews.Name = "GroupBoxViews"
    Me.GroupBoxViews.Size = New System.Drawing.Size(547, 402)
    Me.GroupBoxViews.TabIndex = 9
    Me.GroupBoxViews.TabStop = False
    Me.GroupBoxViews.Text = "Views to Remove"
    '
    'DataGridViewViews
    '
    Me.DataGridViewViews.AllowUserToAddRows = False
    Me.DataGridViewViews.AllowUserToDeleteRows = False
    Me.DataGridViewViews.AllowUserToResizeRows = False
    Me.DataGridViewViews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewViews.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewCheckBoxColumnViews, Me.DataGridViewTextBoxColumn1, Me.Column3, Me.Column4})
    Me.DataGridViewViews.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DataGridViewViews.Location = New System.Drawing.Point(3, 16)
    Me.DataGridViewViews.MultiSelect = False
    Me.DataGridViewViews.Name = "DataGridViewViews"
    Me.DataGridViewViews.RowHeadersVisible = False
    Me.DataGridViewViews.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewViews.Size = New System.Drawing.Size(541, 383)
    Me.DataGridViewViews.TabIndex = 6
    '
    'DataGridViewCheckBoxColumnViews
    '
    Me.DataGridViewCheckBoxColumnViews.DataPropertyName = "isChecked"
    Me.DataGridViewCheckBoxColumnViews.HeaderText = ""
    Me.DataGridViewCheckBoxColumnViews.MinimumWidth = 25
    Me.DataGridViewCheckBoxColumnViews.Name = "DataGridViewCheckBoxColumnViews"
    Me.DataGridViewCheckBoxColumnViews.Width = 25
    '
    'DataGridViewTextBoxColumn1
    '
    Me.DataGridViewTextBoxColumn1.DataPropertyName = "Name"
    Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
    Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
    Me.DataGridViewTextBoxColumn1.ReadOnly = True
    Me.DataGridViewTextBoxColumn1.Width = 200
    '
    'Column3
    '
    Me.Column3.DataPropertyName = "Type"
    Me.Column3.HeaderText = "Type"
    Me.Column3.Name = "Column3"
    Me.Column3.Width = 150
    '
    'Column4
    '
    Me.Column4.DataPropertyName = "Level"
    Me.Column4.HeaderText = "Level"
    Me.Column4.Name = "Column4"
    Me.Column4.Width = 150
    '
    'GroupBoxViewTypes
    '
    Me.GroupBoxViewTypes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxViewTypes.Controls.Add(DataGridViewViewTypes)
    Me.GroupBoxViewTypes.Location = New System.Drawing.Point(6, 6)
    Me.GroupBoxViewTypes.Name = "GroupBoxViewTypes"
    Me.GroupBoxViewTypes.Size = New System.Drawing.Size(628, 142)
    Me.GroupBoxViewTypes.TabIndex = 8
    Me.GroupBoxViewTypes.TabStop = False
    Me.GroupBoxViewTypes.Text = "View Types to Include"
    '
    'DataGridViewViewTypes
    '
    Me.DataGridViewViewTypes.AllowUserToAddRows = False
    Me.DataGridViewViewTypes.AllowUserToDeleteRows = False
    Me.DataGridViewViewTypes.AllowUserToResizeRows = False
    Me.DataGridViewViewTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewViewTypes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewCheckBoxColumnViewFamilyTypes, Me.Column2, Me.DataGridViewTextBoxColumnVT, Me.DataGridViewTextBoxColumn6})
    Me.DataGridViewViewTypes.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DataGridViewViewTypes.Location = New System.Drawing.Point(3, 16)
    Me.DataGridViewViewTypes.MultiSelect = False
    Me.DataGridViewViewTypes.Name = "DataGridViewViewTypes"
    Me.DataGridViewViewTypes.RowHeadersVisible = False
    Me.DataGridViewViewTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewViewTypes.Size = New System.Drawing.Size(622, 123)
    Me.DataGridViewViewTypes.TabIndex = 7
    '
    'DataGridViewCheckBoxColumnViewFamilyTypes
    '
    Me.DataGridViewCheckBoxColumnViewFamilyTypes.DataPropertyName = "isChecked"
    Me.DataGridViewCheckBoxColumnViewFamilyTypes.HeaderText = ""
    Me.DataGridViewCheckBoxColumnViewFamilyTypes.MinimumWidth = 25
    Me.DataGridViewCheckBoxColumnViewFamilyTypes.Name = "DataGridViewCheckBoxColumnViewFamilyTypes"
    Me.DataGridViewCheckBoxColumnViewFamilyTypes.Width = 25
    '
    'Column2
    '
    Me.Column2.DataPropertyName = "ViewKind"
    Me.Column2.HeaderText = "Kind of View"
    Me.Column2.MinimumWidth = 200
    Me.Column2.Name = "Column2"
    Me.Column2.Width = 200
    '
    'DataGridViewTextBoxColumnVT
    '
    Me.DataGridViewTextBoxColumnVT.DataPropertyName = "Type"
    Me.DataGridViewTextBoxColumnVT.HeaderText = "View Family Type"
    Me.DataGridViewTextBoxColumnVT.Name = "DataGridViewTextBoxColumnVT"
    Me.DataGridViewTextBoxColumnVT.Width = 200
    '
    'DataGridViewTextBoxColumn6
    '
    Me.DataGridViewTextBoxColumn6.DataPropertyName = "Count"
    Me.DataGridViewTextBoxColumn6.HeaderText = "Qty."
    Me.DataGridViewTextBoxColumn6.MinimumWidth = 50
    Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
    Me.DataGridViewTextBoxColumn6.Width = 50
    '
    'ButtonViewsNone
    '
    Me.ButtonViewsNone.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonViewsNone.Location = New System.Drawing.Point(559, 200)
    Me.ButtonViewsNone.Name = "ButtonViewsNone"
    Me.ButtonViewsNone.Size = New System.Drawing.Size(75, 40)
    Me.ButtonViewsNone.TabIndex = 4
    Me.ButtonViewsNone.Text = "Select None"
    Me.ButtonViewsNone.UseVisualStyleBackColor = True
    '
    'ButtonViewsAll
    '
    Me.ButtonViewsAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonViewsAll.Location = New System.Drawing.Point(559, 154)
    Me.ButtonViewsAll.Name = "ButtonViewsAll"
    Me.ButtonViewsAll.Size = New System.Drawing.Size(75, 40)
    Me.ButtonViewsAll.TabIndex = 3
    Me.ButtonViewsAll.Text = "Select All"
    Me.ButtonViewsAll.UseVisualStyleBackColor = True
    '
    'TabPage3
    '
    Me.TabPage3.Controls.Add(DataGridViewLinks)
    Me.TabPage3.Controls.Add(ButtonLinksNone)
    Me.TabPage3.Controls.Add(ButtonLinksAll)
    Me.TabPage3.Location = New System.Drawing.Point(4, 22)
    Me.TabPage3.Name = "TabPage3"
    Me.TabPage3.Size = New System.Drawing.Size(640, 559)
    Me.TabPage3.TabIndex = 2
    Me.TabPage3.Text = "Links"
    Me.TabPage3.UseVisualStyleBackColor = True
    '
    'DataGridViewLinks
    '
    Me.DataGridViewLinks.AllowUserToAddRows = False
    Me.DataGridViewLinks.AllowUserToDeleteRows = False
    Me.DataGridViewLinks.AllowUserToResizeRows = False
    Me.DataGridViewLinks.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.DataGridViewLinks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewLinks.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewCheckBoxLinks, Me.Column1})
    Me.DataGridViewLinks.Location = New System.Drawing.Point(3, 6)
    Me.DataGridViewLinks.MultiSelect = False
    Me.DataGridViewLinks.Name = "DataGridViewLinks"
    Me.DataGridViewLinks.RowHeadersVisible = False
    Me.DataGridViewLinks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewLinks.Size = New System.Drawing.Size(550, 553)
    Me.DataGridViewLinks.TabIndex = 5
    '
    'DataGridViewCheckBoxLinks
    '
    Me.DataGridViewCheckBoxLinks.DataPropertyName = "isChecked"
    Me.DataGridViewCheckBoxLinks.HeaderText = ""
    Me.DataGridViewCheckBoxLinks.MinimumWidth = 25
    Me.DataGridViewCheckBoxLinks.Name = "DataGridViewCheckBoxLinks"
    Me.DataGridViewCheckBoxLinks.Width = 25
    '
    'Column1
    '
    Me.Column1.DataPropertyName = "Link"
    Me.Column1.HeaderText = "Link"
    Me.Column1.Name = "Column1"
    Me.Column1.ReadOnly = True
    Me.Column1.Width = 500
    '
    'ButtonLinksNone
    '
    Me.ButtonLinksNone.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonLinksNone.Location = New System.Drawing.Point(559, 52)
    Me.ButtonLinksNone.Name = "ButtonLinksNone"
    Me.ButtonLinksNone.Size = New System.Drawing.Size(75, 40)
    Me.ButtonLinksNone.TabIndex = 4
    Me.ButtonLinksNone.Text = "Select None"
    Me.ButtonLinksNone.UseVisualStyleBackColor = True
    '
    'ButtonLinksAll
    '
    Me.ButtonLinksAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonLinksAll.Location = New System.Drawing.Point(559, 6)
    Me.ButtonLinksAll.Name = "ButtonLinksAll"
    Me.ButtonLinksAll.Size = New System.Drawing.Size(75, 40)
    Me.ButtonLinksAll.TabIndex = 3
    Me.ButtonLinksAll.Text = "Select All"
    Me.ButtonLinksAll.UseVisualStyleBackColor = True
    '
    'CheckBoxTypes
    '
    Me.CheckBoxTypes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.CheckBoxTypes.AutoSize = True
    Me.CheckBoxTypes.Location = New System.Drawing.Point(504, 628)
    Me.CheckBoxTypes.Name = "CheckBoxTypes"
    Me.CheckBoxTypes.Size = New System.Drawing.Size(129, 17)
    Me.CheckBoxTypes.TabIndex = 9
    Me.CheckBoxTypes.Text = "Delete Unused Types"
    Me.CheckBoxTypes.UseVisualStyleBackColor = True
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(382, 660)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(46, 40)
    Me.ButtonHelp.TabIndex = 10
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'form_Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(684, 712)
    Me.Controls.Add(ButtonHelp)
    Me.Controls.Add(CheckBoxTypes)
    Me.Controls.Add(GroupBox1)
    Me.Controls.Add(PictureBox1)
    Me.Controls.Add(CheckBoxLinks)
    Me.Controls.Add(CheckBoxViews)
    Me.Controls.Add(CheckBoxSheets)
    Me.Controls.Add(ButtonOk)
    Me.Controls.Add(ButtonCancel)
    Me.Controls.Add(ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(700, 750)
    Me.Name = "form_Main"
    Me.Text = "Delete Sheets, Views and Revit Links"
    CType(PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox1.ResumeLayout(False)
    Me.TabControl1.ResumeLayout(False)
    Me.TabPage1.ResumeLayout(False)
    CType(DataGridViewSheets, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage2.ResumeLayout(False)
    Me.GroupBoxViews.ResumeLayout(False)
    CType(DataGridViewViews, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBoxViewTypes.ResumeLayout(False)
    CType(DataGridViewViewTypes, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage3.ResumeLayout(False)
    CType(DataGridViewLinks, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents ButtonOk As System.Windows.Forms.Button
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents CheckBoxSheets As System.Windows.Forms.CheckBox
  Friend WithEvents CheckBoxViews As System.Windows.Forms.CheckBox
  Friend WithEvents CheckBoxLinks As System.Windows.Forms.CheckBox
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
  Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
  Friend WithEvents ButtonSheetsNone As System.Windows.Forms.Button
  Friend WithEvents ButtonSheetsAll As System.Windows.Forms.Button
  Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
  Friend WithEvents ButtonViewsNone As System.Windows.Forms.Button
  Friend WithEvents ButtonViewsAll As System.Windows.Forms.Button
  Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
  Friend WithEvents ButtonLinksNone As System.Windows.Forms.Button
  Friend WithEvents ButtonLinksAll As System.Windows.Forms.Button
  Friend WithEvents DataGridViewLinks As System.Windows.Forms.DataGridView
  Friend WithEvents DataGridViewSheets As System.Windows.Forms.DataGridView
  Friend WithEvents DataGridViewViews As System.Windows.Forms.DataGridView
  Friend WithEvents DataGridViewCheckBoxColumnSheets As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewCheckBoxColumnViews As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewCheckBoxLinks As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents CheckBoxTypes As System.Windows.Forms.CheckBox
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
  Friend WithEvents GroupBoxViewTypes As System.Windows.Forms.GroupBox
  Friend WithEvents DataGridViewViewTypes As System.Windows.Forms.DataGridView
  Friend WithEvents GroupBoxViews As System.Windows.Forms.GroupBox
  Friend WithEvents DataGridViewCheckBoxColumnViewFamilyTypes As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumnVT As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class

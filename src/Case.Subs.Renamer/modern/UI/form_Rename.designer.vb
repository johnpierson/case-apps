<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_Rename
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_Rename))
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.ButtonExport = New System.Windows.Forms.Button()
    Me.ButtonImport = New System.Windows.Forms.Button()
    Me.GroupBoxCategories = New System.Windows.Forms.GroupBox()
    Me.ButtonSelectNone = New System.Windows.Forms.Button()
    Me.ButtonSelectAll = New System.Windows.Forms.Button()
    Me.DataGridViewCategories = New System.Windows.Forms.DataGridView()
    Me.ColumnCheckedCategory = New System.Windows.Forms.DataGridViewCheckBoxColumn()
    Me.ColumnCategoryName = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.GroupBoxElements = New System.Windows.Forms.GroupBox()
    Me.DataGridViewElements = New System.Windows.Forms.DataGridView()
    Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.ButtonRename = New System.Windows.Forms.Button()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
    Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
    Me.GroupBoxCategories.SuspendLayout()
    CType(DataGridViewCategories, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBoxElements.SuspendLayout()
    CType(DataGridViewElements, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(707, 410)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(120, 40)
    Me.ButtonCancel.TabIndex = 0
    Me.ButtonCancel.Text = "Close"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ButtonExport
    '
    Me.ButtonExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonExport.Location = New System.Drawing.Point(581, 410)
    Me.ButtonExport.Name = "ButtonExport"
    Me.ButtonExport.Size = New System.Drawing.Size(120, 40)
    Me.ButtonExport.TabIndex = 1
    Me.ButtonExport.Text = "Export to Excel"
    Me.ButtonExport.UseVisualStyleBackColor = True
    '
    'ButtonImport
    '
    Me.ButtonImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonImport.Location = New System.Drawing.Point(455, 410)
    Me.ButtonImport.Name = "ButtonImport"
    Me.ButtonImport.Size = New System.Drawing.Size(120, 40)
    Me.ButtonImport.TabIndex = 2
    Me.ButtonImport.Text = "Import from Excel"
    Me.ButtonImport.UseVisualStyleBackColor = True
    '
    'GroupBoxCategories
    '
    Me.GroupBoxCategories.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxCategories.Controls.Add(ButtonSelectNone)
    Me.GroupBoxCategories.Controls.Add(ButtonSelectAll)
    Me.GroupBoxCategories.Controls.Add(DataGridViewCategories)
    Me.GroupBoxCategories.Location = New System.Drawing.Point(12, 12)
    Me.GroupBoxCategories.Name = "GroupBoxCategories"
    Me.GroupBoxCategories.Size = New System.Drawing.Size(244, 438)
    Me.GroupBoxCategories.TabIndex = 3
    Me.GroupBoxCategories.TabStop = False
    Me.GroupBoxCategories.Text = "Categories"
    '
    'ButtonSelectNone
    '
    Me.ButtonSelectNone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonSelectNone.Location = New System.Drawing.Point(70, 392)
    Me.ButtonSelectNone.Name = "ButtonSelectNone"
    Me.ButtonSelectNone.Size = New System.Drawing.Size(61, 40)
    Me.ButtonSelectNone.TabIndex = 7
    Me.ButtonSelectNone.Text = "None"
    Me.ButtonSelectNone.UseVisualStyleBackColor = True
    '
    'ButtonSelectAll
    '
    Me.ButtonSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonSelectAll.Location = New System.Drawing.Point(3, 392)
    Me.ButtonSelectAll.Name = "ButtonSelectAll"
    Me.ButtonSelectAll.Size = New System.Drawing.Size(61, 40)
    Me.ButtonSelectAll.TabIndex = 6
    Me.ButtonSelectAll.Text = "All"
    Me.ButtonSelectAll.UseVisualStyleBackColor = True
    '
    'DataGridViewCategories
    '
    Me.DataGridViewCategories.AllowUserToAddRows = False
    Me.DataGridViewCategories.AllowUserToDeleteRows = False
    Me.DataGridViewCategories.AllowUserToResizeRows = False
    Me.DataGridViewCategories.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.DataGridViewCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewCategories.ColumnHeadersVisible = False
    Me.DataGridViewCategories.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnCheckedCategory, Me.ColumnCategoryName})
    Me.DataGridViewCategories.Location = New System.Drawing.Point(3, 16)
    Me.DataGridViewCategories.Name = "DataGridViewCategories"
    Me.DataGridViewCategories.RowHeadersVisible = False
    Me.DataGridViewCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewCategories.Size = New System.Drawing.Size(238, 373)
    Me.DataGridViewCategories.TabIndex = 1
    '
    'ColumnCheckedCategory
    '
    Me.ColumnCheckedCategory.DataPropertyName = "isChecked"
    Me.ColumnCheckedCategory.HeaderText = "isChecked"
    Me.ColumnCheckedCategory.MinimumWidth = 30
    Me.ColumnCheckedCategory.Name = "ColumnCheckedCategory"
    Me.ColumnCheckedCategory.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
    Me.ColumnCheckedCategory.ToolTipText = "Include the category in the naming process"
    Me.ColumnCheckedCategory.Width = 30
    '
    'ColumnCategoryName
    '
    Me.ColumnCategoryName.DataPropertyName = "CatName"
    Me.ColumnCategoryName.HeaderText = "Category"
    Me.ColumnCategoryName.MinimumWidth = 200
    Me.ColumnCategoryName.Name = "ColumnCategoryName"
    Me.ColumnCategoryName.ReadOnly = True
    Me.ColumnCategoryName.Width = 200
    '
    'GroupBoxElements
    '
    Me.GroupBoxElements.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxElements.Controls.Add(DataGridViewElements)
    Me.GroupBoxElements.Location = New System.Drawing.Point(262, 12)
    Me.GroupBoxElements.Name = "GroupBoxElements"
    Me.GroupBoxElements.Size = New System.Drawing.Size(565, 392)
    Me.GroupBoxElements.TabIndex = 4
    Me.GroupBoxElements.TabStop = False
    Me.GroupBoxElements.Text = "Element Types"
    '
    'DataGridViewElements
    '
    Me.DataGridViewElements.AllowUserToAddRows = False
    Me.DataGridViewElements.AllowUserToDeleteRows = False
    Me.DataGridViewElements.AllowUserToResizeRows = False
    Me.DataGridViewElements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewElements.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column5, Me.Column3, Me.Column4, Me.Column1, Me.Column2, Me.Column6, Me.Column7, Me.Column8})
    Me.DataGridViewElements.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DataGridViewElements.Location = New System.Drawing.Point(3, 16)
    Me.DataGridViewElements.Name = "DataGridViewElements"
    Me.DataGridViewElements.ReadOnly = True
    Me.DataGridViewElements.RowHeadersVisible = False
    Me.DataGridViewElements.Size = New System.Drawing.Size(559, 373)
    Me.DataGridViewElements.TabIndex = 0
    '
    'Column5
    '
    Me.Column5.DataPropertyName = "CategoryName"
    Me.Column5.HeaderText = "Category"
    Me.Column5.Name = "Column5"
    Me.Column5.ReadOnly = True
    Me.Column5.Width = 150
    '
    'Column3
    '
    Me.Column3.DataPropertyName = "FamilyNameChange"
    Me.Column3.HeaderText = "Family"
    Me.Column3.Name = "Column3"
    Me.Column3.ReadOnly = True
    Me.Column3.Width = 200
    '
    'Column4
    '
    Me.Column4.DataPropertyName = "TypeNameChange"
    Me.Column4.HeaderText = "Type"
    Me.Column4.Name = "Column4"
    Me.Column4.ReadOnly = True
    Me.Column4.Width = 200
    '
    'Column1
    '
    Me.Column1.DataPropertyName = "ElementID"
    Me.Column1.HeaderText = "ElementID"
    Me.Column1.Name = "Column1"
    Me.Column1.ReadOnly = True
    Me.Column1.Visible = False
    '
    'Column2
    '
    Me.Column2.DataPropertyName = "CurrentFamilyName"
    Me.Column2.HeaderText = "Family Current"
    Me.Column2.Name = "Column2"
    Me.Column2.ReadOnly = True
    Me.Column2.Visible = False
    '
    'Column6
    '
    Me.Column6.DataPropertyName = "NewFamilyName"
    Me.Column6.HeaderText = "Family New"
    Me.Column6.Name = "Column6"
    Me.Column6.ReadOnly = True
    Me.Column6.Visible = False
    '
    'Column7
    '
    Me.Column7.DataPropertyName = "CurrentTypeName"
    Me.Column7.HeaderText = "Type Current"
    Me.Column7.Name = "Column7"
    Me.Column7.ReadOnly = True
    Me.Column7.Visible = False
    '
    'Column8
    '
    Me.Column8.DataPropertyName = "NewTypeName"
    Me.Column8.HeaderText = "Type New"
    Me.Column8.Name = "Column8"
    Me.Column8.ReadOnly = True
    Me.Column8.Visible = False
    '
    'ButtonRename
    '
    Me.ButtonRename.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonRename.Location = New System.Drawing.Point(329, 410)
    Me.ButtonRename.Name = "ButtonRename"
    Me.ButtonRename.Size = New System.Drawing.Size(120, 40)
    Me.ButtonRename.TabIndex = 5
    Me.ButtonRename.Text = "Rename Elements"
    Me.ButtonRename.UseVisualStyleBackColor = True
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(262, 410)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(565, 40)
    Me.ProgressBar1.TabIndex = 6
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(265, 410)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(50, 40)
    Me.ButtonHelp.TabIndex = 7
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'SaveFileDialog1
    '
    Me.SaveFileDialog1.DefaultExt = "*.xlsx"
    Me.SaveFileDialog1.FileName = "FamilyNameExport.xlsx"
    Me.SaveFileDialog1.Filter = "Excel 2010 | *.xlsx"
    Me.SaveFileDialog1.Title = "Exported Excel File Location"
    '
    'OpenFileDialog1
    '
    Me.OpenFileDialog1.DefaultExt = "*.xlsx"
    Me.OpenFileDialog1.Filter = "Excel Files | *.xlsx"
    Me.OpenFileDialog1.Title = "Select a Family and Types Rename Formatted Excel Document"
    '
    'form_Rename
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(839, 462)
    Me.Controls.Add(ButtonHelp)
    Me.Controls.Add(ButtonRename)
    Me.Controls.Add(GroupBoxElements)
    Me.Controls.Add(GroupBoxCategories)
    Me.Controls.Add(ButtonImport)
    Me.Controls.Add(ButtonExport)
    Me.Controls.Add(ButtonCancel)
    Me.Controls.Add(ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(855, 500)
    Me.Name = "form_Rename"
    Me.Text = "Subscription Element Renamer"
    Me.GroupBoxCategories.ResumeLayout(False)
    CType(DataGridViewCategories, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBoxElements.ResumeLayout(False)
    CType(DataGridViewElements, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents ButtonExport As System.Windows.Forms.Button
  Friend WithEvents ButtonImport As System.Windows.Forms.Button
  Friend WithEvents GroupBoxCategories As System.Windows.Forms.GroupBox
  Friend WithEvents GroupBoxElements As System.Windows.Forms.GroupBox
  Friend WithEvents DataGridViewElements As System.Windows.Forms.DataGridView
  Friend WithEvents ButtonRename As System.Windows.Forms.Button
  Friend WithEvents DataGridViewCategories As System.Windows.Forms.DataGridView
  Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents ButtonSelectNone As System.Windows.Forms.Button
  Friend WithEvents ButtonSelectAll As System.Windows.Forms.Button
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
  Friend WithEvents ColumnCheckedCategory As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents ColumnCategoryName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
  Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
End Class

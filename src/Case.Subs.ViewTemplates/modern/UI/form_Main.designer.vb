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
    Me.GroupBoxViewTemplates = New System.Windows.Forms.GroupBox()
    Me.TextBoxViewName = New System.Windows.Forms.TextBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.TreeViewViewTemplates = New System.Windows.Forms.TreeView()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    Me.GroupBoxTemplates = New System.Windows.Forms.GroupBox()
    Me.ButtonExportCSV = New System.Windows.Forms.Button()
    Me.ButtonNone = New System.Windows.Forms.Button()
    Me.ButtonAll = New System.Windows.Forms.Button()
    Me.ButtonCheckedDelete = New System.Windows.Forms.Button()
    Me.DataGridViewViewTemplates = New System.Windows.Forms.DataGridView()
    Me.Column1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.CheckBoxShowUnusedOnly = New System.Windows.Forms.CheckBox()
    Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.GroupBoxViewTemplates.SuspendLayout()
    Me.GroupBoxTemplates.SuspendLayout()
    CType(DataGridViewViewTemplates, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SplitContainer2.Panel1.SuspendLayout()
    Me.SplitContainer2.Panel2.SuspendLayout()
    Me.SplitContainer2.SuspendLayout()
    CType(PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(772, 385)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 0
    Me.ButtonCancel.Text = "Close"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'GroupBoxViewTemplates
    '
    Me.GroupBoxViewTemplates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxViewTemplates.Controls.Add(TextBoxViewName)
    Me.GroupBoxViewTemplates.Controls.Add(Label1)
    Me.GroupBoxViewTemplates.Controls.Add(TreeViewViewTemplates)
    Me.GroupBoxViewTemplates.Location = New System.Drawing.Point(2, 11)
    Me.GroupBoxViewTemplates.Margin = New System.Windows.Forms.Padding(2)
    Me.GroupBoxViewTemplates.Name = "GroupBoxViewTemplates"
    Me.GroupBoxViewTemplates.Padding = New System.Windows.Forms.Padding(2)
    Me.GroupBoxViewTemplates.Size = New System.Drawing.Size(342, 364)
    Me.GroupBoxViewTemplates.TabIndex = 2
    Me.GroupBoxViewTemplates.TabStop = False
    Me.GroupBoxViewTemplates.Text = "Drag Views into Template Names to Assign their Templates"
    '
    'TextBoxViewName
    '
    Me.TextBoxViewName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBoxViewName.Location = New System.Drawing.Point(93, 31)
    Me.TextBoxViewName.Name = "TextBoxViewName"
    Me.TextBoxViewName.Size = New System.Drawing.Size(243, 20)
    Me.TextBoxViewName.TabIndex = 2
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(14, 34)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(64, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "View Name:"
    '
    'TreeViewViewTemplates
    '
    Me.TreeViewViewTemplates.AllowDrop = True
    Me.TreeViewViewTemplates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TreeViewViewTemplates.Location = New System.Drawing.Point(2, 66)
    Me.TreeViewViewTemplates.Margin = New System.Windows.Forms.Padding(2)
    Me.TreeViewViewTemplates.Name = "TreeViewViewTemplates"
    Me.TreeViewViewTemplates.Size = New System.Drawing.Size(338, 296)
    Me.TreeViewViewTemplates.TabIndex = 0
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(188, 385)
    Me.ProgressBar1.Margin = New System.Windows.Forms.Padding(2)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(659, 40)
    Me.ProgressBar1.TabIndex = 3
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(716, 385)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(50, 40)
    Me.ButtonHelp.TabIndex = 4
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'GroupBoxTemplates
    '
    Me.GroupBoxTemplates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxTemplates.Controls.Add(ButtonExportCSV)
    Me.GroupBoxTemplates.Controls.Add(ButtonNone)
    Me.GroupBoxTemplates.Controls.Add(ButtonAll)
    Me.GroupBoxTemplates.Controls.Add(ButtonCheckedDelete)
    Me.GroupBoxTemplates.Controls.Add(DataGridViewViewTemplates)
    Me.GroupBoxTemplates.Controls.Add(CheckBoxShowUnusedOnly)
    Me.GroupBoxTemplates.Location = New System.Drawing.Point(12, 12)
    Me.GroupBoxTemplates.Name = "GroupBoxTemplates"
    Me.GroupBoxTemplates.Size = New System.Drawing.Size(466, 363)
    Me.GroupBoxTemplates.TabIndex = 5
    Me.GroupBoxTemplates.TabStop = False
    Me.GroupBoxTemplates.Text = "View Templates Showing Quantity of Views Assigned to:"
    '
    'ButtonExportCSV
    '
    Me.ButtonExportCSV.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonExportCSV.Location = New System.Drawing.Point(254, 317)
    Me.ButtonExportCSV.Name = "ButtonExportCSV"
    Me.ButtonExportCSV.Size = New System.Drawing.Size(100, 40)
    Me.ButtonExportCSV.TabIndex = 9
    Me.ButtonExportCSV.Text = "Export to CSV"
    Me.ButtonExportCSV.UseVisualStyleBackColor = True
    '
    'ButtonNone
    '
    Me.ButtonNone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonNone.Location = New System.Drawing.Point(107, 317)
    Me.ButtonNone.Name = "ButtonNone"
    Me.ButtonNone.Size = New System.Drawing.Size(95, 40)
    Me.ButtonNone.TabIndex = 4
    Me.ButtonNone.Text = "Check None"
    Me.ButtonNone.UseVisualStyleBackColor = True
    '
    'ButtonAll
    '
    Me.ButtonAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonAll.Location = New System.Drawing.Point(6, 317)
    Me.ButtonAll.Name = "ButtonAll"
    Me.ButtonAll.Size = New System.Drawing.Size(95, 40)
    Me.ButtonAll.TabIndex = 3
    Me.ButtonAll.Text = "Check All"
    Me.ButtonAll.UseVisualStyleBackColor = True
    '
    'ButtonCheckedDelete
    '
    Me.ButtonCheckedDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCheckedDelete.Location = New System.Drawing.Point(360, 317)
    Me.ButtonCheckedDelete.Name = "ButtonCheckedDelete"
    Me.ButtonCheckedDelete.Size = New System.Drawing.Size(100, 40)
    Me.ButtonCheckedDelete.TabIndex = 2
    Me.ButtonCheckedDelete.Text = "Delete Checked"
    Me.ButtonCheckedDelete.UseVisualStyleBackColor = True
    '
    'DataGridViewViewTemplates
    '
    Me.DataGridViewViewTemplates.AllowUserToAddRows = False
    Me.DataGridViewViewTemplates.AllowUserToDeleteRows = False
    Me.DataGridViewViewTemplates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.DataGridViewViewTemplates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewViewTemplates.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4})
    Me.DataGridViewViewTemplates.Location = New System.Drawing.Point(6, 60)
    Me.DataGridViewViewTemplates.Name = "DataGridViewViewTemplates"
    Me.DataGridViewViewTemplates.RowHeadersVisible = False
    Me.DataGridViewViewTemplates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewViewTemplates.Size = New System.Drawing.Size(454, 251)
    Me.DataGridViewViewTemplates.TabIndex = 1
    '
    'Column1
    '
    Me.Column1.DataPropertyName = "isChecked"
    Me.Column1.HeaderText = ""
    Me.Column1.MinimumWidth = 30
    Me.Column1.Name = "Column1"
    Me.Column1.Width = 30
    '
    'Column2
    '
    Me.Column2.DataPropertyName = "ViewTemplateName"
    Me.Column2.HeaderText = "Template Name"
    Me.Column2.MinimumWidth = 100
    Me.Column2.Name = "Column2"
    Me.Column2.ReadOnly = True
    Me.Column2.Width = 245
    '
    'Column3
    '
    Me.Column3.DataPropertyName = "ViewTemplateKind"
    Me.Column3.HeaderText = "Template Kind"
    Me.Column3.MinimumWidth = 50
    Me.Column3.Name = "Column3"
    Me.Column3.ReadOnly = True
    '
    'Column4
    '
    Me.Column4.DataPropertyName = "Assignments"
    Me.Column4.HeaderText = "Assigned"
    Me.Column4.MinimumWidth = 60
    Me.Column4.Name = "Column4"
    Me.Column4.ReadOnly = True
    Me.Column4.Width = 60
    '
    'CheckBoxShowUnusedOnly
    '
    Me.CheckBoxShowUnusedOnly.AutoSize = True
    Me.CheckBoxShowUnusedOnly.Location = New System.Drawing.Point(26, 28)
    Me.CheckBoxShowUnusedOnly.Name = "CheckBoxShowUnusedOnly"
    Me.CheckBoxShowUnusedOnly.Size = New System.Drawing.Size(214, 17)
    Me.CheckBoxShowUnusedOnly.TabIndex = 0
    Me.CheckBoxShowUnusedOnly.Text = "Only Show View Templates NOT in Use"
    Me.CheckBoxShowUnusedOnly.UseVisualStyleBackColor = True
    '
    'SplitContainer2
    '
    Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
    Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
    Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(5)
    Me.SplitContainer2.Name = "SplitContainer2"
    '
    'SplitContainer2.Panel1
    '
    Me.SplitContainer2.Panel1.Controls.Add(GroupBoxTemplates)
    '
    'SplitContainer2.Panel2
    '
    Me.SplitContainer2.Panel2.Controls.Add(GroupBoxViewTemplates)
    Me.SplitContainer2.Size = New System.Drawing.Size(847, 378)
    Me.SplitContainer2.SplitterDistance = 481
    Me.SplitContainer2.SplitterWidth = 8
    Me.SplitContainer2.TabIndex = 7
    '
    'PictureBox1
    '
    Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.PictureBox1.Image = Global.[Case].Subs.ViewTemplates.My.Resources.Resources.case_logo_type_48x184
    Me.PictureBox1.Location = New System.Drawing.Point(11, 385)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(172, 40)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 8
    Me.PictureBox1.TabStop = False
    '
    'form_Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(859, 437)
    Me.Controls.Add(PictureBox1)
    Me.Controls.Add(ButtonCancel)
    Me.Controls.Add(ButtonHelp)
    Me.Controls.Add(ProgressBar1)
    Me.Controls.Add(SplitContainer2)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(875, 475)
    Me.Name = "form_Main"
    Me.Text = "Main"
    Me.GroupBoxViewTemplates.ResumeLayout(False)
    Me.GroupBoxViewTemplates.PerformLayout()
    Me.GroupBoxTemplates.ResumeLayout(False)
    Me.GroupBoxTemplates.PerformLayout()
    CType(DataGridViewViewTemplates, System.ComponentModel.ISupportInitialize).EndInit()
    Me.SplitContainer2.Panel1.ResumeLayout(False)
    Me.SplitContainer2.Panel2.ResumeLayout(False)
    CType(SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.SplitContainer2.ResumeLayout(False)
    CType(PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents GroupBoxViewTemplates As System.Windows.Forms.GroupBox
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents TreeViewViewTemplates As System.Windows.Forms.TreeView
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
  Friend WithEvents GroupBoxTemplates As System.Windows.Forms.GroupBox
  Friend WithEvents ButtonNone As System.Windows.Forms.Button
  Friend WithEvents ButtonAll As System.Windows.Forms.Button
  Friend WithEvents ButtonCheckedDelete As System.Windows.Forms.Button
  Friend WithEvents DataGridViewViewTemplates As System.Windows.Forms.DataGridView
  Friend WithEvents CheckBoxShowUnusedOnly As System.Windows.Forms.CheckBox
  Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
  Friend WithEvents TextBoxViewName As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents ButtonExportCSV As System.Windows.Forms.Button
End Class

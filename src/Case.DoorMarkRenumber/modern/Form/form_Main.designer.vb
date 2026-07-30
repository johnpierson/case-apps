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
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_Main))
    Me.CheckBoxShowChecked = New System.Windows.Forms.CheckBox()
    Me.LabelPhase = New System.Windows.Forms.Label()
    Me.RadioButtonSelected = New System.Windows.Forms.RadioButton()
    Me.LabelInstructions = New System.Windows.Forms.Label()
    Me.ContextMenuDatagridView = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.ZoomToElementsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.RadioButtonCurrentLevel = New System.Windows.Forms.RadioButton()
    Me.GroupBoxScope = New System.Windows.Forms.GroupBox()
    Me.LabelPhaseName = New System.Windows.Forms.Label()
    Me.LabelInstances = New System.Windows.Forms.Label()
    Me.DataGridViewDoors = New System.Windows.Forms.DataGridView()
    Me.ButtonOK = New System.Windows.Forms.Button()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    Me.ContextMenuDatagridView.SuspendLayout()
    Me.GroupBoxScope.SuspendLayout()
    CType(Me.DataGridViewDoors, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'CheckBoxShowChecked
    '
    Me.CheckBoxShowChecked.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.CheckBoxShowChecked.AutoSize = True
    Me.CheckBoxShowChecked.Checked = True
    Me.CheckBoxShowChecked.CheckState = System.Windows.Forms.CheckState.Checked
    Me.CheckBoxShowChecked.Location = New System.Drawing.Point(28, 149)
    Me.CheckBoxShowChecked.Name = "CheckBoxShowChecked"
    Me.CheckBoxShowChecked.Size = New System.Drawing.Size(117, 17)
    Me.CheckBoxShowChecked.TabIndex = 19
    Me.CheckBoxShowChecked.Text = "Filter by 'AllowEdits'"
    Me.CheckBoxShowChecked.UseVisualStyleBackColor = True
    '
    'LabelPhase
    '
    Me.LabelPhase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.LabelPhase.AutoSize = True
    Me.LabelPhase.Location = New System.Drawing.Point(6, 118)
    Me.LabelPhase.Name = "LabelPhase"
    Me.LabelPhase.Size = New System.Drawing.Size(66, 13)
    Me.LabelPhase.TabIndex = 17
    Me.LabelPhase.Text = "View Phase:"
    '
    'RadioButtonSelected
    '
    Me.RadioButtonSelected.AutoSize = True
    Me.RadioButtonSelected.Enabled = False
    Me.RadioButtonSelected.Location = New System.Drawing.Point(28, 77)
    Me.RadioButtonSelected.Name = "RadioButtonSelected"
    Me.RadioButtonSelected.Size = New System.Drawing.Size(148, 17)
    Me.RadioButtonSelected.TabIndex = 0
    Me.RadioButtonSelected.Text = "Doors in Current Selection"
    Me.RadioButtonSelected.UseVisualStyleBackColor = True
    '
    'LabelInstructions
    '
    Me.LabelInstructions.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.LabelInstructions.Location = New System.Drawing.Point(602, 198)
    Me.LabelInstructions.Name = "LabelInstructions"
    Me.LabelInstructions.Size = New System.Drawing.Size(270, 62)
    Me.LabelInstructions.TabIndex = 18
    Me.LabelInstructions.Text = "Right-Clicking in the data viewer displays an option to ""Zoom to Elements""... mul" & _
    "tiple selections will zoom to show all elements in the row selection"
    '
    'ContextMenuDatagridView
    '
    Me.ContextMenuDatagridView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ZoomToElementsToolStripMenuItem})
    Me.ContextMenuDatagridView.Name = "ContextMenuDatagridView"
    Me.ContextMenuDatagridView.Size = New System.Drawing.Size(172, 26)
    '
    'ZoomToElementsToolStripMenuItem
    '
    Me.ZoomToElementsToolStripMenuItem.Name = "ZoomToElementsToolStripMenuItem"
    Me.ZoomToElementsToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
    Me.ZoomToElementsToolStripMenuItem.Text = "Zoom to Elements"
    '
    'RadioButtonCurrentLevel
    '
    Me.RadioButtonCurrentLevel.AutoSize = True
    Me.RadioButtonCurrentLevel.Checked = True
    Me.RadioButtonCurrentLevel.Location = New System.Drawing.Point(28, 54)
    Me.RadioButtonCurrentLevel.Name = "RadioButtonCurrentLevel"
    Me.RadioButtonCurrentLevel.Size = New System.Drawing.Size(123, 17)
    Me.RadioButtonCurrentLevel.TabIndex = 1
    Me.RadioButtonCurrentLevel.TabStop = True
    Me.RadioButtonCurrentLevel.Text = "Doors Visible in View"
    Me.RadioButtonCurrentLevel.UseVisualStyleBackColor = True
    '
    'GroupBoxScope
    '
    Me.GroupBoxScope.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxScope.Controls.Add(Me.LabelPhaseName)
    Me.GroupBoxScope.Controls.Add(Me.RadioButtonCurrentLevel)
    Me.GroupBoxScope.Controls.Add(Me.CheckBoxShowChecked)
    Me.GroupBoxScope.Controls.Add(Me.RadioButtonSelected)
    Me.GroupBoxScope.Controls.Add(Me.LabelPhase)
    Me.GroupBoxScope.Controls.Add(Me.LabelInstances)
    Me.GroupBoxScope.Location = New System.Drawing.Point(601, 12)
    Me.GroupBoxScope.Name = "GroupBoxScope"
    Me.GroupBoxScope.Size = New System.Drawing.Size(271, 183)
    Me.GroupBoxScope.TabIndex = 15
    Me.GroupBoxScope.TabStop = False
    Me.GroupBoxScope.Text = "Renumber Door Mark Scope Filter"
    '
    'LabelPhaseName
    '
    Me.LabelPhaseName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.LabelPhaseName.AutoSize = True
    Me.LabelPhaseName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LabelPhaseName.ForeColor = System.Drawing.Color.RoyalBlue
    Me.LabelPhaseName.Location = New System.Drawing.Point(89, 118)
    Me.LabelPhaseName.Name = "LabelPhaseName"
    Me.LabelPhaseName.Size = New System.Drawing.Size(78, 13)
    Me.LabelPhaseName.TabIndex = 20
    Me.LabelPhaseName.Text = "Phase Name"
    '
    'LabelInstances
    '
    Me.LabelInstances.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.LabelInstances.AutoSize = True
    Me.LabelInstances.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LabelInstances.ForeColor = System.Drawing.Color.RoyalBlue
    Me.LabelInstances.Location = New System.Drawing.Point(25, 25)
    Me.LabelInstances.Name = "LabelInstances"
    Me.LabelInstances.Size = New System.Drawing.Size(66, 13)
    Me.LabelInstances.TabIndex = 14
    Me.LabelInstances.Text = "Instances:"
    '
    'DataGridViewDoors
    '
    Me.DataGridViewDoors.AllowUserToAddRows = False
    Me.DataGridViewDoors.AllowUserToDeleteRows = False
    Me.DataGridViewDoors.AllowUserToOrderColumns = True
    Me.DataGridViewDoors.AllowUserToResizeRows = False
    Me.DataGridViewDoors.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.DataGridViewDoors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewDoors.ContextMenuStrip = Me.ContextMenuDatagridView
    Me.DataGridViewDoors.Cursor = System.Windows.Forms.Cursors.Default
    Me.DataGridViewDoors.Location = New System.Drawing.Point(-1, -1)
    Me.DataGridViewDoors.Name = "DataGridViewDoors"
    Me.DataGridViewDoors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewDoors.Size = New System.Drawing.Size(596, 480)
    Me.DataGridViewDoors.TabIndex = 13
    '
    'ButtonOK
    '
    Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.ButtonOK.Location = New System.Drawing.Point(740, 425)
    Me.ButtonOK.Name = "ButtonOK"
    Me.ButtonOK.Size = New System.Drawing.Size(132, 40)
    Me.ButtonOK.TabIndex = 11
    Me.ButtonOK.Text = "Renumber Doors"
    Me.ButtonOK.UseVisualStyleBackColor = True
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.ButtonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.ButtonCancel.Location = New System.Drawing.Point(657, 425)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(76, 40)
    Me.ButtonCancel.TabIndex = 12
    Me.ButtonCancel.Text = "Close"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(601, 425)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(271, 40)
    Me.ProgressBar1.TabIndex = 19
    '
    'PictureBox1
    '
    Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.PictureBox1.Image = Global.[Case].DoorMarkRenumber.My.Resources.Resources.Case_Clearly
    Me.PictureBox1.Location = New System.Drawing.Point(740, 365)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(132, 54)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 20
    Me.PictureBox1.TabStop = False
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.ButtonHelp.Location = New System.Drawing.Point(601, 425)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(50, 40)
    Me.ButtonHelp.TabIndex = 22
    Me.ButtonHelp.Text = "Help"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'form_Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(884, 477)
    Me.Controls.Add(Me.ButtonHelp)
    Me.Controls.Add(Me.GroupBoxScope)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.DataGridViewDoors)
    Me.Controls.Add(Me.LabelInstructions)
    Me.Controls.Add(Me.ButtonOK)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(900, 515)
    Me.Name = "form_Main"
    Me.Text = "Door Mark Update by Room Number"
    Me.ContextMenuDatagridView.ResumeLayout(False)
    Me.GroupBoxScope.ResumeLayout(False)
    Me.GroupBoxScope.PerformLayout()
    CType(Me.DataGridViewDoors, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents CheckBoxShowChecked As System.Windows.Forms.CheckBox
  Friend WithEvents LabelPhase As System.Windows.Forms.Label
  Friend WithEvents RadioButtonSelected As System.Windows.Forms.RadioButton
  Friend WithEvents LabelInstructions As System.Windows.Forms.Label
  Friend WithEvents ContextMenuDatagridView As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents ZoomToElementsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents RadioButtonCurrentLevel As System.Windows.Forms.RadioButton
  Friend WithEvents GroupBoxScope As System.Windows.Forms.GroupBox
  Friend WithEvents DataGridViewDoors As System.Windows.Forms.DataGridView
  Friend WithEvents LabelInstances As System.Windows.Forms.Label
  Friend WithEvents ButtonOK As System.Windows.Forms.Button
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents LabelPhaseName As System.Windows.Forms.Label
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
End Class

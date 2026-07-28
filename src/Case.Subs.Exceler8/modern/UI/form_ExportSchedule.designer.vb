<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_ExportSchedule
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_ExportSchedule))
    Me.ButtonHelp = New System.Windows.Forms.Button()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.ButtonOk = New System.Windows.Forms.Button()
    Me.GroupBoxExcelOptions = New System.Windows.Forms.GroupBox()
    Me.RadioButtonMulti = New System.Windows.Forms.RadioButton()
    Me.RadioButtonSingle = New System.Windows.Forms.RadioButton()
    Me.LabelFileName = New System.Windows.Forms.Label()
    Me.PictureBoxExcel = New System.Windows.Forms.PictureBox()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.GroupBoxConfiguration = New System.Windows.Forms.GroupBox()
    Me.DataGridViewSchedules = New System.Windows.Forms.DataGridView()
    Me.Column1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
    Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.ButtonCheckNone = New System.Windows.Forms.Button()
    Me.ButtonCheckAll = New System.Windows.Forms.Button()
    Me.SaveFileDialogExcel = New System.Windows.Forms.SaveFileDialog()
    Me.GroupBoxExcelOptions.SuspendLayout()
    CType(Me.PictureBoxExcel, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBoxConfiguration.SuspendLayout()
    CType(Me.DataGridViewSchedules, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(360, 510)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(50, 40)
    Me.ButtonHelp.TabIndex = 13
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(497, 510)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 12
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ButtonOk
    '
    Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonOk.Location = New System.Drawing.Point(416, 510)
    Me.ButtonOk.Name = "ButtonOk"
    Me.ButtonOk.Size = New System.Drawing.Size(75, 40)
    Me.ButtonOk.TabIndex = 11
    Me.ButtonOk.Text = "Export"
    Me.ButtonOk.UseVisualStyleBackColor = True
    '
    'GroupBoxExcelOptions
    '
    Me.GroupBoxExcelOptions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxExcelOptions.Controls.Add(Me.RadioButtonMulti)
    Me.GroupBoxExcelOptions.Controls.Add(Me.RadioButtonSingle)
    Me.GroupBoxExcelOptions.Controls.Add(Me.LabelFileName)
    Me.GroupBoxExcelOptions.Controls.Add(Me.PictureBoxExcel)
    Me.GroupBoxExcelOptions.Location = New System.Drawing.Point(12, 12)
    Me.GroupBoxExcelOptions.Name = "GroupBoxExcelOptions"
    Me.GroupBoxExcelOptions.Size = New System.Drawing.Size(560, 153)
    Me.GroupBoxExcelOptions.TabIndex = 14
    Me.GroupBoxExcelOptions.TabStop = False
    Me.GroupBoxExcelOptions.Text = "Excel Document Configuration"
    '
    'RadioButtonMulti
    '
    Me.RadioButtonMulti.AutoSize = True
    Me.RadioButtonMulti.Location = New System.Drawing.Point(167, 100)
    Me.RadioButtonMulti.Name = "RadioButtonMulti"
    Me.RadioButtonMulti.Size = New System.Drawing.Size(294, 17)
    Me.RadioButtonMulti.TabIndex = 1
    Me.RadioButtonMulti.Text = "Multiple Documents - One Excel Document per Schedule"
    Me.RadioButtonMulti.UseVisualStyleBackColor = True
    '
    'RadioButtonSingle
    '
    Me.RadioButtonSingle.AutoSize = True
    Me.RadioButtonSingle.Checked = True
    Me.RadioButtonSingle.Location = New System.Drawing.Point(167, 68)
    Me.RadioButtonSingle.Name = "RadioButtonSingle"
    Me.RadioButtonSingle.Size = New System.Drawing.Size(224, 17)
    Me.RadioButtonSingle.TabIndex = 0
    Me.RadioButtonSingle.TabStop = True
    Me.RadioButtonSingle.Text = "Single Document - One Schedule Per Tab"
    Me.RadioButtonSingle.UseVisualStyleBackColor = True
    '
    'LabelFileName
    '
    Me.LabelFileName.AutoSize = True
    Me.LabelFileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LabelFileName.Location = New System.Drawing.Point(148, 33)
    Me.LabelFileName.Name = "LabelFileName"
    Me.LabelFileName.Size = New System.Drawing.Size(158, 20)
    Me.LabelFileName.TabIndex = 1
    Me.LabelFileName.Text = "Document Options"
    '
    'PictureBoxExcel
    '
    Me.PictureBoxExcel.Image = Global.[Case].Subs.Exceler8.My.Resources.Resources.icon_excel
    Me.PictureBoxExcel.Location = New System.Drawing.Point(6, 19)
    Me.PictureBoxExcel.Name = "PictureBoxExcel"
    Me.PictureBoxExcel.Size = New System.Drawing.Size(120, 116)
    Me.PictureBoxExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBoxExcel.TabIndex = 0
    Me.PictureBoxExcel.TabStop = False
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(12, 510)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(560, 40)
    Me.ProgressBar1.TabIndex = 16
    '
    'GroupBoxConfiguration
    '
    Me.GroupBoxConfiguration.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxConfiguration.Controls.Add(Me.DataGridViewSchedules)
    Me.GroupBoxConfiguration.Location = New System.Drawing.Point(12, 171)
    Me.GroupBoxConfiguration.Name = "GroupBoxConfiguration"
    Me.GroupBoxConfiguration.Size = New System.Drawing.Size(560, 333)
    Me.GroupBoxConfiguration.TabIndex = 17
    Me.GroupBoxConfiguration.TabStop = False
    Me.GroupBoxConfiguration.Text = "Select Schedules to Export"
    '
    'DataGridViewSchedules
    '
    Me.DataGridViewSchedules.AllowUserToAddRows = False
    Me.DataGridViewSchedules.AllowUserToDeleteRows = False
    Me.DataGridViewSchedules.AllowUserToResizeRows = False
    Me.DataGridViewSchedules.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.DataGridViewSchedules.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
    Me.DataGridViewSchedules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewSchedules.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column3, Me.Column4, Me.Column2})
    Me.DataGridViewSchedules.Location = New System.Drawing.Point(6, 19)
    Me.DataGridViewSchedules.Name = "DataGridViewSchedules"
    Me.DataGridViewSchedules.RowHeadersVisible = False
    Me.DataGridViewSchedules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewSchedules.Size = New System.Drawing.Size(548, 308)
    Me.DataGridViewSchedules.TabIndex = 0
    '
    'Column1
    '
    Me.Column1.DataPropertyName = "isChecked"
    Me.Column1.FillWeight = 30.0!
    Me.Column1.HeaderText = ""
    Me.Column1.Name = "Column1"
    Me.Column1.Width = 30
    '
    'Column3
    '
    Me.Column3.DataPropertyName = "Name"
    Me.Column3.HeaderText = "Schedule Name"
    Me.Column3.MinimumWidth = 200
    Me.Column3.Name = "Column3"
    Me.Column3.Width = 300
    '
    'Column4
    '
    Me.Column4.DataPropertyName = "FieldCount"
    Me.Column4.HeaderText = "Fields"
    Me.Column4.Name = "Column4"
    Me.Column4.Width = 40
    '
    'Column2
    '
    Me.Column2.DataPropertyName = "CategoryName"
    Me.Column2.HeaderText = "Category"
    Me.Column2.MinimumWidth = 100
    Me.Column2.Name = "Column2"
    Me.Column2.Width = 150
    '
    'ButtonCheckNone
    '
    Me.ButtonCheckNone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonCheckNone.Location = New System.Drawing.Point(99, 510)
    Me.ButtonCheckNone.Name = "ButtonCheckNone"
    Me.ButtonCheckNone.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCheckNone.TabIndex = 12
    Me.ButtonCheckNone.Text = "Check None"
    Me.ButtonCheckNone.UseVisualStyleBackColor = True
    '
    'ButtonCheckAll
    '
    Me.ButtonCheckAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonCheckAll.Location = New System.Drawing.Point(18, 510)
    Me.ButtonCheckAll.Name = "ButtonCheckAll"
    Me.ButtonCheckAll.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCheckAll.TabIndex = 11
    Me.ButtonCheckAll.Text = "Check All"
    Me.ButtonCheckAll.UseVisualStyleBackColor = True
    '
    'SaveFileDialogExcel
    '
    Me.SaveFileDialogExcel.DefaultExt = "*.xlsx"
    Me.SaveFileDialogExcel.Filter = "Excel Files | *.xlsx"
    Me.SaveFileDialogExcel.Title = "Save Location for Excel BIM Data"
    '
    'form_ExportSchedule
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(584, 562)
    Me.Controls.Add(Me.ButtonCheckNone)
    Me.Controls.Add(Me.GroupBoxConfiguration)
    Me.Controls.Add(Me.ButtonCheckAll)
    Me.Controls.Add(Me.GroupBoxExcelOptions)
    Me.Controls.Add(Me.ButtonHelp)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.ButtonOk)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(600, 600)
    Me.Name = "form_ExportSchedule"
    Me.Text = "X"
    Me.GroupBoxExcelOptions.ResumeLayout(False)
    Me.GroupBoxExcelOptions.PerformLayout()
    CType(Me.PictureBoxExcel, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBoxConfiguration.ResumeLayout(False)
    CType(Me.DataGridViewSchedules, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents ButtonOk As System.Windows.Forms.Button
  Friend WithEvents GroupBoxExcelOptions As System.Windows.Forms.GroupBox
  Friend WithEvents RadioButtonMulti As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButtonSingle As System.Windows.Forms.RadioButton
  Friend WithEvents LabelFileName As System.Windows.Forms.Label
  Friend WithEvents PictureBoxExcel As System.Windows.Forms.PictureBox
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents GroupBoxConfiguration As System.Windows.Forms.GroupBox
  Friend WithEvents DataGridViewSchedules As System.Windows.Forms.DataGridView
  Friend WithEvents ButtonCheckNone As System.Windows.Forms.Button
  Friend WithEvents ButtonCheckAll As System.Windows.Forms.Button
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents SaveFileDialogExcel As System.Windows.Forms.SaveFileDialog
End Class

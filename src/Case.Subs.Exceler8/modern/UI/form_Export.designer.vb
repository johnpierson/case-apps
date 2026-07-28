<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_Export
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_Export))
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.GroupBoxExcelOptions = New System.Windows.Forms.GroupBox()
    Me.RadioButtonMulti = New System.Windows.Forms.RadioButton()
    Me.RadioButtonSingle = New System.Windows.Forms.RadioButton()
    Me.LabelFileName = New System.Windows.Forms.Label()
    Me.PictureBoxExcel = New System.Windows.Forms.PictureBox()
    Me.ButtonOk = New System.Windows.Forms.Button()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    Me.GroupBoxConfiguration = New System.Windows.Forms.GroupBox()
    Me.LabelFind = New System.Windows.Forms.Label()
    Me.TextBoxFind = New System.Windows.Forms.TextBox()
    Me.RadioButtonType = New System.Windows.Forms.RadioButton()
    Me.RadioButtonInst = New System.Windows.Forms.RadioButton()
    Me.RadioButtonInstTypes = New System.Windows.Forms.RadioButton()
    Me.ButtonCheckNone = New System.Windows.Forms.Button()
    Me.ButtonCheckAll = New System.Windows.Forms.Button()
    Me.LabelParameterHeader = New System.Windows.Forms.Label()
    Me.DataGridViewCategories = New System.Windows.Forms.DataGridView()
    Me.Column1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.SaveFileDialogExcel = New System.Windows.Forms.SaveFileDialog()
    Me.GroupBoxSavedExport = New System.Windows.Forms.GroupBox()
    Me.ButtonRemove = New System.Windows.Forms.Button()
    Me.ButtonSavedNew = New System.Windows.Forms.Button()
    Me.ComboBoxConfigurations = New System.Windows.Forms.ComboBox()
    Me.LabelConfigurations = New System.Windows.Forms.Label()
    Me.GroupBoxNumeric = New System.Windows.Forms.GroupBox()
    Me.RadioButtonNumericElementID = New System.Windows.Forms.RadioButton()
    Me.RadioButtonAsNumeric = New System.Windows.Forms.RadioButton()
    Me.RadioButtonAsText = New System.Windows.Forms.RadioButton()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.GroupBoxExcelOptions.SuspendLayout()
    CType(Me.PictureBoxExcel, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBoxConfiguration.SuspendLayout()
    CType(Me.DataGridViewCategories, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBoxSavedExport.SuspendLayout()
    Me.GroupBoxNumeric.SuspendLayout()
    Me.SuspendLayout()
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(487, 700)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 9
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'GroupBoxExcelOptions
    '
    Me.GroupBoxExcelOptions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxExcelOptions.Controls.Add(Me.RadioButtonMulti)
    Me.GroupBoxExcelOptions.Controls.Add(Me.RadioButtonSingle)
    Me.GroupBoxExcelOptions.Controls.Add(Me.LabelFileName)
    Me.GroupBoxExcelOptions.Controls.Add(Me.PictureBoxExcel)
    Me.GroupBoxExcelOptions.Location = New System.Drawing.Point(12, 131)
    Me.GroupBoxExcelOptions.Name = "GroupBoxExcelOptions"
    Me.GroupBoxExcelOptions.Size = New System.Drawing.Size(560, 153)
    Me.GroupBoxExcelOptions.TabIndex = 2
    Me.GroupBoxExcelOptions.TabStop = False
    Me.GroupBoxExcelOptions.Text = "Excel Document Configuration"
    '
    'RadioButtonMulti
    '
    Me.RadioButtonMulti.AutoSize = True
    Me.RadioButtonMulti.Location = New System.Drawing.Point(167, 100)
    Me.RadioButtonMulti.Name = "RadioButtonMulti"
    Me.RadioButtonMulti.Size = New System.Drawing.Size(291, 17)
    Me.RadioButtonMulti.TabIndex = 1
    Me.RadioButtonMulti.Text = "One Excel Document per Category (Multiple Documents)"
    Me.RadioButtonMulti.UseVisualStyleBackColor = True
    '
    'RadioButtonSingle
    '
    Me.RadioButtonSingle.AutoSize = True
    Me.RadioButtonSingle.Checked = True
    Me.RadioButtonSingle.Location = New System.Drawing.Point(167, 68)
    Me.RadioButtonSingle.Name = "RadioButtonSingle"
    Me.RadioButtonSingle.Size = New System.Drawing.Size(261, 17)
    Me.RadioButtonSingle.TabIndex = 0
    Me.RadioButtonSingle.TabStop = True
    Me.RadioButtonSingle.Text = "One Category Per Tab in Excel (Single Document)"
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
    'ButtonOk
    '
    Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonOk.Location = New System.Drawing.Point(406, 700)
    Me.ButtonOk.Name = "ButtonOk"
    Me.ButtonOk.Size = New System.Drawing.Size(75, 40)
    Me.ButtonOk.TabIndex = 8
    Me.ButtonOk.Text = "Export"
    Me.ButtonOk.UseVisualStyleBackColor = True
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(350, 700)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(50, 40)
    Me.ButtonHelp.TabIndex = 10
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'GroupBoxConfiguration
    '
    Me.GroupBoxConfiguration.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxConfiguration.Controls.Add(Me.LabelFind)
    Me.GroupBoxConfiguration.Controls.Add(Me.TextBoxFind)
    Me.GroupBoxConfiguration.Controls.Add(Me.RadioButtonType)
    Me.GroupBoxConfiguration.Controls.Add(Me.RadioButtonInst)
    Me.GroupBoxConfiguration.Controls.Add(Me.RadioButtonInstTypes)
    Me.GroupBoxConfiguration.Controls.Add(Me.ButtonCheckNone)
    Me.GroupBoxConfiguration.Controls.Add(Me.ButtonCheckAll)
    Me.GroupBoxConfiguration.Controls.Add(Me.LabelParameterHeader)
    Me.GroupBoxConfiguration.Controls.Add(Me.DataGridViewCategories)
    Me.GroupBoxConfiguration.Location = New System.Drawing.Point(12, 371)
    Me.GroupBoxConfiguration.Name = "GroupBoxConfiguration"
    Me.GroupBoxConfiguration.Size = New System.Drawing.Size(560, 323)
    Me.GroupBoxConfiguration.TabIndex = 4
    Me.GroupBoxConfiguration.TabStop = False
    Me.GroupBoxConfiguration.Text = "Choose Categories and Parameter Settings to Export"
    '
    'LabelFind
    '
    Me.LabelFind.AutoSize = True
    Me.LabelFind.Location = New System.Drawing.Point(21, 24)
    Me.LabelFind.Name = "LabelFind"
    Me.LabelFind.Size = New System.Drawing.Size(30, 13)
    Me.LabelFind.TabIndex = 13
    Me.LabelFind.Text = "Find:"
    '
    'TextBoxFind
    '
    Me.TextBoxFind.Location = New System.Drawing.Point(57, 21)
    Me.TextBoxFind.Name = "TextBoxFind"
    Me.TextBoxFind.Size = New System.Drawing.Size(224, 20)
    Me.TextBoxFind.TabIndex = 16
    '
    'RadioButtonType
    '
    Me.RadioButtonType.AutoSize = True
    Me.RadioButtonType.Location = New System.Drawing.Point(318, 135)
    Me.RadioButtonType.Name = "RadioButtonType"
    Me.RadioButtonType.Size = New System.Drawing.Size(181, 17)
    Me.RadioButtonType.TabIndex = 15
    Me.RadioButtonType.Text = "Only Types (Includes ALL Types)"
    Me.RadioButtonType.UseVisualStyleBackColor = True
    '
    'RadioButtonInst
    '
    Me.RadioButtonInst.AutoSize = True
    Me.RadioButtonInst.Location = New System.Drawing.Point(318, 96)
    Me.RadioButtonInst.Name = "RadioButtonInst"
    Me.RadioButtonInst.Size = New System.Drawing.Size(95, 17)
    Me.RadioButtonInst.TabIndex = 14
    Me.RadioButtonInst.Text = "Only Instances"
    Me.RadioButtonInst.UseVisualStyleBackColor = True
    '
    'RadioButtonInstTypes
    '
    Me.RadioButtonInstTypes.AutoSize = True
    Me.RadioButtonInstTypes.Checked = True
    Me.RadioButtonInstTypes.Location = New System.Drawing.Point(318, 57)
    Me.RadioButtonInstTypes.Name = "RadioButtonInstTypes"
    Me.RadioButtonInstTypes.Size = New System.Drawing.Size(231, 17)
    Me.RadioButtonInstTypes.TabIndex = 13
    Me.RadioButtonInstTypes.TabStop = True
    Me.RadioButtonInstTypes.Text = "Type and Instance (Includes Placed Types)"
    Me.RadioButtonInstTypes.UseVisualStyleBackColor = True
    '
    'ButtonCheckNone
    '
    Me.ButtonCheckNone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonCheckNone.Location = New System.Drawing.Point(87, 277)
    Me.ButtonCheckNone.Name = "ButtonCheckNone"
    Me.ButtonCheckNone.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCheckNone.TabIndex = 12
    Me.ButtonCheckNone.Text = "Check None"
    Me.ButtonCheckNone.UseVisualStyleBackColor = True
    '
    'ButtonCheckAll
    '
    Me.ButtonCheckAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonCheckAll.Location = New System.Drawing.Point(6, 277)
    Me.ButtonCheckAll.Name = "ButtonCheckAll"
    Me.ButtonCheckAll.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCheckAll.TabIndex = 11
    Me.ButtonCheckAll.Text = "Check All"
    Me.ButtonCheckAll.UseVisualStyleBackColor = True
    '
    'LabelParameterHeader
    '
    Me.LabelParameterHeader.AutoSize = True
    Me.LabelParameterHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LabelParameterHeader.Location = New System.Drawing.Point(298, 19)
    Me.LabelParameterHeader.Name = "LabelParameterHeader"
    Me.LabelParameterHeader.Size = New System.Drawing.Size(164, 20)
    Me.LabelParameterHeader.TabIndex = 3
    Me.LabelParameterHeader.Text = "Parameter Settings"
    '
    'DataGridViewCategories
    '
    Me.DataGridViewCategories.AllowUserToAddRows = False
    Me.DataGridViewCategories.AllowUserToDeleteRows = False
    Me.DataGridViewCategories.AllowUserToResizeRows = False
    Me.DataGridViewCategories.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.DataGridViewCategories.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
    Me.DataGridViewCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewCategories.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
    Me.DataGridViewCategories.Location = New System.Drawing.Point(6, 47)
    Me.DataGridViewCategories.Name = "DataGridViewCategories"
    Me.DataGridViewCategories.RowHeadersVisible = False
    Me.DataGridViewCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewCategories.Size = New System.Drawing.Size(275, 224)
    Me.DataGridViewCategories.TabIndex = 0
    '
    'Column1
    '
    Me.Column1.DataPropertyName = "isChecked"
    Me.Column1.FillWeight = 30.0!
    Me.Column1.HeaderText = ""
    Me.Column1.Name = "Column1"
    Me.Column1.Width = 30
    '
    'Column2
    '
    Me.Column2.DataPropertyName = "CategoryName"
    Me.Column2.HeaderText = "Category"
    Me.Column2.MinimumWidth = 240
    Me.Column2.Name = "Column2"
    Me.Column2.Width = 240
    '
    'SaveFileDialogExcel
    '
    Me.SaveFileDialogExcel.DefaultExt = "*.xlsx"
    Me.SaveFileDialogExcel.Filter = "Excel Files | *.xlsx"
    Me.SaveFileDialogExcel.Title = "Save Location for Excel BIM Data"
    '
    'GroupBoxSavedExport
    '
    Me.GroupBoxSavedExport.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxSavedExport.Controls.Add(Me.ButtonRemove)
    Me.GroupBoxSavedExport.Controls.Add(Me.ButtonSavedNew)
    Me.GroupBoxSavedExport.Controls.Add(Me.ComboBoxConfigurations)
    Me.GroupBoxSavedExport.Controls.Add(Me.LabelConfigurations)
    Me.GroupBoxSavedExport.Location = New System.Drawing.Point(12, 12)
    Me.GroupBoxSavedExport.Name = "GroupBoxSavedExport"
    Me.GroupBoxSavedExport.Size = New System.Drawing.Size(560, 113)
    Me.GroupBoxSavedExport.TabIndex = 11
    Me.GroupBoxSavedExport.TabStop = False
    Me.GroupBoxSavedExport.Text = "Reusable Export Configurations"
    '
    'ButtonRemove
    '
    Me.ButtonRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonRemove.Location = New System.Drawing.Point(475, 54)
    Me.ButtonRemove.Name = "ButtonRemove"
    Me.ButtonRemove.Size = New System.Drawing.Size(75, 40)
    Me.ButtonRemove.TabIndex = 12
    Me.ButtonRemove.Text = "Remove"
    Me.ButtonRemove.UseVisualStyleBackColor = True
    '
    'ButtonSavedNew
    '
    Me.ButtonSavedNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonSavedNew.Location = New System.Drawing.Point(394, 54)
    Me.ButtonSavedNew.Name = "ButtonSavedNew"
    Me.ButtonSavedNew.Size = New System.Drawing.Size(75, 40)
    Me.ButtonSavedNew.TabIndex = 9
    Me.ButtonSavedNew.Text = "Save As"
    Me.ButtonSavedNew.UseVisualStyleBackColor = True
    '
    'ComboBoxConfigurations
    '
    Me.ComboBoxConfigurations.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ComboBoxConfigurations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxConfigurations.FormattingEnabled = True
    Me.ComboBoxConfigurations.Location = New System.Drawing.Point(189, 27)
    Me.ComboBoxConfigurations.Name = "ComboBoxConfigurations"
    Me.ComboBoxConfigurations.Size = New System.Drawing.Size(361, 21)
    Me.ComboBoxConfigurations.TabIndex = 1
    '
    'LabelConfigurations
    '
    Me.LabelConfigurations.AutoSize = True
    Me.LabelConfigurations.Location = New System.Drawing.Point(21, 30)
    Me.LabelConfigurations.Name = "LabelConfigurations"
    Me.LabelConfigurations.Size = New System.Drawing.Size(141, 13)
    Me.LabelConfigurations.TabIndex = 0
    Me.LabelConfigurations.Text = "Saved Export Configurations"
    '
    'GroupBoxNumeric
    '
    Me.GroupBoxNumeric.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxNumeric.Controls.Add(Me.RadioButtonNumericElementID)
    Me.GroupBoxNumeric.Controls.Add(Me.RadioButtonAsNumeric)
    Me.GroupBoxNumeric.Controls.Add(Me.RadioButtonAsText)
    Me.GroupBoxNumeric.Location = New System.Drawing.Point(12, 290)
    Me.GroupBoxNumeric.Name = "GroupBoxNumeric"
    Me.GroupBoxNumeric.Size = New System.Drawing.Size(560, 75)
    Me.GroupBoxNumeric.TabIndex = 12
    Me.GroupBoxNumeric.TabStop = False
    Me.GroupBoxNumeric.Text = "Parameter Value Export Configuration"
    '
    'RadioButtonNumericElementID
    '
    Me.RadioButtonNumericElementID.AutoSize = True
    Me.RadioButtonNumericElementID.Location = New System.Drawing.Point(338, 31)
    Me.RadioButtonNumericElementID.Name = "RadioButtonNumericElementID"
    Me.RadioButtonNumericElementID.Size = New System.Drawing.Size(207, 17)
    Me.RadioButtonNumericElementID.TabIndex = 4
    Me.RadioButtonNumericElementID.Text = "As Numeric Including ElementID (60.5)"
    Me.RadioButtonNumericElementID.UseVisualStyleBackColor = True
    '
    'RadioButtonAsNumeric
    '
    Me.RadioButtonAsNumeric.AutoSize = True
    Me.RadioButtonAsNumeric.Location = New System.Drawing.Point(212, 31)
    Me.RadioButtonAsNumeric.Name = "RadioButtonAsNumeric"
    Me.RadioButtonAsNumeric.Size = New System.Drawing.Size(109, 17)
    Me.RadioButtonAsNumeric.TabIndex = 2
    Me.RadioButtonAsNumeric.Text = "As Numeric (60.5)"
    Me.RadioButtonAsNumeric.UseVisualStyleBackColor = True
    '
    'RadioButtonAsText
    '
    Me.RadioButtonAsText.AutoSize = True
    Me.RadioButtonAsText.Checked = True
    Me.RadioButtonAsText.Location = New System.Drawing.Point(24, 31)
    Me.RadioButtonAsText.Name = "RadioButtonAsText"
    Me.RadioButtonAsText.Size = New System.Drawing.Size(167, 17)
    Me.RadioButtonAsText.TabIndex = 1
    Me.RadioButtonAsText.TabStop = True
    Me.RadioButtonAsText.Text = "As Text (16' - 0"" / or 2500mm)"
    Me.RadioButtonAsText.UseVisualStyleBackColor = True
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(12, 700)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(560, 40)
    Me.ProgressBar1.TabIndex = 13
    '
    'form_Export
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(584, 752)
    Me.Controls.Add(Me.GroupBoxNumeric)
    Me.Controls.Add(Me.GroupBoxSavedExport)
    Me.Controls.Add(Me.GroupBoxConfiguration)
    Me.Controls.Add(Me.ButtonHelp)
    Me.Controls.Add(Me.GroupBoxExcelOptions)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.ButtonOk)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(600, 790)
    Me.Name = "form_Export"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Main"
    Me.GroupBoxExcelOptions.ResumeLayout(False)
    Me.GroupBoxExcelOptions.PerformLayout()
    CType(Me.PictureBoxExcel, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBoxConfiguration.ResumeLayout(False)
    Me.GroupBoxConfiguration.PerformLayout()
    CType(Me.DataGridViewCategories, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBoxSavedExport.ResumeLayout(False)
    Me.GroupBoxSavedExport.PerformLayout()
    Me.GroupBoxNumeric.ResumeLayout(False)
    Me.GroupBoxNumeric.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents GroupBoxExcelOptions As System.Windows.Forms.GroupBox
  Friend WithEvents PictureBoxExcel As System.Windows.Forms.PictureBox
  Friend WithEvents ButtonOk As System.Windows.Forms.Button
  Friend WithEvents LabelFileName As System.Windows.Forms.Label
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
  Friend WithEvents GroupBoxConfiguration As System.Windows.Forms.GroupBox
  Friend WithEvents DataGridViewCategories As System.Windows.Forms.DataGridView
  Friend WithEvents LabelParameterHeader As System.Windows.Forms.Label
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents RadioButtonMulti As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButtonSingle As System.Windows.Forms.RadioButton
  Friend WithEvents ButtonCheckNone As System.Windows.Forms.Button
  Friend WithEvents ButtonCheckAll As System.Windows.Forms.Button
  Friend WithEvents SaveFileDialogExcel As System.Windows.Forms.SaveFileDialog
  Friend WithEvents GroupBoxSavedExport As System.Windows.Forms.GroupBox
  Friend WithEvents ButtonRemove As System.Windows.Forms.Button
  Friend WithEvents ButtonSavedNew As System.Windows.Forms.Button
  Friend WithEvents ComboBoxConfigurations As System.Windows.Forms.ComboBox
  Friend WithEvents LabelConfigurations As System.Windows.Forms.Label
  Friend WithEvents GroupBoxNumeric As System.Windows.Forms.GroupBox
  Friend WithEvents RadioButtonAsNumeric As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButtonAsText As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButtonType As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButtonInst As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButtonInstTypes As System.Windows.Forms.RadioButton
  Friend WithEvents LabelFind As System.Windows.Forms.Label
  Friend WithEvents TextBoxFind As System.Windows.Forms.TextBox
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents RadioButtonNumericElementID As System.Windows.Forms.RadioButton
End Class

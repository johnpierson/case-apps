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
    Me.GroupBoxOptions = New System.Windows.Forms.GroupBox()
    Me.RadioButtonDependent = New System.Windows.Forms.RadioButton()
    Me.RadioButtonDet = New System.Windows.Forms.RadioButton()
    Me.RadioButtonDup = New System.Windows.Forms.RadioButton()
    Me.GroupBoxViews = New System.Windows.Forms.GroupBox()
    Me.DataGridViewViews = New System.Windows.Forms.DataGridView()
    Me.Column1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.GroupBoxNaming = New System.Windows.Forms.GroupBox()
    Me.RadioButtonSuffix = New System.Windows.Forms.RadioButton()
    Me.RadioButtonPrefix = New System.Windows.Forms.RadioButton()
    Me.TextBoxNameAdd = New System.Windows.Forms.TextBox()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.RadioButtonNameContains = New System.Windows.Forms.RadioButton()
    Me.RadioButtonNameNotContain = New System.Windows.Forms.RadioButton()
    Me.TextBoxName = New System.Windows.Forms.TextBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.ComboBoxKind = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.ComboBoxTypes = New System.Windows.Forms.ComboBox()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    Me.ButtonAll = New System.Windows.Forms.Button()
    Me.ButtonNone = New System.Windows.Forms.Button()
    Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
    Me.LabelCopies = New System.Windows.Forms.Label()
    Me.GroupBoxOptions.SuspendLayout()
    Me.GroupBoxViews.SuspendLayout()
    CType(DataGridViewViews, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBoxNaming.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    CType(NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(457, 660)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 0
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ButtonOk
    '
    Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonOk.Location = New System.Drawing.Point(376, 660)
    Me.ButtonOk.Name = "ButtonOk"
    Me.ButtonOk.Size = New System.Drawing.Size(75, 40)
    Me.ButtonOk.TabIndex = 1
    Me.ButtonOk.Text = "OK"
    Me.ButtonOk.UseVisualStyleBackColor = True
    '
    'GroupBoxOptions
    '
    Me.GroupBoxOptions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxOptions.Controls.Add(RadioButtonDependent)
    Me.GroupBoxOptions.Controls.Add(RadioButtonDet)
    Me.GroupBoxOptions.Controls.Add(RadioButtonDup)
    Me.GroupBoxOptions.Location = New System.Drawing.Point(12, 12)
    Me.GroupBoxOptions.Name = "GroupBoxOptions"
    Me.GroupBoxOptions.Size = New System.Drawing.Size(520, 78)
    Me.GroupBoxOptions.TabIndex = 2
    Me.GroupBoxOptions.TabStop = False
    Me.GroupBoxOptions.Text = "View Duplication Options"
    '
    'RadioButtonDependent
    '
    Me.RadioButtonDependent.AutoSize = True
    Me.RadioButtonDependent.Location = New System.Drawing.Point(361, 33)
    Me.RadioButtonDependent.Name = "RadioButtonDependent"
    Me.RadioButtonDependent.Size = New System.Drawing.Size(93, 17)
    Me.RadioButtonDependent.TabIndex = 2
    Me.RadioButtonDependent.Text = "As Dependent"
    Me.RadioButtonDependent.UseVisualStyleBackColor = True
    '
    'RadioButtonDet
    '
    Me.RadioButtonDet.AutoSize = True
    Me.RadioButtonDet.Location = New System.Drawing.Point(195, 33)
    Me.RadioButtonDet.Name = "RadioButtonDet"
    Me.RadioButtonDet.Size = New System.Drawing.Size(91, 17)
    Me.RadioButtonDet.TabIndex = 1
    Me.RadioButtonDet.Text = "With Detailing"
    Me.RadioButtonDet.UseVisualStyleBackColor = True
    '
    'RadioButtonDup
    '
    Me.RadioButtonDup.AutoSize = True
    Me.RadioButtonDup.Checked = True
    Me.RadioButtonDup.Location = New System.Drawing.Point(50, 33)
    Me.RadioButtonDup.Name = "RadioButtonDup"
    Me.RadioButtonDup.Size = New System.Drawing.Size(70, 17)
    Me.RadioButtonDup.TabIndex = 0
    Me.RadioButtonDup.TabStop = True
    Me.RadioButtonDup.Text = "Duplicate"
    Me.RadioButtonDup.UseVisualStyleBackColor = True
    '
    'GroupBoxViews
    '
    Me.GroupBoxViews.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxViews.Controls.Add(DataGridViewViews)
    Me.GroupBoxViews.Location = New System.Drawing.Point(12, 303)
    Me.GroupBoxViews.Name = "GroupBoxViews"
    Me.GroupBoxViews.Size = New System.Drawing.Size(520, 351)
    Me.GroupBoxViews.TabIndex = 3
    Me.GroupBoxViews.TabStop = False
    Me.GroupBoxViews.Text = "Views to Duplicate"
    '
    'DataGridViewViews
    '
    Me.DataGridViewViews.AllowUserToAddRows = False
    Me.DataGridViewViews.AllowUserToDeleteRows = False
    Me.DataGridViewViews.AllowUserToResizeRows = False
    Me.DataGridViewViews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewViews.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5})
    Me.DataGridViewViews.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DataGridViewViews.Location = New System.Drawing.Point(3, 16)
    Me.DataGridViewViews.Name = "DataGridViewViews"
    Me.DataGridViewViews.RowHeadersVisible = False
    Me.DataGridViewViews.Size = New System.Drawing.Size(514, 332)
    Me.DataGridViewViews.TabIndex = 0
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
    Me.Column2.DataPropertyName = "Name"
    Me.Column2.HeaderText = "View"
    Me.Column2.Name = "Column2"
    Me.Column2.Width = 200
    '
    'Column3
    '
    Me.Column3.DataPropertyName = "ViewType"
    Me.Column3.HeaderText = "Type"
    Me.Column3.Name = "Column3"
    Me.Column3.Width = 175
    '
    'Column4
    '
    Me.Column4.DataPropertyName = "ViewKind"
    Me.Column4.HeaderText = "Kind"
    Me.Column4.Name = "Column4"
    '
    'Column5
    '
    Me.Column5.DataPropertyName = "ViewElement"
    Me.Column5.HeaderText = "X"
    Me.Column5.Name = "Column5"
    Me.Column5.Visible = False
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(12, 660)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(520, 40)
    Me.ProgressBar1.TabIndex = 4
    '
    'GroupBoxNaming
    '
    Me.GroupBoxNaming.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxNaming.Controls.Add(RadioButtonSuffix)
    Me.GroupBoxNaming.Controls.Add(RadioButtonPrefix)
    Me.GroupBoxNaming.Controls.Add(TextBoxNameAdd)
    Me.GroupBoxNaming.Location = New System.Drawing.Point(12, 96)
    Me.GroupBoxNaming.Name = "GroupBoxNaming"
    Me.GroupBoxNaming.Size = New System.Drawing.Size(520, 78)
    Me.GroupBoxNaming.TabIndex = 3
    Me.GroupBoxNaming.TabStop = False
    Me.GroupBoxNaming.Text = "Optional Text to Add to View names"
    '
    'RadioButtonSuffix
    '
    Me.RadioButtonSuffix.AutoSize = True
    Me.RadioButtonSuffix.Location = New System.Drawing.Point(361, 33)
    Me.RadioButtonSuffix.Name = "RadioButtonSuffix"
    Me.RadioButtonSuffix.Size = New System.Drawing.Size(66, 17)
    Me.RadioButtonSuffix.TabIndex = 3
    Me.RadioButtonSuffix.Text = "As Suffix"
    Me.RadioButtonSuffix.UseVisualStyleBackColor = True
    '
    'RadioButtonPrefix
    '
    Me.RadioButtonPrefix.AutoSize = True
    Me.RadioButtonPrefix.Checked = True
    Me.RadioButtonPrefix.Location = New System.Drawing.Point(195, 33)
    Me.RadioButtonPrefix.Name = "RadioButtonPrefix"
    Me.RadioButtonPrefix.Size = New System.Drawing.Size(66, 17)
    Me.RadioButtonPrefix.TabIndex = 2
    Me.RadioButtonPrefix.TabStop = True
    Me.RadioButtonPrefix.Text = "As Prefix"
    Me.RadioButtonPrefix.UseVisualStyleBackColor = True
    '
    'TextBoxNameAdd
    '
    Me.TextBoxNameAdd.Location = New System.Drawing.Point(50, 33)
    Me.TextBoxNameAdd.Name = "TextBoxNameAdd"
    Me.TextBoxNameAdd.Size = New System.Drawing.Size(104, 20)
    Me.TextBoxNameAdd.TabIndex = 0
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(RadioButtonNameContains)
    Me.GroupBox1.Controls.Add(RadioButtonNameNotContain)
    Me.GroupBox1.Controls.Add(TextBoxName)
    Me.GroupBox1.Controls.Add(Label3)
    Me.GroupBox1.Controls.Add(ComboBoxKind)
    Me.GroupBox1.Controls.Add(Label2)
    Me.GroupBox1.Controls.Add(Label1)
    Me.GroupBox1.Controls.Add(ComboBoxTypes)
    Me.GroupBox1.Location = New System.Drawing.Point(12, 180)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(520, 117)
    Me.GroupBox1.TabIndex = 4
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Filtering Options"
    '
    'RadioButtonNameContains
    '
    Me.RadioButtonNameContains.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.RadioButtonNameContains.AutoSize = True
    Me.RadioButtonNameContains.Checked = True
    Me.RadioButtonNameContains.Location = New System.Drawing.Point(259, 72)
    Me.RadioButtonNameContains.Name = "RadioButtonNameContains"
    Me.RadioButtonNameContains.Size = New System.Drawing.Size(66, 17)
    Me.RadioButtonNameContains.TabIndex = 6
    Me.RadioButtonNameContains.TabStop = True
    Me.RadioButtonNameContains.Text = "Contains"
    Me.RadioButtonNameContains.UseVisualStyleBackColor = True
    '
    'RadioButtonNameNotContain
    '
    Me.RadioButtonNameNotContain.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.RadioButtonNameNotContain.AutoSize = True
    Me.RadioButtonNameNotContain.Location = New System.Drawing.Point(361, 72)
    Me.RadioButtonNameNotContain.Name = "RadioButtonNameNotContain"
    Me.RadioButtonNameNotContain.Size = New System.Drawing.Size(109, 17)
    Me.RadioButtonNameNotContain.TabIndex = 5
    Me.RadioButtonNameNotContain.Text = "Does Not Contain"
    Me.RadioButtonNameNotContain.UseVisualStyleBackColor = True
    '
    'TextBoxName
    '
    Me.TextBoxName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBoxName.Location = New System.Drawing.Point(70, 71)
    Me.TextBoxName.Name = "TextBoxName"
    Me.TextBoxName.Size = New System.Drawing.Size(161, 20)
    Me.TextBoxName.TabIndex = 4
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(9, 74)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(38, 13)
    Me.Label3.TabIndex = 4
    Me.Label3.Text = "Name:"
    '
    'ComboBoxKind
    '
    Me.ComboBoxKind.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ComboBoxKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxKind.FormattingEnabled = True
    Me.ComboBoxKind.Location = New System.Drawing.Point(338, 27)
    Me.ComboBoxKind.Name = "ComboBoxKind"
    Me.ComboBoxKind.Size = New System.Drawing.Size(161, 21)
    Me.ComboBoxKind.TabIndex = 3
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(9, 33)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(34, 13)
    Me.Label2.TabIndex = 2
    Me.Label2.Text = "Type:"
    '
    'Label1
    '
    Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(256, 30)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(31, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Kind:"
    '
    'ComboBoxTypes
    '
    Me.ComboBoxTypes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ComboBoxTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxTypes.FormattingEnabled = True
    Me.ComboBoxTypes.Location = New System.Drawing.Point(70, 30)
    Me.ComboBoxTypes.Name = "ComboBoxTypes"
    Me.ComboBoxTypes.Size = New System.Drawing.Size(161, 21)
    Me.ComboBoxTypes.TabIndex = 0
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(318, 660)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(52, 40)
    Me.ButtonHelp.TabIndex = 5
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'ButtonAll
    '
    Me.ButtonAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonAll.Location = New System.Drawing.Point(12, 660)
    Me.ButtonAll.Name = "ButtonAll"
    Me.ButtonAll.Size = New System.Drawing.Size(75, 40)
    Me.ButtonAll.TabIndex = 6
    Me.ButtonAll.Text = "All"
    Me.ButtonAll.UseVisualStyleBackColor = True
    '
    'ButtonNone
    '
    Me.ButtonNone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonNone.Location = New System.Drawing.Point(93, 660)
    Me.ButtonNone.Name = "ButtonNone"
    Me.ButtonNone.Size = New System.Drawing.Size(75, 40)
    Me.ButtonNone.TabIndex = 7
    Me.ButtonNone.Text = "None"
    Me.ButtonNone.UseVisualStyleBackColor = True
    '
    'NumericUpDown1
    '
    Me.NumericUpDown1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.NumericUpDown1.Location = New System.Drawing.Point(232, 672)
    Me.NumericUpDown1.Name = "NumericUpDown1"
    Me.NumericUpDown1.Size = New System.Drawing.Size(66, 20)
    Me.NumericUpDown1.TabIndex = 8
    Me.NumericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'LabelCopies
    '
    Me.LabelCopies.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.LabelCopies.AutoSize = True
    Me.LabelCopies.Location = New System.Drawing.Point(185, 674)
    Me.LabelCopies.Name = "LabelCopies"
    Me.LabelCopies.Size = New System.Drawing.Size(42, 13)
    Me.LabelCopies.TabIndex = 9
    Me.LabelCopies.Text = "Copies:"
    '
    'form_Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(544, 712)
    Me.Controls.Add(LabelCopies)
    Me.Controls.Add(NumericUpDown1)
    Me.Controls.Add(ButtonNone)
    Me.Controls.Add(ButtonAll)
    Me.Controls.Add(ButtonHelp)
    Me.Controls.Add(GroupBox1)
    Me.Controls.Add(GroupBoxNaming)
    Me.Controls.Add(GroupBoxViews)
    Me.Controls.Add(GroupBoxOptions)
    Me.Controls.Add(ButtonOk)
    Me.Controls.Add(ButtonCancel)
    Me.Controls.Add(ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(560, 750)
    Me.Name = "form_Main"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "X"
    Me.GroupBoxOptions.ResumeLayout(False)
    Me.GroupBoxOptions.PerformLayout()
    Me.GroupBoxViews.ResumeLayout(False)
    CType(DataGridViewViews, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBoxNaming.ResumeLayout(False)
    Me.GroupBoxNaming.PerformLayout()
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents ButtonOk As System.Windows.Forms.Button
  Friend WithEvents GroupBoxOptions As System.Windows.Forms.GroupBox
  Friend WithEvents RadioButtonDependent As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButtonDet As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButtonDup As System.Windows.Forms.RadioButton
  Friend WithEvents GroupBoxViews As System.Windows.Forms.GroupBox
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents DataGridViewViews As System.Windows.Forms.DataGridView
  Friend WithEvents GroupBoxNaming As System.Windows.Forms.GroupBox
  Friend WithEvents RadioButtonSuffix As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButtonPrefix As System.Windows.Forms.RadioButton
  Friend WithEvents TextBoxNameAdd As System.Windows.Forms.TextBox
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents ComboBoxTypes As System.Windows.Forms.ComboBox
  Friend WithEvents ComboBoxKind As System.Windows.Forms.ComboBox
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
  Friend WithEvents ButtonAll As System.Windows.Forms.Button
  Friend WithEvents ButtonNone As System.Windows.Forms.Button
  Friend WithEvents RadioButtonNameContains As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButtonNameNotContain As System.Windows.Forms.RadioButton
  Friend WithEvents TextBoxName As System.Windows.Forms.TextBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
  Friend WithEvents LabelCopies As System.Windows.Forms.Label
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class

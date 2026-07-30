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
    Me.GroupBoxWalls = New System.Windows.Forms.GroupBox()
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.LabelParameter = New System.Windows.Forms.Label()
    Me.ComboBoxParameter = New System.Windows.Forms.ComboBox()
    Me.RadioButtonAngle = New System.Windows.Forms.RadioButton()
    Me.RadioButtonText = New System.Windows.Forms.RadioButton()
    Me.GroupBoxWrite = New System.Windows.Forms.GroupBox()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    Me.ButtonOk = New System.Windows.Forms.Button()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.GroupBoxWalls.SuspendLayout()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBoxWrite.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'GroupBoxWalls
    '
    Me.GroupBoxWalls.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxWalls.Controls.Add(Me.DataGridView1)
    Me.GroupBoxWalls.Location = New System.Drawing.Point(12, 12)
    Me.GroupBoxWalls.Name = "GroupBoxWalls"
    Me.GroupBoxWalls.Size = New System.Drawing.Size(516, 388)
    Me.GroupBoxWalls.TabIndex = 2
    Me.GroupBoxWalls.TabStop = False
    Me.GroupBoxWalls.Text = "External Walls Found in Model"
    '
    'DataGridView1
    '
    Me.DataGridView1.AllowUserToAddRows = False
    Me.DataGridView1.AllowUserToDeleteRows = False
    Me.DataGridView1.AllowUserToResizeRows = False
    Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5})
    Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DataGridView1.Location = New System.Drawing.Point(3, 16)
    Me.DataGridView1.Name = "DataGridView1"
    Me.DataGridView1.ReadOnly = True
    Me.DataGridView1.RowHeadersVisible = False
    Me.DataGridView1.Size = New System.Drawing.Size(510, 369)
    Me.DataGridView1.TabIndex = 0
    '
    'Column1
    '
    Me.Column1.HeaderText = "ElementID"
    Me.Column1.Name = "Column1"
    Me.Column1.ReadOnly = True
    '
    'Column2
    '
    Me.Column2.HeaderText = "Type"
    Me.Column2.Name = "Column2"
    Me.Column2.ReadOnly = True
    '
    'Column3
    '
    Me.Column3.HeaderText = "Facing"
    Me.Column3.Name = "Column3"
    Me.Column3.ReadOnly = True
    '
    'Column4
    '
    Me.Column4.HeaderText = "Length"
    Me.Column4.Name = "Column4"
    Me.Column4.ReadOnly = True
    '
    'Column5
    '
    Me.Column5.HeaderText = "Level"
    Me.Column5.Name = "Column5"
    Me.Column5.ReadOnly = True
    '
    'LabelParameter
    '
    Me.LabelParameter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.LabelParameter.AutoSize = True
    Me.LabelParameter.Location = New System.Drawing.Point(534, 22)
    Me.LabelParameter.Name = "LabelParameter"
    Me.LabelParameter.Size = New System.Drawing.Size(120, 13)
    Me.LabelParameter.TabIndex = 3
    Me.LabelParameter.Text = "Write Facing Results to:"
    '
    'ComboBoxParameter
    '
    Me.ComboBoxParameter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ComboBoxParameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxParameter.FormattingEnabled = True
    Me.ComboBoxParameter.Location = New System.Drawing.Point(537, 41)
    Me.ComboBoxParameter.Name = "ComboBoxParameter"
    Me.ComboBoxParameter.Size = New System.Drawing.Size(209, 21)
    Me.ComboBoxParameter.TabIndex = 4
    '
    'RadioButtonAngle
    '
    Me.RadioButtonAngle.AutoSize = True
    Me.RadioButtonAngle.Location = New System.Drawing.Point(39, 30)
    Me.RadioButtonAngle.Name = "RadioButtonAngle"
    Me.RadioButtonAngle.Size = New System.Drawing.Size(94, 17)
    Me.RadioButtonAngle.TabIndex = 5
    Me.RadioButtonAngle.Text = "Write as Angle"
    Me.RadioButtonAngle.UseVisualStyleBackColor = True
    '
    'RadioButtonText
    '
    Me.RadioButtonText.AutoSize = True
    Me.RadioButtonText.Checked = True
    Me.RadioButtonText.Location = New System.Drawing.Point(39, 63)
    Me.RadioButtonText.Name = "RadioButtonText"
    Me.RadioButtonText.Size = New System.Drawing.Size(88, 17)
    Me.RadioButtonText.TabIndex = 6
    Me.RadioButtonText.TabStop = True
    Me.RadioButtonText.Text = "Write as Text"
    Me.RadioButtonText.UseVisualStyleBackColor = True
    '
    'GroupBoxWrite
    '
    Me.GroupBoxWrite.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxWrite.Controls.Add(Me.RadioButtonAngle)
    Me.GroupBoxWrite.Controls.Add(Me.RadioButtonText)
    Me.GroupBoxWrite.Location = New System.Drawing.Point(537, 80)
    Me.GroupBoxWrite.Name = "GroupBoxWrite"
    Me.GroupBoxWrite.Size = New System.Drawing.Size(209, 106)
    Me.GroupBoxWrite.TabIndex = 7
    Me.GroupBoxWrite.TabStop = False
    Me.GroupBoxWrite.Text = "Record Direction as:"
    '
    'PictureBox1
    '
    Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.PictureBox1.Image = Global.[Case].Directionality.My.Resources.Resources.Case_Clearly
    Me.PictureBox1.Location = New System.Drawing.Point(541, 287)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(205, 67)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 9
    Me.PictureBox1.TabStop = False
    '
    'Label1
    '
    Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label1.Location = New System.Drawing.Point(541, 189)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(205, 54)
    Me.Label1.TabIndex = 10
    Me.Label1.Text = "All wall instances of wall types with their 'Function' parameter set to 'External" & _
    "' are recognized by this utility."
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(541, 360)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(206, 40)
    Me.ProgressBar1.TabIndex = 11
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(537, 360)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(60, 40)
    Me.ButtonHelp.TabIndex = 15
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'ButtonOk
    '
    Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonOk.Location = New System.Drawing.Point(620, 360)
    Me.ButtonOk.Name = "ButtonOk"
    Me.ButtonOk.Size = New System.Drawing.Size(60, 40)
    Me.ButtonOk.TabIndex = 14
    Me.ButtonOk.Text = "OK"
    Me.ButtonOk.UseVisualStyleBackColor = True
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(686, 360)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(60, 40)
    Me.ButtonCancel.TabIndex = 13
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'form_Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(759, 412)
    Me.Controls.Add(Me.ButtonHelp)
    Me.Controls.Add(Me.ButtonOk)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.GroupBoxWrite)
    Me.Controls.Add(Me.ComboBoxParameter)
    Me.Controls.Add(Me.LabelParameter)
    Me.Controls.Add(Me.GroupBoxWalls)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(775, 450)
    Me.Name = "form_Main"
    Me.Text = "External Wall Facings"
    Me.GroupBoxWalls.ResumeLayout(False)
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBoxWrite.ResumeLayout(False)
    Me.GroupBoxWrite.PerformLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents GroupBoxWalls As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LabelParameter As System.Windows.Forms.Label
    Friend WithEvents ComboBoxParameter As System.Windows.Forms.ComboBox
    Friend WithEvents RadioButtonAngle As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonText As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBoxWrite As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
  Friend WithEvents ButtonOk As System.Windows.Forms.Button
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
End Class

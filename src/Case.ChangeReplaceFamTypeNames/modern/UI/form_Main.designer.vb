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
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.CheckBoxTypes = New System.Windows.Forms.CheckBox()
    Me.TextBoxReplace = New System.Windows.Forms.TextBox()
    Me.LabelReplace = New System.Windows.Forms.Label()
    Me.TextBoxFind = New System.Windows.Forms.TextBox()
    Me.CheckBoxFamilies = New System.Windows.Forms.CheckBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    Me.GroupBox1.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(362, 175)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 0
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ButtonOk
    '
    Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonOk.Location = New System.Drawing.Point(281, 175)
    Me.ButtonOk.Name = "ButtonOk"
    Me.ButtonOk.Size = New System.Drawing.Size(75, 40)
    Me.ButtonOk.TabIndex = 1
    Me.ButtonOk.Text = "OK"
    Me.ButtonOk.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.CheckBoxTypes)
    Me.GroupBox1.Controls.Add(Me.TextBoxReplace)
    Me.GroupBox1.Controls.Add(Me.LabelReplace)
    Me.GroupBox1.Controls.Add(Me.TextBoxFind)
    Me.GroupBox1.Controls.Add(Me.CheckBoxFamilies)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(425, 157)
    Me.GroupBox1.TabIndex = 2
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Find and Replace (Do Not Use Wildcards)"
    '
    'CheckBoxTypes
    '
    Me.CheckBoxTypes.AutoSize = True
    Me.CheckBoxTypes.Checked = True
    Me.CheckBoxTypes.CheckState = System.Windows.Forms.CheckState.Checked
    Me.CheckBoxTypes.Location = New System.Drawing.Point(269, 113)
    Me.CheckBoxTypes.Name = "CheckBoxTypes"
    Me.CheckBoxTypes.Size = New System.Drawing.Size(126, 17)
    Me.CheckBoxTypes.TabIndex = 5
    Me.CheckBoxTypes.Text = "Change Type Names"
    Me.CheckBoxTypes.UseVisualStyleBackColor = True
    '
    'TextBoxReplace
    '
    Me.TextBoxReplace.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBoxReplace.Location = New System.Drawing.Point(84, 70)
    Me.TextBoxReplace.Name = "TextBoxReplace"
    Me.TextBoxReplace.Size = New System.Drawing.Size(335, 20)
    Me.TextBoxReplace.TabIndex = 4
    '
    'LabelReplace
    '
    Me.LabelReplace.AutoSize = True
    Me.LabelReplace.Location = New System.Drawing.Point(22, 73)
    Me.LabelReplace.Name = "LabelReplace"
    Me.LabelReplace.Size = New System.Drawing.Size(50, 13)
    Me.LabelReplace.TabIndex = 3
    Me.LabelReplace.Text = "Replace:"
    '
    'TextBoxFind
    '
    Me.TextBoxFind.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBoxFind.Location = New System.Drawing.Point(84, 32)
    Me.TextBoxFind.Name = "TextBoxFind"
    Me.TextBoxFind.Size = New System.Drawing.Size(335, 20)
    Me.TextBoxFind.TabIndex = 2
    '
    'CheckBoxFamilies
    '
    Me.CheckBoxFamilies.AutoSize = True
    Me.CheckBoxFamilies.Checked = True
    Me.CheckBoxFamilies.CheckState = System.Windows.Forms.CheckState.Checked
    Me.CheckBoxFamilies.Location = New System.Drawing.Point(84, 113)
    Me.CheckBoxFamilies.Name = "CheckBoxFamilies"
    Me.CheckBoxFamilies.Size = New System.Drawing.Size(131, 17)
    Me.CheckBoxFamilies.TabIndex = 1
    Me.CheckBoxFamilies.Text = "Change Family Names"
    Me.CheckBoxFamilies.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(22, 35)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(30, 13)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Find:"
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(177, 175)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(260, 40)
    Me.ProgressBar1.TabIndex = 6
    '
    'PictureBox1
    '
    Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.PictureBox1.Image = Global.[Case].ChangeReplaceFamTypeNames.My.Resources.Resources.Case_Clearly
    Me.PictureBox1.Location = New System.Drawing.Point(12, 175)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(159, 40)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 6
    Me.PictureBox1.TabStop = False
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(225, 175)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(50, 40)
    Me.ButtonHelp.TabIndex = 8
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'form_Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(449, 227)
    Me.Controls.Add(Me.ButtonHelp)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.ButtonOk)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MaximumSize = New System.Drawing.Size(465, 265)
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(465, 265)
    Me.Name = "form_Main"
    Me.Text = "Change and Replace Family and Type Names"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents ButtonOk As System.Windows.Forms.Button
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents TextBoxFind As System.Windows.Forms.TextBox
  Friend WithEvents CheckBoxFamilies As System.Windows.Forms.CheckBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents TextBoxReplace As System.Windows.Forms.TextBox
  Friend WithEvents LabelReplace As System.Windows.Forms.Label
  Friend WithEvents CheckBoxTypes As System.Windows.Forms.CheckBox
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
End Class

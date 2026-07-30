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
    Me.RadioButtonValues = New System.Windows.Forms.RadioButton()
    Me.RadioButtonText = New System.Windows.Forms.RadioButton()
    Me.ComboBoxParamTarget = New System.Windows.Forms.ComboBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.ComboBoxParamSource = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.ComboBoxCategory = New System.Windows.Forms.ComboBox()
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
    Me.ButtonCancel.Location = New System.Drawing.Point(437, 240)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 0
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ButtonOk
    '
    Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonOk.Location = New System.Drawing.Point(356, 240)
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
    Me.GroupBox1.Controls.Add(Me.RadioButtonValues)
    Me.GroupBox1.Controls.Add(Me.RadioButtonText)
    Me.GroupBox1.Controls.Add(Me.ComboBoxParamTarget)
    Me.GroupBox1.Controls.Add(Me.Label3)
    Me.GroupBox1.Controls.Add(Me.ComboBoxParamSource)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.ComboBoxCategory)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(500, 222)
    Me.GroupBox1.TabIndex = 2
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Migrate Uschedulable Data to Selected Parameter"
    '
    'RadioButtonValues
    '
    Me.RadioButtonValues.AutoSize = True
    Me.RadioButtonValues.Location = New System.Drawing.Point(306, 178)
    Me.RadioButtonValues.Name = "RadioButtonValues"
    Me.RadioButtonValues.Size = New System.Drawing.Size(148, 17)
    Me.RadioButtonValues.TabIndex = 7
    Me.RadioButtonValues.Text = "As Numerical or ID Values"
    Me.RadioButtonValues.UseVisualStyleBackColor = True
    '
    'RadioButtonText
    '
    Me.RadioButtonText.AutoSize = True
    Me.RadioButtonText.Checked = True
    Me.RadioButtonText.Location = New System.Drawing.Point(163, 178)
    Me.RadioButtonText.Name = "RadioButtonText"
    Me.RadioButtonText.Size = New System.Drawing.Size(98, 17)
    Me.RadioButtonText.TabIndex = 6
    Me.RadioButtonText.TabStop = True
    Me.RadioButtonText.Text = "As Human Text"
    Me.RadioButtonText.UseVisualStyleBackColor = True
    '
    'ComboBoxParamTarget
    '
    Me.ComboBoxParamTarget.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ComboBoxParamTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxParamTarget.FormattingEnabled = True
    Me.ComboBoxParamTarget.Location = New System.Drawing.Point(154, 125)
    Me.ComboBoxParamTarget.Name = "ComboBoxParamTarget"
    Me.ComboBoxParamTarget.Size = New System.Drawing.Size(340, 21)
    Me.ComboBoxParamTarget.TabIndex = 5
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(19, 128)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(92, 13)
    Me.Label3.TabIndex = 4
    Me.Label3.Text = "Target Parameter:"
    '
    'ComboBoxParamSource
    '
    Me.ComboBoxParamSource.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ComboBoxParamSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxParamSource.FormattingEnabled = True
    Me.ComboBoxParamSource.Location = New System.Drawing.Point(154, 86)
    Me.ComboBoxParamSource.Name = "ComboBoxParamSource"
    Me.ComboBoxParamSource.Size = New System.Drawing.Size(340, 21)
    Me.ComboBoxParamSource.TabIndex = 3
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(19, 89)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(95, 13)
    Me.Label2.TabIndex = 2
    Me.Label2.Text = "Source Parameter:"
    '
    'ComboBoxCategory
    '
    Me.ComboBoxCategory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ComboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxCategory.FormattingEnabled = True
    Me.ComboBoxCategory.Location = New System.Drawing.Point(154, 32)
    Me.ComboBoxCategory.Name = "ComboBoxCategory"
    Me.ComboBoxCategory.Size = New System.Drawing.Size(340, 21)
    Me.ComboBoxCategory.TabIndex = 1
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(19, 35)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(52, 13)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Category:"
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(186, 240)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(326, 40)
    Me.ProgressBar1.TabIndex = 3
    '
    'PictureBox1
    '
    Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.PictureBox1.Image = Global.[Case].HiddenParameterToParameter.My.Resources.Resources.Case_Clearly
    Me.PictureBox1.Location = New System.Drawing.Point(12, 240)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(168, 40)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 8
    Me.PictureBox1.TabStop = False
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(300, 240)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(50, 40)
    Me.ButtonHelp.TabIndex = 10
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'form_Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(524, 292)
    Me.Controls.Add(Me.ButtonHelp)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.ButtonOk)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MaximumSize = New System.Drawing.Size(540, 330)
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(540, 330)
    Me.Name = "form_Main"
    Me.Text = "Main"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents ButtonOk As System.Windows.Forms.Button
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents ComboBoxParamTarget As System.Windows.Forms.ComboBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents ComboBoxParamSource As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents ComboBoxCategory As System.Windows.Forms.ComboBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents RadioButtonValues As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButtonText As System.Windows.Forms.RadioButton
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
End Class

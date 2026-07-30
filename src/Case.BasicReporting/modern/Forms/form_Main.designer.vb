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
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.CheckBoxFormulas = New System.Windows.Forms.CheckBox()
    Me.CheckBoxHost = New System.Windows.Forms.CheckBox()
    Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(322, 105)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 0
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ButtonOk
    '
    Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonOk.Location = New System.Drawing.Point(241, 105)
    Me.ButtonOk.Name = "ButtonOk"
    Me.ButtonOk.Size = New System.Drawing.Size(75, 40)
    Me.ButtonOk.TabIndex = 1
    Me.ButtonOk.Text = "OK"
    Me.ButtonOk.UseVisualStyleBackColor = True
    '
    'PictureBox1
    '
    Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.PictureBox1.Image = Global.[Case].BasicReporting.My.Resources.Resources.Case_Clearly
    Me.PictureBox1.Location = New System.Drawing.Point(12, 105)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(166, 40)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 8
    Me.PictureBox1.TabStop = False
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(184, 105)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(213, 40)
    Me.ProgressBar1.TabIndex = 9
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.CheckBoxFormulas)
    Me.GroupBox1.Controls.Add(Me.CheckBoxHost)
    Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(385, 87)
    Me.GroupBox1.TabIndex = 10
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Reporting Options"
    '
    'CheckBoxFormulas
    '
    Me.CheckBoxFormulas.AutoSize = True
    Me.CheckBoxFormulas.Location = New System.Drawing.Point(218, 38)
    Me.CheckBoxFormulas.Name = "CheckBoxFormulas"
    Me.CheckBoxFormulas.Size = New System.Drawing.Size(137, 17)
    Me.CheckBoxFormulas.TabIndex = 2
    Me.CheckBoxFormulas.Text = "Family Symbol Formulas"
    Me.CheckBoxFormulas.UseVisualStyleBackColor = True
    '
    'CheckBoxHost
    '
    Me.CheckBoxHost.AutoSize = True
    Me.CheckBoxHost.Location = New System.Drawing.Point(39, 38)
    Me.CheckBoxHost.Name = "CheckBoxHost"
    Me.CheckBoxHost.Size = New System.Drawing.Size(94, 17)
    Me.CheckBoxHost.TabIndex = 0
    Me.CheckBoxHost.Text = "Host Elements"
    Me.CheckBoxHost.UseVisualStyleBackColor = True
    '
    'SaveFileDialog1
    '
    Me.SaveFileDialog1.DefaultExt = "*.csv"
    Me.SaveFileDialog1.Filter = "CSV Files | *.csv"
    Me.SaveFileDialog1.Title = "Select Save Location for your CSV Report"
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(184, 105)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(51, 40)
    Me.ButtonHelp.TabIndex = 12
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'form_Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(409, 157)
    Me.Controls.Add(Me.ButtonHelp)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.ButtonOk)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MaximumSize = New System.Drawing.Size(425, 195)
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(425, 195)
    Me.Name = "form_Main"
    Me.Text = "Main"
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents ButtonOk As System.Windows.Forms.Button
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents CheckBoxFormulas As System.Windows.Forms.CheckBox
  Friend WithEvents CheckBoxHost As System.Windows.Forms.CheckBox
  Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
End Class

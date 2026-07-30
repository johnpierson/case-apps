<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_Progress
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_Progress))
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'PictureBox1
    '
    Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.PictureBox1.Image = Global.[Case].Subs.Exceler8.My.Resources.Resources.case_logo_type_32x122
    Me.PictureBox1.Location = New System.Drawing.Point(12, 13)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(143, 42)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
    Me.PictureBox1.TabIndex = 3
    Me.PictureBox1.TabStop = False
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(161, 13)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(311, 42)
    Me.ProgressBar1.TabIndex = 2
    '
    'form_Progress
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(484, 67)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MaximumSize = New System.Drawing.Size(500, 105)
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(500, 105)
    Me.Name = "form_Progress"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Loading..."
    Me.TopMost = True
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
End Class

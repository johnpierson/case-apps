Namespace UI
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
      Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
      Me.SuspendLayout()
      '
      'ProgressBar1
      '
      Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Fill
      Me.ProgressBar1.Location = New System.Drawing.Point(0, 0)
      Me.ProgressBar1.Name = "ProgressBar1"
      Me.ProgressBar1.Size = New System.Drawing.Size(419, 42)
      Me.ProgressBar1.TabIndex = 0
      '
      'form_Progress
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(419, 42)
      Me.Controls.Add(ProgressBar1)
      Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
      Me.MaximizeBox = False
      Me.MaximumSize = New System.Drawing.Size(435, 80)
      Me.MinimizeBox = False
      Me.MinimumSize = New System.Drawing.Size(435, 80)
      Me.Name = "form_Progress"
      Me.Text = "Doing Stuff"
      Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  End Class
End Namespace
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_Sync
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_Sync))
    Me.ButtonSave = New System.Windows.Forms.Button()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.ComboBoxDocuments = New System.Windows.Forms.ComboBox()
    Me.SuspendLayout()
    '
    'ButtonSave
    '
    Me.ButtonSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonSave.Location = New System.Drawing.Point(416, 70)
    Me.ButtonSave.Name = "ButtonSave"
    Me.ButtonSave.Size = New System.Drawing.Size(75, 40)
    Me.ButtonSave.TabIndex = 13
    Me.ButtonSave.Text = "Sync"
    Me.ButtonSave.UseVisualStyleBackColor = True
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(497, 70)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 12
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(28, 70)
    Me.ProgressBar1.MarqueeAnimationSpeed = 250
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(544, 39)
    Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
    Me.ProgressBar1.TabIndex = 14
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(25, 31)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(172, 13)
    Me.Label1.TabIndex = 19
    Me.Label1.Text = "Linked Model to Sync Rooms from:"
    '
    'ComboBoxDocuments
    '
    Me.ComboBoxDocuments.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ComboBoxDocuments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxDocuments.FormattingEnabled = True
    Me.ComboBoxDocuments.Location = New System.Drawing.Point(216, 28)
    Me.ComboBoxDocuments.Name = "ComboBoxDocuments"
    Me.ComboBoxDocuments.Size = New System.Drawing.Size(356, 21)
    Me.ComboBoxDocuments.TabIndex = 20
    '
    'form_Sync
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(584, 122)
    Me.Controls.Add(Me.ComboBoxDocuments)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.ButtonSave)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MaximumSize = New System.Drawing.Size(600, 160)
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(600, 160)
    Me.Name = "form_Sync"
    Me.Text = "Room Data Synchronization"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ButtonSave As System.Windows.Forms.Button
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents ComboBoxDocuments As System.Windows.Forms.ComboBox
End Class

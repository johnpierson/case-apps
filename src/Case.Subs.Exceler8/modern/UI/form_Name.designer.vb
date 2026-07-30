<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_Name
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_Name))
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.ButtonOk = New System.Windows.Forms.Button()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.TextBoxName = New System.Windows.Forms.TextBox()
    Me.SuspendLayout()
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(247, 60)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 2
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ButtonOk
    '
    Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonOk.Location = New System.Drawing.Point(166, 60)
    Me.ButtonOk.Name = "ButtonOk"
    Me.ButtonOk.Size = New System.Drawing.Size(75, 40)
    Me.ButtonOk.TabIndex = 1
    Me.ButtonOk.Text = "OK"
    Me.ButtonOk.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(23, 24)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(38, 13)
    Me.Label1.TabIndex = 12
    Me.Label1.Text = "Name:"
    '
    'TextBoxName
    '
    Me.TextBoxName.Location = New System.Drawing.Point(83, 21)
    Me.TextBoxName.Name = "TextBoxName"
    Me.TextBoxName.Size = New System.Drawing.Size(239, 20)
    Me.TextBoxName.TabIndex = 0
    '
    'form_Name
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(334, 112)
    Me.Controls.Add(Me.TextBoxName)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.ButtonOk)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MaximumSize = New System.Drawing.Size(350, 150)
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(350, 150)
    Me.Name = "form_Name"
    Me.Text = "Enter a Name"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents ButtonOk As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents TextBoxName As System.Windows.Forms.TextBox
End Class

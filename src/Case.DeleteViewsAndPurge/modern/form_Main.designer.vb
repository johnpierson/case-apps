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
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.CheckBoxSheets = New System.Windows.Forms.CheckBox()
    Me.CheckBoxViews = New System.Windows.Forms.CheckBox()
    Me.CheckBoxLinks = New System.Windows.Forms.CheckBox()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(353, 115)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 0
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ButtonOk
    '
    Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonOk.Location = New System.Drawing.Point(272, 115)
    Me.ButtonOk.Name = "ButtonOk"
    Me.ButtonOk.Size = New System.Drawing.Size(75, 40)
    Me.ButtonOk.TabIndex = 1
    Me.ButtonOk.Text = "OK"
    Me.ButtonOk.UseVisualStyleBackColor = True
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(12, 115)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(416, 40)
    Me.ProgressBar1.TabIndex = 3
    '
    'CheckBoxSheets
    '
    Me.CheckBoxSheets.AutoSize = True
    Me.CheckBoxSheets.Checked = True
    Me.CheckBoxSheets.CheckState = System.Windows.Forms.CheckState.Checked
    Me.CheckBoxSheets.Location = New System.Drawing.Point(273, 12)
    Me.CheckBoxSheets.Name = "CheckBoxSheets"
    Me.CheckBoxSheets.Size = New System.Drawing.Size(93, 17)
    Me.CheckBoxSheets.TabIndex = 4
    Me.CheckBoxSheets.Text = "Delete Sheets"
    Me.CheckBoxSheets.UseVisualStyleBackColor = True
    '
    'CheckBoxViews
    '
    Me.CheckBoxViews.AutoSize = True
    Me.CheckBoxViews.Checked = True
    Me.CheckBoxViews.CheckState = System.Windows.Forms.CheckState.Checked
    Me.CheckBoxViews.Location = New System.Drawing.Point(273, 46)
    Me.CheckBoxViews.Name = "CheckBoxViews"
    Me.CheckBoxViews.Size = New System.Drawing.Size(88, 17)
    Me.CheckBoxViews.TabIndex = 5
    Me.CheckBoxViews.Text = "Delete Views"
    Me.CheckBoxViews.UseVisualStyleBackColor = True
    '
    'CheckBoxLinks
    '
    Me.CheckBoxLinks.AutoSize = True
    Me.CheckBoxLinks.Checked = True
    Me.CheckBoxLinks.CheckState = System.Windows.Forms.CheckState.Checked
    Me.CheckBoxLinks.Location = New System.Drawing.Point(273, 84)
    Me.CheckBoxLinks.Name = "CheckBoxLinks"
    Me.CheckBoxLinks.Size = New System.Drawing.Size(122, 17)
    Me.CheckBoxLinks.TabIndex = 6
    Me.CheckBoxLinks.Text = "Remove Revit Links"
    Me.CheckBoxLinks.UseVisualStyleBackColor = True
    '
    'PictureBox1
    '
    Me.PictureBox1.Image = Global.[Case].DeleteViewsAndPurge.My.Resources.Resources.Case_Clearly
    Me.PictureBox1.Location = New System.Drawing.Point(26, 28)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(208, 61)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 7
    Me.PictureBox1.TabStop = False
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(216, 115)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(50, 40)
    Me.ButtonHelp.TabIndex = 9
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'form_Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(440, 167)
    Me.Controls.Add(Me.ButtonHelp)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.CheckBoxLinks)
    Me.Controls.Add(Me.CheckBoxViews)
    Me.Controls.Add(Me.CheckBoxSheets)
    Me.Controls.Add(Me.ButtonOk)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MaximumSize = New System.Drawing.Size(456, 205)
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(456, 205)
    Me.Name = "form_Main"
    Me.Text = "Delete Sheets, Views and Revit Links"
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents ButtonOk As System.Windows.Forms.Button
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents CheckBoxSheets As System.Windows.Forms.CheckBox
  Friend WithEvents CheckBoxViews As System.Windows.Forms.CheckBox
  Friend WithEvents CheckBoxLinks As System.Windows.Forms.CheckBox
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
End Class

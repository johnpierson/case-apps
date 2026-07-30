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
    Me.GroupBoxViewTemplates = New System.Windows.Forms.GroupBox()
    Me.TreeViewViewTemplates = New System.Windows.Forms.TreeView()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    Me.GroupBoxViewTemplates.SuspendLayout()
    Me.SuspendLayout()
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(395, 403)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 0
    Me.ButtonCancel.Text = "Close"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'GroupBoxViewTemplates
    '
    Me.GroupBoxViewTemplates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxViewTemplates.Controls.Add(Me.TreeViewViewTemplates)
    Me.GroupBoxViewTemplates.Location = New System.Drawing.Point(9, 10)
    Me.GroupBoxViewTemplates.Margin = New System.Windows.Forms.Padding(2)
    Me.GroupBoxViewTemplates.Name = "GroupBoxViewTemplates"
    Me.GroupBoxViewTemplates.Padding = New System.Windows.Forms.Padding(2)
    Me.GroupBoxViewTemplates.Size = New System.Drawing.Size(461, 388)
    Me.GroupBoxViewTemplates.TabIndex = 2
    Me.GroupBoxViewTemplates.TabStop = False
    Me.GroupBoxViewTemplates.Text = "Drag Views into Template Names to Assign their Templates"
    '
    'TreeViewViewTemplates
    '
    Me.TreeViewViewTemplates.AllowDrop = True
    Me.TreeViewViewTemplates.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TreeViewViewTemplates.Location = New System.Drawing.Point(2, 15)
    Me.TreeViewViewTemplates.Margin = New System.Windows.Forms.Padding(2)
    Me.TreeViewViewTemplates.Name = "TreeViewViewTemplates"
    Me.TreeViewViewTemplates.Size = New System.Drawing.Size(457, 371)
    Me.TreeViewViewTemplates.TabIndex = 0
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(9, 403)
    Me.ProgressBar1.Margin = New System.Windows.Forms.Padding(2)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(461, 40)
    Me.ProgressBar1.TabIndex = 3
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(339, 403)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(50, 40)
    Me.ButtonHelp.TabIndex = 4
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'form_Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(484, 462)
    Me.Controls.Add(Me.ButtonHelp)
    Me.Controls.Add(Me.GroupBoxViewTemplates)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(500, 500)
    Me.Name = "form_Main"
    Me.Text = "Main"
    Me.GroupBoxViewTemplates.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents GroupBoxViewTemplates As System.Windows.Forms.GroupBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents TreeViewViewTemplates As System.Windows.Forms.TreeView
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
End Class

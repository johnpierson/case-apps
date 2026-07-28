<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_Families
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_Families))
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.CheckedListBoxCategories = New System.Windows.Forms.CheckedListBox()
    Me.ButtonSelectNone = New System.Windows.Forms.Button()
    Me.ButtonSelectAll = New System.Windows.Forms.Button()
    Me.LabelFileName = New System.Windows.Forms.Label()
    Me.ButtonBrowse = New System.Windows.Forms.Button()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
    Me.LabelExportPath = New System.Windows.Forms.Label()
    Me.LabelExport = New System.Windows.Forms.Label()
    Me.ButtonExport = New System.Windows.Forms.Button()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    Me.GroupBox1.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.CheckedListBoxCategories)
    Me.GroupBox1.Controls.Add(Me.ButtonSelectNone)
    Me.GroupBox1.Controls.Add(Me.ButtonSelectAll)
    Me.GroupBox1.Controls.Add(Me.LabelFileName)
    Me.GroupBox1.Location = New System.Drawing.Point(7, 82)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(423, 352)
    Me.GroupBox1.TabIndex = 40
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Select Categories to Export"
    '
    'CheckedListBoxCategories
    '
    Me.CheckedListBoxCategories.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.CheckedListBoxCategories.FormattingEnabled = True
    Me.CheckedListBoxCategories.Location = New System.Drawing.Point(6, 19)
    Me.CheckedListBoxCategories.Name = "CheckedListBoxCategories"
    Me.CheckedListBoxCategories.Size = New System.Drawing.Size(306, 319)
    Me.CheckedListBoxCategories.TabIndex = 32
    '
    'ButtonSelectNone
    '
    Me.ButtonSelectNone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonSelectNone.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.ButtonSelectNone.Location = New System.Drawing.Point(318, 65)
    Me.ButtonSelectNone.Name = "ButtonSelectNone"
    Me.ButtonSelectNone.Size = New System.Drawing.Size(97, 40)
    Me.ButtonSelectNone.TabIndex = 39
    Me.ButtonSelectNone.Text = "Check None"
    Me.ButtonSelectNone.UseVisualStyleBackColor = True
    '
    'ButtonSelectAll
    '
    Me.ButtonSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonSelectAll.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.ButtonSelectAll.Location = New System.Drawing.Point(318, 19)
    Me.ButtonSelectAll.Name = "ButtonSelectAll"
    Me.ButtonSelectAll.Size = New System.Drawing.Size(97, 40)
    Me.ButtonSelectAll.TabIndex = 38
    Me.ButtonSelectAll.Text = "Check All"
    Me.ButtonSelectAll.UseVisualStyleBackColor = True
    '
    'LabelFileName
    '
    Me.LabelFileName.Location = New System.Drawing.Point(6, 267)
    Me.LabelFileName.Name = "LabelFileName"
    Me.LabelFileName.Size = New System.Drawing.Size(406, 17)
    Me.LabelFileName.TabIndex = 25
    Me.LabelFileName.Text = "FILENAME"
    '
    'ButtonBrowse
    '
    Me.ButtonBrowse.Location = New System.Drawing.Point(7, 4)
    Me.ButtonBrowse.Name = "ButtonBrowse"
    Me.ButtonBrowse.Size = New System.Drawing.Size(25, 23)
    Me.ButtonBrowse.TabIndex = 37
    Me.ButtonBrowse.Text = "..."
    Me.ButtonBrowse.UseVisualStyleBackColor = True
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.ButtonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.ButtonCancel.Location = New System.Drawing.Point(325, 440)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(97, 40)
    Me.ButtonCancel.TabIndex = 35
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'LabelExportPath
    '
    Me.LabelExportPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.LabelExportPath.ForeColor = System.Drawing.Color.Blue
    Me.LabelExportPath.Location = New System.Drawing.Point(7, 36)
    Me.LabelExportPath.Name = "LabelExportPath"
    Me.LabelExportPath.Size = New System.Drawing.Size(404, 43)
    Me.LabelExportPath.TabIndex = 34
    Me.LabelExportPath.Text = "EXPORTPATH"
    Me.LabelExportPath.TextAlign = System.Drawing.ContentAlignment.BottomRight
    '
    'LabelExport
    '
    Me.LabelExport.AutoSize = True
    Me.LabelExport.Location = New System.Drawing.Point(38, 9)
    Me.LabelExport.Name = "LabelExport"
    Me.LabelExport.Size = New System.Drawing.Size(172, 13)
    Me.LabelExport.TabIndex = 33
    Me.LabelExport.Text = "Your RFA Files Will be Exported to:"
    '
    'ButtonExport
    '
    Me.ButtonExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonExport.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.ButtonExport.Location = New System.Drawing.Point(232, 440)
    Me.ButtonExport.Name = "ButtonExport"
    Me.ButtonExport.Size = New System.Drawing.Size(87, 40)
    Me.ButtonExport.TabIndex = 32
    Me.ButtonExport.Text = "Export"
    Me.ButtonExport.UseVisualStyleBackColor = True
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(176, 440)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(246, 40)
    Me.ProgressBar1.TabIndex = 36
    '
    'PictureBox1
    '
    Me.PictureBox1.Image = Global.[Case].Export.Families.My.Resources.Resources.Case_Clearly
    Me.PictureBox1.Location = New System.Drawing.Point(13, 440)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(157, 40)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 40
    Me.PictureBox1.TabStop = False
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.ButtonHelp.Location = New System.Drawing.Point(176, 440)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(50, 40)
    Me.ButtonHelp.TabIndex = 42
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'form_Families
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(434, 492)
    Me.Controls.Add(Me.ButtonHelp)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.ButtonBrowse)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.LabelExportPath)
    Me.Controls.Add(Me.LabelExport)
    Me.Controls.Add(Me.ButtonExport)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(450, 530)
    Me.Name = "form_Families"
    Me.Text = "Export Family RFA Files"
    Me.GroupBox1.ResumeLayout(False)
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckedListBoxCategories As System.Windows.Forms.CheckedListBox
    Friend WithEvents LabelFileName As System.Windows.Forms.Label
    Friend WithEvents ButtonSelectAll As System.Windows.Forms.Button
    Friend WithEvents ButtonSelectNone As System.Windows.Forms.Button
    Friend WithEvents ButtonBrowse As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents LabelExportPath As System.Windows.Forms.Label
    Friend WithEvents LabelExport As System.Windows.Forms.Label
    Friend WithEvents ButtonExport As System.Windows.Forms.Button
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
End Class

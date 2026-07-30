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
    Me.OpenFileDialogImages = New System.Windows.Forms.OpenFileDialog()
    Me.TextBoxWidth = New System.Windows.Forms.TextBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.ButtonSelectImages = New System.Windows.Forms.Button()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.TextBoxHeight = New System.Windows.Forms.TextBox()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    Me.GroupBox1.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'OpenFileDialogImages
    '
    Me.OpenFileDialogImages.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.tif;*.tiff"
    Me.OpenFileDialogImages.Multiselect = True
    Me.OpenFileDialogImages.Title = "Select Image Files to Import into Drafting Views"
    '
    'TextBoxWidth
    '
    Me.TextBoxWidth.Location = New System.Drawing.Point(64, 24)
    Me.TextBoxWidth.Name = "TextBoxWidth"
    Me.TextBoxWidth.Size = New System.Drawing.Size(57, 20)
    Me.TextBoxWidth.TabIndex = 9
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(16, 27)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(35, 13)
    Me.Label1.TabIndex = 10
    Me.Label1.Text = "Width"
    '
    'ButtonSelectImages
    '
    Me.ButtonSelectImages.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonSelectImages.Location = New System.Drawing.Point(181, 90)
    Me.ButtonSelectImages.Name = "ButtonSelectImages"
    Me.ButtonSelectImages.Size = New System.Drawing.Size(178, 40)
    Me.ButtonSelectImages.TabIndex = 11
    Me.ButtonSelectImages.Text = "Select and Import Images"
    Me.ButtonSelectImages.UseVisualStyleBackColor = True
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(365, 90)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(92, 40)
    Me.ButtonCancel.TabIndex = 12
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.TextBoxHeight)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.TextBoxWidth)
    Me.GroupBox1.Location = New System.Drawing.Point(181, 12)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(276, 64)
    Me.GroupBox1.TabIndex = 13
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Optional: Override Image Sizes"
    '
    'Label2
    '
    Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(136, 27)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(38, 13)
    Me.Label2.TabIndex = 12
    Me.Label2.Text = "Height"
    '
    'TextBoxHeight
    '
    Me.TextBoxHeight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBoxHeight.Location = New System.Drawing.Point(184, 24)
    Me.TextBoxHeight.Name = "TextBoxHeight"
    Me.TextBoxHeight.Size = New System.Drawing.Size(57, 20)
    Me.TextBoxHeight.TabIndex = 11
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(12, 90)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(445, 40)
    Me.ProgressBar1.TabIndex = 13
    '
    'PictureBox1
    '
    Me.PictureBox1.Image = Global.[Case].ImageToDraftingView.My.Resources.Resources.Case_Clearly
    Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(163, 64)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 14
    Me.PictureBox1.TabStop = False
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(125, 90)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(50, 40)
    Me.ButtonHelp.TabIndex = 16
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'form_Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(469, 142)
    Me.Controls.Add(Me.ButtonHelp)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.ButtonSelectImages)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MaximumSize = New System.Drawing.Size(485, 180)
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(485, 180)
    Me.Name = "form_Main"
    Me.Text = "AUTO"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
    Friend WithEvents OpenFileDialogImages As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TextBoxWidth As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButtonSelectImages As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxHeight As System.Windows.Forms.TextBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
End Class

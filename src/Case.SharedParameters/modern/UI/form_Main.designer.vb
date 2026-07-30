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
    Me.ComboBoxGroup = New System.Windows.Forms.ComboBox()
    Me.RadioButtonParamType = New System.Windows.Forms.RadioButton()
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.TextBoxFilterCategories = New System.Windows.Forms.TextBox()
    Me.LabelFilterCat = New System.Windows.Forms.Label()
    Me.ButtonCatsNone = New System.Windows.Forms.Button()
    Me.ButtonCatsAll = New System.Windows.Forms.Button()
    Me.TreeViewCategories = New System.Windows.Forms.TreeView()
    Me.RadioButtonParamInst = New System.Windows.Forms.RadioButton()
    Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
    Me.ButtonLoad = New System.Windows.Forms.Button()
    Me.ButtonBrowseShared = New System.Windows.Forms.Button()
    Me.TreeViewParameters = New System.Windows.Forms.TreeView()
    Me.ButtonNone = New System.Windows.Forms.Button()
    Me.RadioButtonByFormat = New System.Windows.Forms.RadioButton()
    Me.ButtonAll = New System.Windows.Forms.Button()
    Me.RadioButtonByGroup = New System.Windows.Forms.RadioButton()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.TextBoxFilterParams = New System.Windows.Forms.TextBox()
    Me.LabelFilterParam = New System.Windows.Forms.Label()
    Me.LabelParameterFilePath = New System.Windows.Forms.Label()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    Me.GroupBox2.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ComboBoxGroup
    '
    Me.ComboBoxGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ComboBoxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxGroup.FormattingEnabled = True
    Me.ComboBoxGroup.Location = New System.Drawing.Point(18, 559)
    Me.ComboBoxGroup.Name = "ComboBoxGroup"
    Me.ComboBoxGroup.Size = New System.Drawing.Size(288, 21)
    Me.ComboBoxGroup.TabIndex = 17
    '
    'RadioButtonParamType
    '
    Me.RadioButtonParamType.AutoSize = True
    Me.RadioButtonParamType.Location = New System.Drawing.Point(175, 25)
    Me.RadioButtonParamType.Name = "RadioButtonParamType"
    Me.RadioButtonParamType.Size = New System.Drawing.Size(90, 17)
    Me.RadioButtonParamType.TabIndex = 15
    Me.RadioButtonParamType.Text = "Add as Types"
    Me.RadioButtonParamType.UseVisualStyleBackColor = True
    '
    'GroupBox2
    '
    Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox2.Controls.Add(Me.TextBoxFilterCategories)
    Me.GroupBox2.Controls.Add(Me.LabelFilterCat)
    Me.GroupBox2.Controls.Add(Me.ButtonCatsNone)
    Me.GroupBox2.Controls.Add(Me.ButtonCatsAll)
    Me.GroupBox2.Controls.Add(Me.RadioButtonParamType)
    Me.GroupBox2.Controls.Add(Me.TreeViewCategories)
    Me.GroupBox2.Controls.Add(Me.RadioButtonParamInst)
    Me.GroupBox2.Location = New System.Drawing.Point(318, 58)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(300, 474)
    Me.GroupBox2.TabIndex = 13
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Categories"
    '
    'TextBoxFilterCategories
    '
    Me.TextBoxFilterCategories.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBoxFilterCategories.Location = New System.Drawing.Point(48, 60)
    Me.TextBoxFilterCategories.Name = "TextBoxFilterCategories"
    Me.TextBoxFilterCategories.Size = New System.Drawing.Size(246, 20)
    Me.TextBoxFilterCategories.TabIndex = 18
    Me.TextBoxFilterCategories.Text = "<subscription only>"
    '
    'LabelFilterCat
    '
    Me.LabelFilterCat.AutoSize = True
    Me.LabelFilterCat.Location = New System.Drawing.Point(10, 63)
    Me.LabelFilterCat.Name = "LabelFilterCat"
    Me.LabelFilterCat.Size = New System.Drawing.Size(32, 13)
    Me.LabelFilterCat.TabIndex = 17
    Me.LabelFilterCat.Text = "Filter:"
    '
    'ButtonCatsNone
    '
    Me.ButtonCatsNone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCatsNone.Location = New System.Drawing.Point(208, 428)
    Me.ButtonCatsNone.Name = "ButtonCatsNone"
    Me.ButtonCatsNone.Size = New System.Drawing.Size(86, 40)
    Me.ButtonCatsNone.TabIndex = 12
    Me.ButtonCatsNone.Text = "Check None"
    Me.ButtonCatsNone.UseVisualStyleBackColor = True
    '
    'ButtonCatsAll
    '
    Me.ButtonCatsAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonCatsAll.Location = New System.Drawing.Point(6, 428)
    Me.ButtonCatsAll.Name = "ButtonCatsAll"
    Me.ButtonCatsAll.Size = New System.Drawing.Size(86, 40)
    Me.ButtonCatsAll.TabIndex = 11
    Me.ButtonCatsAll.Text = "Check All"
    Me.ButtonCatsAll.UseVisualStyleBackColor = True
    '
    'TreeViewCategories
    '
    Me.TreeViewCategories.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TreeViewCategories.CheckBoxes = True
    Me.TreeViewCategories.Location = New System.Drawing.Point(6, 86)
    Me.TreeViewCategories.Name = "TreeViewCategories"
    Me.TreeViewCategories.Size = New System.Drawing.Size(288, 336)
    Me.TreeViewCategories.TabIndex = 3
    '
    'RadioButtonParamInst
    '
    Me.RadioButtonParamInst.AutoSize = True
    Me.RadioButtonParamInst.Checked = True
    Me.RadioButtonParamInst.Location = New System.Drawing.Point(13, 25)
    Me.RadioButtonParamInst.Name = "RadioButtonParamInst"
    Me.RadioButtonParamInst.Size = New System.Drawing.Size(107, 17)
    Me.RadioButtonParamInst.TabIndex = 16
    Me.RadioButtonParamInst.TabStop = True
    Me.RadioButtonParamInst.Text = "Add as Instances"
    Me.RadioButtonParamInst.UseVisualStyleBackColor = True
    '
    'OpenFileDialog1
    '
    Me.OpenFileDialog1.DefaultExt = "*.txt"
    Me.OpenFileDialog1.Filter = "Shared Parameter File | *.txt"
    Me.OpenFileDialog1.Title = "Select a Shared Parameter File"
    '
    'ButtonLoad
    '
    Me.ButtonLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonLoad.Location = New System.Drawing.Point(416, 540)
    Me.ButtonLoad.Name = "ButtonLoad"
    Me.ButtonLoad.Size = New System.Drawing.Size(196, 40)
    Me.ButtonLoad.TabIndex = 14
    Me.ButtonLoad.Text = "Add Checked Parameters"
    Me.ButtonLoad.UseVisualStyleBackColor = True
    '
    'ButtonBrowseShared
    '
    Me.ButtonBrowseShared.Location = New System.Drawing.Point(18, 15)
    Me.ButtonBrowseShared.Name = "ButtonBrowseShared"
    Me.ButtonBrowseShared.Size = New System.Drawing.Size(29, 23)
    Me.ButtonBrowseShared.TabIndex = 10
    Me.ButtonBrowseShared.Text = "..."
    Me.ButtonBrowseShared.UseVisualStyleBackColor = True
    '
    'TreeViewParameters
    '
    Me.TreeViewParameters.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TreeViewParameters.CheckBoxes = True
    Me.TreeViewParameters.Location = New System.Drawing.Point(6, 86)
    Me.TreeViewParameters.Name = "TreeViewParameters"
    Me.TreeViewParameters.Size = New System.Drawing.Size(288, 336)
    Me.TreeViewParameters.TabIndex = 4
    '
    'ButtonNone
    '
    Me.ButtonNone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonNone.Location = New System.Drawing.Point(208, 428)
    Me.ButtonNone.Name = "ButtonNone"
    Me.ButtonNone.Size = New System.Drawing.Size(86, 40)
    Me.ButtonNone.TabIndex = 8
    Me.ButtonNone.Text = "Check None"
    Me.ButtonNone.UseVisualStyleBackColor = True
    '
    'RadioButtonByFormat
    '
    Me.RadioButtonByFormat.AutoSize = True
    Me.RadioButtonByFormat.Location = New System.Drawing.Point(175, 25)
    Me.RadioButtonByFormat.Name = "RadioButtonByFormat"
    Me.RadioButtonByFormat.Size = New System.Drawing.Size(90, 17)
    Me.RadioButtonByFormat.TabIndex = 10
    Me.RadioButtonByFormat.Text = "By Data Type"
    Me.RadioButtonByFormat.UseVisualStyleBackColor = True
    '
    'ButtonAll
    '
    Me.ButtonAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonAll.Location = New System.Drawing.Point(6, 428)
    Me.ButtonAll.Name = "ButtonAll"
    Me.ButtonAll.Size = New System.Drawing.Size(86, 40)
    Me.ButtonAll.TabIndex = 7
    Me.ButtonAll.Text = "Check All"
    Me.ButtonAll.UseVisualStyleBackColor = True
    '
    'RadioButtonByGroup
    '
    Me.RadioButtonByGroup.AutoSize = True
    Me.RadioButtonByGroup.Checked = True
    Me.RadioButtonByGroup.Location = New System.Drawing.Point(13, 25)
    Me.RadioButtonByGroup.Name = "RadioButtonByGroup"
    Me.RadioButtonByGroup.Size = New System.Drawing.Size(126, 17)
    Me.RadioButtonByGroup.TabIndex = 9
    Me.RadioButtonByGroup.TabStop = True
    Me.RadioButtonByGroup.Text = "By Group Association"
    Me.RadioButtonByGroup.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.TextBoxFilterParams)
    Me.GroupBox1.Controls.Add(Me.LabelFilterParam)
    Me.GroupBox1.Controls.Add(Me.ButtonNone)
    Me.GroupBox1.Controls.Add(Me.RadioButtonByFormat)
    Me.GroupBox1.Controls.Add(Me.ButtonAll)
    Me.GroupBox1.Controls.Add(Me.RadioButtonByGroup)
    Me.GroupBox1.Controls.Add(Me.TreeViewParameters)
    Me.GroupBox1.Location = New System.Drawing.Point(12, 58)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(300, 474)
    Me.GroupBox1.TabIndex = 12
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Shared Parameters"
    '
    'TextBoxFilterParams
    '
    Me.TextBoxFilterParams.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBoxFilterParams.Location = New System.Drawing.Point(48, 60)
    Me.TextBoxFilterParams.Name = "TextBoxFilterParams"
    Me.TextBoxFilterParams.Size = New System.Drawing.Size(246, 20)
    Me.TextBoxFilterParams.TabIndex = 12
    Me.TextBoxFilterParams.Text = "<subscription only>"
    '
    'LabelFilterParam
    '
    Me.LabelFilterParam.AutoSize = True
    Me.LabelFilterParam.Location = New System.Drawing.Point(10, 63)
    Me.LabelFilterParam.Name = "LabelFilterParam"
    Me.LabelFilterParam.Size = New System.Drawing.Size(32, 13)
    Me.LabelFilterParam.TabIndex = 11
    Me.LabelFilterParam.Text = "Filter:"
    '
    'LabelParameterFilePath
    '
    Me.LabelParameterFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.LabelParameterFilePath.Location = New System.Drawing.Point(53, 17)
    Me.LabelParameterFilePath.Name = "LabelParameterFilePath"
    Me.LabelParameterFilePath.Size = New System.Drawing.Size(385, 35)
    Me.LabelParameterFilePath.TabIndex = 11
    Me.LabelParameterFilePath.Text = "<Shared Parameter File>"
    '
    'PictureBox1
    '
    Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.PictureBox1.Image = Global.[Case].SharedParameters.My.Resources.Resources.Case_Clearly
    Me.PictureBox1.Location = New System.Drawing.Point(454, 12)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(158, 40)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 13
    Me.PictureBox1.TabStop = False
    '
    'Label2
    '
    Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(17, 540)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(152, 13)
    Me.Label2.TabIndex = 18
    Me.Label2.Text = "Group New Parameters Under:"
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(324, 540)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(86, 40)
    Me.ButtonHelp.TabIndex = 19
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'form_Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(630, 592)
    Me.Controls.Add(Me.ButtonHelp)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.ComboBoxGroup)
    Me.Controls.Add(Me.GroupBox2)
    Me.Controls.Add(Me.ButtonLoad)
    Me.Controls.Add(Me.ButtonBrowseShared)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.LabelParameterFilePath)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(646, 630)
    Me.Name = "form_Main"
    Me.Text = "Super Shared Parameter Loader"
    Me.GroupBox2.ResumeLayout(False)
    Me.GroupBox2.PerformLayout()
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ComboBoxGroup As System.Windows.Forms.ComboBox
  Friend WithEvents RadioButtonParamType As System.Windows.Forms.RadioButton
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
  Friend WithEvents ButtonCatsNone As System.Windows.Forms.Button
  Friend WithEvents ButtonCatsAll As System.Windows.Forms.Button
  Friend WithEvents TreeViewCategories As System.Windows.Forms.TreeView
  Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
  Friend WithEvents RadioButtonParamInst As System.Windows.Forms.RadioButton
  Friend WithEvents ButtonLoad As System.Windows.Forms.Button
  Friend WithEvents ButtonBrowseShared As System.Windows.Forms.Button
  Friend WithEvents TreeViewParameters As System.Windows.Forms.TreeView
  Friend WithEvents ButtonNone As System.Windows.Forms.Button
  Friend WithEvents RadioButtonByFormat As System.Windows.Forms.RadioButton
  Friend WithEvents ButtonAll As System.Windows.Forms.Button
  Friend WithEvents RadioButtonByGroup As System.Windows.Forms.RadioButton
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents LabelParameterFilePath As System.Windows.Forms.Label
  Friend WithEvents TextBoxFilterParams As System.Windows.Forms.TextBox
  Friend WithEvents LabelFilterParam As System.Windows.Forms.Label
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents TextBoxFilterCategories As System.Windows.Forms.TextBox
  Friend WithEvents LabelFilterCat As System.Windows.Forms.Label
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
End Class

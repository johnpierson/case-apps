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
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.ButtonSwapStyles = New System.Windows.Forms.Button()
    Me.ComboBoxStyleFind = New System.Windows.Forms.ComboBox()
    Me.ComboBoxStyleReplace = New System.Windows.Forms.ComboBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.GroupBoxLineStyles = New System.Windows.Forms.GroupBox()
    Me.GroupBoxScope = New System.Windows.Forms.GroupBox()
    Me.RadioButtonScopeSelected = New System.Windows.Forms.RadioButton()
    Me.RadioButtonScopeAll = New System.Windows.Forms.RadioButton()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    Me.GroupBoxLineStyles.SuspendLayout()
    Me.GroupBoxScope.SuspendLayout()
    Me.SuspendLayout()
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(12, 235)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(309, 40)
    Me.ProgressBar1.TabIndex = 26
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.ButtonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.ButtonCancel.Location = New System.Drawing.Point(224, 235)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(97, 40)
    Me.ButtonCancel.TabIndex = 24
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ButtonSwapStyles
    '
    Me.ButtonSwapStyles.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonSwapStyles.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.ButtonSwapStyles.Location = New System.Drawing.Point(120, 235)
    Me.ButtonSwapStyles.Name = "ButtonSwapStyles"
    Me.ButtonSwapStyles.Size = New System.Drawing.Size(97, 40)
    Me.ButtonSwapStyles.TabIndex = 21
    Me.ButtonSwapStyles.Text = "Replace Styles"
    Me.ButtonSwapStyles.UseVisualStyleBackColor = True
    '
    'ComboBoxStyleFind
    '
    Me.ComboBoxStyleFind.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ComboBoxStyleFind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxStyleFind.FormattingEnabled = True
    Me.ComboBoxStyleFind.Location = New System.Drawing.Point(21, 43)
    Me.ComboBoxStyleFind.Name = "ComboBoxStyleFind"
    Me.ComboBoxStyleFind.Size = New System.Drawing.Size(282, 21)
    Me.ComboBoxStyleFind.TabIndex = 27
    '
    'ComboBoxStyleReplace
    '
    Me.ComboBoxStyleReplace.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ComboBoxStyleReplace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxStyleReplace.FormattingEnabled = True
    Me.ComboBoxStyleReplace.Location = New System.Drawing.Point(21, 92)
    Me.ComboBoxStyleReplace.Name = "ComboBoxStyleReplace"
    Me.ComboBoxStyleReplace.Size = New System.Drawing.Size(282, 21)
    Me.ComboBoxStyleReplace.TabIndex = 28
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(18, 27)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(65, 13)
    Me.Label1.TabIndex = 29
    Me.Label1.Text = "Style to Find"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(18, 76)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(85, 13)
    Me.Label2.TabIndex = 30
    Me.Label2.Text = "Style to Replace"
    '
    'GroupBoxLineStyles
    '
    Me.GroupBoxLineStyles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxLineStyles.Controls.Add(Me.ComboBoxStyleFind)
    Me.GroupBoxLineStyles.Controls.Add(Me.ComboBoxStyleReplace)
    Me.GroupBoxLineStyles.Controls.Add(Me.Label1)
    Me.GroupBoxLineStyles.Controls.Add(Me.Label2)
    Me.GroupBoxLineStyles.Location = New System.Drawing.Point(12, 12)
    Me.GroupBoxLineStyles.Name = "GroupBoxLineStyles"
    Me.GroupBoxLineStyles.Size = New System.Drawing.Size(309, 131)
    Me.GroupBoxLineStyles.TabIndex = 31
    Me.GroupBoxLineStyles.TabStop = False
    Me.GroupBoxLineStyles.Text = "Line Styles"
    '
    'GroupBoxScope
    '
    Me.GroupBoxScope.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxScope.Controls.Add(Me.RadioButtonScopeSelected)
    Me.GroupBoxScope.Controls.Add(Me.RadioButtonScopeAll)
    Me.GroupBoxScope.Location = New System.Drawing.Point(12, 149)
    Me.GroupBoxScope.Name = "GroupBoxScope"
    Me.GroupBoxScope.Size = New System.Drawing.Size(309, 76)
    Me.GroupBoxScope.TabIndex = 32
    Me.GroupBoxScope.TabStop = False
    Me.GroupBoxScope.Text = "Operation Scope"
    '
    'RadioButtonScopeSelected
    '
    Me.RadioButtonScopeSelected.AutoSize = True
    Me.RadioButtonScopeSelected.Location = New System.Drawing.Point(157, 33)
    Me.RadioButtonScopeSelected.Name = "RadioButtonScopeSelected"
    Me.RadioButtonScopeSelected.Size = New System.Drawing.Size(130, 17)
    Me.RadioButtonScopeSelected.TabIndex = 1
    Me.RadioButtonScopeSelected.Text = "Selected Objects Only"
    Me.RadioButtonScopeSelected.UseVisualStyleBackColor = True
    '
    'RadioButtonScopeAll
    '
    Me.RadioButtonScopeAll.AutoSize = True
    Me.RadioButtonScopeAll.Checked = True
    Me.RadioButtonScopeAll.Location = New System.Drawing.Point(21, 33)
    Me.RadioButtonScopeAll.Name = "RadioButtonScopeAll"
    Me.RadioButtonScopeAll.Size = New System.Drawing.Size(107, 17)
    Me.RadioButtonScopeAll.TabIndex = 0
    Me.RadioButtonScopeAll.TabStop = True
    Me.RadioButtonScopeAll.Text = "All Lines in Model"
    Me.RadioButtonScopeAll.UseVisualStyleBackColor = True
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.ButtonHelp.Location = New System.Drawing.Point(65, 235)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(50, 40)
    Me.ButtonHelp.TabIndex = 34
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'form_Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(333, 287)
    Me.Controls.Add(Me.ButtonHelp)
    Me.Controls.Add(Me.GroupBoxScope)
    Me.Controls.Add(Me.GroupBoxLineStyles)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.ButtonSwapStyles)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MaximumSize = New System.Drawing.Size(349, 325)
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(349, 325)
    Me.Name = "form_Main"
    Me.Text = "Find and Replace Line Styles"
    Me.GroupBoxLineStyles.ResumeLayout(False)
    Me.GroupBoxLineStyles.PerformLayout()
    Me.GroupBoxScope.ResumeLayout(False)
    Me.GroupBoxScope.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonSwapStyles As System.Windows.Forms.Button
    Friend WithEvents ComboBoxStyleFind As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxStyleReplace As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBoxLineStyles As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBoxScope As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonScopeSelected As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButtonScopeAll As System.Windows.Forms.RadioButton
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
End Class

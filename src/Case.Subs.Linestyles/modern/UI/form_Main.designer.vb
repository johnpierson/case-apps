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
    Me.GroupBoxLineStyles = New System.Windows.Forms.GroupBox()
    Me.DataGridViewViews = New System.Windows.Forms.DataGridView()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column1 = New System.Windows.Forms.DataGridViewComboBoxColumn()
    Me.GroupBoxScope = New System.Windows.Forms.GroupBox()
    Me.RadioButtonScopeSelected = New System.Windows.Forms.RadioButton()
    Me.RadioButtonScopeAll = New System.Windows.Forms.RadioButton()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    Me.GroupBoxLineStyles.SuspendLayout()
    CType(DataGridViewViews, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBoxScope.SuspendLayout()
    Me.SuspendLayout()
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(12, 440)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(490, 40)
    Me.ProgressBar1.TabIndex = 26
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.ButtonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.ButtonCancel.Location = New System.Drawing.Point(427, 440)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 24
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ButtonSwapStyles
    '
    Me.ButtonSwapStyles.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonSwapStyles.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.ButtonSwapStyles.Location = New System.Drawing.Point(346, 440)
    Me.ButtonSwapStyles.Name = "ButtonSwapStyles"
    Me.ButtonSwapStyles.Size = New System.Drawing.Size(75, 40)
    Me.ButtonSwapStyles.TabIndex = 21
    Me.ButtonSwapStyles.Text = "Replace"
    Me.ButtonSwapStyles.UseVisualStyleBackColor = True
    '
    'GroupBoxLineStyles
    '
    Me.GroupBoxLineStyles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxLineStyles.Controls.Add(DataGridViewViews)
    Me.GroupBoxLineStyles.Location = New System.Drawing.Point(12, 94)
    Me.GroupBoxLineStyles.Name = "GroupBoxLineStyles"
    Me.GroupBoxLineStyles.Size = New System.Drawing.Size(490, 340)
    Me.GroupBoxLineStyles.TabIndex = 31
    Me.GroupBoxLineStyles.TabStop = False
    Me.GroupBoxLineStyles.Text = "Line Styles Replacement Settings:"
    '
    'DataGridViewViews
    '
    Me.DataGridViewViews.AllowUserToAddRows = False
    Me.DataGridViewViews.AllowUserToDeleteRows = False
    Me.DataGridViewViews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewViews.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column2, Me.Column1})
    Me.DataGridViewViews.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DataGridViewViews.Location = New System.Drawing.Point(3, 16)
    Me.DataGridViewViews.Name = "DataGridViewViews"
    Me.DataGridViewViews.RowHeadersVisible = False
    Me.DataGridViewViews.Size = New System.Drawing.Size(484, 321)
    Me.DataGridViewViews.TabIndex = 4
    '
    'Column2
    '
    Me.Column2.HeaderText = "Old Style"
    Me.Column2.Name = "Column2"
    Me.Column2.ReadOnly = True
    Me.Column2.Width = 225
    '
    'Column1
    '
    Me.Column1.HeaderText = "New Style"
    Me.Column1.Name = "Column1"
    Me.Column1.Width = 225
    '
    'GroupBoxScope
    '
    Me.GroupBoxScope.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxScope.Controls.Add(RadioButtonScopeSelected)
    Me.GroupBoxScope.Controls.Add(RadioButtonScopeAll)
    Me.GroupBoxScope.Location = New System.Drawing.Point(12, 12)
    Me.GroupBoxScope.Name = "GroupBoxScope"
    Me.GroupBoxScope.Size = New System.Drawing.Size(490, 76)
    Me.GroupBoxScope.TabIndex = 32
    Me.GroupBoxScope.TabStop = False
    Me.GroupBoxScope.Text = "Line Element Scope"
    '
    'RadioButtonScopeSelected
    '
    Me.RadioButtonScopeSelected.AutoSize = True
    Me.RadioButtonScopeSelected.Location = New System.Drawing.Point(239, 33)
    Me.RadioButtonScopeSelected.Name = "RadioButtonScopeSelected"
    Me.RadioButtonScopeSelected.Size = New System.Drawing.Size(119, 17)
    Me.RadioButtonScopeSelected.TabIndex = 1
    Me.RadioButtonScopeSelected.Text = "Selected Lines Only"
    Me.RadioButtonScopeSelected.UseVisualStyleBackColor = True
    '
    'RadioButtonScopeAll
    '
    Me.RadioButtonScopeAll.AutoSize = True
    Me.RadioButtonScopeAll.Checked = True
    Me.RadioButtonScopeAll.Location = New System.Drawing.Point(41, 33)
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
    Me.ButtonHelp.Location = New System.Drawing.Point(265, 440)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(75, 40)
    Me.ButtonHelp.TabIndex = 33
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'form_Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(514, 492)
    Me.Controls.Add(ButtonHelp)
    Me.Controls.Add(GroupBoxScope)
    Me.Controls.Add(GroupBoxLineStyles)
    Me.Controls.Add(ButtonCancel)
    Me.Controls.Add(ButtonSwapStyles)
    Me.Controls.Add(ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(530, 530)
    Me.Name = "form_Main"
    Me.Text = "Find and Replace Line Styles"
    Me.GroupBoxLineStyles.ResumeLayout(False)
    CType(DataGridViewViews, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBoxScope.ResumeLayout(False)
    Me.GroupBoxScope.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonSwapStyles As System.Windows.Forms.Button
  Friend WithEvents GroupBoxLineStyles As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBoxScope As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonScopeSelected As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButtonScopeAll As System.Windows.Forms.RadioButton
  Friend WithEvents DataGridViewViews As System.Windows.Forms.DataGridView
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
End Class

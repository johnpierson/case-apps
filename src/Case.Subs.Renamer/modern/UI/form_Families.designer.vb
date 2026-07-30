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
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.ButtonOk = New System.Windows.Forms.Button()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Button2 = New System.Windows.Forms.Button()
    Me.TextBox2 = New System.Windows.Forms.TextBox()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.TextBox1 = New System.Windows.Forms.TextBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.ButtonSourceBefore = New System.Windows.Forms.Button()
    Me.ComboBoxParameter = New System.Windows.Forms.ComboBox()
    Me.ButtonSourceAfter = New System.Windows.Forms.Button()
    Me.Button1 = New System.Windows.Forms.Button()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.ButtonSourceRemove = New System.Windows.Forms.Button()
    Me.ButtonAddConstant = New System.Windows.Forms.Button()
    Me.TextBoxConstant = New System.Windows.Forms.TextBox()
    Me.DataGridViewConfiguration = New System.Windows.Forms.DataGridView()
    Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.CheckBoxFilterSharedNot = New System.Windows.Forms.CheckBox()
    Me.CheckBoxFilterSharedYes = New System.Windows.Forms.CheckBox()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.ComboBoxFilterHost = New System.Windows.Forms.ComboBox()
    Me.ButtonPreview = New System.Windows.Forms.Button()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.DataGridViewResults = New System.Windows.Forms.DataGridView()
    Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.TextBoxFilterName = New System.Windows.Forms.TextBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.ComboBoxFilterCategory = New System.Windows.Forms.ComboBox()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.GroupBox1.SuspendLayout()
    CType(DataGridViewConfiguration, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox2.SuspendLayout()
    CType(DataGridViewResults, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(1016, 615)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 0
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ButtonOk
    '
    Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonOk.Location = New System.Drawing.Point(935, 615)
    Me.ButtonOk.Name = "ButtonOk"
    Me.ButtonOk.Size = New System.Drawing.Size(75, 40)
    Me.ButtonOk.TabIndex = 1
    Me.ButtonOk.Text = "OK"
    Me.ButtonOk.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Label7)
    Me.GroupBox1.Controls.Add(Button2)
    Me.GroupBox1.Controls.Add(TextBox2)
    Me.GroupBox1.Controls.Add(Label6)
    Me.GroupBox1.Controls.Add(TextBox1)
    Me.GroupBox1.Controls.Add(Label5)
    Me.GroupBox1.Controls.Add(ButtonSourceBefore)
    Me.GroupBox1.Controls.Add(ComboBoxParameter)
    Me.GroupBox1.Controls.Add(ButtonSourceAfter)
    Me.GroupBox1.Controls.Add(Button1)
    Me.GroupBox1.Controls.Add(Label2)
    Me.GroupBox1.Controls.Add(ButtonSourceRemove)
    Me.GroupBox1.Controls.Add(ButtonAddConstant)
    Me.GroupBox1.Controls.Add(TextBoxConstant)
    Me.GroupBox1.Controls.Add(DataGridViewConfiguration)
    Me.GroupBox1.Controls.Add(Label1)
    Me.GroupBox1.Location = New System.Drawing.Point(738, 12)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(353, 597)
    Me.GroupBox1.TabIndex = 2
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Naming Configuration"
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(114, 168)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(16, 13)
    Me.Label7.TabIndex = 16
    Me.Label7.Text = "->"
    '
    'Button2
    '
    Me.Button2.Location = New System.Drawing.Point(235, 163)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(104, 23)
    Me.Button2.TabIndex = 15
    Me.Button2.Text = "Add Replacement"
    Me.Button2.UseVisualStyleBackColor = True
    '
    'TextBox2
    '
    Me.TextBox2.Location = New System.Drawing.Point(138, 165)
    Me.TextBox2.Name = "TextBox2"
    Me.TextBox2.Size = New System.Drawing.Size(91, 20)
    Me.TextBox2.TabIndex = 14
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(135, 146)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(32, 13)
    Me.Label6.TabIndex = 13
    Me.Label6.Text = "With:"
    '
    'TextBox1
    '
    Me.TextBox1.Location = New System.Drawing.Point(15, 165)
    Me.TextBox1.Name = "TextBox1"
    Me.TextBox1.Size = New System.Drawing.Size(91, 20)
    Me.TextBox1.TabIndex = 12
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(12, 146)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(50, 13)
    Me.Label5.TabIndex = 11
    Me.Label5.Text = "Replace:"
    '
    'ButtonSourceBefore
    '
    Me.ButtonSourceBefore.Location = New System.Drawing.Point(15, 320)
    Me.ButtonSourceBefore.Name = "ButtonSourceBefore"
    Me.ButtonSourceBefore.Size = New System.Drawing.Size(104, 23)
    Me.ButtonSourceBefore.TabIndex = 10
    Me.ButtonSourceBefore.Text = "Move Before"
    Me.ButtonSourceBefore.UseVisualStyleBackColor = True
    '
    'ComboBoxParameter
    '
    Me.ComboBoxParameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxParameter.FormattingEnabled = True
    Me.ComboBoxParameter.Location = New System.Drawing.Point(15, 100)
    Me.ComboBoxParameter.Name = "ComboBoxParameter"
    Me.ComboBoxParameter.Size = New System.Drawing.Size(214, 21)
    Me.ComboBoxParameter.TabIndex = 6
    '
    'ButtonSourceAfter
    '
    Me.ButtonSourceAfter.Location = New System.Drawing.Point(125, 320)
    Me.ButtonSourceAfter.Name = "ButtonSourceAfter"
    Me.ButtonSourceAfter.Size = New System.Drawing.Size(104, 23)
    Me.ButtonSourceAfter.TabIndex = 9
    Me.ButtonSourceAfter.Text = "Move After"
    Me.ButtonSourceAfter.UseVisualStyleBackColor = True
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(235, 98)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(104, 23)
    Me.Button1.TabIndex = 5
    Me.Button1.Text = "Add Parameter"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 81)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(264, 13)
    Me.Label2.TabIndex = 3
    Me.Label2.Text = "Type Parameter (Only Affects Types - Not File Names):"
    '
    'ButtonSourceRemove
    '
    Me.ButtonSourceRemove.Location = New System.Drawing.Point(235, 320)
    Me.ButtonSourceRemove.Name = "ButtonSourceRemove"
    Me.ButtonSourceRemove.Size = New System.Drawing.Size(104, 23)
    Me.ButtonSourceRemove.TabIndex = 8
    Me.ButtonSourceRemove.Text = "Remove"
    Me.ButtonSourceRemove.UseVisualStyleBackColor = True
    '
    'ButtonAddConstant
    '
    Me.ButtonAddConstant.Location = New System.Drawing.Point(235, 42)
    Me.ButtonAddConstant.Name = "ButtonAddConstant"
    Me.ButtonAddConstant.Size = New System.Drawing.Size(104, 23)
    Me.ButtonAddConstant.TabIndex = 2
    Me.ButtonAddConstant.Text = "Add Constant"
    Me.ButtonAddConstant.UseVisualStyleBackColor = True
    '
    'TextBoxConstant
    '
    Me.TextBoxConstant.Location = New System.Drawing.Point(15, 44)
    Me.TextBoxConstant.Name = "TextBoxConstant"
    Me.TextBoxConstant.Size = New System.Drawing.Size(214, 20)
    Me.TextBoxConstant.TabIndex = 1
    '
    'DataGridViewConfiguration
    '
    Me.DataGridViewConfiguration.AllowUserToAddRows = False
    Me.DataGridViewConfiguration.AllowUserToDeleteRows = False
    Me.DataGridViewConfiguration.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.DataGridViewConfiguration.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewConfiguration.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2, Me.Column7})
    Me.DataGridViewConfiguration.Location = New System.Drawing.Point(15, 349)
    Me.DataGridViewConfiguration.Name = "DataGridViewConfiguration"
    Me.DataGridViewConfiguration.ReadOnly = True
    Me.DataGridViewConfiguration.RowHeadersVisible = False
    Me.DataGridViewConfiguration.Size = New System.Drawing.Size(324, 242)
    Me.DataGridViewConfiguration.TabIndex = 7
    '
    'DataGridViewTextBoxColumn2
    '
    Me.DataGridViewTextBoxColumn2.HeaderText = "Type"
    Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
    Me.DataGridViewTextBoxColumn2.ReadOnly = True
    '
    'Column7
    '
    Me.Column7.HeaderText = "Source"
    Me.Column7.Name = "Column7"
    Me.Column7.ReadOnly = True
    Me.Column7.Width = 200
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 25)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(52, 13)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Constant:"
    '
    'GroupBox2
    '
    Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox2.Controls.Add(CheckBoxFilterSharedNot)
    Me.GroupBox2.Controls.Add(CheckBoxFilterSharedYes)
    Me.GroupBox2.Controls.Add(Label8)
    Me.GroupBox2.Controls.Add(ComboBoxFilterHost)
    Me.GroupBox2.Controls.Add(ButtonPreview)
    Me.GroupBox2.Controls.Add(Label4)
    Me.GroupBox2.Controls.Add(DataGridViewResults)
    Me.GroupBox2.Controls.Add(TextBoxFilterName)
    Me.GroupBox2.Controls.Add(Label3)
    Me.GroupBox2.Controls.Add(ComboBoxFilterCategory)
    Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(720, 597)
    Me.GroupBox2.TabIndex = 3
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Object Names"
    '
    'CheckBoxFilterSharedNot
    '
    Me.CheckBoxFilterSharedNot.AutoSize = True
    Me.CheckBoxFilterSharedNot.Checked = True
    Me.CheckBoxFilterSharedNot.CheckState = System.Windows.Forms.CheckState.Checked
    Me.CheckBoxFilterSharedNot.Location = New System.Drawing.Point(372, 102)
    Me.CheckBoxFilterSharedNot.Name = "CheckBoxFilterSharedNot"
    Me.CheckBoxFilterSharedNot.Size = New System.Drawing.Size(80, 17)
    Me.CheckBoxFilterSharedNot.TabIndex = 14
    Me.CheckBoxFilterSharedNot.Text = "Not Shared"
    Me.CheckBoxFilterSharedNot.UseVisualStyleBackColor = True
    '
    'CheckBoxFilterSharedYes
    '
    Me.CheckBoxFilterSharedYes.AutoSize = True
    Me.CheckBoxFilterSharedYes.Checked = True
    Me.CheckBoxFilterSharedYes.CheckState = System.Windows.Forms.CheckState.Checked
    Me.CheckBoxFilterSharedYes.Location = New System.Drawing.Point(263, 102)
    Me.CheckBoxFilterSharedYes.Name = "CheckBoxFilterSharedYes"
    Me.CheckBoxFilterSharedYes.Size = New System.Drawing.Size(60, 17)
    Me.CheckBoxFilterSharedYes.TabIndex = 13
    Me.CheckBoxFilterSharedYes.Text = "Shared"
    Me.CheckBoxFilterSharedYes.UseVisualStyleBackColor = True
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(21, 81)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(59, 13)
    Me.Label8.TabIndex = 11
    Me.Label8.Text = "Host Type:"
    '
    'ComboBoxFilterHost
    '
    Me.ComboBoxFilterHost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxFilterHost.FormattingEnabled = True
    Me.ComboBoxFilterHost.Location = New System.Drawing.Point(24, 100)
    Me.ComboBoxFilterHost.Name = "ComboBoxFilterHost"
    Me.ComboBoxFilterHost.Size = New System.Drawing.Size(214, 21)
    Me.ComboBoxFilterHost.TabIndex = 12
    '
    'ButtonPreview
    '
    Me.ButtonPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonPreview.Location = New System.Drawing.Point(483, 33)
    Me.ButtonPreview.Name = "ButtonPreview"
    Me.ButtonPreview.Size = New System.Drawing.Size(231, 40)
    Me.ButtonPreview.TabIndex = 5
    Me.ButtonPreview.Text = "Preview Results"
    Me.ButtonPreview.UseVisualStyleBackColor = True
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(260, 25)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(102, 13)
    Me.Label4.TabIndex = 10
    Me.Label4.Text = "Existing Name Filter:"
    '
    'DataGridViewResults
    '
    Me.DataGridViewResults.AllowUserToAddRows = False
    Me.DataGridViewResults.AllowUserToDeleteRows = False
    Me.DataGridViewResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.DataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewResults.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6})
    Me.DataGridViewResults.Location = New System.Drawing.Point(6, 146)
    Me.DataGridViewResults.Name = "DataGridViewResults"
    Me.DataGridViewResults.RowHeadersVisible = False
    Me.DataGridViewResults.Size = New System.Drawing.Size(708, 445)
    Me.DataGridViewResults.TabIndex = 0
    '
    'Column1
    '
    Me.Column1.HeaderText = "eid"
    Me.Column1.Name = "Column1"
    Me.Column1.Visible = False
    '
    'Column2
    '
    Me.Column2.HeaderText = "Category"
    Me.Column2.Name = "Column2"
    '
    'Column3
    '
    Me.Column3.HeaderText = "Family"
    Me.Column3.Name = "Column3"
    Me.Column3.Width = 150
    '
    'Column4
    '
    Me.Column4.HeaderText = "Type"
    Me.Column4.Name = "Column4"
    Me.Column4.Width = 150
    '
    'Column5
    '
    Me.Column5.HeaderText = "New Family Name"
    Me.Column5.Name = "Column5"
    Me.Column5.Width = 150
    '
    'Column6
    '
    Me.Column6.HeaderText = "New Type Name"
    Me.Column6.Name = "Column6"
    Me.Column6.Width = 150
    '
    'TextBoxFilterName
    '
    Me.TextBoxFilterName.Location = New System.Drawing.Point(263, 44)
    Me.TextBoxFilterName.Name = "TextBoxFilterName"
    Me.TextBoxFilterName.Size = New System.Drawing.Size(214, 20)
    Me.TextBoxFilterName.TabIndex = 9
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(21, 25)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(77, 13)
    Me.Label3.TabIndex = 7
    Me.Label3.Text = "Category Filter:"
    '
    'ComboBoxFilterCategory
    '
    Me.ComboBoxFilterCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxFilterCategory.FormattingEnabled = True
    Me.ComboBoxFilterCategory.Location = New System.Drawing.Point(24, 44)
    Me.ComboBoxFilterCategory.Name = "ComboBoxFilterCategory"
    Me.ComboBoxFilterCategory.Size = New System.Drawing.Size(214, 21)
    Me.ComboBoxFilterCategory.TabIndex = 8
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(12, 615)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(1079, 40)
    Me.ProgressBar1.TabIndex = 4
    '
    'form_Families
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1103, 667)
    Me.Controls.Add(GroupBox2)
    Me.Controls.Add(GroupBox1)
    Me.Controls.Add(ButtonOk)
    Me.Controls.Add(ButtonCancel)
    Me.Controls.Add(ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "form_Families"
    Me.Text = "Rename Families"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(DataGridViewConfiguration, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox2.ResumeLayout(False)
    Me.GroupBox2.PerformLayout()
    CType(DataGridViewResults, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents ButtonOk As System.Windows.Forms.Button
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
  Friend WithEvents DataGridViewResults As System.Windows.Forms.DataGridView
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents DataGridViewConfiguration As System.Windows.Forms.DataGridView
  Friend WithEvents ComboBoxParameter As System.Windows.Forms.ComboBox
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents ButtonAddConstant As System.Windows.Forms.Button
  Friend WithEvents TextBoxConstant As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents ButtonPreview As System.Windows.Forms.Button
  Friend WithEvents ButtonSourceBefore As System.Windows.Forms.Button
  Friend WithEvents ButtonSourceAfter As System.Windows.Forms.Button
  Friend WithEvents ButtonSourceRemove As System.Windows.Forms.Button
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents TextBoxFilterName As System.Windows.Forms.TextBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents ComboBoxFilterCategory As System.Windows.Forms.ComboBox
  Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Button2 As System.Windows.Forms.Button
  Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents CheckBoxFilterSharedNot As System.Windows.Forms.CheckBox
  Friend WithEvents CheckBoxFilterSharedYes As System.Windows.Forms.CheckBox
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents ComboBoxFilterHost As System.Windows.Forms.ComboBox
End Class

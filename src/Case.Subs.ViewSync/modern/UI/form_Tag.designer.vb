<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_Tag
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_Tag))
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.DataGridViewFamilies = New System.Windows.Forms.DataGridView()
    Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.ButtonOk = New System.Windows.Forms.Button()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.ComboBoxTagFamily = New System.Windows.Forms.ComboBox()
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.TextBoxViewRef = New System.Windows.Forms.TextBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.TextBoxShtName = New System.Windows.Forms.TextBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.TextBoxDetNo = New System.Windows.Forms.TextBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.TextBoxShtNo = New System.Windows.Forms.TextBox()
    Me.GroupBoxTagSelection = New System.Windows.Forms.GroupBox()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.GroupBox1.SuspendLayout()
    CType(Me.DataGridViewFamilies, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox2.SuspendLayout()
    Me.GroupBoxTagSelection.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.DataGridViewFamilies)
    Me.GroupBox1.Location = New System.Drawing.Point(12, 207)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(660, 227)
    Me.GroupBox1.TabIndex = 8
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Select a View to Reference with the Tag:"
    '
    'DataGridViewFamilies
    '
    Me.DataGridViewFamilies.AllowUserToAddRows = False
    Me.DataGridViewFamilies.AllowUserToDeleteRows = False
    Me.DataGridViewFamilies.AllowUserToResizeRows = False
    Me.DataGridViewFamilies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
    Me.DataGridViewFamilies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewFamilies.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column5, Me.Column6, Me.Column1, Me.Column4, Me.Column3, Me.Column2, Me.Column7, Me.Column8})
    Me.DataGridViewFamilies.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DataGridViewFamilies.Location = New System.Drawing.Point(3, 16)
    Me.DataGridViewFamilies.MultiSelect = False
    Me.DataGridViewFamilies.Name = "DataGridViewFamilies"
    Me.DataGridViewFamilies.ReadOnly = True
    Me.DataGridViewFamilies.RowHeadersVisible = False
    Me.DataGridViewFamilies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewFamilies.Size = New System.Drawing.Size(654, 208)
    Me.DataGridViewFamilies.TabIndex = 0
    '
    'Column5
    '
    Me.Column5.DataPropertyName = "GUID"
    Me.Column5.HeaderText = "guid"
    Me.Column5.Name = "Column5"
    Me.Column5.ReadOnly = True
    Me.Column5.Visible = False
    Me.Column5.Width = 33
    '
    'Column6
    '
    Me.Column6.DataPropertyName = "Eid"
    Me.Column6.HeaderText = "eid"
    Me.Column6.Name = "Column6"
    Me.Column6.ReadOnly = True
    Me.Column6.Visible = False
    Me.Column6.Width = 27
    '
    'Column1
    '
    Me.Column1.DataPropertyName = "SheetNumber"
    Me.Column1.HeaderText = "Sht No."
    Me.Column1.Name = "Column1"
    Me.Column1.ReadOnly = True
    Me.Column1.Width = 68
    '
    'Column4
    '
    Me.Column4.DataPropertyName = "DetailNumber"
    Me.Column4.HeaderText = "Detail No."
    Me.Column4.Name = "Column4"
    Me.Column4.ReadOnly = True
    Me.Column4.Width = 79
    '
    'Column3
    '
    Me.Column3.DataPropertyName = "SheetName"
    Me.Column3.HeaderText = "Sht Name"
    Me.Column3.Name = "Column3"
    Me.Column3.ReadOnly = True
    Me.Column3.Width = 79
    '
    'Column2
    '
    Me.Column2.DataPropertyName = "ViewName"
    Me.Column2.HeaderText = "View Name"
    Me.Column2.Name = "Column2"
    Me.Column2.ReadOnly = True
    Me.Column2.Width = 86
    '
    'Column7
    '
    Me.Column7.DataPropertyName = "TitleOnSheet"
    Me.Column7.HeaderText = "TitleOnSheet"
    Me.Column7.Name = "Column7"
    Me.Column7.ReadOnly = True
    Me.Column7.Visible = False
    Me.Column7.Width = 94
    '
    'Column8
    '
    Me.Column8.DataPropertyName = "ViewState"
    Me.Column8.HeaderText = "ViewState"
    Me.Column8.Name = "Column8"
    Me.Column8.ReadOnly = True
    Me.Column8.Visible = False
    Me.Column8.Width = 80
    '
    'ButtonOk
    '
    Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonOk.Location = New System.Drawing.Point(516, 440)
    Me.ButtonOk.Name = "ButtonOk"
    Me.ButtonOk.Size = New System.Drawing.Size(75, 40)
    Me.ButtonOk.TabIndex = 6
    Me.ButtonOk.Text = "Place Tag"
    Me.ButtonOk.UseVisualStyleBackColor = True
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(597, 440)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 7
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ComboBoxTagFamily
    '
    Me.ComboBoxTagFamily.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ComboBoxTagFamily.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxTagFamily.FormattingEnabled = True
    Me.ComboBoxTagFamily.Location = New System.Drawing.Point(26, 36)
    Me.ComboBoxTagFamily.Name = "ComboBoxTagFamily"
    Me.ComboBoxTagFamily.Size = New System.Drawing.Size(610, 21)
    Me.ComboBoxTagFamily.TabIndex = 5
    '
    'GroupBox2
    '
    Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox2.Controls.Add(Me.TextBoxViewRef)
    Me.GroupBox2.Controls.Add(Me.Label5)
    Me.GroupBox2.Controls.Add(Me.TextBoxShtName)
    Me.GroupBox2.Controls.Add(Me.Label4)
    Me.GroupBox2.Controls.Add(Me.Label3)
    Me.GroupBox2.Controls.Add(Me.TextBoxDetNo)
    Me.GroupBox2.Controls.Add(Me.Label1)
    Me.GroupBox2.Controls.Add(Me.TextBoxShtNo)
    Me.GroupBox2.Location = New System.Drawing.Point(12, 108)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(660, 93)
    Me.GroupBox2.TabIndex = 11
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Find Views to Tag (Do Not Use Wildcards):"
    '
    'TextBoxViewRef
    '
    Me.TextBoxViewRef.Location = New System.Drawing.Point(436, 53)
    Me.TextBoxViewRef.Name = "TextBoxViewRef"
    Me.TextBoxViewRef.Size = New System.Drawing.Size(200, 20)
    Me.TextBoxViewRef.TabIndex = 4
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(343, 56)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(64, 13)
    Me.Label5.TabIndex = 8
    Me.Label5.Text = "View Name:"
    '
    'TextBoxShtName
    '
    Me.TextBoxShtName.Location = New System.Drawing.Point(436, 27)
    Me.TextBoxShtName.Name = "TextBoxShtName"
    Me.TextBoxShtName.Size = New System.Drawing.Size(200, 20)
    Me.TextBoxShtName.TabIndex = 2
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(343, 30)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(57, 13)
    Me.Label4.TabIndex = 6
    Me.Label4.Text = "Sht Name:"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(23, 56)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(57, 13)
    Me.Label3.TabIndex = 4
    Me.Label3.Text = "Detail No.:"
    '
    'TextBoxDetNo
    '
    Me.TextBoxDetNo.Location = New System.Drawing.Point(116, 53)
    Me.TextBoxDetNo.Name = "TextBoxDetNo"
    Me.TextBoxDetNo.Size = New System.Drawing.Size(200, 20)
    Me.TextBoxDetNo.TabIndex = 3
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(23, 30)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(46, 13)
    Me.Label1.TabIndex = 2
    Me.Label1.Text = "Sht No.:"
    '
    'TextBoxShtNo
    '
    Me.TextBoxShtNo.Location = New System.Drawing.Point(116, 27)
    Me.TextBoxShtNo.Name = "TextBoxShtNo"
    Me.TextBoxShtNo.Size = New System.Drawing.Size(200, 20)
    Me.TextBoxShtNo.TabIndex = 1
    '
    'GroupBoxTagSelection
    '
    Me.GroupBoxTagSelection.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxTagSelection.Controls.Add(Me.ComboBoxTagFamily)
    Me.GroupBoxTagSelection.Location = New System.Drawing.Point(12, 12)
    Me.GroupBoxTagSelection.Name = "GroupBoxTagSelection"
    Me.GroupBoxTagSelection.Size = New System.Drawing.Size(660, 90)
    Me.GroupBoxTagSelection.TabIndex = 12
    Me.GroupBoxTagSelection.TabStop = False
    Me.GroupBoxTagSelection.Text = "Select a Tag Family to Place:"
    '
    'PictureBox1
    '
    Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.PictureBox1.Image = Global.[Case].Subs.ViewSync.My.Resources.Resources.case_logo_type_32x122
    Me.PictureBox1.Location = New System.Drawing.Point(12, 440)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(174, 40)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 13
    Me.PictureBox1.TabStop = False
    '
    'form_Tag
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(684, 492)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.GroupBoxTagSelection)
    Me.Controls.Add(Me.GroupBox2)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.ButtonOk)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(700, 530)
    Me.Name = "form_Tag"
    Me.Text = "X"
    Me.GroupBox1.ResumeLayout(False)
    CType(Me.DataGridViewFamilies, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox2.ResumeLayout(False)
    Me.GroupBox2.PerformLayout()
    Me.GroupBoxTagSelection.ResumeLayout(False)
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents DataGridViewFamilies As System.Windows.Forms.DataGridView
  Friend WithEvents ButtonOk As System.Windows.Forms.Button
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents ComboBoxTagFamily As System.Windows.Forms.ComboBox
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
  Friend WithEvents TextBoxViewRef As System.Windows.Forms.TextBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents TextBoxShtName As System.Windows.Forms.TextBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents TextBoxDetNo As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents TextBoxShtNo As System.Windows.Forms.TextBox
  Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents GroupBoxTagSelection As System.Windows.Forms.GroupBox
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class

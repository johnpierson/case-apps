<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_ModifyViewReference
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_ModifyViewReference))
    Me.ButtonOk = New System.Windows.Forms.Button()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.GroupBoxFilters = New System.Windows.Forms.GroupBox()
    Me.TextBoxViewRef = New System.Windows.Forms.TextBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.TextBoxShtName = New System.Windows.Forms.TextBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.TextBoxDetNo = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.TextBoxShtNo = New System.Windows.Forms.TextBox()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.DataGridViewViews = New System.Windows.Forms.DataGridView()
    Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.GroupBoxFilters.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    CType(Me.DataGridViewViews, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ButtonOk
    '
    Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonOk.Location = New System.Drawing.Point(523, 360)
    Me.ButtonOk.Name = "ButtonOk"
    Me.ButtonOk.Size = New System.Drawing.Size(75, 40)
    Me.ButtonOk.TabIndex = 5
    Me.ButtonOk.Text = "Modify Tag"
    Me.ButtonOk.UseVisualStyleBackColor = True
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(604, 360)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 6
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'GroupBoxFilters
    '
    Me.GroupBoxFilters.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxFilters.Controls.Add(Me.TextBoxViewRef)
    Me.GroupBoxFilters.Controls.Add(Me.Label5)
    Me.GroupBoxFilters.Controls.Add(Me.TextBoxShtName)
    Me.GroupBoxFilters.Controls.Add(Me.Label4)
    Me.GroupBoxFilters.Controls.Add(Me.Label3)
    Me.GroupBoxFilters.Controls.Add(Me.TextBoxDetNo)
    Me.GroupBoxFilters.Controls.Add(Me.Label2)
    Me.GroupBoxFilters.Controls.Add(Me.TextBoxShtNo)
    Me.GroupBoxFilters.Location = New System.Drawing.Point(15, 12)
    Me.GroupBoxFilters.Name = "GroupBoxFilters"
    Me.GroupBoxFilters.Size = New System.Drawing.Size(667, 97)
    Me.GroupBoxFilters.TabIndex = 16
    Me.GroupBoxFilters.TabStop = False
    Me.GroupBoxFilters.Text = "Search and Filter Tags (Do Not Use Wildcards)"
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
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(23, 30)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(46, 13)
    Me.Label2.TabIndex = 2
    Me.Label2.Text = "Sht No.:"
    '
    'TextBoxShtNo
    '
    Me.TextBoxShtNo.Location = New System.Drawing.Point(116, 27)
    Me.TextBoxShtNo.Name = "TextBoxShtNo"
    Me.TextBoxShtNo.Size = New System.Drawing.Size(200, 20)
    Me.TextBoxShtNo.TabIndex = 1
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.DataGridViewViews)
    Me.GroupBox1.Location = New System.Drawing.Point(15, 115)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(667, 239)
    Me.GroupBox1.TabIndex = 17
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Select a Tag to Modify:"
    '
    'DataGridViewViews
    '
    Me.DataGridViewViews.AllowUserToAddRows = False
    Me.DataGridViewViews.AllowUserToDeleteRows = False
    Me.DataGridViewViews.AllowUserToResizeRows = False
    Me.DataGridViewViews.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
    Me.DataGridViewViews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewViews.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column5, Me.Column6, Me.Column1, Me.Column4, Me.Column3, Me.Column2, Me.Column7, Me.Column8})
    Me.DataGridViewViews.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DataGridViewViews.Location = New System.Drawing.Point(3, 16)
    Me.DataGridViewViews.MultiSelect = False
    Me.DataGridViewViews.Name = "DataGridViewViews"
    Me.DataGridViewViews.ReadOnly = True
    Me.DataGridViewViews.RowHeadersVisible = False
    Me.DataGridViewViews.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewViews.Size = New System.Drawing.Size(661, 220)
    Me.DataGridViewViews.TabIndex = 0
    '
    'Column5
    '
    Me.Column5.DataPropertyName = "GUID"
    Me.Column5.HeaderText = "guid"
    Me.Column5.Name = "Column5"
    Me.Column5.ReadOnly = True
    Me.Column5.Visible = False
    '
    'Column6
    '
    Me.Column6.DataPropertyName = "Eid"
    Me.Column6.HeaderText = "eid"
    Me.Column6.Name = "Column6"
    Me.Column6.ReadOnly = True
    Me.Column6.Visible = False
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
    '
    'Column8
    '
    Me.Column8.DataPropertyName = "ViewState"
    Me.Column8.HeaderText = "ViewState"
    Me.Column8.Name = "Column8"
    Me.Column8.ReadOnly = True
    Me.Column8.Visible = False
    '
    'PictureBox1
    '
    Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.PictureBox1.Image = Global.[Case].Subs.ViewSync.My.Resources.Resources.case_logo_type_32x122
    Me.PictureBox1.Location = New System.Drawing.Point(18, 360)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(174, 40)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 18
    Me.PictureBox1.TabStop = False
    '
    'form_ModifyViewReference
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(694, 412)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.GroupBoxFilters)
    Me.Controls.Add(Me.ButtonOk)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(710, 450)
    Me.Name = "form_ModifyViewReference"
    Me.Text = "Modify View Reference"
    Me.GroupBoxFilters.ResumeLayout(False)
    Me.GroupBoxFilters.PerformLayout()
    Me.GroupBox1.ResumeLayout(False)
    CType(Me.DataGridViewViews, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ButtonOk As System.Windows.Forms.Button
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents GroupBoxFilters As System.Windows.Forms.GroupBox
  Friend WithEvents TextBoxViewRef As System.Windows.Forms.TextBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents TextBoxShtName As System.Windows.Forms.TextBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents TextBoxDetNo As System.Windows.Forms.TextBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents TextBoxShtNo As System.Windows.Forms.TextBox
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents DataGridViewViews As System.Windows.Forms.DataGridView
  Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_RoomMasses
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
    Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_RoomMasses))
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.ButtonParameters = New System.Windows.Forms.Button()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.ComboBoxMaterial = New System.Windows.Forms.ComboBox()
    Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
    Me.CheckBoxPurge = New System.Windows.Forms.CheckBox()
    Me.CheckBoxSaveMass = New System.Windows.Forms.CheckBox()
    Me.ButtonNone = New System.Windows.Forms.Button()
    Me.ButtonAll = New System.Windows.Forms.Button()
    Me.DataGridViewRooms = New System.Windows.Forms.DataGridView()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
    Me.colNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colDept = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colLevel = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.ButtonGenerateMasses = New System.Windows.Forms.Button()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
    Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    Me.GroupBox2.SuspendLayout()
    CType(NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(DataGridViewRooms, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'GroupBox2
    '
    Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox2.Controls.Add(ButtonParameters)
    Me.GroupBox2.Controls.Add(Label2)
    Me.GroupBox2.Controls.Add(Label1)
    Me.GroupBox2.Controls.Add(ComboBoxMaterial)
    Me.GroupBox2.Controls.Add(NumericUpDown1)
    Me.GroupBox2.Controls.Add(CheckBoxPurge)
    Me.GroupBox2.Controls.Add(CheckBoxSaveMass)
    Me.GroupBox2.Controls.Add(ButtonNone)
    Me.GroupBox2.Controls.Add(ButtonAll)
    Me.GroupBox2.Controls.Add(DataGridViewRooms)
    Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(735, 446)
    Me.GroupBox2.TabIndex = 25
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Select Rooms to Generate Mass From"
    '
    'ButtonParameters
    '
    Me.ButtonParameters.Location = New System.Drawing.Point(507, 22)
    Me.ButtonParameters.Name = "ButtonParameters"
    Me.ButtonParameters.Size = New System.Drawing.Size(222, 40)
    Me.ButtonParameters.TabIndex = 31
    Me.ButtonParameters.Text = "Map && Update Parameters"
    Me.ButtonParameters.UseVisualStyleBackColor = True
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(22, 36)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(95, 13)
    Me.Label2.TabIndex = 30
    Me.Label2.Text = "Material Parameter"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(349, 36)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(72, 13)
    Me.Label1.TabIndex = 29
    Me.Label1.Text = "Transparency"
    '
    'ComboBoxMaterial
    '
    Me.ComboBoxMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxMaterial.FormattingEnabled = True
    Me.ComboBoxMaterial.Location = New System.Drawing.Point(123, 33)
    Me.ComboBoxMaterial.Name = "ComboBoxMaterial"
    Me.ComboBoxMaterial.Size = New System.Drawing.Size(189, 21)
    Me.ComboBoxMaterial.TabIndex = 26
    '
    'NumericUpDown1
    '
    Me.NumericUpDown1.Location = New System.Drawing.Point(437, 34)
    Me.NumericUpDown1.Name = "NumericUpDown1"
    Me.NumericUpDown1.Size = New System.Drawing.Size(45, 20)
    Me.NumericUpDown1.TabIndex = 28
    Me.NumericUpDown1.Value = New Decimal(New Integer() {50, 0, 0, 0})
    '
    'CheckBoxPurge
    '
    Me.CheckBoxPurge.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.CheckBoxPurge.AutoSize = True
    Me.CheckBoxPurge.Checked = True
    Me.CheckBoxPurge.CheckState = System.Windows.Forms.CheckState.Checked
    Me.CheckBoxPurge.Location = New System.Drawing.Point(507, 413)
    Me.CheckBoxPurge.Name = "CheckBoxPurge"
    Me.CheckBoxPurge.Size = New System.Drawing.Size(193, 17)
    Me.CheckBoxPurge.TabIndex = 27
    Me.CheckBoxPurge.Text = "Purge Unused Room Mass Families"
    Me.CheckBoxPurge.UseVisualStyleBackColor = True
    '
    'CheckBoxSaveMass
    '
    Me.CheckBoxSaveMass.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.CheckBoxSaveMass.AutoSize = True
    Me.CheckBoxSaveMass.Location = New System.Drawing.Point(352, 413)
    Me.CheckBoxSaveMass.Name = "CheckBoxSaveMass"
    Me.CheckBoxSaveMass.Size = New System.Drawing.Size(135, 17)
    Me.CheckBoxSaveMass.TabIndex = 26
    Me.CheckBoxSaveMass.Text = "Save Mass Family Files"
    Me.CheckBoxSaveMass.UseVisualStyleBackColor = True
    '
    'ButtonNone
    '
    Me.ButtonNone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonNone.Location = New System.Drawing.Point(81, 400)
    Me.ButtonNone.Name = "ButtonNone"
    Me.ButtonNone.Size = New System.Drawing.Size(68, 40)
    Me.ButtonNone.TabIndex = 21
    Me.ButtonNone.Text = "None"
    Me.ButtonNone.UseVisualStyleBackColor = True
    '
    'ButtonAll
    '
    Me.ButtonAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonAll.Location = New System.Drawing.Point(7, 400)
    Me.ButtonAll.Name = "ButtonAll"
    Me.ButtonAll.Size = New System.Drawing.Size(68, 40)
    Me.ButtonAll.TabIndex = 20
    Me.ButtonAll.Text = "All"
    Me.ButtonAll.UseVisualStyleBackColor = True
    '
    'DataGridViewRooms
    '
    Me.DataGridViewRooms.AllowUserToAddRows = False
    Me.DataGridViewRooms.AllowUserToDeleteRows = False
    Me.DataGridViewRooms.AllowUserToResizeRows = False
    Me.DataGridViewRooms.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.DataGridViewRooms.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
    Me.DataGridViewRooms.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.DataGridViewRooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewRooms.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column2, Me.Column1, Me.colNumber, Me.colName, Me.colDept, Me.colLevel})
    DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
    DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
    DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight
    DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
    DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.DataGridViewRooms.DefaultCellStyle = DataGridViewCellStyle1
    Me.DataGridViewRooms.GridColor = System.Drawing.Color.LightGray
    Me.DataGridViewRooms.Location = New System.Drawing.Point(7, 77)
    Me.DataGridViewRooms.Name = "DataGridViewRooms"
    Me.DataGridViewRooms.RowHeadersVisible = False
    Me.DataGridViewRooms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewRooms.Size = New System.Drawing.Size(722, 317)
    Me.DataGridViewRooms.TabIndex = 2
    '
    'Column2
    '
    Me.Column2.DataPropertyName = "RoomElement"
    Me.Column2.HeaderText = "RoomElement"
    Me.Column2.Name = "Column2"
    Me.Column2.Visible = False
    '
    'Column1
    '
    Me.Column1.DataPropertyName = "isChecked"
    Me.Column1.HeaderText = ""
    Me.Column1.MinimumWidth = 20
    Me.Column1.Name = "Column1"
    Me.Column1.Width = 20
    '
    'colNumber
    '
    Me.colNumber.DataPropertyName = "Number"
    Me.colNumber.HeaderText = "Number"
    Me.colNumber.Name = "colNumber"
    Me.colNumber.ReadOnly = True
    '
    'colName
    '
    Me.colName.DataPropertyName = "Name"
    Me.colName.HeaderText = "Name"
    Me.colName.Name = "colName"
    Me.colName.ReadOnly = True
    Me.colName.Width = 200
    '
    'colDept
    '
    Me.colDept.DataPropertyName = "Department"
    Me.colDept.HeaderText = "Department"
    Me.colDept.Name = "colDept"
    Me.colDept.ReadOnly = True
    Me.colDept.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
    Me.colDept.Width = 150
    '
    'colLevel
    '
    Me.colLevel.DataPropertyName = "Level"
    Me.colLevel.HeaderText = "Level"
    Me.colLevel.Name = "colLevel"
    Me.colLevel.ReadOnly = True
    Me.colLevel.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
    '
    'ButtonGenerateMasses
    '
    Me.ButtonGenerateMasses.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonGenerateMasses.Location = New System.Drawing.Point(595, 464)
    Me.ButtonGenerateMasses.Name = "ButtonGenerateMasses"
    Me.ButtonGenerateMasses.Size = New System.Drawing.Size(70, 40)
    Me.ButtonGenerateMasses.TabIndex = 18
    Me.ButtonGenerateMasses.Text = "OK"
    Me.ButtonGenerateMasses.UseVisualStyleBackColor = True
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(175, 464)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(569, 40)
    Me.ProgressBar1.TabIndex = 23
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(671, 464)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(70, 40)
    Me.ButtonCancel.TabIndex = 19
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'FolderBrowserDialog
    '
    Me.FolderBrowserDialog.Description = "Location to Save Mass Families"
    Me.FolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer
    '
    'FolderBrowserDialog1
    '
    Me.FolderBrowserDialog1.Description = "Mass Family Save Location"
    '
    'PictureBox1
    '
    Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.PictureBox1.Image = Global.[Case].Subs.RoomsToMass.My.Resources.Resources.Case_Clearly
    Me.PictureBox1.Location = New System.Drawing.Point(12, 464)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(157, 40)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 3
    Me.PictureBox1.TabStop = False
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(519, 464)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(70, 40)
    Me.ButtonHelp.TabIndex = 28
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'form_RoomMasses
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(759, 512)
    Me.Controls.Add(ButtonHelp)
    Me.Controls.Add(PictureBox1)
    Me.Controls.Add(GroupBox2)
    Me.Controls.Add(ButtonGenerateMasses)
    Me.Controls.Add(ButtonCancel)
    Me.Controls.Add(ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(775, 550)
    Me.Name = "form_RoomMasses"
    Me.Text = "Extrude Rooms as Masses"
    Me.GroupBox2.ResumeLayout(False)
    Me.GroupBox2.PerformLayout()
    CType(NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(DataGridViewRooms, System.ComponentModel.ISupportInitialize).EndInit()
    CType(PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
  Friend WithEvents DataGridViewRooms As System.Windows.Forms.DataGridView
  Friend WithEvents ButtonGenerateMasses As System.Windows.Forms.Button
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents FolderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
  Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents CheckBoxSaveMass As System.Windows.Forms.CheckBox
  Friend WithEvents CheckBoxPurge As System.Windows.Forms.CheckBox
  Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents colNumber As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colDept As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colLevel As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents ButtonNone As System.Windows.Forms.Button
  Friend WithEvents ButtonAll As System.Windows.Forms.Button
  Friend WithEvents ComboBoxMaterial As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
  Friend WithEvents ButtonParameters As System.Windows.Forms.Button
End Class

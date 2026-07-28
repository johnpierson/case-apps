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
    Me.DataGridViewRooms = New System.Windows.Forms.DataGridView()
    Me.colRevElementID = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
    CType(Me.DataGridViewRooms, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'GroupBox2
    '
    Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox2.Controls.Add(Me.DataGridViewRooms)
    Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(487, 316)
    Me.GroupBox2.TabIndex = 25
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Rooms to Generate Masses From"
    '
    'DataGridViewRooms
    '
    Me.DataGridViewRooms.AllowUserToAddRows = False
    Me.DataGridViewRooms.AllowUserToDeleteRows = False
    Me.DataGridViewRooms.AllowUserToResizeRows = False
    Me.DataGridViewRooms.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
    Me.DataGridViewRooms.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.DataGridViewRooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewRooms.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRevElementID, Me.colNumber, Me.colName, Me.colDept, Me.colLevel})
    DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
    DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
    DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight
    DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
    DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.DataGridViewRooms.DefaultCellStyle = DataGridViewCellStyle1
    Me.DataGridViewRooms.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DataGridViewRooms.GridColor = System.Drawing.Color.LightGray
    Me.DataGridViewRooms.Location = New System.Drawing.Point(3, 16)
    Me.DataGridViewRooms.Name = "DataGridViewRooms"
    Me.DataGridViewRooms.ReadOnly = True
    Me.DataGridViewRooms.RowHeadersVisible = False
    Me.DataGridViewRooms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewRooms.Size = New System.Drawing.Size(481, 297)
    Me.DataGridViewRooms.TabIndex = 2
    '
    'colRevElementID
    '
    Me.colRevElementID.HeaderText = "UniqueID"
    Me.colRevElementID.Name = "colRevElementID"
    Me.colRevElementID.ReadOnly = True
    Me.colRevElementID.Visible = False
    '
    'colNumber
    '
    Me.colNumber.HeaderText = "Number"
    Me.colNumber.Name = "colNumber"
    Me.colNumber.ReadOnly = True
    Me.colNumber.Width = 75
    '
    'colName
    '
    Me.colName.HeaderText = "Name"
    Me.colName.Name = "colName"
    Me.colName.ReadOnly = True
    Me.colName.Width = 150
    '
    'colDept
    '
    Me.colDept.HeaderText = "Department"
    Me.colDept.Name = "colDept"
    Me.colDept.ReadOnly = True
    Me.colDept.Width = 150
    '
    'colLevel
    '
    Me.colLevel.HeaderText = "Level"
    Me.colLevel.Name = "colLevel"
    Me.colLevel.ReadOnly = True
    '
    'ButtonGenerateMasses
    '
    Me.ButtonGenerateMasses.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonGenerateMasses.Location = New System.Drawing.Point(266, 334)
    Me.ButtonGenerateMasses.Name = "ButtonGenerateMasses"
    Me.ButtonGenerateMasses.Size = New System.Drawing.Size(132, 40)
    Me.ButtonGenerateMasses.TabIndex = 18
    Me.ButtonGenerateMasses.Text = "Generate Masses"
    Me.ButtonGenerateMasses.UseVisualStyleBackColor = True
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ProgressBar1.Location = New System.Drawing.Point(184, 334)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(315, 40)
    Me.ProgressBar1.TabIndex = 23
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(404, 334)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(95, 40)
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
    Me.PictureBox1.Image = Global.[Case].ExtrudeRoomsToMass.My.Resources.Resources.Case_Clearly
    Me.PictureBox1.Location = New System.Drawing.Point(12, 334)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(166, 40)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 3
    Me.PictureBox1.TabStop = False
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(210, 334)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(50, 40)
    Me.ButtonHelp.TabIndex = 27
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'form_RoomMasses
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(514, 382)
    Me.Controls.Add(Me.ButtonHelp)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.GroupBox2)
    Me.Controls.Add(Me.ButtonGenerateMasses)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(530, 420)
    Me.Name = "form_RoomMasses"
    Me.Text = "Extrude Rooms as Masses"
    Me.GroupBox2.ResumeLayout(False)
    CType(Me.DataGridViewRooms, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridViewRooms As System.Windows.Forms.DataGridView
    Friend WithEvents ButtonGenerateMasses As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents colRevElementID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDept As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLevel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
End Class

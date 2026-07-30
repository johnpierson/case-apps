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
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.ButtonOk = New System.Windows.Forms.Button()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.ComboBoxSymbol = New System.Windows.Forms.ComboBox()
    Me.TextBoxHeight = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.NumericUpDownV = New System.Windows.Forms.NumericUpDown()
    Me.RadioButtonExact = New System.Windows.Forms.RadioButton()
    Me.TextBoxDistanceV = New System.Windows.Forms.TextBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.NumericUpDownU = New System.Windows.Forms.NumericUpDown()
    Me.TextBoxDistanceU = New System.Windows.Forms.TextBox()
    Me.RadioButtonEvenly = New System.Windows.Forms.RadioButton()
    Me.GroupBoxElements = New System.Windows.Forms.GroupBox()
    Me.DataGridViewRooms = New System.Windows.Forms.DataGridView()
    Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.GroupBox3 = New System.Windows.Forms.GroupBox()
    Me.RadioButtonCentroid = New System.Windows.Forms.RadioButton()
    Me.RadioButtonCp = New System.Windows.Forms.RadioButton()
    Me.GroupBox1.SuspendLayout()
    Me.GroupBox2.SuspendLayout()
    CType(Me.NumericUpDownV, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NumericUpDownU, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBoxElements.SuspendLayout()
    CType(Me.DataGridViewRooms, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox3.SuspendLayout()
    Me.SuspendLayout()
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(432, 555)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 0
    Me.ButtonCancel.Text = "Close"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ButtonOk
    '
    Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonOk.Location = New System.Drawing.Point(294, 555)
    Me.ButtonOk.Name = "ButtonOk"
    Me.ButtonOk.Size = New System.Drawing.Size(132, 40)
    Me.ButtonOk.TabIndex = 1
    Me.ButtonOk.Text = "Light the Room"
    Me.ButtonOk.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.ComboBoxSymbol)
    Me.GroupBox1.Controls.Add(Me.TextBoxHeight)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(495, 119)
    Me.GroupBox1.TabIndex = 2
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Host and Symbol to Place (Revit 2021 Dual Level Hosting Family Only!)"
    '
    'ComboBoxSymbol
    '
    Me.ComboBoxSymbol.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ComboBoxSymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxSymbol.FormattingEnabled = True
    Me.ComboBoxSymbol.Location = New System.Drawing.Point(178, 68)
    Me.ComboBoxSymbol.Name = "ComboBoxSymbol"
    Me.ComboBoxSymbol.Size = New System.Drawing.Size(296, 21)
    Me.ComboBoxSymbol.TabIndex = 3
    '
    'TextBoxHeight
    '
    Me.TextBoxHeight.Location = New System.Drawing.Point(178, 31)
    Me.TextBoxHeight.Name = "TextBoxHeight"
    Me.TextBoxHeight.Size = New System.Drawing.Size(77, 20)
    Me.TextBoxHeight.TabIndex = 12
    Me.TextBoxHeight.Text = "8"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(31, 71)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(104, 13)
    Me.Label2.TabIndex = 1
    Me.Label2.Text = "Light Fixture Symbol:"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(31, 34)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(122, 13)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Fixture Mounting Height:"
    '
    'GroupBox2
    '
    Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox2.Controls.Add(Me.Label7)
    Me.GroupBox2.Controls.Add(Me.Label5)
    Me.GroupBox2.Controls.Add(Me.Label6)
    Me.GroupBox2.Controls.Add(Me.NumericUpDownV)
    Me.GroupBox2.Controls.Add(Me.RadioButtonExact)
    Me.GroupBox2.Controls.Add(Me.TextBoxDistanceV)
    Me.GroupBox2.Controls.Add(Me.Label4)
    Me.GroupBox2.Controls.Add(Me.Label3)
    Me.GroupBox2.Controls.Add(Me.NumericUpDownU)
    Me.GroupBox2.Controls.Add(Me.TextBoxDistanceU)
    Me.GroupBox2.Controls.Add(Me.RadioButtonEvenly)
    Me.GroupBox2.Location = New System.Drawing.Point(12, 204)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(495, 142)
    Me.GroupBox2.TabIndex = 3
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Fixture Layout"
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(75, 34)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(77, 13)
    Me.Label7.TabIndex = 11
    Me.Label7.Text = "Fixture Counts:"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(317, 97)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(17, 13)
    Me.Label5.TabIndex = 9
    Me.Label5.Text = "V:"
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(317, 34)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(17, 13)
    Me.Label6.TabIndex = 8
    Me.Label6.Text = "V:"
    '
    'NumericUpDownV
    '
    Me.NumericUpDownV.Location = New System.Drawing.Point(343, 32)
    Me.NumericUpDownV.Name = "NumericUpDownV"
    Me.NumericUpDownV.Size = New System.Drawing.Size(83, 20)
    Me.NumericUpDownV.TabIndex = 7
    Me.NumericUpDownV.Value = New Decimal(New Integer() {2, 0, 0, 0})
    '
    'RadioButtonExact
    '
    Me.RadioButtonExact.AutoSize = True
    Me.RadioButtonExact.Location = New System.Drawing.Point(34, 95)
    Me.RadioButtonExact.Name = "RadioButtonExact"
    Me.RadioButtonExact.Size = New System.Drawing.Size(99, 17)
    Me.RadioButtonExact.TabIndex = 0
    Me.RadioButtonExact.Text = "Exact Spacings"
    Me.RadioButtonExact.UseVisualStyleBackColor = True
    '
    'TextBoxDistanceV
    '
    Me.TextBoxDistanceV.Location = New System.Drawing.Point(343, 94)
    Me.TextBoxDistanceV.Name = "TextBoxDistanceV"
    Me.TextBoxDistanceV.Size = New System.Drawing.Size(83, 20)
    Me.TextBoxDistanceV.TabIndex = 6
    Me.TextBoxDistanceV.Text = "4"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(175, 97)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(18, 13)
    Me.Label4.TabIndex = 5
    Me.Label4.Text = "U:"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(175, 34)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(18, 13)
    Me.Label3.TabIndex = 4
    Me.Label3.Text = "U:"
    '
    'NumericUpDownU
    '
    Me.NumericUpDownU.Location = New System.Drawing.Point(201, 32)
    Me.NumericUpDownU.Name = "NumericUpDownU"
    Me.NumericUpDownU.Size = New System.Drawing.Size(83, 20)
    Me.NumericUpDownU.TabIndex = 3
    Me.NumericUpDownU.Value = New Decimal(New Integer() {2, 0, 0, 0})
    '
    'TextBoxDistanceU
    '
    Me.TextBoxDistanceU.Location = New System.Drawing.Point(201, 94)
    Me.TextBoxDistanceU.Name = "TextBoxDistanceU"
    Me.TextBoxDistanceU.Size = New System.Drawing.Size(83, 20)
    Me.TextBoxDistanceU.TabIndex = 2
    Me.TextBoxDistanceU.Text = "4"
    '
    'RadioButtonEvenly
    '
    Me.RadioButtonEvenly.AutoSize = True
    Me.RadioButtonEvenly.Checked = True
    Me.RadioButtonEvenly.Location = New System.Drawing.Point(34, 62)
    Me.RadioButtonEvenly.Name = "RadioButtonEvenly"
    Me.RadioButtonEvenly.Size = New System.Drawing.Size(91, 17)
    Me.RadioButtonEvenly.TabIndex = 1
    Me.RadioButtonEvenly.TabStop = True
    Me.RadioButtonEvenly.Text = "Space Evenly"
    Me.RadioButtonEvenly.UseVisualStyleBackColor = True
    '
    'GroupBoxElements
    '
    Me.GroupBoxElements.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxElements.Controls.Add(Me.DataGridViewRooms)
    Me.GroupBoxElements.Location = New System.Drawing.Point(12, 352)
    Me.GroupBoxElements.Name = "GroupBoxElements"
    Me.GroupBoxElements.Size = New System.Drawing.Size(495, 197)
    Me.GroupBoxElements.TabIndex = 4
    Me.GroupBoxElements.TabStop = False
    Me.GroupBoxElements.Text = "Select a Room to Light Up"
    '
    'DataGridViewRooms
    '
    Me.DataGridViewRooms.AllowUserToAddRows = False
    Me.DataGridViewRooms.AllowUserToDeleteRows = False
    Me.DataGridViewRooms.AllowUserToResizeRows = False
    Me.DataGridViewRooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewRooms.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4})
    Me.DataGridViewRooms.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DataGridViewRooms.Location = New System.Drawing.Point(3, 16)
    Me.DataGridViewRooms.Name = "DataGridViewRooms"
    Me.DataGridViewRooms.RowHeadersVisible = False
    Me.DataGridViewRooms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewRooms.Size = New System.Drawing.Size(489, 178)
    Me.DataGridViewRooms.TabIndex = 0
    '
    'Column1
    '
    Me.Column1.DataPropertyName = "RmNumber"
    Me.Column1.HeaderText = "Number"
    Me.Column1.MinimumWidth = 60
    Me.Column1.Name = "Column1"
    Me.Column1.Width = 125
    '
    'Column2
    '
    Me.Column2.DataPropertyName = "RmName"
    Me.Column2.HeaderText = "Name"
    Me.Column2.MinimumWidth = 50
    Me.Column2.Name = "Column2"
    Me.Column2.Width = 150
    '
    'Column3
    '
    Me.Column3.DataPropertyName = "RmLevel"
    Me.Column3.HeaderText = "Level"
    Me.Column3.Name = "Column3"
    Me.Column3.Width = 125
    '
    'Column4
    '
    Me.Column4.DataPropertyName = "RmArea"
    Me.Column4.HeaderText = "Area"
    Me.Column4.Name = "Column4"
    Me.Column4.Width = 75
    '
    'GroupBox3
    '
    Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox3.Controls.Add(Me.RadioButtonCentroid)
    Me.GroupBox3.Controls.Add(Me.RadioButtonCp)
    Me.GroupBox3.Location = New System.Drawing.Point(12, 137)
    Me.GroupBox3.Name = "GroupBox3"
    Me.GroupBox3.Size = New System.Drawing.Size(495, 61)
    Me.GroupBox3.TabIndex = 5
    Me.GroupBox3.TabStop = False
    Me.GroupBox3.Text = "Center Point for Fixture Layout"
    '
    'RadioButtonCentroid
    '
    Me.RadioButtonCentroid.AutoSize = True
    Me.RadioButtonCentroid.Location = New System.Drawing.Point(249, 26)
    Me.RadioButtonCentroid.Name = "RadioButtonCentroid"
    Me.RadioButtonCentroid.Size = New System.Drawing.Size(132, 17)
    Me.RadioButtonCentroid.TabIndex = 3
    Me.RadioButtonCentroid.Text = "Centroid of Living Area"
    Me.RadioButtonCentroid.UseVisualStyleBackColor = True
    '
    'RadioButtonCp
    '
    Me.RadioButtonCp.AutoSize = True
    Me.RadioButtonCp.Checked = True
    Me.RadioButtonCp.Location = New System.Drawing.Point(34, 26)
    Me.RadioButtonCp.Name = "RadioButtonCp"
    Me.RadioButtonCp.Size = New System.Drawing.Size(165, 17)
    Me.RadioButtonCp.TabIndex = 2
    Me.RadioButtonCp.TabStop = True
    Me.RadioButtonCp.Text = "Mid Point of Total Dimensions"
    Me.RadioButtonCp.UseVisualStyleBackColor = True
    '
    'form_Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(519, 607)
    Me.Controls.Add(Me.GroupBox3)
    Me.Controls.Add(Me.GroupBoxElements)
    Me.Controls.Add(Me.GroupBox2)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.ButtonOk)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(535, 645)
    Me.Name = "form_Main"
    Me.Text = "X"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.GroupBox2.ResumeLayout(False)
    Me.GroupBox2.PerformLayout()
    CType(Me.NumericUpDownV, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NumericUpDownU, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBoxElements.ResumeLayout(False)
    CType(Me.DataGridViewRooms, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox3.ResumeLayout(False)
    Me.GroupBox3.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents ButtonOk As System.Windows.Forms.Button
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents ComboBoxSymbol As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
  Friend WithEvents NumericUpDownU As System.Windows.Forms.NumericUpDown
  Friend WithEvents TextBoxDistanceU As System.Windows.Forms.TextBox
  Friend WithEvents RadioButtonEvenly As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButtonExact As System.Windows.Forms.RadioButton
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents NumericUpDownV As System.Windows.Forms.NumericUpDown
  Friend WithEvents TextBoxDistanceV As System.Windows.Forms.TextBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents GroupBoxElements As System.Windows.Forms.GroupBox
  Friend WithEvents DataGridViewRooms As System.Windows.Forms.DataGridView
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
  Friend WithEvents RadioButtonCentroid As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButtonCp As System.Windows.Forms.RadioButton
  Friend WithEvents TextBoxHeight As System.Windows.Forms.TextBox
End Class

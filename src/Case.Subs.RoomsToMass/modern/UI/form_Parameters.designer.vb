<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_Parameters
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_Parameters))
    Me.ButtonGenerateMasses = New System.Windows.Forms.Button()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.DataGridViewParameters = New System.Windows.Forms.DataGridView()
    Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column2 = New System.Windows.Forms.DataGridViewComboBoxColumn()
    Me.GroupBox1.SuspendLayout()
    CType(DataGridViewParameters, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ButtonGenerateMasses
    '
    Me.ButtonGenerateMasses.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonGenerateMasses.Location = New System.Drawing.Point(281, 360)
    Me.ButtonGenerateMasses.Name = "ButtonGenerateMasses"
    Me.ButtonGenerateMasses.Size = New System.Drawing.Size(70, 40)
    Me.ButtonGenerateMasses.TabIndex = 20
    Me.ButtonGenerateMasses.Text = "OK"
    Me.ButtonGenerateMasses.UseVisualStyleBackColor = True
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(357, 360)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(70, 40)
    Me.ButtonCancel.TabIndex = 21
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(DataGridViewParameters)
    Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(415, 342)
    Me.GroupBox1.TabIndex = 22
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Parameters"
    '
    'DataGridViewParameters
    '
    Me.DataGridViewParameters.AllowUserToAddRows = False
    Me.DataGridViewParameters.AllowUserToDeleteRows = False
    Me.DataGridViewParameters.AllowUserToResizeRows = False
    Me.DataGridViewParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewParameters.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
    Me.DataGridViewParameters.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DataGridViewParameters.Location = New System.Drawing.Point(3, 16)
    Me.DataGridViewParameters.Name = "DataGridViewParameters"
    Me.DataGridViewParameters.RowHeadersVisible = False
    Me.DataGridViewParameters.Size = New System.Drawing.Size(409, 323)
    Me.DataGridViewParameters.TabIndex = 0
    '
    'Column1
    '
    Me.Column1.HeaderText = "Room Parameter"
    Me.Column1.MinimumWidth = 100
    Me.Column1.Name = "Column1"
    Me.Column1.ReadOnly = True
    Me.Column1.Width = 190
    '
    'Column2
    '
    Me.Column2.HeaderText = "Destination Mass Parameter"
    Me.Column2.MinimumWidth = 100
    Me.Column2.Name = "Column2"
    Me.Column2.Width = 200
    '
    'form_Parameters
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(439, 412)
    Me.Controls.Add(GroupBox1)
    Me.Controls.Add(ButtonGenerateMasses)
    Me.Controls.Add(ButtonCancel)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(455, 450)
    Me.Name = "form_Parameters"
    Me.Text = "Mass Parameter Mapping"
    Me.GroupBox1.ResumeLayout(False)
    CType(DataGridViewParameters, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ButtonGenerateMasses As System.Windows.Forms.Button
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents DataGridViewParameters As System.Windows.Forms.DataGridView
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewComboBoxColumn
End Class

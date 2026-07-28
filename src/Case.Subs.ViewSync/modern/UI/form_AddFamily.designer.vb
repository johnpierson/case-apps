<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_AddFamily
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_AddFamily))
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.DataGridViewFamilies = New System.Windows.Forms.DataGridView()
    Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.ButtonOk = New System.Windows.Forms.Button()
    Me.GroupBox2.SuspendLayout()
    CType(Me.DataGridViewFamilies, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'GroupBox2
    '
    Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox2.Controls.Add(Me.DataGridViewFamilies)
    Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(460, 237)
    Me.GroupBox2.TabIndex = 1
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Select the Detail Item Family and Types to Add"
    '
    'DataGridViewFamilies
    '
    Me.DataGridViewFamilies.AllowUserToAddRows = False
    Me.DataGridViewFamilies.AllowUserToDeleteRows = False
    Me.DataGridViewFamilies.AllowUserToResizeRows = False
    Me.DataGridViewFamilies.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3})
    Me.DataGridViewFamilies.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DataGridViewFamilies.Location = New System.Drawing.Point(3, 16)
    Me.DataGridViewFamilies.Name = "DataGridViewFamilies"
    Me.DataGridViewFamilies.ReadOnly = True
    Me.DataGridViewFamilies.RowHeadersVisible = False
    Me.DataGridViewFamilies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewFamilies.Size = New System.Drawing.Size(454, 218)
    Me.DataGridViewFamilies.TabIndex = 0
    '
    'Column1
    '
    Me.Column1.DataPropertyName = "FamilyName"
    Me.Column1.HeaderText = "Family"
    Me.Column1.MinimumWidth = 50
    Me.Column1.Name = "Column1"
    Me.Column1.ReadOnly = True
    Me.Column1.Width = 200
    '
    'Column2
    '
    Me.Column2.DataPropertyName = "TypeName"
    Me.Column2.HeaderText = "Type"
    Me.Column2.MinimumWidth = 50
    Me.Column2.Name = "Column2"
    Me.Column2.ReadOnly = True
    Me.Column2.Width = 200
    '
    'Column3
    '
    Me.Column3.DataPropertyName = "DisplayName"
    Me.Column3.HeaderText = "Display"
    Me.Column3.Name = "Column3"
    Me.Column3.ReadOnly = True
    Me.Column3.Visible = False
    Me.Column3.Width = 66
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonCancel.Location = New System.Drawing.Point(394, 255)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(75, 40)
    Me.ButtonCancel.TabIndex = 4
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'ButtonOk
    '
    Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonOk.Location = New System.Drawing.Point(253, 255)
    Me.ButtonOk.Name = "ButtonOk"
    Me.ButtonOk.Size = New System.Drawing.Size(135, 40)
    Me.ButtonOk.TabIndex = 3
    Me.ButtonOk.Text = "Add Selected"
    Me.ButtonOk.UseVisualStyleBackColor = True
    '
    'form_AddFamily
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(484, 307)
    Me.Controls.Add(Me.ButtonOk)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.GroupBox2)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(500, 345)
    Me.Name = "form_AddFamily"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "X"
    Me.GroupBox2.ResumeLayout(False)
    CType(Me.DataGridViewFamilies, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
  Friend WithEvents DataGridViewFamilies As System.Windows.Forms.DataGridView
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents ButtonOk As System.Windows.Forms.Button
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class

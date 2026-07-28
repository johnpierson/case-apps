<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_Config
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_Config))
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.PictureBoxParamIni = New System.Windows.Forms.PictureBox()
    Me.ButtonConfigNew = New System.Windows.Forms.Button()
    Me.ButtonConfigOpen = New System.Windows.Forms.Button()
    Me.LabelIniFile = New System.Windows.Forms.Label()
    Me.PictureBox4 = New System.Windows.Forms.PictureBox()
    Me.GroupBoxDetailItems = New System.Windows.Forms.GroupBox()
    Me.DataGridViewFamilies = New System.Windows.Forms.DataGridView()
    Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.ButtonFamilyAddModel = New System.Windows.Forms.Button()
    Me.ButtonFamilyRemove = New System.Windows.Forms.Button()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.OpenFileDialogIni = New System.Windows.Forms.OpenFileDialog()
    Me.SaveFileDialogIni = New System.Windows.Forms.SaveFileDialog()
    Me.GroupBoxModelData = New System.Windows.Forms.GroupBox()
    Me.TextBoxModelPath = New System.Windows.Forms.TextBox()
    Me.ButtonModelBrowse = New System.Windows.Forms.Button()
    Me.PictureBox3 = New System.Windows.Forms.PictureBox()
    Me.OpenFileDialogRvt = New System.Windows.Forms.OpenFileDialog()
    Me.TabControl1 = New System.Windows.Forms.TabControl()
    Me.TabPage1 = New System.Windows.Forms.TabPage()
    Me.GroupBoxLogs = New System.Windows.Forms.GroupBox()
    Me.PictureBox5 = New System.Windows.Forms.PictureBox()
    Me.CheckBoxLogTagPlacements = New System.Windows.Forms.CheckBox()
    Me.CheckBoxLogSyncs = New System.Windows.Forms.CheckBox()
    Me.CheckBoxLogConfigs = New System.Windows.Forms.CheckBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.TextBoxStaticHelpTab1 = New System.Windows.Forms.TextBox()
    Me.TabPage2 = New System.Windows.Forms.TabPage()
    Me.GroupBoxParameters = New System.Windows.Forms.GroupBox()
    Me.ButtonParamSelect = New System.Windows.Forms.Button()
    Me.LabelSharedParamView = New System.Windows.Forms.Label()
    Me.LabelSharedParamSheet = New System.Windows.Forms.Label()
    Me.PictureBox2 = New System.Windows.Forms.PictureBox()
    Me.TextBoxStaticHelpTab2 = New System.Windows.Forms.TextBox()
    Me.GroupBox1.SuspendLayout()
    CType(Me.PictureBoxParamIni, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBoxDetailItems.SuspendLayout()
    CType(Me.DataGridViewFamilies, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBoxModelData.SuspendLayout()
    CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabControl1.SuspendLayout()
    Me.TabPage1.SuspendLayout()
    Me.GroupBoxLogs.SuspendLayout()
    CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage2.SuspendLayout()
    Me.GroupBoxParameters.SuspendLayout()
    CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.PictureBoxParamIni)
    Me.GroupBox1.Controls.Add(Me.ButtonConfigNew)
    Me.GroupBox1.Controls.Add(Me.ButtonConfigOpen)
    Me.GroupBox1.Controls.Add(Me.LabelIniFile)
    Me.GroupBox1.Controls.Add(Me.PictureBox4)
    Me.GroupBox1.Location = New System.Drawing.Point(12, 244)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(514, 136)
    Me.GroupBox1.TabIndex = 2
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Data Storage Location"
    '
    'PictureBoxParamIni
    '
    Me.PictureBoxParamIni.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.PictureBoxParamIni.Image = Global.[Case].Subs.ViewSync.My.Resources.Resources.ok
    Me.PictureBoxParamIni.Location = New System.Drawing.Point(445, 19)
    Me.PictureBoxParamIni.Name = "PictureBoxParamIni"
    Me.PictureBoxParamIni.Size = New System.Drawing.Size(55, 55)
    Me.PictureBoxParamIni.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
    Me.PictureBoxParamIni.TabIndex = 15
    Me.PictureBoxParamIni.TabStop = False
    '
    'ButtonConfigNew
    '
    Me.ButtonConfigNew.Location = New System.Drawing.Point(224, 35)
    Me.ButtonConfigNew.Name = "ButtonConfigNew"
    Me.ButtonConfigNew.Size = New System.Drawing.Size(80, 29)
    Me.ButtonConfigNew.TabIndex = 4
    Me.ButtonConfigNew.Text = "Save New"
    Me.ButtonConfigNew.UseVisualStyleBackColor = True
    '
    'ButtonConfigOpen
    '
    Me.ButtonConfigOpen.Location = New System.Drawing.Point(121, 35)
    Me.ButtonConfigOpen.Name = "ButtonConfigOpen"
    Me.ButtonConfigOpen.Size = New System.Drawing.Size(80, 29)
    Me.ButtonConfigOpen.TabIndex = 3
    Me.ButtonConfigOpen.Text = "Open"
    Me.ButtonConfigOpen.UseVisualStyleBackColor = True
    '
    'LabelIniFile
    '
    Me.LabelIniFile.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.LabelIniFile.Location = New System.Drawing.Point(121, 77)
    Me.LabelIniFile.Name = "LabelIniFile"
    Me.LabelIniFile.Size = New System.Drawing.Size(379, 56)
    Me.LabelIniFile.TabIndex = 1
    Me.LabelIniFile.Text = "No Data File"
    '
    'PictureBox4
    '
    Me.PictureBox4.Image = Global.[Case].Subs.ViewSync.My.Resources.Resources.basic_data
    Me.PictureBox4.Location = New System.Drawing.Point(26, 35)
    Me.PictureBox4.Name = "PictureBox4"
    Me.PictureBox4.Size = New System.Drawing.Size(65, 65)
    Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
    Me.PictureBox4.TabIndex = 14
    Me.PictureBox4.TabStop = False
    '
    'GroupBoxDetailItems
    '
    Me.GroupBoxDetailItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxDetailItems.Controls.Add(Me.DataGridViewFamilies)
    Me.GroupBoxDetailItems.Controls.Add(Me.ButtonFamilyAddModel)
    Me.GroupBoxDetailItems.Controls.Add(Me.ButtonFamilyRemove)
    Me.GroupBoxDetailItems.Location = New System.Drawing.Point(6, 230)
    Me.GroupBoxDetailItems.Name = "GroupBoxDetailItems"
    Me.GroupBoxDetailItems.Size = New System.Drawing.Size(520, 287)
    Me.GroupBoxDetailItems.TabIndex = 3
    Me.GroupBoxDetailItems.TabStop = False
    Me.GroupBoxDetailItems.Text = "Registered View Reference Tag Families (Detail Items)"
    '
    'DataGridViewFamilies
    '
    Me.DataGridViewFamilies.AllowUserToAddRows = False
    Me.DataGridViewFamilies.AllowUserToDeleteRows = False
    Me.DataGridViewFamilies.AllowUserToResizeRows = False
    Me.DataGridViewFamilies.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.DataGridViewFamilies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewFamilies.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4})
    Me.DataGridViewFamilies.Location = New System.Drawing.Point(6, 19)
    Me.DataGridViewFamilies.Name = "DataGridViewFamilies"
    Me.DataGridViewFamilies.ReadOnly = True
    Me.DataGridViewFamilies.RowHeadersVisible = False
    Me.DataGridViewFamilies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewFamilies.Size = New System.Drawing.Size(508, 216)
    Me.DataGridViewFamilies.TabIndex = 0
    '
    'Column1
    '
    Me.Column1.DataPropertyName = "FamilyName"
    Me.Column1.HeaderText = "Family Name"
    Me.Column1.Name = "Column1"
    Me.Column1.ReadOnly = True
    Me.Column1.Width = 220
    '
    'Column2
    '
    Me.Column2.DataPropertyName = "TypeName"
    Me.Column2.HeaderText = "Type Name"
    Me.Column2.Name = "Column2"
    Me.Column2.ReadOnly = True
    Me.Column2.Width = 220
    '
    'Column3
    '
    Me.Column3.DataPropertyName = "DisplayName"
    Me.Column3.HeaderText = "DisplayName"
    Me.Column3.Name = "Column3"
    Me.Column3.ReadOnly = True
    Me.Column3.Visible = False
    '
    'Column4
    '
    Me.Column4.DataPropertyName = "InstQty"
    Me.Column4.HeaderText = "Qty"
    Me.Column4.MinimumWidth = 60
    Me.Column4.Name = "Column4"
    Me.Column4.ReadOnly = True
    Me.Column4.Width = 60
    '
    'ButtonFamilyAddModel
    '
    Me.ButtonFamilyAddModel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonFamilyAddModel.Location = New System.Drawing.Point(334, 241)
    Me.ButtonFamilyAddModel.Name = "ButtonFamilyAddModel"
    Me.ButtonFamilyAddModel.Size = New System.Drawing.Size(180, 40)
    Me.ButtonFamilyAddModel.TabIndex = 7
    Me.ButtonFamilyAddModel.Text = "Add From Model"
    Me.ButtonFamilyAddModel.UseVisualStyleBackColor = True
    '
    'ButtonFamilyRemove
    '
    Me.ButtonFamilyRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ButtonFamilyRemove.Location = New System.Drawing.Point(6, 241)
    Me.ButtonFamilyRemove.Name = "ButtonFamilyRemove"
    Me.ButtonFamilyRemove.Size = New System.Drawing.Size(180, 40)
    Me.ButtonFamilyRemove.TabIndex = 8
    Me.ButtonFamilyRemove.Text = "Remove Selected"
    Me.ButtonFamilyRemove.UseVisualStyleBackColor = True
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(18, 71)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(78, 13)
    Me.Label2.TabIndex = 3
    Me.Label2.Text = "Sheet Number:"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(18, 38)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(73, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "View Number:"
    '
    'OpenFileDialogIni
    '
    Me.OpenFileDialogIni.DefaultExt = "*.ini"
    Me.OpenFileDialogIni.FileName = "CaseViewSync.ini"
    Me.OpenFileDialogIni.Filter = "Ini Files | *.ini"
    Me.OpenFileDialogIni.Title = "Select an Ini File"
    '
    'SaveFileDialogIni
    '
    Me.SaveFileDialogIni.DefaultExt = "*.ini"
    Me.SaveFileDialogIni.FileName = "CaseDesignViewSync.ini"
    Me.SaveFileDialogIni.Filter = "Ini Files | *ini"
    Me.SaveFileDialogIni.Title = "Save New View Sync INI File"
    '
    'GroupBoxModelData
    '
    Me.GroupBoxModelData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxModelData.Controls.Add(Me.TextBoxModelPath)
    Me.GroupBoxModelData.Controls.Add(Me.ButtonModelBrowse)
    Me.GroupBoxModelData.Controls.Add(Me.PictureBox3)
    Me.GroupBoxModelData.Location = New System.Drawing.Point(12, 114)
    Me.GroupBoxModelData.Name = "GroupBoxModelData"
    Me.GroupBoxModelData.Size = New System.Drawing.Size(514, 124)
    Me.GroupBoxModelData.TabIndex = 4
    Me.GroupBoxModelData.TabStop = False
    Me.GroupBoxModelData.Text = "Master View Source Model"
    '
    'TextBoxModelPath
    '
    Me.TextBoxModelPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBoxModelPath.Location = New System.Drawing.Point(121, 84)
    Me.TextBoxModelPath.Name = "TextBoxModelPath"
    Me.TextBoxModelPath.Size = New System.Drawing.Size(369, 20)
    Me.TextBoxModelPath.TabIndex = 6
    '
    'ButtonModelBrowse
    '
    Me.ButtonModelBrowse.Location = New System.Drawing.Point(121, 29)
    Me.ButtonModelBrowse.Name = "ButtonModelBrowse"
    Me.ButtonModelBrowse.Size = New System.Drawing.Size(80, 29)
    Me.ButtonModelBrowse.TabIndex = 5
    Me.ButtonModelBrowse.Text = "Browse"
    Me.ButtonModelBrowse.UseVisualStyleBackColor = True
    '
    'PictureBox3
    '
    Me.PictureBox3.Image = Global.[Case].Subs.ViewSync.My.Resources.Resources.view_process_all_tree
    Me.PictureBox3.Location = New System.Drawing.Point(26, 35)
    Me.PictureBox3.Name = "PictureBox3"
    Me.PictureBox3.Size = New System.Drawing.Size(65, 65)
    Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
    Me.PictureBox3.TabIndex = 13
    Me.PictureBox3.TabStop = False
    '
    'OpenFileDialogRvt
    '
    Me.OpenFileDialogRvt.DefaultExt = "*.rvt"
    Me.OpenFileDialogRvt.Filter = "Revit Models | *.rvt"
    Me.OpenFileDialogRvt.Title = "Select the Master Document Model"
    '
    'TabControl1
    '
    Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TabControl1.Controls.Add(Me.TabPage1)
    Me.TabControl1.Controls.Add(Me.TabPage2)
    Me.TabControl1.Location = New System.Drawing.Point(12, 12)
    Me.TabControl1.Name = "TabControl1"
    Me.TabControl1.SelectedIndex = 0
    Me.TabControl1.Size = New System.Drawing.Size(540, 549)
    Me.TabControl1.TabIndex = 6
    '
    'TabPage1
    '
    Me.TabPage1.Controls.Add(Me.GroupBoxLogs)
    Me.TabPage1.Controls.Add(Me.PictureBox1)
    Me.TabPage1.Controls.Add(Me.TextBoxStaticHelpTab1)
    Me.TabPage1.Controls.Add(Me.GroupBoxModelData)
    Me.TabPage1.Controls.Add(Me.GroupBox1)
    Me.TabPage1.Location = New System.Drawing.Point(4, 22)
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage1.Size = New System.Drawing.Size(532, 523)
    Me.TabPage1.TabIndex = 0
    Me.TabPage1.Text = "Project Information: Master View Model and Data"
    Me.TabPage1.UseVisualStyleBackColor = True
    '
    'GroupBoxLogs
    '
    Me.GroupBoxLogs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxLogs.Controls.Add(Me.PictureBox5)
    Me.GroupBoxLogs.Controls.Add(Me.CheckBoxLogTagPlacements)
    Me.GroupBoxLogs.Controls.Add(Me.CheckBoxLogSyncs)
    Me.GroupBoxLogs.Controls.Add(Me.CheckBoxLogConfigs)
    Me.GroupBoxLogs.Controls.Add(Me.Label5)
    Me.GroupBoxLogs.Location = New System.Drawing.Point(12, 386)
    Me.GroupBoxLogs.Name = "GroupBoxLogs"
    Me.GroupBoxLogs.Size = New System.Drawing.Size(514, 127)
    Me.GroupBoxLogs.TabIndex = 15
    Me.GroupBoxLogs.TabStop = False
    Me.GroupBoxLogs.Text = "Event Logging"
    '
    'PictureBox5
    '
    Me.PictureBox5.Image = Global.[Case].Subs.ViewSync.My.Resources.Resources.edit_file
    Me.PictureBox5.Location = New System.Drawing.Point(26, 35)
    Me.PictureBox5.Name = "PictureBox5"
    Me.PictureBox5.Size = New System.Drawing.Size(65, 65)
    Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
    Me.PictureBox5.TabIndex = 15
    Me.PictureBox5.TabStop = False
    '
    'CheckBoxLogTagPlacements
    '
    Me.CheckBoxLogTagPlacements.AutoSize = True
    Me.CheckBoxLogTagPlacements.Checked = True
    Me.CheckBoxLogTagPlacements.CheckState = System.Windows.Forms.CheckState.Checked
    Me.CheckBoxLogTagPlacements.Location = New System.Drawing.Point(345, 59)
    Me.CheckBoxLogTagPlacements.Name = "CheckBoxLogTagPlacements"
    Me.CheckBoxLogTagPlacements.Size = New System.Drawing.Size(124, 17)
    Me.CheckBoxLogTagPlacements.TabIndex = 12
    Me.CheckBoxLogTagPlacements.Text = "Log Tag Placements"
    Me.CheckBoxLogTagPlacements.UseVisualStyleBackColor = True
    '
    'CheckBoxLogSyncs
    '
    Me.CheckBoxLogSyncs.AutoSize = True
    Me.CheckBoxLogSyncs.Checked = True
    Me.CheckBoxLogSyncs.CheckState = System.Windows.Forms.CheckState.Checked
    Me.CheckBoxLogSyncs.Location = New System.Drawing.Point(124, 83)
    Me.CheckBoxLogSyncs.Name = "CheckBoxLogSyncs"
    Me.CheckBoxLogSyncs.Size = New System.Drawing.Size(127, 17)
    Me.CheckBoxLogSyncs.TabIndex = 13
    Me.CheckBoxLogSyncs.Text = "Log Synchronizations"
    Me.CheckBoxLogSyncs.UseVisualStyleBackColor = True
    '
    'CheckBoxLogConfigs
    '
    Me.CheckBoxLogConfigs.AutoSize = True
    Me.CheckBoxLogConfigs.Checked = True
    Me.CheckBoxLogConfigs.CheckState = System.Windows.Forms.CheckState.Checked
    Me.CheckBoxLogConfigs.Location = New System.Drawing.Point(124, 59)
    Me.CheckBoxLogConfigs.Name = "CheckBoxLogConfigs"
    Me.CheckBoxLogConfigs.Size = New System.Drawing.Size(154, 17)
    Me.CheckBoxLogConfigs.TabIndex = 14
    Me.CheckBoxLogConfigs.Text = "Log Configuration Changes"
    Me.CheckBoxLogConfigs.UseVisualStyleBackColor = True
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(121, 35)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(327, 13)
    Me.Label5.TabIndex = 13
    Me.Label5.Text = "File will be saved adjacent to the main INI file as Case.ViewSync.log"
    '
    'PictureBox1
    '
    Me.PictureBox1.Image = Global.[Case].Subs.ViewSync.My.Resources.Resources.imgres
    Me.PictureBox1.Location = New System.Drawing.Point(12, 6)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(110, 102)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 6
    Me.PictureBox1.TabStop = False
    '
    'TextBoxStaticHelpTab1
    '
    Me.TextBoxStaticHelpTab1.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.TextBoxStaticHelpTab1.Location = New System.Drawing.Point(133, 18)
    Me.TextBoxStaticHelpTab1.Multiline = True
    Me.TextBoxStaticHelpTab1.Name = "TextBoxStaticHelpTab1"
    Me.TextBoxStaticHelpTab1.Size = New System.Drawing.Size(379, 90)
    Me.TextBoxStaticHelpTab1.TabIndex = 5
    Me.TextBoxStaticHelpTab1.Text = resources.GetString("TextBoxStaticHelpTab1.Text")
    '
    'TabPage2
    '
    Me.TabPage2.Controls.Add(Me.GroupBoxParameters)
    Me.TabPage2.Controls.Add(Me.PictureBox2)
    Me.TabPage2.Controls.Add(Me.TextBoxStaticHelpTab2)
    Me.TabPage2.Controls.Add(Me.GroupBoxDetailItems)
    Me.TabPage2.Location = New System.Drawing.Point(4, 22)
    Me.TabPage2.Name = "TabPage2"
    Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage2.Size = New System.Drawing.Size(532, 523)
    Me.TabPage2.TabIndex = 1
    Me.TabPage2.Text = "Detail Item: Pseudo View Tag Families and Parameters"
    Me.TabPage2.UseVisualStyleBackColor = True
    '
    'GroupBoxParameters
    '
    Me.GroupBoxParameters.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxParameters.Controls.Add(Me.ButtonParamSelect)
    Me.GroupBoxParameters.Controls.Add(Me.LabelSharedParamView)
    Me.GroupBoxParameters.Controls.Add(Me.LabelSharedParamSheet)
    Me.GroupBoxParameters.Controls.Add(Me.Label1)
    Me.GroupBoxParameters.Controls.Add(Me.Label2)
    Me.GroupBoxParameters.Location = New System.Drawing.Point(6, 114)
    Me.GroupBoxParameters.Name = "GroupBoxParameters"
    Me.GroupBoxParameters.Size = New System.Drawing.Size(520, 110)
    Me.GroupBoxParameters.TabIndex = 12
    Me.GroupBoxParameters.TabStop = False
    Me.GroupBoxParameters.Text = "Detail Item Shared Parameters for Pseudo View Tag Data"
    '
    'ButtonParamSelect
    '
    Me.ButtonParamSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonParamSelect.Location = New System.Drawing.Point(403, 38)
    Me.ButtonParamSelect.Name = "ButtonParamSelect"
    Me.ButtonParamSelect.Size = New System.Drawing.Size(111, 40)
    Me.ButtonParamSelect.TabIndex = 6
    Me.ButtonParamSelect.Text = "Select from Model"
    Me.ButtonParamSelect.UseVisualStyleBackColor = True
    '
    'LabelSharedParamView
    '
    Me.LabelSharedParamView.AutoSize = True
    Me.LabelSharedParamView.ForeColor = System.Drawing.Color.Blue
    Me.LabelSharedParamView.Location = New System.Drawing.Point(122, 38)
    Me.LabelSharedParamView.Name = "LabelSharedParamView"
    Me.LabelSharedParamView.Size = New System.Drawing.Size(43, 13)
    Me.LabelSharedParamView.TabIndex = 4
    Me.LabelSharedParamView.Text = "<none>"
    '
    'LabelSharedParamSheet
    '
    Me.LabelSharedParamSheet.AutoSize = True
    Me.LabelSharedParamSheet.ForeColor = System.Drawing.Color.Blue
    Me.LabelSharedParamSheet.Location = New System.Drawing.Point(122, 71)
    Me.LabelSharedParamSheet.Name = "LabelSharedParamSheet"
    Me.LabelSharedParamSheet.Size = New System.Drawing.Size(43, 13)
    Me.LabelSharedParamSheet.TabIndex = 5
    Me.LabelSharedParamSheet.Text = "<none>"
    '
    'PictureBox2
    '
    Me.PictureBox2.Image = Global.[Case].Subs.ViewSync.My.Resources.Resources.imgres
    Me.PictureBox2.Location = New System.Drawing.Point(12, 6)
    Me.PictureBox2.Name = "PictureBox2"
    Me.PictureBox2.Size = New System.Drawing.Size(110, 102)
    Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox2.TabIndex = 11
    Me.PictureBox2.TabStop = False
    '
    'TextBoxStaticHelpTab2
    '
    Me.TextBoxStaticHelpTab2.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.TextBoxStaticHelpTab2.Location = New System.Drawing.Point(133, 18)
    Me.TextBoxStaticHelpTab2.Multiline = True
    Me.TextBoxStaticHelpTab2.Name = "TextBoxStaticHelpTab2"
    Me.TextBoxStaticHelpTab2.Size = New System.Drawing.Size(379, 90)
    Me.TextBoxStaticHelpTab2.TabIndex = 10
    Me.TextBoxStaticHelpTab2.Text = "Any tag family to be used to represent pseudo view tags data must be registered h" & _
    "ere and contain the shared parameters selected below for 'View Number' and 'Shee" & _
    "t Number'"
    '
    'form_Config
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(564, 573)
    Me.Controls.Add(Me.TabControl1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(580, 611)
    Me.Name = "form_Config"
    Me.Text = "View Sync Configuration"
    Me.GroupBox1.ResumeLayout(False)
    CType(Me.PictureBoxParamIni, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBoxDetailItems.ResumeLayout(False)
    CType(Me.DataGridViewFamilies, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBoxModelData.ResumeLayout(False)
    Me.GroupBoxModelData.PerformLayout()
    CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabControl1.ResumeLayout(False)
    Me.TabPage1.ResumeLayout(False)
    Me.TabPage1.PerformLayout()
    Me.GroupBoxLogs.ResumeLayout(False)
    Me.GroupBoxLogs.PerformLayout()
    CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage2.ResumeLayout(False)
    Me.TabPage2.PerformLayout()
    Me.GroupBoxParameters.ResumeLayout(False)
    Me.GroupBoxParameters.PerformLayout()
    CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents LabelIniFile As System.Windows.Forms.Label
  Friend WithEvents GroupBoxDetailItems As System.Windows.Forms.GroupBox
  Friend WithEvents ButtonFamilyRemove As System.Windows.Forms.Button
  Friend WithEvents ButtonFamilyAddModel As System.Windows.Forms.Button
  Friend WithEvents DataGridViewFamilies As System.Windows.Forms.DataGridView
  Friend WithEvents ButtonConfigNew As System.Windows.Forms.Button
  Friend WithEvents ButtonConfigOpen As System.Windows.Forms.Button
  Friend WithEvents OpenFileDialogIni As System.Windows.Forms.OpenFileDialog
  Friend WithEvents SaveFileDialogIni As System.Windows.Forms.SaveFileDialog
  Friend WithEvents GroupBoxModelData As System.Windows.Forms.GroupBox
  Friend WithEvents ButtonModelBrowse As System.Windows.Forms.Button
  Friend WithEvents OpenFileDialogRvt As System.Windows.Forms.OpenFileDialog
  Friend WithEvents TextBoxModelPath As System.Windows.Forms.TextBox
  Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
  Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
  Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents TextBoxStaticHelpTab1 As System.Windows.Forms.TextBox
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents CheckBoxLogConfigs As System.Windows.Forms.CheckBox
  Friend WithEvents CheckBoxLogTagPlacements As System.Windows.Forms.CheckBox
  Friend WithEvents CheckBoxLogSyncs As System.Windows.Forms.CheckBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
  Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
  Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
  Friend WithEvents GroupBoxLogs As System.Windows.Forms.GroupBox
  Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
  Friend WithEvents TextBoxStaticHelpTab2 As System.Windows.Forms.TextBox
  Friend WithEvents GroupBoxParameters As System.Windows.Forms.GroupBox
  Friend WithEvents PictureBoxParamIni As System.Windows.Forms.PictureBox
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents ButtonParamSelect As System.Windows.Forms.Button
  Friend WithEvents LabelSharedParamView As System.Windows.Forms.Label
  Friend WithEvents LabelSharedParamSheet As System.Windows.Forms.Label
End Class

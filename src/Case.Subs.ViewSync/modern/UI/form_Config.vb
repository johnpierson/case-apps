Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports System.Windows.Forms
Imports System.IO
Imports [Case].Subs.ViewSync.Data

Public Class form_Config

  Private _s As clsSettings

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s

  End Sub

#Region "Private Members"

  ''' <summary>
  ''' Instances for each family
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetInstanceCountsForFamilies()

    Try

      ' Gather Symbols and Instances
      _s.GetSymbols()
      _s.GetInstances()

      ' Process Each
      For Each x In _s.FamTagSymbols
        For Each f In _s.ConfigData.Families
          If f.DisplayName.ToLower = (x.Family.Name & " (" & x.Name & ")").ToLower Then

            ' This is the Type
            f.idFamily = x.Id.Value
            f.InstQty = _s.FamTagInst(x.Id.Value).Count
            Exit For

          End If
        Next
      Next

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Form Controls by Config Settings
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub SetFormControlsFromConfig()

    ' Config File?
    If _s.ConfigData Is Nothing Then
      Return
    End If

    Try

      ' Valid?
      If _s.ConfigData.isValid = True Then
        SetFormControlsEnable(True, GroupBoxModelData)
        SetFormControlsEnable(True, GroupBoxLogs)
        TextBoxModelPath.Text = _s.ConfigData.DocumentationModelPath
        CheckBoxLogConfigs.Checked = _s.ConfigData.LogConfig
        CheckBoxLogSyncs.Checked = _s.ConfigData.LogSyncs
        CheckBoxLogTagPlacements.Checked = _s.ConfigData.LogPlacements
        LabelSharedParamView.Text = _s.ConfigData.ParamView
        LabelSharedParamSheet.Text = _s.ConfigData.ParamShtn
      End If

    Catch
    End Try

    Try

      ' Master Doc for Views
      If Not String.IsNullOrEmpty(_s.ConfigData.DocumentationModelPath) Then

        ' Exists?
        If File.Exists(_s.ConfigData.DocumentationModelPath) Then

          ' Master Documentation Model
          TextBoxModelPath.Text = _s.ConfigData.DocumentationModelPath

        Else

          ' Bad Path
          If Not _s.ConfigData.DocumentationModelPath.ToLower.Contains("file not found!") Then
            _s.ConfigData.DocumentationModelPath = "File Not Found!!!!  " & _s.ConfigData.DocumentationModelPath
          End If
          TextBoxModelPath.Text = _s.ConfigData.DocumentationModelPath

        End If

      End If

    Catch
    End Try

    ' List of clsFam
    Dim m_fams As New List(Of clsFam)

    Try

      ' From Config File
      For Each x As clsFam In _s.ConfigData.Families
        m_fams.Add(x)
      Next

      ' Bind to Control
      DataGridViewFamilies.DataSource = m_fams

    Catch
    End Try

    Try

      ' Param Button
      If DataGridViewFamilies.Rows.Count > 0 Then
        ButtonParamSelect.Enabled = True
        ButtonParamSelect.Text = "Select from Model"
      Else
        ButtonParamSelect.Enabled = False
        ButtonParamSelect.Text = "Families First"
      End If

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Enable or Disable Controls by Groupbox
  ''' </summary>
  ''' <param name="isEnabled"></param>
  ''' <param name="gb"></param>
  ''' <remarks></remarks>
  Private Sub SetFormControlsEnable(isEnabled As Boolean, gb As GroupBox)

    Try

      ' Set Controls
      For Each x As System.Windows.Forms.Control In gb.Controls
        x.Enabled = isEnabled
      Next

    Catch
    End Try

  End Sub

#End Region

#Region "Form Controls & Events"

  ''' <summary>
  ''' Setup
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_Config_Load(sender As Object, e As EventArgs) Handles Me.Load

    ' Controls
    PictureBoxParamIni.Visible = False
    SetFormControlsEnable(False, GroupBoxModelData)
    SetFormControlsEnable(False, GroupBoxLogs)

    ' Config State
    Select Case _s.ConfigData.ConfigDataState

      Case EnumCfgState.IsOk

        Try

          ' File Exists
          LabelIniFile.Text = _s.ConfigData.ConfigPath
          PictureBoxParamIni.Visible = True

          ' Type Data
          GetInstanceCountsForFamilies()

          ' Controls
          SetFormControlsFromConfig()

        Catch
        End Try

      Case EnumCfgState.IsError

        Try

          ' Message, Log & Label
          Dim m_msg As String = "Error reading config file path"
          _s.ConfigData.WriteLogLine(m_msg, _s.DocName, EnumLogKind.IsConfig)
          LabelIniFile.Text = m_msg

        Catch
        End Try

      Case EnumCfgState.IsNullValue

        Try

          ' Message, Log & Label
          Dim m_msg As String = "No Config File Path Saved in This Model"
          _s.ConfigData.WriteLogLine(m_msg, _s.DocName, EnumLogKind.IsConfig)
          LabelIniFile.Text = m_msg

        Catch
        End Try

      Case EnumCfgState.IsPathNotFound

        Try

          ' Message, Log & Label
          Dim m_msg As String = "Path Not Found: " & _s.ConfigData.ConfigPath
          _s.ConfigData.WriteLogLine(m_msg, _s.DocName, EnumLogKind.IsConfig)
          LabelIniFile.Text = m_msg

        Catch
        End Try

    End Select

  End Sub

  ''' <summary>
  ''' Clear the File Lock
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_Config_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    Try
      ' Clear Lock
      _s.ConfigData.LockedBy = ""
      _s.ConfigData.UpdateConfigFile()
    Catch
    End Try
  End Sub

  ''' <summary>
  ''' Check for Locked File
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_Config_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    ' Title
    Text = "Subscription View Tag Sync Configuration" & _s.Version

    Try

      ' Locked?
      If _s.ConfigData.ConfigDataState = EnumCfgState.IsOk Then

        If _s.ConfigData.IsConfigLocked = True Then

          ' Task Dialog
          Using td As New Autodesk.Revit.UI.TaskDialog("Configuration File Locked")
            With td

              .MainInstruction = "Config File Locked By: " & _s.ConfigData.LockedBy
              .MainContent = "If you think this may be in error, you can reset this file." & vbCr &
                             "Resetting this file will result in total loss of configuration changes being made by the other user." & vbCr & vbCr & _s.ConfigData.ConfigPath

              .AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "Remove Lock from Config File")
              .AddCommandLink(TaskDialogCommandLinkId.CommandLink2, "Cancel and do Nothing")


            End With

            ' Result
            Select Case td.Show

              Case Autodesk.Revit.UI.TaskDialogResult.CommandLink1
                If MsgBox("This will result in total loss for whoever might be editing the file!",
                          MsgBoxStyle.YesNo,
                          "Are You Sure??") = MsgBoxResult.Yes Then

                  ' Update and Remove Lock
                  _s.ConfigData.LockedBy = ""
                  If _s.ConfigData.UpdateConfigFile = False Then

                    ' Failed - Close
                    MsgBox("Failed to remove lock from config file...", MsgBoxStyle.Critical, "Error")
                    Close()

                  End If

                Else

                  ' Close
                  Close()

                End If

              Case Else

                ' Close
                Close()

            End Select


          End Using

        End If

      End If

    Catch
    End Try

    ' Update Config File
    _s.ConfigData.UpdateConfigFile()

  End Sub

#Region "Form Controls & Events - Info Data Protection"

  ''' <summary>
  ''' Prevent Text Change
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub TextBoxStaticHelpTab1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxStaticHelpTab1.KeyDown
    e.Handled = True
  End Sub

  ''' <summary>
  ''' Prevent Text Change
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub TextBoxStaticHelpTab2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxStaticHelpTab2.KeyDown
    e.Handled = True
  End Sub

#End Region

#Region "Form Controls & Events - Master Model"

  ''' <summary>
  ''' Enter Key in Model Box
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub TextBoxModelPath_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxModelPath.KeyDown
    If e.KeyCode = Keys.Enter Then
      ButtonModelBrowse_Click(Nothing, Nothing)
    End If
  End Sub

  ''' <summary>
  ''' Set Master Documentation Model
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonModelBrowse_Click(sender As Object, e As EventArgs) Handles ButtonModelBrowse.Click

    If sender Is Nothing And e Is Nothing Then

      ' Update the Config File
      _s.ConfigData.DocumentationModelPath = TextBoxModelPath.Text

      ' Update the File
      _s.ConfigData.UpdateConfigFile()

      ' Log
      _s.ConfigData.WriteLogLine("Updated master model document path to: " & TextBoxModelPath.Text, _s.DocName, EnumLogKind.IsConfig)

    Else

      ' Get the Model
      If OpenFileDialogRvt.ShowDialog = DialogResult.OK Then

        ' File Name
        Dim m_fileName As String = OpenFileDialogRvt.FileName

        ' Does it Exist
        If Not String.IsNullOrEmpty(m_fileName) And File.Exists(m_fileName) Then

          ' Update the Config File
          _s.ConfigData.DocumentationModelPath = m_fileName

          ' Update the File
          _s.ConfigData.UpdateConfigFile()

          ' Update Textbox
          TextBoxModelPath.Text = m_fileName

          ' Log
          _s.ConfigData.WriteLogLine("Updated master model document path to: " & m_fileName, _s.DocName, EnumLogKind.IsConfig)

        Else

          ' Log
          _s.ConfigData.WriteLogLine("Failed to update master model document path to: " & m_fileName, _s.DocName, EnumLogKind.IsConfig)

        End If

      End If

    End If

  End Sub

#End Region

#Region "Form Controls & Events - Config Data"

  ''' <summary>
  ''' Open Existing INI
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonConfigOpen_Click(sender As Object, e As EventArgs) Handles ButtonConfigOpen.Click

    ' Get the File
    If OpenFileDialogIni.ShowDialog = DialogResult.OK Then

      ' File Name
      Dim m_fileName As String = OpenFileDialogIni.FileName

      ' Valid file?
      If Not String.IsNullOrEmpty(m_fileName) Then

        ' Save the Result in the Parameter
        Using col As New FilteredElementCollector(_s.Doc)
          col.WhereElementIsNotElementType()
          col.OfCategory(BuiltInCategory.OST_ProjectInformation)
          For Each x In col.ToElements
            Using t As New Transaction(_s.Doc, "Updated Config File Path")
              If t.Start Then
                Try
                  Dim m_p As Parameter = x.LookupParameter(_s.ConfigData.ParamData)
                  m_p.Set(m_fileName)
                  t.Commit()

                  ' File Exists
                  PictureBoxParamIni.Visible = True
                  _s.ConfigData.ConfigPath = m_fileName
                  _s.ConfigData.GetProjectInfo(_s.Doc, _s.ConfigData.ParamData)

                  ' Message
                  Dim m_msg As String = _s.ConfigData.ConfigPath

                  ' Log
                  _s.ConfigData.WriteLogLine("Updated config file path (Open Existing): " & m_msg, _s.DocName, EnumLogKind.IsConfig)

                  LabelIniFile.Text = m_msg

                Catch

                  ' Message
                  PictureBoxParamIni.Visible = False
                  Dim m_msg As String = "Error Updating Configuration File Path Parameter"

                  ' Log
                  _s.ConfigData.WriteLogLine(m_msg, _s.DocName, EnumLogKind.IsConfig)

                  ' Display Message
                  MsgBox(m_msg, MsgBoxStyle.Critical, "Error")

                End Try
              End If
            End Using
          Next
        End Using

      End If

    End If

    ' Controls
    SetFormControlsFromConfig()

  End Sub

  ''' <summary>
  ''' Save a New INI
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonConfigNew_Click(sender As Object, e As EventArgs) Handles ButtonConfigNew.Click

    ' Get the File
    If SaveFileDialogIni.ShowDialog = DialogResult.OK Then

      ' File Name
      Dim m_fileName As String = SaveFileDialogIni.FileName

      ' Valid file?
      If Not String.IsNullOrEmpty(m_fileName) Then

        ' Save the Result in the Parameter
        Using col As New FilteredElementCollector(_s.Doc)
          col.WhereElementIsNotElementType()
          col.OfCategory(BuiltInCategory.OST_ProjectInformation)
          For Each x In col.ToElements
            Using t As New Transaction(_s.Doc, "Updated Config File Path")
              If t.Start Then
                Try
                  Dim m_p As Parameter = x.LookupParameter(_s.ConfigData.ParamData)
                  m_p.Set(m_fileName)
                  t.Commit()

                  ' File Exists
                  PictureBoxParamIni.Visible = True
                  _s.ConfigData.ConfigPath = m_fileName

                  ' Write the File
                  _s.ConfigData = New clsConfig()
                  _s.ConfigData.UpdateConfigFile()

                  ' Read the Updates
                  _s.ConfigData.GetProjectInfo(_s.Doc, _s.ConfigData.ParamData)

                  ' Message
                  Dim m_msg As String = _s.ConfigData.ConfigPath

                  ' Log
                  _s.ConfigData.WriteLogLine("Updated config file path (Save New): " & m_msg, _s.DocName, EnumLogKind.IsConfig)
                  LabelIniFile.Text = m_msg

                  ' Enable Controls
                  SetFormControlsEnable(True, GroupBoxLogs)
                  SetFormControlsEnable(True, GroupBoxModelData)

                Catch

                  ' Message
                  PictureBoxParamIni.Visible = False
                  Dim m_msg As String = "Error Updating Configuration File Path Parameter"

                  ' Log
                  _s.ConfigData.WriteLogLine(m_msg, _s.DocName, EnumLogKind.IsConfig)

                  ' Display Message
                  MsgBox(m_msg, MsgBoxStyle.Critical, "Error")

                End Try
              End If
            End Using
          Next
        End Using

      End If

    End If

    ' Controls
    SetFormControlsFromConfig()

  End Sub

#End Region

#Region "Form Controls & Events - Family Tag Config"

  ''' <summary>
  ''' Add a Family
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonFamilyAddModel_Click(sender As Object, e As EventArgs) Handles ButtonFamilyAddModel.Click

    ' Show Form
    Using d As New form_AddFamily(_s)
      d.ShowDialog()

      ' Any New Families?
      If d.FamTags.Count > 0 Then

        ' Iterate Each
        For Each x In d.FamTags

          Try

            ' Unique?
            If Not _s.ConfigData.HasFamily(x.FamilyName & "|" & x.TypeName) Then

              ' Add it
              _s.ConfigData.Families.Add(x)

              ' Log
              _s.ConfigData.WriteLogLine("Added new family/type: " & x.FamilyName & "|" & x.TypeName, _s.DocName, EnumLogKind.IsConfig)

            End If

          Catch
          End Try

        Next

        ' Write the Updated File
        _s.ConfigData.UpdateConfigFile()

        ' Symbol Data
        GetInstanceCountsForFamilies()

        ' Load the Families
        SetFormControlsFromConfig()

      End If

    End Using

  End Sub

  ''' <summary>
  ''' Remove Selected Family
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonFamilyRemove_Click(sender As Object, e As EventArgs) Handles ButtonFamilyRemove.Click

    ' Are they Sure?
    If MsgBox("Are you sure you want to remove the selected item?",
              MsgBoxStyle.YesNo,
              "Cannot Undo This!") = MsgBoxResult.Yes Then

      ' Get the Selected Item
      For Each x As DataGridViewRow In DataGridViewFamilies.SelectedRows

        Try

          ' Get the Bound Item
          Dim m_fam As clsFam = x.DataBoundItem

          ' Remove it from the Config File
          For Each f In _s.ConfigData.Families
            If m_fam.FamilyName.ToLower = f.FamilyName.ToLower Then
              If m_fam.TypeName.ToLower = f.TypeName.ToLower Then

                ' Remove
                _s.ConfigData.Families.Remove(f)

                ' Log
                _s.ConfigData.WriteLogLine("Removed family/type: " & m_fam.FamilyName & "|" & m_fam.TypeName,
                                           _s.DocName,
                                           EnumLogKind.IsConfig)

                ' Next
                Exit For

              End If
            End If
          Next

        Catch
        End Try

      Next

      Try

        ' Update the File
        _s.ConfigData.UpdateConfigFile()

        ' Symbol Data
        GetInstanceCountsForFamilies()

        ' Update the Grid
        SetFormControlsFromConfig()

      Catch
      End Try

    End If

  End Sub

#End Region

#Region "Form Controls & Events - Logs"

  Private Sub CheckBoxLogTagPlacements_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxLogTagPlacements.CheckedChanged
    Try
      _s.ConfigData.LogPlacements = CheckBoxLogTagPlacements.Checked
      _s.ConfigData.UpdateConfigFile()

      ' Log
      _s.ConfigData.WriteLogLine("Changed 'Log Tag Placements Log' to : " & CheckBoxLogTagPlacements.Checked.ToString, _s.DocName, EnumLogKind.IsOther)

    Catch
    End Try
  End Sub

  Private Sub CheckBoxLogSyncs_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxLogSyncs.CheckedChanged
    Try
      _s.ConfigData.LogSyncs = CheckBoxLogSyncs.Checked
      _s.ConfigData.UpdateConfigFile()

      ' Log
      _s.ConfigData.WriteLogLine("Changed 'Log Syncs Log' to : " & CheckBoxLogSyncs.Checked.ToString, _s.DocName, EnumLogKind.IsOther)

    Catch
    End Try
  End Sub

  Private Sub CheckBoxLogConfigs_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxLogConfigs.CheckedChanged
    Try
      _s.ConfigData.LogConfig = CheckBoxLogConfigs.Checked
      _s.ConfigData.UpdateConfigFile()

      ' Log
      _s.ConfigData.WriteLogLine("Changed 'Log Syncs Log' to : " & CheckBoxLogConfigs.Checked.ToString, _s.DocName, EnumLogKind.IsOther)

    Catch
    End Try
  End Sub

#End Region

#End Region

  End Class
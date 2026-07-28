Imports Autodesk.Revit.DB
Imports System.Diagnostics
Imports System.Windows.Forms
Imports [Case].Subs.Exceler8.Data

Public Class form_Export

  Private _s As clsSettings

  Private Enum EnumCheckState
    IsTrue
    IsFalse
    IsNone
  End Enum

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

#Region "Private Members - DATA Export"

  ''' <summary>
  ''' Export the Excel Data
  ''' </summary>
  ''' <param name="c">Configuration to Export</param>
  ''' <remarks></remarks>
  Friend Sub ExportData(c As clsIoConfig)

    ' Fresh List
    _s.ExportCategories = New List(Of clsRvtCategoryData)

    ' Default Scopes
    Dim m_doInst As Boolean = True
    Dim m_doType As Boolean = True
    Dim m_kind As EnumRevitElementTableType = EnumRevitElementTableType.isAllData

    Try

      ' Instances?
      If c.isInstOnly = False And c.isInstWithTypes = False And c.isTypesOnly = False Then

        ' Failure - nothing to export - This should NEVER happen
        MsgBox("Since you did no select any type or instance data to export, there is nothing for me to do",
               MsgBoxStyle.Critical,
               "Nothing to Export")
        Exit Sub

      End If

      ' Types Only
      If c.isTypesOnly = True Then
        m_doInst = False
        m_kind = EnumRevitElementTableType.isJustTypes
      End If

      ' Instance Only
      If c.isInstOnly = True Then
        m_doType = False
        m_kind = EnumRevitElementTableType.isJustInstances
      End If

      ' Get Elements from Categories
      For Each x In c.CatList
        Try

          ' Helper
          Dim m_category As Category = x.GetCategory
          If Not m_category Is Nothing Then

            ' Get the Element Data from Category
            Dim m_catData As New clsRvtCategoryData(m_category,
                                                 _s.Doc,
                                                 m_doInst,
                                                 m_doType,
                                                 c.IsNumeric,
                                                 c.isNumEid,
                                                 m_kind,
                                                 _s.UserWorksets)

            ' Any Elements?
            Dim m_eCnt As Integer = m_catData.InstElem.Count + m_catData.TypeElem.Count
            If m_eCnt > 1 Then

              ' Add the Helper
              _s.ExportCategories.Add(m_catData)

            End If

          End If
        Catch
        End Try
      Next

    Catch
    End Try

    Try

      ' Configure Progress Bar
      With ProgressBar1
        .Minimum = 0
        .Maximum = 4
        .Value = 1
      End With

    Catch
    End Try

    Try

      Dim m_excel As clsExcel

      ' File Export Kind?
      If c.isMultifile = True Then

        ' One File Per Category
        For Each x As clsRvtCategoryData In _s.ExportCategories

          Try

            ' File Name
            Dim m_fileName As String = Replace(SaveFileDialogExcel.FileName,
                                               ".xlsx", "_" & x.CategoryName & ".xlsx", , , CompareMethod.Text)

            ' New Excel Application
            m_excel = New clsExcel(m_fileName, _s, EnumExcelSrartupMode.isNewFile)
            Dim m_names As New List(Of String)
            m_names.Add(x.CategoryName)

            ' Rename First Tab
            m_excel.AddWorksheets(m_names)

            Try

              ' Get All Values
              Dim m_values As Array = x.GetAllPropertyValues(ProgressBar1)

              ' Export to Worksheet
              m_excel.UpdateWorksheetFromArray(x.CategoryName, x.AllParams, m_values, True)

            Catch
            End Try

            ' Close Excel?
            m_excel.ExcelShutDown()

          Catch
          End Try

        Next

      Else

        ' Single File - Multiple Tabs: New Excel Application
        m_excel = New clsExcel(SaveFileDialogExcel.FileName, _s, EnumExcelSrartupMode.isNewFile)

        ' Create Worksheet Tab for Each Category
        Dim m_names As New List(Of String)
        For Each x As clsRvtCategoryData In _s.ExportCategories
          m_names.Add(x.CategoryName)
        Next
        m_excel.AddWorksheets(m_names)

        ' Each Category
        For Each x As clsRvtCategoryData In _s.ExportCategories

          Try

            ' Get All Values
            Dim m_values As Array = x.GetAllPropertyValues(ProgressBar1)

            ' Export to Worksheet
            m_excel.UpdateWorksheetFromArray(x.CategoryName, x.AllParams, m_values, True)

          Catch
          End Try

        Next


        Try
          ProgressBar1.Increment(1)
          Application.doevents()
        Catch
        End Try

        ' Close Excel?
        m_excel.ExcelShutDown()

      End If

    Catch
    End Try

  End Sub

#End Region

#Region "Private Members - Configurations"

  ''' <summary>
  ''' Load Categories List
  ''' </summary>
  ''' <param name="ch">Set as Checked?</param>
  ''' <remarks></remarks>
  Private Sub LoadCategories(ch As EnumCheckState)

    Try

      ' Release Data
      DataGridViewCategories.DataSource = Nothing

      ' New Lists
      Dim m_catsDict As New SortedDictionary(Of String, clsRvtCategory)
      Dim m_cats As New SortableBindingList(Of clsRvtCategory)

      ' Process Each Category
      For Each x As Category In _s.Doc.Settings.Categories

        ' Name Filtering
        If Not String.IsNullOrEmpty(TextBoxFind.Text) Then
          If Not x.Name.ToLower.Contains(TextBoxFind.Text.ToLower) Then Continue For
        End If

        ' Helper
        Dim c As New clsRvtCategory(x)

        ' Check Status
        Select Case ch
          Case EnumCheckState.isFalse
            c.isChecked = False
          Case EnumCheckState.isTrue
            c.isChecked = True
          Case EnumCheckState.isNone
            ' No Check Enforcement
        End Select

        Try
          ' Add to Master Dictionary
          Dim m_test As String = x.Name
          If Not m_catsDict.ContainsKey(m_test) Then
            m_catsDict.Add(x.Name, c)
          Else
            m_catsDict.Add(x.Name & "_", c)
          End If
        Catch
        End Try

      Next

      ' To List
      For Each x In m_catsDict.Values
        m_cats.Add(x)
      Next

      ' Bind to Control
      DataGridViewCategories.DataSource = m_cats

      ' Configure Columns
      DataGridViewCategories.Columns(0).HeaderText = ""
      DataGridViewCategories.Columns(0).Width = 30
      DataGridViewCategories.Columns(1).HeaderText = "Category"
      DataGridViewCategories.Columns(1).Width = 225

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Load Configurations
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub LoadConfigurations(Optional n As String = "")

    Try

      ' Saved Configurations
      ComboBoxConfigurations.DataSource = Nothing
      ComboBoxConfigurations.DataSource = _s.Configurations
      ComboBoxConfigurations.DisplayMember = "ConfigName"

      ' Index
      If Not String.IsNullOrEmpty(n) Then


        Try

          ' Get the Config and Set it
          For Each x As clsIoConfig In ComboBoxConfigurations.Items
            If x.ConfigName = n Then
              ComboBoxConfigurations.SelectedItem = x
              Exit For
            End If
          Next

        Catch
        End Try

      Else

        ' Index to 0
        ComboBoxConfigurations.SelectedIndex = 0

      End If

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Configuration By Form Settings
  ''' </summary>
  ''' <param name="n"></param>
  ''' <remarks></remarks>
  Private Function GetConfigFromForm(n As String) As clsIoConfig

    ' New Configuration
    Dim m_c As New clsIoConfig(_s.PathAppData & "\" & n & ".Exceler8")

    Try

      ' Settings
      m_c.isMultifile = RadioButtonMulti.Checked
      m_c.IsNumeric = RadioButtonAsNumeric.Checked
      m_c.isInstWithTypes = RadioButtonInstTypes.Checked
      m_c.isInstOnly = RadioButtonInst.Checked
      m_c.isTypesOnly = RadioButtonType.Checked

      Try

        ' Checked Categories
        Dim m_checkedCatString As New List(Of String)
        Dim m_checkedCats As New List(Of clsRvtCategory)
        For Each x As clsRvtCategory In DataGridViewCategories.DataSource
          If x.isChecked = True Then
            m_checkedCats.Add(x)
            m_checkedCatString.Add(x.CategoryName)
          End If
        Next

        ' Get the Data and Save it Out
        m_c.CatList = m_checkedCats
        m_c.Categories = m_checkedCatString

      Catch
      End Try

    Catch
    End Try

    ' Return Value
    Return m_c

  End Function

#End Region

#Region "Form Controls & Events"

  ''' <summary>
  ''' Form Loaded
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_Export_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    Try

      ' Form Config
      SetFormViz(EnumFormViz.isStandby)

      ' Title
      Text = "Subscription Exeler8 Data Export v" & My.Application.Info.Version.ToString()

      ' Categories
      LoadCategories(EnumCheckState.isFalse)

      ' Configurations
      LoadConfigurations()

    Catch
    End Try

  End Sub

#Region "Private Members - Form Viz"

  Private Enum EnumFormViz
    IsStandby
    IsProcessing
  End Enum

  ''' <summary>
  ''' Form Settings Based on State
  ''' </summary>
  ''' <param name="fv"></param>
  ''' <remarks></remarks>
  Private Sub SetFormViz(fv As EnumFormViz)

    Select Case fv
      Case EnumFormViz.isStandby
        ProgressBar1.Hide()
        ButtonOk.Show()
        ButtonCancel.Show()
        ButtonHelp.Show()
        ButtonCheckAll.Enabled = True
        ButtonCheckNone.Enabled = True
        ButtonSavedNew.Enabled = True
        ButtonRemove.Enabled = True
        RadioButtonAsNumeric.Enabled = True
        RadioButtonAsText.Enabled = True
        RadioButtonInst.Enabled = True
        RadioButtonInstTypes.Enabled = True
        RadioButtonMulti.Enabled = True
        RadioButtonSingle.Enabled = True
        RadioButtonType.Enabled = True
        ComboBoxConfigurations.Enabled = True
        DataGridViewCategories.Enabled = True
      Case EnumFormViz.isProcessing
        ProgressBar1.Show()
        ButtonOk.Hide()
        ButtonCancel.Hide()
        ButtonHelp.Hide()
        ButtonCheckAll.Enabled = False
        ButtonCheckNone.Enabled = False
        ButtonSavedNew.Enabled = False
        ButtonRemove.Enabled = False
        RadioButtonAsNumeric.Enabled = False
        RadioButtonAsText.Enabled = False
        RadioButtonInst.Enabled = False
        RadioButtonInstTypes.Enabled = False
        RadioButtonMulti.Enabled = False
        RadioButtonSingle.Enabled = False
        RadioButtonType.Enabled = False
        ComboBoxConfigurations.Enabled = True
        DataGridViewCategories.Enabled = False
    End Select

  End Sub

#End Region

  ''' <summary>
  ''' Get a Name for the New Item
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonSavedNew_Click(sender As System.Object, e As EventArgs) Handles ButtonSavedNew.Click

    Try

      ' Get the Name
      Using d As New form_Name
        d.ShowDialog()
        If Not String.IsNullOrEmpty(d.TextBoxName.Text) Then

          ' Does it exist?
          For Each x In _s.Configurations
            If x.ConfigName.ToLower = d.TextBoxName.Text.ToLower Then
              If MsgBox("Do you want overwrite the existing configuration named '" & d.TextBoxName.Text & "'?",
                        MsgBoxStyle.YesNo, "Overwrite the Existing?") = MsgBoxResult.No Then
                Exit Sub
              End If
            End If
          Next

          Try

            ' Save the Settings
            Dim m_c As clsIoConfig = GetConfigFromForm(d.TextBoxName.Text)
            m_c.WriteData()
            _s.Configurations.Add(m_c)

            ' Update Configurations
            LoadConfigurations(m_c.ConfigName)

          Catch
          End Try

        End If
      End Using

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Delete Selected Combo Item
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonRemove_Click(sender As System.Object, e As EventArgs) Handles ButtonRemove.Click

    Try

      ' Get the Selected Item
      If ComboBoxConfigurations.SelectedIndex = 0 Then

        ' Can't do that
        MsgBox("Cannot Delete the Default Unnamed Item", MsgBoxStyle.Information, "Cannot Remove this One ;)")

      Else

        ' Are they Sure?
        Dim m_config As clsIoConfig = ComboBoxConfigurations.SelectedItem
        If MsgBox("Are you sure you want to remove '" & m_config.ConfigName & "'?", MsgBoxStyle.YesNo, "Are You Sure?") = MsgBoxResult.Yes Then

          ' Remove It
          For Each x In _s.Configurations
            If x.ConfigName = m_config.ConfigName Then

              ' Delete the File
              If x.DeleteFile = False Then

                MsgBox("Failed to Delete the Configuration File", MsgBoxStyle.Critical, "Failed")

              Else

                ' Remove from Settings
                _s.Configurations.Remove(x)

                ' Reload the Items
                LoadConfigurations()

              End If

            End If
          Next

        End If

      End If

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Change Selected Configuration Settings
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ComboBoxConfigurations_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles ComboBoxConfigurations.SelectedIndexChanged

    Try

      ' Default?
      If ComboBoxConfigurations.SelectedIndex = 0 Then
        RadioButtonAsText.Checked = True
        RadioButtonAsNumeric.Checked = False
        RadioButtonNumericElementID.Checked = False
        RadioButtonSingle.Checked = True
        RadioButtonMulti.Checked = False
        RadioButtonInstTypes.Checked = True
        RadioButtonInst.Checked = False
        RadioButtonType.Checked = False
        LoadCategories(EnumCheckState.IsNone)
      Else

        ' Get the Config Item
        Dim x As clsIoConfig = ComboBoxConfigurations.SelectedItem
        If Not x Is Nothing Then
          RadioButtonAsNumeric.Checked = x.IsNumeric
          RadioButtonNumericElementID.Checked = x.isNumEid
          RadioButtonAsText.Checked = Not x.IsNumeric
          RadioButtonMulti.Checked = x.isMultifile
          RadioButtonSingle.Checked = Not x.isMultifile
          RadioButtonInstTypes.Checked = x.isInstWithTypes
          RadioButtonInst.Checked = x.isInstOnly
          RadioButtonType.Checked = x.isTypesOnly

          ' Release Data
          DataGridViewCategories.DataSource = Nothing

          ' New Lists
          Dim m_catsDict As New SortedDictionary(Of String, clsRvtCategory)
          Dim m_cats As New List(Of clsRvtCategory)

          ' Categories
          Try

            ' Process Each Category
            For Each z As Category In _s.Doc.Settings.Categories
              Dim clsc As New clsRvtCategory(z)
              clsc.isChecked = x.Categories.Contains(clsc.CategoryName)
              m_catsDict.Add(z.Name, clsc)
            Next

            ' To List
            For Each i In m_catsDict.Values
              m_cats.Add(i)
            Next

            ' Bind to Control
            DataGridViewCategories.DataSource = m_cats

            ' Configure Columns
            DataGridViewCategories.Columns(0).HeaderText = ""
            DataGridViewCategories.Columns(0).Width = 30
            DataGridViewCategories.Columns(1).HeaderText = "Category"
            DataGridViewCategories.Columns(1).Width = 225

          Catch
          End Try

        End If

      End If

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Documentation Page 
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As System.Object, e As EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/subscription-exceler8-revit")
  End Sub

  ''' <summary>
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As System.Object, e As EventArgs) Handles ButtonOk.Click

    ' Form Config
    SetFormViz(EnumFormViz.IsProcessing)

    ' Get a Filename
    If SaveFileDialogExcel.ShowDialog = System.Windows.Forms.DialogResult.OK Then
      If Not String.IsNullOrEmpty(SaveFileDialogExcel.FileName) Then

        ' Form Configuration
        Dim m_configFromUi As clsIoConfig = GetConfigFromForm("Unnamed")

        ' Is there a Configuration Selected?
        If ComboBoxConfigurations.SelectedIndex > 0 Then

          Try

            ' Compare with Form - Ask to Save
            Dim m_configFromFile As clsIoConfig = ComboBoxConfigurations.SelectedItem

            ' Comparison
            Dim m_same As Boolean = True
            If m_configFromUi.IsMultifile <> m_configFromFile.IsMultifile Then m_same = False
            If m_configFromUi.IsNumeric <> m_configFromFile.IsNumeric Then m_same = False
            If m_configFromUi.IsInstWithTypes <> m_configFromFile.IsInstWithTypes Then m_same = False
            If m_configFromUi.IsInstOnly <> m_configFromFile.IsInstOnly Then m_same = False
            If m_configFromUi.IsTypesOnly <> m_configFromFile.IsTypesOnly Then m_same = False
            If m_configFromUi.Categories.Count <> m_configFromFile.Categories.Count Then
              m_same = False
            Else
              ' Compare Each
              For Each x In m_configFromUi.Categories
                If Not m_configFromFile.Categories.Contains(x) Then m_same = False
              Next
            End If

            ' Same?
            If m_same = False Then
              If MsgBox("Do you want to save your changes to '" & m_configFromFile.ConfigName & "'?", MsgBoxStyle.YesNo, "Save Changes?") = MsgBoxResult.Yes Then
                m_configFromFile.WriteData()
              End If
            End If

          Catch
          End Try

        End If

        ' Export the Data
        ExportData(m_configFromUi)

      End If
    End If

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Close
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As System.Object, e As EventArgs) Handles ButtonCancel.Click

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Check All Categories
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCheckAll_Click(sender As System.Object, e As EventArgs) Handles ButtonCheckAll.Click
    LoadCategories(EnumCheckState.IsTrue)
  End Sub

  ''' <summary>
  ''' Check None Categories
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCheckNone_Click(sender As System.Object, e As EventArgs) Handles ButtonCheckNone.Click
    LoadCategories(EnumCheckState.IsFalse)
  End Sub

  ''' <summary>
  ''' Filter Categories List
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub TextBoxFind_TextChanged(sender As System.Object, e As EventArgs) Handles TextBoxFind.TextChanged
    LoadCategories(EnumCheckState.IsNone)
  End Sub

#End Region

End Class
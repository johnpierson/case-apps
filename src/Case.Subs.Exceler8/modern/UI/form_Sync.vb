Imports System.Diagnostics
Imports System.Windows.Forms
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports System.Linq
Imports [Case].Subs.Exceler8.Data

Public Class form_Sync

  Private _s As clsSettings
  Private _excel As clsExcel
  Private _doEvents As Boolean = False
  Private _category As Category = Nothing

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="s">Settings</param>
  ''' <param name="e">Excel Helper</param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings, e As clsExcel)
    InitializeComponent()

    ' Widen Scope
    _s = s
    _excel = e

  End Sub

#Region "Private Members"

  ''' <summary>
  ''' Get the category from first found element and store all keys with array row numbers
  ''' </summary>
  ''' <param name="wsName">Worksheet Name to Process</param>
  ''' <returns>True if a type element set</returns>
  ''' <remarks></remarks>
  Private Function GetCategoryAndKeys(wsName As String) As Boolean

    ' Types
    Dim isTypes As Boolean = False

    ' Fres Rows Listing
    If Not _excel.WorkSheetRows.ContainsKey(wsName) Then
      _excel.WorkSheetRows.Add(wsName, New Dictionary(Of String, Integer))
    End If

    ' Results
    _category = Nothing

    ' Find an Element
    Dim iBlank As Integer = 0
    For i = 4 To _excel.WorkSheetData(wsName).GetUpperBound(0)

      ' Only allow 3 blanks
      If iBlank = 3 Then Return isTypes

      Try

        ' Empty?
        Dim m_key As String = _excel.WorkSheetData(wsName)(i, 1).ToString
        If String.IsNullOrEmpty(m_key) Then
          iBlank += 1
          Continue For
        End If

        ' Valid Key GUID Length
        If m_key.Length > 40 Then

          ' Store in Rows Listing
          _excel.WorkSheetRows(wsName).Add(m_key.ToLower, i)

          ' Category Found Yet?
          If _category Is Nothing Then

            ' Get the Element Category
            Dim m_e As Element = _s.Doc.GetElement(m_key)
            If Not m_e Is Nothing Then
              If Not m_e.Category Is Nothing Then
                If TypeOf m_e Is ElementType Then
                  isTypes = True
                Else
                  isTypes = False
                End If
                _category = m_e.Category
              End If
            End If

          End If

        End If

      Catch
        iBlank += 1
      End Try

    Next

    ' Types
    Return isTypes

  End Function

  ''' <summary>
  ''' Synchronize Data
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub SyncExcelAndRevit()

    ' Worksheet
    Dim m_wsName As String = ""

    ' Progress
    Using prog As New form_Progress("Reading Data...", 20)
      prog.Show()
      prog.StepProgress()

      Try

        ' Worksheet Name
        m_wsName = ComboBoxWorksheets.SelectedItem.ToString

        ' Get the Values from Excel
        _excel.GetValues(m_wsName)

        ' Category
        Dim m_isElementTypes As Boolean = GetCategoryAndKeys(m_wsName)

        ' Get the Elements
        If _category Is Nothing Then

          ' Failure Message
          MsgBox("Failed to retrieve existing elements..." & vbCr &
                 "Try exporting to a fresh document",
                 MsgBoxStyle.Critical,
                 "No Elements to Sync")
          prog.Close()

        Else

          ' Get the Elements
          Dim m_elements As New List(Of Element)
          m_elements = _s.GetElements(m_isElementTypes, _category.Id)

          ' Elements to Dictionary
          Dim m_eDict As New Dictionary(Of String, Element)
          For Each x As Element In m_elements
            If Not m_eDict.ContainsKey(x.UniqueId.ToString.ToLower) Then
              m_eDict.Add(x.UniqueId.ToString.ToLower, x)
            End If
          Next

          ' Types, Instances, or Both (Parameters)
          Dim m_hasInst As Boolean = False
          Dim m_hasType As Boolean = False
          For Each x In _excel.SelectedSyncHeaderColumns
            If x.Value.Kind = EnumExcelHeaderKind.isI Then m_hasInst = True
            If x.Value.Kind = EnumExcelHeaderKind.isT Then m_hasType = True
          Next

          ' Find Deleted and Process Known Elements
          For i = 4 To _excel.WorkSheetData(m_wsName).GetUpperBound(0)

            ' Key Valid?
            Dim m_key As String = _excel.WorkSheetData(m_wsName)(i, 1).ToString
            If String.IsNullOrEmpty(m_key) Then Continue For
            If m_key.ToLower.StartsWith("dele") Then Continue For

            ' Exists in New List of Elements?
            If Not m_eDict.ContainsKey(m_key.ToLower) Then

              ' Flag as Deleted
              _excel.WorkSheetData(m_wsName)(i, 1) = "DELETED"

            Else

              ' Get the Element
              Dim m_e As Element = m_eDict(m_key.ToLower)
              If Not m_e Is Nothing Then

                ' Update Item
                UpdateElementData(_excel.WorkSheetData(m_wsName), m_e, m_wsName, m_hasType, m_hasInst, i, _excel.SelectedSyncHeaderColumns, False)

              End If

            End If

          Next ' Existing Array Iteration

          ' Build Header Object from Known Header Data ; Write Header = FALSE
          Dim m_headerDict As New Dictionary(Of String, SortedDictionary(Of String, SortedDictionary(Of String, clsExcelHeader)))
          m_headerDict.Add("e", New SortedDictionary(Of String, SortedDictionary(Of String, clsExcelHeader)))
          m_headerDict.Add("i", New SortedDictionary(Of String, SortedDictionary(Of String, clsExcelHeader)))
          m_headerDict.Add("t", New SortedDictionary(Of String, SortedDictionary(Of String, clsExcelHeader)))

          ' Build from Existing Header Data
          For Each x As clsExcelHeader In _excel.WorkSheetColumns(m_wsName).Values

            Try

              ' Scope
              Dim m_scope As String = ""
              Select Case x.Kind
                Case EnumExcelHeaderKind.isE
                  m_scope = "e"
                Case EnumExcelHeaderKind.isI
                  m_scope = "i"
                Case EnumExcelHeaderKind.isT
                  m_scope = "t"
              End Select

              ' Group
              If Not m_headerDict(m_scope).ContainsKey(x.GroupName) Then
                m_headerDict(m_scope).Add(x.GroupName, New SortedDictionary(Of String, clsExcelHeader))
              End If

              ' Add Final Object
              If Not m_headerDict(m_scope)(x.GroupName).ContainsKey(x.Name) Then
                m_headerDict(m_scope)(x.GroupName).Add(x.Name, x)
              End If

            Catch
            End Try

          Next

          ' Post Updated Array to Excel
          _excel.UpdateWorksheetFromArray(m_wsName, m_headerDict, _excel.WorkSheetData(m_wsName), False)

          ' Find and Add New Element Data
          Dim m_newElements As New List(Of Element)
          For Each x In m_eDict
            If Not _excel.WorkSheetRows(m_wsName).ContainsKey(x.Key.ToLower) Then

              ' This is a New Element
              m_newElements.Add(x.Value)

            End If
          Next

          ' New Elements to Add?
          If m_newElements.Count > 0 Then


            _excel.GetHeaderData("key", True)


            ' New Array
            Dim m_array(m_newElements.Count - 1, _excel.WorkSheetData(m_wsName).GetUpperBound(1) - 1) As Object

            ' Set All Columns to Excel
            For Each c In _excel.WorkSheetColumns(m_wsName).Values
              c.Direction = EnumSyncDir.toExcel
            Next

            ' Process Each
            Dim iCnt As Integer = 0
            For Each e As Element In m_newElements

              ' Update Data
              UpdateElementData(m_array, e, m_wsName, m_hasType, m_hasInst, iCnt, _excel.WorkSheetColumns(m_wsName), True)

              ' Next
              iCnt += 1

            Next

            ' Write Update to Excel
            _excel.UpdateWorksheetFromArray(m_wsName, m_headerDict, m_array, False, _excel.WorkSheetData(m_wsName).GetUpperBound(0) + 6)

          End If

        End If ' Category

      Catch
      End Try

      ' Save Excel and Shutdown
      _excel.ExcelShutDown()

      ' Close Progress
      prog.Close()

    End Using

  End Sub

  ''' <summary>
  ''' Update Data from Elements
  ''' </summary>
  ''' <param name="ar">Array</param>
  ''' <param name="e">Element</param>
  ''' <param name="wsName">Worksheet Name</param>
  ''' <param name="hasType">TRUE if has types</param>
  ''' <param name="hasInst">TRUE if has instances</param>
  ''' <param name="i">row id</param>
  ''' <param name="isZeroBased">Subtract 1 from x.key if True</param>
  ''' <remarks></remarks>
  Private Sub UpdateElementData(ByRef ar As Array,
                                    e As Element,
                                    wsName As String,
                                    hasType As Boolean,
                                    hasInst As Boolean,
                                    i As Integer,
                                    syncColumns As SortedDictionary(Of Integer, clsExcelHeader),
                                    isZeroBased As Boolean)

    Try

      ' Zero Based Correction
      Dim iZ As Integer = 0
      'If isZeroBased = True Then iZ = 1

      ' List of Direct and Parent Params
      Dim m_directParams As New List(Of clsValue)
      Dim m_parentParams As New List(Of clsValue)

      ' Gather Data
      For Each x In syncColumns

        ' Value - Verify Correct Row and Column Numbering from Array
        Dim m_value As String = ""
        Try
          m_value = ar(i, x.Key - iZ)
        Catch
        End Try
        If String.IsNullOrEmpty(m_value) Then m_value = ""

        ' Types?
        Dim m_typeElement As Element = _s.Doc.GetElement(e.GetTypeId)

        ' All Element Data Done Here
        If x.Value.Kind = EnumExcelHeaderKind.isE Then
          Select Case x.Value.Name.ToLower

            Case "0key"
              Try
                If Not m_value = e.UniqueId.ToString Then
                  ar(i, x.Key - iZ) = e.UniqueId.ToString
                End If
              Catch
              End Try

            Case "elementid"
              Try
                If Not m_value = e.Id.ToString Then
                  ar(i, x.Key - iZ) = e.Id.ToString
                End If
              Catch
              End Try

            Case "family"
              Try
                If Not m_typeElement Is Nothing Then
                  Dim m_famName As String = m_typeElement.Parameter(BuiltInParameter.SYMBOL_FAMILY_NAME_PARAM).AsString
                  If Not m_value.ToLower = m_famName.ToLower Then
                    ar(i, x.Key - iZ) = m_famName
                  End If
                Else
                  If TypeOf e Is ElementType Then
                    Dim m_famName As String = e.Parameter(BuiltInParameter.SYMBOL_FAMILY_NAME_PARAM).AsString
                    ar(i, x.Key - iZ) = m_famName
                  End If
                End If
              Catch
              End Try

            Case "type"
              Try
                If Not m_typeElement Is Nothing Then
                  Dim m_typeName As String = m_typeElement.Parameter(BuiltInParameter.SYMBOL_NAME_PARAM).AsString
                  If Not m_value.ToLower = m_typeName.ToLower Then
                    ar(i, x.Key - iZ) = m_typeName
                  End If
                Else
                  If TypeOf e Is ElementType Then
                    Dim m_typeName As String = e.Parameter(BuiltInParameter.SYMBOL_NAME_PARAM).AsString
                    ar(i, x.Key - iZ) = m_typeName
                  End If
                End If
              Catch
              End Try

            Case "workset"
              If Not TypeOf e Is ElementType Then
                Try

                  ' Workset Name
                  If _s.UserWorksets.ContainsKey(e.WorksetId.IntegerValue) Then
                    If Not m_value.ToLower = _s.UserWorksets(e.WorksetId.IntegerValue).Name.ToLower Then
                      ar(i, x.Key - iZ) = _s.UserWorksets(e.WorksetId.IntegerValue).Name
                    End If
                  End If

                Catch
                End Try

              End If

            Case "xyz"
              If Not TypeOf e Is ElementType Then

                Try

                  ' Get the XYZ Location
                  Dim _xyz As XYZ = Nothing
                  If TypeOf e Is Autodesk.Revit.DB.Panel Then

                    ' Panel Element
                    Dim m_panel As Autodesk.Revit.DB.Panel = TryCast(e, Autodesk.Revit.DB.Panel)
                    If Not m_panel Is Nothing Then
                      _xyz = m_panel.Transform.Origin
                      ar(i, x.Key - iZ) = _xyz.ToString
                    End If

                  Else

                    ' Regular Location
                    If Not e.Location Is Nothing Then
                      Dim m_location As Location = e.Location
                      If TypeOf m_location Is LocationPoint Then
                        _xyz = DirectCast(m_location, LocationPoint).Point
                        If Not _xyz Is Nothing Then
                          ar(i, x.Key - iZ) = _xyz.ToString
                        End If
                      Else
                        ar(i, x.Key - iZ) = "n/a"
                      End If
                    End If

                  End If

                Catch
                End Try

              End If

          End Select

          ' Next - Because ValueKind was E
          Continue For

        End If

        ' Value Object
        Dim m_v As New clsValue(x.Value.GroupName, x.Value.Name, m_value)

        Try

          m_v.Direction = x.Value.Direction

          ' Both?
          If hasInst = True And hasType = True Then

            ' E, I, or T
            If x.Value.Kind = EnumExcelHeaderKind.isT Then

              ' Types are Parent
              m_parentParams.Add(m_v)

            Else

              ' E and I are Direct
              m_directParams.Add(m_v)

            End If

          Else

            ' Only Direct
            m_directParams.Add(m_v)

          End If

        Catch
        End Try

      Next

      ' Update Revit and Get Updated Information
      Dim m_values As New Dictionary(Of String, List(Of clsValue))
      m_values = _s.UpdateElementValues(e, m_directParams, m_parentParams, CheckBoxIsNumeric.Checked)

      ' Post Changes to Excel Array - Direct
      For Each v In m_values("d")
        For Each x In syncColumns
          If v.Name = x.Value.Name Then
            If v.Group = x.Value.GroupName Then

              ' Update Array
              ar(i, x.Key - iZ) = v.NewValue

            End If
          End If
        Next
      Next

      ' Post Changes to Excel Array - Parent
      For Each v In m_values("p")
        For Each x In syncColumns
          If v.Name = x.Value.Name Then
            If v.Group = x.Value.GroupName Then

              ' Update Array
              ar(i, x.Key - iZ) = v.NewValue

            End If
          End If
        Next
      Next

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Save Settings to INI File
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub SaveIniFile()

    ' Fresh Lists
    Dim m_inst As New List(Of clsDirection)
    Dim m_type As New List(Of clsDirection)

    Try

      ' Instances
      For Each x As DataGridViewRow In DataGridViewInst.Rows
        Select Case x.Cells(2).Value.ToString.ToLower
          Case "to revit"
            m_inst.Add(New clsDirection(x.Cells(0).Value & "|" & x.Cells(1).Value, EnumSyncDir.toRevit))
          Case "to excel"
            m_inst.Add(New clsDirection(x.Cells(0).Value & "|" & x.Cells(1).Value, EnumSyncDir.toExcel))
          Case "ignore"
            m_inst.Add(New clsDirection(x.Cells(0).Value & "|" & x.Cells(1).Value, EnumSyncDir.isIgnore))
        End Select
      Next

    Catch
    End Try

    Try

      ' Types
      For Each x As DataGridViewRow In DataGridViewType.Rows
        Select Case x.Cells(2).Value.ToString.ToLower
          Case "to revit"
            m_type.Add(New clsDirection(x.Cells(0).Value & "|" & x.Cells(1).Value, EnumSyncDir.toRevit))
          Case "to excel"
            m_type.Add(New clsDirection(x.Cells(0).Value & "|" & x.Cells(1).Value, EnumSyncDir.toExcel))
          Case "ignore"
            m_type.Add(New clsDirection(x.Cells(0).Value & "|" & x.Cells(1).Value, EnumSyncDir.isIgnore))
        End Select
      Next

    Catch
    End Try

    Try

      ' Update INI Helper
      _s.iniFile.UpdateSetting(_excel.KeyName,
                               ComboBoxWorksheets.SelectedItem.ToString,
                               m_type,
                               m_inst, CheckBoxIsNumeric.Checked)

      ' Save File
      _s.iniFile.WriteIniFile()

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Display Parameter Data for Selected Worksheet in Datagrids
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateWorksheetData()

    ' Clear All Data
    DataGridViewInst.Rows.Clear()
    DataGridViewType.Rows.Clear()

    Try

      ' Populate the Header Info
      For Each x In _excel.WorkSheetColumns(ComboBoxWorksheets.SelectedItem.ToString).Values

        ' Skip Element Data Items
        If x.Kind = EnumExcelHeaderKind.isE Then Continue For

        ' Combo
        Dim m_comboCell As New DataGridViewComboBoxCell
        m_comboCell.Items.Add("Ignore")
        m_comboCell.Items.Add("To Revit")
        m_comboCell.Items.Add("To Excel")
        m_comboCell.Value = "Ignore"

        ' Row
        Dim m_dr As New DataGridViewRow

        ' Name
        Dim m_cellName As New DataGridViewTextBoxCell
        m_cellName.Value = x.Name
        m_dr.Cells.Add(m_cellName)

        ' Parameter Group
        Dim m_cellGroup As New DataGridViewTextBoxCell
        m_cellGroup.Value = x.GroupName
        m_dr.Cells.Add(m_cellGroup)

        ' Combobox
        m_dr.Cells.Add(m_comboCell)

        ' Type and Instance Handling
        Select Case x.Kind

          Case EnumExcelHeaderKind.isI

            ' Add the Cells
            DataGridViewInst.Rows.Add(m_dr)

          Case EnumExcelHeaderKind.isT

            ' Add the Cells
            DataGridViewType.Rows.Add(m_dr)

        End Select

      Next

    Catch
    End Try

    Try

      ' Saved Values
      Dim m_key As String = _excel.KeyName.ToLower & "[" & ComboBoxWorksheets.SelectedItem.ToString.ToLower & "]"
      If _s.iniFile.FileSettings.ContainsKey(m_key) Then

        ' Sync as Numeric
        CheckBoxIsNumeric.Checked = _s.iniFile.SyncAsNumeric

        ' Process Each Row - Inst
        For Each r As DataGridViewRow In DataGridViewInst.Rows

          ' Find the Matching Item
          For Each x In _s.iniFile.FileSettings(m_key)("inst")

            ' Name Match?
            If x.NameAndGroup.ToLower = r.Cells(0).Value.ToString.ToLower & "|" & r.Cells(1).Value.ToString.ToLower Then

              ' Set Value for Direction
              Select Case x.Direction

                Case EnumSyncDir.toExcel
                  r.Cells(2).Value = "To Excel"

                Case EnumSyncDir.toRevit
                  r.Cells(2).Value = "To Revit"

                Case EnumSyncDir.isIgnore
                  r.Cells(2).Value = "Ignore"

              End Select

              ' Next Item
              Exit For

            End If

          Next
        Next

        ' Process Each Row - Type
        For Each r As DataGridViewRow In DataGridViewType.Rows

          ' Find the Matching Item
          For Each x In _s.iniFile.FileSettings(m_key)("type")

            ' Name Match?
            If x.NameAndGroup.ToLower = r.Cells(0).Value.ToString.ToLower & "|" & r.Cells(1).Value.ToString.ToLower Then

              ' Set Value for Direction
              Select Case x.Direction

                Case EnumSyncDir.toExcel
                  r.Cells(2).Value = "To Excel"

                Case EnumSyncDir.toRevit
                  r.Cells(2).Value = "To Revit"

                Case EnumSyncDir.isIgnore
                  r.Cells(2).Value = "Ignore"

              End Select

              ' Next Item
              Exit For

            End If

          Next

        Next

      End If

    Catch
    End Try

  End Sub

#End Region

#Region "Form Controls & Events"

#Region "Form Viz Settings"

  Private Enum formViz
    isProcessing
    isStandby
  End Enum

  ''' <summary>
  ''' Form Controls by State
  ''' </summary>
  ''' <param name="fv"></param>
  ''' <remarks></remarks>
  Private Sub SetFormViz(fv As formViz)

    Select Case fv
      Case formViz.isProcessing
        ButtonCancel.Hide()
        ButtonHelp.Hide()
        ButtonOk.Hide()
        DataGridViewInst.Enabled = False
        DataGridViewType.Enabled = False
        ButtonInstExport.Enabled = False
        ButtonInstImport.Enabled = False
        ButtonInstNone.Enabled = False
        ButtonTypeExport.Enabled = False
        ButtonTypeImport.Enabled = False
        ButtonTypeNone.Enabled = False
      Case formViz.isStandby
        ButtonCancel.Show()
        ButtonHelp.Show()
        ButtonOk.Show()
        DataGridViewInst.Enabled = True
        DataGridViewType.Enabled = True
        ButtonInstExport.Enabled = True
        ButtonInstImport.Enabled = True
        ButtonInstNone.Enabled = True
        ButtonTypeExport.Enabled = True
        ButtonTypeImport.Enabled = True
        ButtonTypeNone.Enabled = True
    End Select

  End Sub

#End Region

  ''' <summary>
  ''' Form Setup
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_Import_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    Try

      ' Form Controls
      SetFormViz(formViz.isStandby)

      ' Title
      Text = "Exceler8 Sync v" & My.Application.Info.Version.ToString()

      ' Load Worksheet List
      For Each x In _excel.WorkSheetColumns
        If x.Value.Count > 0 Then
          ComboBoxWorksheets.Items.Add(x.Key)
        End If
      Next
      If ComboBoxWorksheets.Items.Count > 0 Then
        ComboBoxWorksheets.SelectedIndex = 0
      End If

      ' Update the Worksheet Header Listings with Directions
      UpdateWorksheetData()

      ' Turn Events On
      _doEvents = True

    Catch
    End Try

    Try

      ' Must have proper document
      If DataGridViewInst.Rows.Count + DataGridViewType.Rows.Count = 0 Then
        MsgBox("The Excel document you are trying to sync is either malformed, or not supported for use with Exceler8",
               MsgBoxStyle.Critical, "Malformed Document")
        Close()

      Else

        ' Hide Tabs?
        If DataGridViewInst.Rows.Count < 1 Then
          TabControlParameters.TabPages.RemoveAt(0) '.Hide()
        End If
        If DataGridViewType.Rows.Count < 1 Then
          TabControlParameters.TabPages.RemoveAt(1) '.Hide()
        End If

      End If

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Instance to all Import
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonInstImport_Click(sender As System.Object, e As EventArgs) Handles ButtonInstImport.Click
    Try
      For Each x As DataGridViewRow In DataGridViewInst.Rows
        Try
          x.Cells(2).Value = "To Revit"
        Catch
        End Try
      Next
    Catch
    End Try
  End Sub

  ''' <summary>
  ''' Instance to all Export
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonInstExport_Click(sender As System.Object, e As EventArgs) Handles ButtonInstExport.Click
    Try
      For Each x As DataGridViewRow In DataGridViewInst.Rows
        Try
          x.Cells(2).Value = "To Excel"
        Catch
        End Try
      Next
    Catch
    End Try
  End Sub

  ''' <summary>
  ''' Instance to all Ignore
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonInstNone_Click(sender As System.Object, e As EventArgs) Handles ButtonInstNone.Click
    Try
      For Each x As DataGridViewRow In DataGridViewInst.Rows
        Try
          x.Cells(2).Value = "Ignore"
        Catch
        End Try
      Next
    Catch
    End Try
  End Sub

  ''' <summary>
  ''' Type to all Import
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonTypeImport_Click(sender As System.Object, e As EventArgs) Handles ButtonTypeImport.Click
    Try
      For Each x As DataGridViewRow In DataGridViewType.Rows
        Try
          x.Cells(2).Value = "To Revit"
        Catch
        End Try
      Next
    Catch
    End Try
  End Sub

  ''' <summary>
  ''' Type to all Export
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonTypeExport_Click(sender As System.Object, e As EventArgs) Handles ButtonTypeExport.Click
    Try
      For Each x As DataGridViewRow In DataGridViewType.Rows
        Try
          x.Cells(2).Value = "To Excel"
        Catch
        End Try
      Next
    Catch
    End Try
  End Sub

  ''' <summary>
  ''' Type to all Ignore
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonTypeNone_Click(sender As System.Object, e As EventArgs) Handles ButtonTypeNone.Click
    Try
      For Each x As DataGridViewRow In DataGridViewType.Rows
        Try
          x.Cells(2).Value = "Ignore"
        Catch
        End Try
      Next
    Catch
    End Try
  End Sub

  ''' <summary>
  ''' Cancel 
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As System.Object, e As EventArgs) Handles ButtonCancel.Click

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As System.Object, e As EventArgs) Handles ButtonOk.Click

    ' Form Viz
    SetFormViz(formViz.isProcessing)

    ' Save INI File?
    If CheckBoxAutoSave.Checked = True Then
      SaveIniFile()
    End If

    Try

      ' Sync Changes
      SyncExcelAndRevit()

    Catch
    End Try

    ' Inform User
    Dim m_todo As String = ""

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Launc Help Site
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As System.Object, e As EventArgs) Handles ButtonHelp.Click
    Try
      Process.Start("http://apps.case-inc.com/content/subscription-exceler8-revit")
    Catch
    End Try
  End Sub

  ''' <summary>
  ''' Tab Selection Change
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ComboBoxWorksheets_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles ComboBoxWorksheets.SelectedIndexChanged

    Try

      ' Only If Enabled
      If _doEvents = True Then

        ' Save INI File?
        If CheckBoxAutoSave.Checked = True Then
          SaveIniFile()
        End If

        ' Update the Form
        UpdateWorksheetData()

      End If

    Catch
    End Try

  End Sub

#End Region

End Class
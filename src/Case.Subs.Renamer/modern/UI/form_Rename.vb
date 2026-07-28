Imports Autodesk.Revit.DB
Imports System.Windows.Forms
Imports Autodesk.Revit.UI
Imports System.Linq
Imports [Case].Subs.Renamer.Data

Public Class form_Rename

  Private _s As clsSettings
  Private _familyTypes As New SortableBindingList(Of clsFamilyType)

  ''' <summary>
  ''' Rename Some Elements
  ''' </summary>
  ''' <param name="s">Setting Manager</param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s
    Setup()

  End Sub

  ''' <summary>
  ''' Form Class Setup
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub Setup()

    Try

      ' Form Title
      SetFormViz(formViz.isStandby)
      Text = "Subscription Element Renamer " & _s.Version

      ' Bind the Categories List
      Dim m_cats_dict As New SortedDictionary(Of String, clsCategory)
      Dim m_cats = From e In New FilteredElementCollector(_s.Doc) _
                   .WhereElementIsElementType
                   Select e

      For Each x In m_cats.ToList
        If Not x.Category Is Nothing Then
          If Not m_cats_dict.ContainsKey(x.Category.Name.ToLower) Then
            If x.Name.ToLower.Contains("rooms") Then Continue For
            If x.Name.ToLower.Contains("spaces") Then Continue For
            If x.Name.ToLower.Contains("zones") Then Continue For
            If x.Name.ToLower.Contains("project") Then Continue For
            m_cats_dict.Add(x.Category.Name.ToLower, New clsCategory(x.Category))
          End If
        Else
          
        End If
      Next

      'For Each x As Category In _s.Doc.Settings.Categories
      '  If x.AllowsBoundParameters = True Then
      '    If x.Name.ToLower.Contains("rooms") Then Continue For
      '    If x.Name.ToLower.Contains("spaces") Then Continue For
      '    If x.Name.ToLower.Contains("zones") Then Continue For
      '    If x.Name.ToLower.Contains("project") Then Continue For
      '    m_cats_dict.Add(x.Name, New clsCategory(x))
      '  End If
      'Next

      ' To List
      For Each c As clsCategory In m_cats_dict.Values
        _s.CategoryListing.Add(c)
      Next
      DataGridViewCategories.DataSource = _s.CategoryListing
      DataGridViewCategories.Columns(0).Width = 30

    Catch
    End Try

  End Sub

#Region "Form Viz"

  Private Enum formViz
    isProcessing
    isStandby
  End Enum

  ''' <summary>
  ''' Form Controls by Form State
  ''' </summary>
  ''' <param name="fv"></param>
  ''' <remarks></remarks>
  Private Sub SetFormViz(fv As formViz)
    Select Case fv
      Case formViz.isProcessing
        Me.ProgressBar1.Show()
        Me.ButtonCancel.Hide()
        Me.ButtonExport.Hide()
        Me.ButtonHelp.Hide()
        Me.ButtonImport.Hide()
        Me.ButtonRename.Hide()
        Me.ButtonSelectAll.Enabled = False
        Me.ButtonSelectNone.Enabled = False
      Case formViz.isStandby
        Me.ProgressBar1.Hide()
        Me.ButtonCancel.Show()
        Me.ButtonExport.Show()
        Me.ButtonHelp.Show()
        Me.ButtonImport.Show()
        Me.ButtonRename.Show()
        Me.ButtonSelectAll.Enabled = True
        Me.ButtonSelectNone.Enabled = True
    End Select
  End Sub

#End Region

  ''' <summary>
  ''' Update the Master Listing of Elements
  ''' </summary>
  ''' <param name="isImport"></param>
  ''' <remarks></remarks>
  Private Sub UpdateTypesListing(isImport As Boolean)

    ' Fresh List
    _familyTypes = New SortableBindingList(Of clsFamilyType)

    Try

      ' Import or Export?
      If isImport = True Then

        ' Load them All
        For Each x1 In _s.FamilyItems.Values
          Try
            For Each x2 As clsFamilyType In x1.Values
              Try
                _familyTypes.Add(x2)
              Catch
              End Try
            Next
          Catch
          End Try
        Next

      Else

        ' Does the Master have the data?
        For Each c As clsCategory In Me.DataGridViewCategories.DataSource
          If c.isChecked Then

            ' Do We Have the Elements?
            If Not _s.FamilyItems.ContainsKey(c.CatName) Then

              ' Get the Items
              Dim m_newList As New Dictionary(Of String, clsFamilyType)
              Using col As New FilteredElementCollector(_s.Doc)
                col.WhereElementIsElementType()
                col.OfCategoryId(c.GetCategory.Id)
                For Each x In col.ToElements
                  m_newList.Add(x.UniqueId.ToString, New clsFamilyType(x))
                Next
              End Using

              ' Add the Items
              _s.FamilyItems.Add(c.CatName, m_newList)

            End If

            ' Add to Master
            For Each cc As clsFamilyType In _s.FamilyItems(c.CatName).Values
              _familyTypes.Add(cc)
            Next

          End If
        Next

      End If

    Catch
    End Try

    Try

      ' Bind to the Controls
      Me.DataGridViewElements.DataSource = _familyTypes

    Catch
    End Try

  End Sub

#Region "Form Controls & Events"

  ''' <summary>
  ''' Help
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/subscription-rename-families-and-types-excel")
  End Sub

  ''' <summary>
  ''' Select All Categories
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonSelectAll_Click(sender As System.Object, e As System.EventArgs) Handles ButtonSelectAll.Click
    Try
      For Each x In _s.CategoryListing
        x.isChecked = True
      Next
      Me.DataGridViewCategories.DataSource = Nothing
      Me.DataGridViewCategories.DataSource = _s.CategoryListing
      Me.DataGridViewCategories.Columns(0).Width = 30
      Me.DataGridViewCategories.Columns(1).Width = 200
      UpdateTypesListing(False)
    Catch
    End Try
  End Sub

  ''' <summary>
  ''' Select None Categories
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonSelectNone_Click(sender As System.Object, e As System.EventArgs) Handles ButtonSelectNone.Click
    Try
      For Each x In _s.CategoryListing
        x.isChecked = False
      Next
      Me.DataGridViewCategories.DataSource = Nothing
      Me.DataGridViewCategories.DataSource = _s.CategoryListing
      Me.DataGridViewCategories.Columns(0).Width = 30
      Me.DataGridViewCategories.Columns(1).Width = 200
      UpdateTypesListing(False)
    Catch
    End Try
  End Sub

  ''' <summary>
  ''' Immediate Cell Click Update
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub DataGridViewCategories_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles DataGridViewCategories.CurrentCellDirtyStateChanged
    Try
      Me.DataGridViewCategories.CommitEdit(DataGridViewDataErrorContexts.Commit)
      UpdateTypesListing(False)
    Catch
    End Try
  End Sub

  ''' <summary>
  ''' Rename Stuff
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonRename_Click(sender As System.Object, e As System.EventArgs) Handles ButtonRename.Click

    ' Form Viz
    SetFormViz(formViz.isProcessing)

    ' Errors
    Dim m_errorsFam As New List(Of String)
    Dim m_errorsType As New List(Of String)

    ' Same
    Dim iSameFam As Integer = 0
    Dim iSameType As Integer = 0

    ' Success
    Dim iSucFam As Integer = 0
    Dim iSucType As Integer = 0

    ' Count Items
    With Me.ProgressBar1
      .Maximum = Me.DataGridViewElements.RowCount + 1
      .Value = 0
    End With

    ' Rename Each
    For Each x As clsFamilyType In Me.DataGridViewElements.DataSource

      Try
        ' Step Progress
        Me.ProgressBar1.Increment(1)
      Catch
      End Try

      Try

        Dim m_result As clsRenameResult = x.RenameElement

        ' family
        If String.IsNullOrEmpty(m_result.famResult) Then
          iSucFam += 1
        Else
          If m_result.famResult = "Same" Then
            iSameFam += 1
          Else
            If Not m_errorsFam.Contains(m_result.famResult) Then
              m_errorsFam.Add(m_result.famResult)
            End If
          End If
        End If

        ' types
        If String.IsNullOrEmpty(m_result.typResult) Then
          iSucType += 1
        Else
          If m_result.typResult = "Same" Then
            iSameType += 1
          Else
            If Not m_errorsType.Contains(m_result.typResult) Then
              m_errorsType.Add(m_result.typResult)
            End If
          End If
        End If

      Catch
      End Try

    Next

    ' Report to User
    Using td As New Autodesk.Revit.UI.TaskDialog("Here's What Just Happened:")
      td.MainInstruction = "Rename Attempt"
      Dim m_msg As String = iSameFam.ToString & " : Same Family Names (not changed)" & vbCr
      m_msg += iSucFam & " : Family Renames Succeeded..." & vbCr & vbCr
      m_msg += iSameType.ToString & " : Same Type Names (not changed)" & vbCr
      m_msg += iSucType & " : Type Renames Succeeded..." & vbCr
      If m_errorsFam.Count > 0 Then
        For Each x In m_errorsFam
          m_msg += vbCr & x
        Next
      End If
      m_msg += vbCr
      If m_errorsType.Count > 0 Then
        For Each x In m_errorsType
          m_msg += vbCr & x
        Next
      End If
      td.MainContent = m_msg
      td.Show()
    End Using

    ' Close
    Me.Close()

  End Sub

  ''' <summary>
  ''' Export Selected Information to Excel
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonExport_Click(sender As System.Object, e As System.EventArgs) Handles ButtonExport.Click

    ' Form Viz
    SetFormViz(formViz.isProcessing)

    With Me.ProgressBar1
      .Maximum = Me.DataGridViewElements.RowCount + 1
      .Value = 0
    End With

    Try

      ' Export to Excel
      If SaveFileDialog1.ShowDialog = DialogResult.OK Then

        ' Build the Datatable
        Dim m_dt As New DataTable("FamilyExport")
        With m_dt.Columns
          .Add("Category")
          .Add("Current Family Name")
          .Add("Current Type Name")
          .Add("New Family Name")
          .Add("New Type Name")
          .Add("ElementID")
          .Add("UniqueID")
        End With

        Try

          ' Add the Rows
          For Each x As clsFamilyType In Me.DataGridViewElements.DataSource

            Try
              ' Step Progress
              Me.ProgressBar1.Increment(1)
            Catch
            End Try

            ' The Row
            Dim m_dr As DataRow = m_dt.Rows.Add
            m_dr(0) = x.CategoryName
            m_dr(1) = x.CurrentFamilyName
            m_dr(2) = x.CurrentTypeName
            m_dr(3) = x.NewFamilyName
            m_dr(4) = x.NewTypeName
            m_dr(5) = x.ElementId
            m_dr(6) = x.GetUid

          Next

        Catch
        End Try

        ' Launch Excel and Write The Data
        Dim m_f As New clsExcel(SaveFileDialog1.FileName, _s, EnumExcelWriteMode.IsWrite)
        If m_f.IsFailed = False Then

          ' Write the Data
          If m_f.FillExcelWorksheetFromDataTable(m_dt) = False Then

            ' Failure
            MsgBox("Failed to Write Data to Excel", MsgBoxStyle.Exclamation, "Something Went Wrong")

          End If

        Else

          ' Failure
          MsgBox("Failed to Start or Attach to Running Excel Application",
                 MsgBoxStyle.Exclamation,
                 "Something Went Wrong")

        End If

      End If

    Catch
    End Try

    ' Form Viz
    SetFormViz(formViz.isStandby)

  End Sub

  ''' <summary>
  ''' Import Selected Information to Excel
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonImport_Click(sender As System.Object, e As System.EventArgs) Handles ButtonImport.Click

    Try

      ' Hide Categories
      Me.GroupBoxCategories.Hide()
      Me.ButtonExport.Enabled = False

      Try

        ' Widen Main Elements Group
        Me.GroupBoxElements.Location = New System.Drawing.Point(12, 12)
        Me.ProgressBar1.Location = New System.Drawing.Point(12, Me.ProgressBar1.Location.Y)

        ' Add 250 to the width
        Me.GroupBoxElements.Width = Me.GroupBoxElements.Width + 250
        Me.ProgressBar1.Width = Me.ProgressBar1.Width + 250

      Catch
      End Try


      ' Import from Excel
      If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then

        ' Launch Excel and Write The Data
        Dim m_f As New clsExcel(OpenFileDialog1.FileName, _s, EnumExcelWriteMode.IsReadOnly)
        If m_f.IsFailed = False Then

          Try

            ' Get the Result as a Datatable
            Dim m_dt As New DataTable("ImportedData")
            m_dt = m_f.FillDataTableFromExcelWorksheet("Family and Types")
            _s.ImportData(m_dt)

            ' Update Listing
            UpdateTypesListing(True)

          Catch
          End Try

        Else

          ' Failure
          MsgBox("Failed to Start or Attach to Running Excel Application",
                 MsgBoxStyle.Exclamation,
                 "Something Went Wrong")

        End If

      End If

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Cancel
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCancel.Click

    ' Close
    Me.Close()

  End Sub

#End Region

End Class
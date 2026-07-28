Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports System.Windows.Forms
Imports System.IO
Imports System.Diagnostics
Imports [Case].Export.Families.API
Imports [Case].Export.Families.Data

Public Class form_Families

  Private _s As clsSettings

  ''' <summary>
  ''' Form class constructor
  ''' </summary>
  ''' <param name="s">The Settings object</param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s

  End Sub

  ''' <summary>
  ''' Make sure the path does not contain any invalid file naming characters
  ''' </summary>
  ''' <param name="fileName">The Filename to check</param>
  ''' <returns>A string</returns>
  ''' <remarks></remarks>
  Private Function CheckValidFileName(ByVal fileName As String) As String

    ' Check Characters
    For Each c In Path.GetInvalidFileNameChars()
      If fileName.Contains(c) Then
        Return ""
      End If
    Next

    ' Result
    Return fileName

  End Function

  ''' <summary>
  ''' This routine performs the exports
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub doExport()

    ' Only export families that belong to our selected categories!
    Dim m_SelectedCategories = CheckedListBoxCategories.CheckedItems
    Dim m_CatHash As New Hashtable
    If m_SelectedCategories.Count = 0 Then
      MsgBox("You did not select any categories..." & vbCr & "Nothing to do...", _
             MsgBoxStyle.Information, "No Categories Selected! You're Boring :(")
      Exit Sub
    End If

    ' A hashtable comes in handy when verifying multiple situations... or 1
    For Each x In m_SelectedCategories
      m_CatHash.Add(x.ToString, "Category")
    Next

    Try ' If the parent export directory is missing, create it
      Directory.CreateDirectory(Replace(LabelExportPath.Text, "/", "\", , , CompareMethod.Text))
    Catch ex As Exception
      ' Message to show any errors
      MsgBox(Err.Description, MsgBoxStyle.Information, Err.Source)
      Exit Sub
    End Try

    ' Start the progressbar
    Dim m_iCnt As Integer = 0
    With ProgressBar1
      .Minimum = 0
      .Maximum = _s.SymbolElements.Count
      .Value = m_iCnt
    End With

    ' Dictionary of Exported Family Names
    Dim m_ExportedFams As New Dictionary(Of String, String)

    ' Failures
    Dim m_failedfams As New Dictionary(Of String, String)
    Dim m_fl As New List(Of String)

    ' Iterate and Export Selections
    For Each m_FamSymb As FamilySymbol In _s.SymbolElements
      m_iCnt += 1

      ' The Family Object
      Dim m_FamInst As Family = m_FamSymb.Family
      Dim m_FamName As String = m_FamInst.Name

      ' Get the category and build subdirectory
      If Not (m_FamSymb.Category Is Nothing) Then

        ' Is it a selected category?
        If m_CatHash.Contains(m_FamSymb.Category.Name) Then

          ' Target Directroy Path
          Dim m_ExportPath As String = Replace(LabelExportPath.Text & "\" & m_FamSymb.Category.Name & "\", "/", "\", , , CompareMethod.Text)
          m_ExportPath = Replace(m_ExportPath, "\\", "\", , , CompareMethod.Text)

          ' Does it Exist?
          If Not Directory.Exists(m_ExportPath) Then
            Try ' Create the subdirectory
              Directory.CreateDirectory(m_ExportPath)
            Catch ex As Exception
              ' User Permissions
            End Try
          End If

          Dim m_finalPath As String = m_ExportPath & m_FamName & ".rfa"

          ' Did we already export this family?
          If m_ExportedFams.ContainsKey(m_finalPath.ToLower) Then
            GoTo StepProgress
          Else
            m_ExportedFams.Add(m_finalPath.ToLower, m_finalPath.ToLower)
          End If

          ' Is the Family Path and Name Valid
          If CheckValidFileName(m_FamName) <> "" Then

            ' Update the File Action Text
            LabelFileName.Text = "...\" & m_FamSymb.Category.Name & "\" & m_FamInst.Name & ".rfa"

            ' Family Save Options to Overwrite
            Dim m_opt As New SaveAsOptions
            m_opt.OverwriteExistingFile = True

            '' ''If m_FamSymb.Category.Name.ToLower = "mass" Then
            '' ''  Dim m_t As String = ""
            '' ''End If
            '' ''If m_FamSymb.Category.Name.ToLower = "curtain wall mullions" Then
            '' ''  Dim m_t As String = ""
            '' ''End If
            '' ''If m_FamSymb.Category.Name.ToLower = "curtain panels" Then
            '' ''  Dim m_t As String = ""
            '' ''End If

            Try

              ' Get the Family Document
              Dim famDoc As Document = _s.Doc.EditFamily(m_FamInst)

              ' Save the Family Out
              famDoc.SaveAs(m_finalPath, m_opt)

              ' Always Close Without Saving
              famDoc.Close(False)

            Catch ex As Exception

              If Not m_failedfams.ContainsKey(m_FamName) Then
                m_failedfams.Add(m_FamName, "<" & m_FamSymb.Category.Name & "> Failed Because: " & ex.Message)
                m_fl.Add(m_FamName & " <" & m_FamSymb.Category.Name & "> Failed Because: " & ex.Message & vbCr)
              End If

            End Try
          End If
        End If
      End If
StepProgress:
      Try
        ' Step the progress bar
        ProgressBar1.Value = m_iCnt
        Application.DoEvents()
      Catch ex As Exception
        ' Quiet Fail
      End Try
    Next

    ' Report Mishaps
    If m_fl.Count < 1 Then Exit Sub

    Dim m_td As New Autodesk.Revit.UI.TaskDialog("Families That Couldn't Export")
    Dim m_Message As String = ""
    For Each x As String In m_fl
      m_Message += x
    Next
    m_td.MainInstruction = "In Place Masses or Related Components Cannot Export to RFA:"
    m_td.MainContent = m_Message
    m_td.ExpandedContent = "We're Sorry :("
    m_td.Show()

  End Sub

#Region "Form Visibility"

  Public Enum FormVis
    StandBy
    Processing
  End Enum

  ''' <summary>
  ''' Set the Form Control Visibilities
  ''' </summary>
  ''' <param name="p_vis"></param>
  ''' <remarks></remarks>
  Private Sub SetFormVis(p_vis As FormVis)
    Select Case p_vis
      Case FormVis.Processing
        ButtonCancel.Hide()
        ButtonExport.Hide()
        ButtonSelectAll.Hide()
        ButtonSelectNone.Hide()
        ProgressBar1.Show()
      Case FormVis.StandBy
        ButtonCancel.Show()
        ButtonExport.Show()
        ButtonSelectAll.Show()
        ButtonSelectNone.Show()
        ProgressBar1.Hide()
    End Select
  End Sub

#End Region

#Region "Form Controls & Events"

  ''' <summary>
  ''' Setup
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_Families_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    ' Form configurations
    Text = "Batch Export Family RFA's " & _s.AppVersion

    SetFormVis(FormVis.StandBy)
    Try
      LabelExportPath.Text = Path.GetDirectoryName(_s.DocName) & "\Exported Families\"
    Catch
    End Try

    ' Setup the list
    CheckedListBoxCategories.Items.Clear()
    CheckedListBoxCategories.CheckOnClick = True
    LabelFileName.Text = ""

    ' Categories
    For Each x As Category In _s.Categories.Values

      ' Add the category
      CheckedListBoxCategories.Items.Add(x.Name)

    Next

  End Sub

  ''' <summary>
  ''' Help Documentation
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As System.Object, e As System.EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/free-export-families-rfa-files-category")
  End Sub

  ''' <summary>
  ''' Case Site
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
    Process.Start("http://www.case-inc.com")
  End Sub

  ''' <summary>
  ''' Export the families and then quietly close
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonExport_Click(ByVal sender As System.Object,
                                 ByVal e As System.EventArgs) Handles ButtonExport.Click

    ' Start Processing
    SetFormVis(FormVis.Processing)

    ' Export
    doExport()

    ' Close Form
    Close()

  End Sub

  ''' <summary>
  ''' Cancel and do nothing
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(ByVal sender As System.Object,
                                 ByVal e As System.EventArgs) Handles ButtonCancel.Click
    Close()
  End Sub

  ''' <summary>
  ''' Brose to an export directory
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonBrowse_Click(ByVal sender As System.Object,
                                 ByVal e As EventArgs) Handles ButtonBrowse.Click
    ' Browse to select the parent export path
    ButtonExport.Enabled = False
    If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
      If Not String.IsNullOrEmpty(FolderBrowserDialog1.SelectedPath.ToString) Then
        LabelExportPath.Text = FolderBrowserDialog1.SelectedPath.ToString
      End If
      Try
        ' Enable the Export Button
        If Directory.Exists(LabelExportPath.Text) = True Then
          ButtonExport.Enabled = True
        End If
      Catch
      End Try
    End If
  End Sub

  ''' <summary>
  ''' Uncheck all items in the listbox
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonSelectNone_Click(ByVal sender As System.Object,
                                     ByVal e As System.EventArgs) Handles ButtonSelectNone.Click
    For i As Integer = 0 To CheckedListBoxCategories.Items.Count - 1
      CheckedListBoxCategories.SetItemChecked(i, False)
    Next
  End Sub

  ''' <summary>
  ''' Check all items in listbox
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonSelectAll_Click(ByVal sender As System.Object,
                                    ByVal e As System.EventArgs) Handles ButtonSelectAll.Click
    For i As Integer = 0 To CheckedListBoxCategories.Items.Count - 1
      CheckedListBoxCategories.SetItemChecked(i, True)
    Next
  End Sub

#End Region

End Class
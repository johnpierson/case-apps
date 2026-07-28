Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].ChangeReplaceFamTypeNames.Data

Public Class form_Main

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
    AllowOK()
    ProgressBar1.Hide()
    Text = "Change & Replace Family/Type Names v" & _s.Version

  End Sub

  ''' <summary>
  ''' Minimum Text Requirements
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AllowOk()

    If Not (String.IsNullOrEmpty(TextBoxFind.Text) And String.IsNullOrEmpty(TextBoxReplace.Text)) Then
      If Not (CheckBoxFamilies.Checked = False And Me.CheckBoxTypes.Checked = False) Then
        ButtonOk.Enabled = True
      Else
        ButtonOk.Enabled = False
      End If
    Else
      ButtonOk.Enabled = False
    End If

  End Sub

  ''' <summary>
  ''' Rename the Items
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub RenameStuff()

    ' Get the Families
    _s.GetFams()

    ' Only Rename Families Once
    Dim m_famFixed As New SortedDictionary(Of Integer, Integer)

    ' Progress Bar
    With Me.ProgressBar1
      .Value = 0
      .Minimum = 0
      .Maximum = _s.FamilyElements.Count
      .Show()
    End With

    ButtonOk.Hide()
    ButtonCancel.Hide()

    ' User Reporting
    Dim iFail As Integer = 0
    Dim iSuc As Integer = 0

    ' Process each Item
    For Each x In _s.FamilyElements

      ' Step the Progress Bar
      Try
        ProgressBar1.Increment(1)
      Catch
      End Try

      ' Renamed
      Dim m_renamed As Integer = 0

      ' Start a New Transaction
      Using t As New Transaction(_s.Doc, "CASE Renamer")
        If t.Start Then

          Try

            ' Check for Family Names
            If Me.CheckBoxFamilies.Checked = True Then

              ' Have we Processed the Family?
              If Not m_famFixed.ContainsKey(x.Family.Id.Value) Then

                ' Add it
                m_famFixed.Add(x.Family.Id.Value, x.Family.Id.Value)

                ' Check It
                If Not String.IsNullOrEmpty(TextBoxFind.Text) Then
                  If x.Family.Name.Contains(TextBoxFind.Text) Then
                    ' Rename This Part
                    Dim m_newName As String = Replace(x.Family.Name, Me.TextBoxFind.Text, Me.TextBoxReplace.Text, , , CompareMethod.Text)
                    x.Family.Name = m_newName
                    m_renamed += 1
                  End If
                End If

              End If

            End If

            ' Check for Type Names
            If Me.CheckBoxFamilies.Checked = True Then
              If Not String.IsNullOrEmpty(TextBoxFind.Text) Then
                If x.Name.Contains(TextBoxFind.Text) Then
                  ' Rename This Part
                  Dim m_newName As String = Replace(x.Name, Me.TextBoxFind.Text, Me.TextBoxReplace.Text, , , CompareMethod.Text)
                  x.Name = m_newName
                  m_renamed += 1
                End If
              End If
            End If

            ' Commit
            t.Commit()

            ' Count only if changes
            If m_renamed > 0 Then iSuc += m_renamed

          Catch
            iFail += 1
          End Try

        End If
      End Using

    Next

    ' Report to user
    Using td As New Autodesk.Revit.UI.TaskDialog("Here's What Heppened:")
      td.MainInstruction = "Family and/or Type Renaming"
      td.MainContent = iSuc.ToString & " Updated Succesfully" & vbCr
      td.MainContent += iFail.ToString & " Items Failed to Rename"
      td.Show()
    End Using

  End Sub

#Region "Form Controls & Events"

  ''' <summary>
  ''' Help Documentation
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As System.Object, e As System.EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/free-change-replace-family-andor-type-names")
  End Sub

  ''' <summary>
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOk.Click

    ' Commit
    RenameStuff()

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Close
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCancel.Click

    ' Close
    Close()

  End Sub

  Private Sub TextBoxFind_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBoxFind.TextChanged
    AllowOK()
  End Sub

  Private Sub TextBoxReplace_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBoxReplace.TextChanged
    AllowOK()
  End Sub

  Private Sub CheckBoxFamilies_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxFamilies.CheckedChanged
    AllowOK()
  End Sub

  Private Sub CheckBoxTypes_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxTypes.CheckedChanged
    AllowOK()
  End Sub

  Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
    Try
      Process.Start("http://apps.case-inc.com/")
    Catch
    End Try
  End Sub

#End Region

End Class
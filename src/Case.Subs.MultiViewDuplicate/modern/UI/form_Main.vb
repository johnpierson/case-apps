Imports Autodesk.Revit.DB
Imports System.Windows.Forms
Imports [Case].Subs.MultiViewDuplicate.Data

Public Class form_Main

  Private _s As clsSettings
  Private _doEvents As Boolean = False

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s

    ' Setup
    Setup()

  End Sub

#Region "Private Members"
  
  ''' <summary>
  ''' Form Setup
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub Setup()

    Try

      ' Title
      Text = "Multiple View Duplicator v" & _s.Version
      ProgressBar1.Hide()

      ' Combo Selections
      ComboBoxKind.Items.Add("<ALL>")
      ComboBoxKind.SelectedIndex = 0
      For Each x In _s.ViewKinds
        ComboBoxKind.Items.Add(x)
      Next

      ComboBoxTypes.Items.Add("<ALL>")
      ComboBoxTypes.SelectedIndex = 0
      For Each x In _s.ViewTypes
        ComboBoxTypes.Items.Add(x)
      Next

    Catch
    End Try

    ' Refresh
    _doEvents = True
    RefreshViewList()

  End Sub

  ''' <summary>
  ''' Duplicate Checked Views
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub DuplicateViews()

    ' Progress
    With ProgressBar1
      .Minimum = 0
      .Maximum = (NumericUpDown1.Value * DataGridViewViews.Rows.Count) + 1
      .Value = 1
      .Show()
    End With

    ' Process Each View
    For Each x As DataGridViewRow In DataGridViewViews.Rows

      ' Continue for each numeric
      For iii = 1 To NumericUpDown1.Value

        Try
          ' Step Progress
          ProgressBar1.Increment(1)
          Application.DoEvents()
        Catch
        End Try

        Try

          ' Get the Helper
          Dim m_v As clsView = x.DataBoundItem
          If Not m_v Is Nothing Then

            ' Only Process Checked Views
            If m_v.isChecked = False Then Continue For

            ' Start a New Transaction
            Using t As New Transaction(_s.Doc, "View Duplicator: " & m_v.Name)
              If t.Start = TransactionStatus.Started Then

                Try

                  ' New Name
                  Dim m_newName As String = ""
                  Dim m_nameAdd As String = TextBoxNameAdd.Text
                  If Not String.IsNullOrEmpty(m_nameAdd) Then
                    If RadioButtonPrefix.Checked = True Then
                      m_newName = Replace(m_nameAdd & m_v.Name, "  ", " ", , , CompareMethod.Text)
                    Else
                      m_newName = Replace(m_v.Name & m_nameAdd, "  ", " ", , , CompareMethod.Text)
                    End If
                  End If


                  Try

                    ' Check for Unique Name Requirements
                    If _s.ViewNames(m_v.ViewKind.ToLower).ContainsKey(m_newName.ToLower) Then

                      ' Find Next Available
                      For i = 1 To 1000

                        ' Test
                        Dim m_test As String = m_newName & "(" & i.ToString & ")"

                        ' Unique?
                        If Not _s.ViewNames(m_v.ViewKind.ToLower).ContainsKey(m_test.ToLower) Then

                          ' Unique Name
                          _s.ViewNames(m_v.ViewKind.ToLower).Add(m_test.ToLower, m_test)
                          m_newName = m_test
                          Exit For

                        End If

                      Next

                    End If

                  Catch
                  End Try

                  ' Duplicate Options
                  Dim m_vOpt As ViewDuplicateOption
                  If RadioButtonDup.Checked = True Then m_vOpt = ViewDuplicateOption.Duplicate
                  If RadioButtonDet.Checked = True Then m_vOpt = ViewDuplicateOption.WithDetailing
                  If RadioButtonDependent.Checked = True Then m_vOpt = ViewDuplicateOption.AsDependent

                  ' Create and Rename New View
                  Dim m_newViewId As ElementId = m_v.GetView.Duplicate(m_vOpt)
                  Dim m_newViewElement As Autodesk.Revit.DB.View = _s.Doc.GetElement(m_newViewId)

                  ' Total
                  If CInt(NumericUpDown1.Value) > 1 Then
                    m_newName += " (" & CInt(iii).ToString & ")"
                  End If
                  m_newViewElement.Name = m_newName

                  ' Success
                  t.Commit()

                Catch
                End Try

              End If

            End Using

          End If

        Catch
        End Try

      Next

    Next

  End Sub

  ''' <summary>
  ''' Load View List
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub RefreshViewList(Optional ch As String = "")

    ' Events off?
    If _doEvents = False Then Exit Sub

    ' Fresh List
    Dim m_v As New SortableBindingList(Of clsView)

    Try

      ' Load Views
      For Each x In _s.Views

        If x.ViewType.ToLower.Contains("sched") Then
          If RadioButtonDup.Checked = False Then Continue For
        End If

        If x.ViewType.ToLower.Contains("render") Then
          If RadioButtonDependent.Checked = True Then Continue For
        End If
        If x.ViewType.ToLower.Contains("three") Then
          If RadioButtonDependent.Checked = True Then Continue For
        End If
        If x.ViewType.ToLower.Contains("draft") Then
          If RadioButtonDependent.Checked = True Then Continue For
        End If

        ' Combo Filtering?
        If ComboBoxKind.SelectedIndex > 0 Then
          If Not ComboBoxKind.SelectedItem.ToString.ToLower = x.ViewKind.ToLower Then Continue For
        End If
        If ComboBoxTypes.SelectedIndex > 0 Then
          If Not ComboBoxTypes.SelectedItem.ToString.ToLower = x.ViewType.ToLower Then Continue For
        End If

        ' Naming?
        If Not String.IsNullOrEmpty(TextBoxName.Text) Then
          If Not x.Name.ToLower.Contains(TextBoxName.Text.ToLower) = RadioButtonNameContains.Checked Then
            Continue For
          End If
        End If

        ' Force Check?
        If Not String.IsNullOrEmpty(ch) Then
          If ch.ToLower.Contains("f") Then
            x.isChecked = False
          Else
            x.isChecked = True
          End If
        End If

        ' Add the View
        m_v.Add(x)

      Next

      ' Bind to Grid
      DataGridViewViews.DataSource = m_v

    Catch
    End Try

  End Sub

#End Region

#Region "Form Controls & Events"

  ''' <summary>
  ''' Help
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click
    ' Process.Start("http://apps.case-inc.com/content/free-multiple-view-duplicator")
  End Sub

  ''' <summary>
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As System.Object, e As EventArgs) Handles ButtonOk.Click

    ' Form Controls
    ButtonCancel.Hide()
    ButtonOk.Hide()
    ButtonHelp.Hide()
    ButtonAll.Hide()
    ButtonNone.Hide()
    NumericUpDown1.Hide()
    LabelCopies.Hide()

    ' Progress
    DuplicateViews()

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
  ''' Check All Views
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonAll_Click(sender As Object, e As EventArgs) Handles ButtonAll.Click
    RefreshViewList("t")
  End Sub

  ''' <summary>
  ''' Unchaeck All Views
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonNone_Click(sender As Object, e As EventArgs) Handles ButtonNone.Click
    RefreshViewList("f")
  End Sub

  Private Sub RadioButtonDependent_CheckedChanged(sender As System.Object, e As EventArgs) Handles RadioButtonDependent.CheckedChanged
    RefreshViewList()
  End Sub

  Private Sub RadioButtonDet_CheckedChanged(sender As System.Object, e As EventArgs) Handles RadioButtonDet.CheckedChanged
    RefreshViewList()
  End Sub

  Private Sub RadioButtonDup_CheckedChanged(sender As System.Object, e As EventArgs) Handles RadioButtonDup.CheckedChanged
    RefreshViewList()
  End Sub

  Private Sub ComboBoxTypes_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles ComboBoxTypes.SelectedIndexChanged
    RefreshViewList()
  End Sub

  Private Sub ComboBoxKind_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles ComboBoxKind.SelectedIndexChanged
    RefreshViewList()
  End Sub

  Private Sub TextBoxName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxName.TextChanged
    RefreshViewList()
  End Sub

  Private Sub RadioButtonNameContains_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonNameContains.CheckedChanged
    RefreshViewList()
  End Sub

  Private Sub RadioButtonNameNotContain_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonNameNotContain.CheckedChanged
    RefreshViewList()
  End Sub

#End Region

End Class
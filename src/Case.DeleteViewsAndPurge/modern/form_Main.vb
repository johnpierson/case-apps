Imports Autodesk.Revit.DB
Imports System.Windows.Forms
Imports System.Reflection
Imports System.Linq
Imports [Case].DeleteViewsAndPurge.API
Imports [Case].DeleteViewsAndPurge.Data

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

  End Sub

  ''' <summary>
  ''' Remove all Views
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub DeleteItems()

    Dim iCntshts As Integer = 0
    Dim iCntview As Integer = 0
    Dim iCntLinks As Integer = 0

    ' Delete Links?
    If Me.CheckBoxLinks.Checked = True Then
      Try
        iCntLinks = _s.DeleteLinks
      Catch
      End Try
    End If

    ' Delete Sheets?
    If Me.CheckBoxSheets.Checked = True Then

      ' New Transaction
      Using t As New Transaction(_s.Doc, "Delete Sheets")
        If t.Start = TransactionStatus.Started Then
          Try

            Try
              ProgressBar1.Increment(_s.Sheets.Count)
              Application.DoEvents()
            Catch
            End Try

            ' Delete the Sheets
            _s.Doc.Delete(_s.Sheets)

            ' How many got deleted
            iCntshts = _s.Sheets.Count

            ' Success
            t.Commit()

          Catch
          End Try
        End If
      End Using

    End If

    ' Delete Views?
    If Me.CheckBoxViews.Checked = True Then

      ' Delete Views
      Dim m_iDeletedItems As Integer = -1
      Do Until m_iDeletedItems = 0

        ' Delete
        m_iDeletedItems = _s.DeleteViews(ProgressBar1)
        iCntview += m_iDeletedItems

      Loop

    End If

    ' Tell User
    Using td As New Autodesk.Revit.UI.TaskDialog("Here's What Just Happened!:")
      td.MainInstruction = iCntshts.ToString & " Sheets were removed..." & vbCr &
          iCntview.ToString & " Views were removed..." & vbCr & vbCr &
          iCntLinks.ToString & " Revit Links succesfully removed..." & vbCr & vbCr &
          "Please use the Purge command (repeatedly) if you need to remove unused items from the model"
      td.MainContent = "If this makes you sad, do not save the model!"
      td.Show()
    End Using

  End Sub

  '' '' ''' <summary>
  '' '' ''' Delete the Unused Types
  '' '' ''' </summary>
  '' '' ''' <remarks></remarks>
  '' ''Private Sub DeleteUnusedTypes()

  '' ''  ' Get Unused types
  '' ''  For Each x As Category In _s.Doc.Settings.Categories

  '' ''    ' Step the Progress
  '' ''    Try
  '' ''      ProgressBar1.Increment(1)
  '' ''      Application.DoEvents()
  '' ''    Catch ex As Exception
  '' ''    End Try

  '' ''    ' Get the Elements
  '' ''    Dim m_ebc As New clsElementsByCategory(x, _s.Doc)
  '' ''    If m_ebc.TypeElements.Count < 1 Then Continue For
  '' ''    If m_ebc.PurgeItems.Values.Count < 1 Then Continue For

  '' ''    ' Transaction
  '' ''    Using t As New Transaction(_s.Doc, "Purging Unused Types - " & x.Name)
  '' ''      If t.Start Then
  '' ''        Dim m_attempt As Integer = 0

  '' ''        Try

  '' ''          ' Delete the Unused Types
  '' ''          Dim m_purge As New List(Of ElementId)
  '' ''          For Each e As ElementId In m_ebc.PurgeItems.Values
  '' ''            m_purge.Add(e)
  '' ''            m_attempt += 1
  '' ''          Next

  '' ''          If m_attempt = 0 Then
  '' ''            t.RollBack()
  '' ''            Continue For
  '' ''          End If

  '' ''          ' Delete from Model
  '' ''          _s.Doc.Delete(m_purge)

  '' ''          ' Purge Success
  '' ''          _purgeSuccess += m_attempt

  '' ''          ' Commit
  '' ''          t.Commit()

  '' ''        Catch

  '' ''          ' Purge Failure
  '' ''          _purgeFailure += m_attempt

  '' ''          ' Rollback Failures
  '' ''          t.RollBack()

  '' ''        End Try
  '' ''      End If
  '' ''    End Using

  '' ''  Next

  '' ''End Sub

#Region "Form Visibility"

  Private Enum FormViz
    Processing
    StandBy
  End Enum

  ''' <summary>
  ''' Set the Form Visibility Basedon State
  ''' </summary>
  ''' <param name="v"></param>
  ''' <remarks></remarks>
  Private Sub SetFormViz(v As FormViz)
    Select Case v
      Case FormViz.Processing
        ProgressBar1.Show()
        ButtonCancel.Hide()
        ButtonOk.Hide()
        ButtonHelp.Hide()
      Case FormViz.StandBy
        ProgressBar1.Hide()
        ButtonCancel.Show()
        ButtonOk.Show()
        ButtonHelp.Show()
    End Select
  End Sub

#End Region

#Region "Form Controls & Events"

  ''' <summary>
  ''' Form Shown
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_Main_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    ' Form Setup
    SetFormViz(FormViz.StandBy)
    Text = "Delete Sheets, Views and Links v" & Assembly.GetExecutingAssembly.GetName.Version.ToString

  End Sub

  ''' <summary>
  ''' Help Documentation
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As System.Object, e As System.EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/free-delete-sheets-views-and-revit-links")
  End Sub

  ''' <summary>
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOk.Click

    ' Set for Visibility
    SetFormViz(FormViz.Processing)

    ' Are they sure?
    If MsgBox("It is recommended that you only run this utility on detached models!!" & vbCr & vbCr &
              "Are You Sure You Want to Continue??" & vbCr & "Removing Links Cannot be Undone!",
              MsgBoxStyle.YesNoCancel,
              "Are You  SURE?!") = MsgBoxResult.Yes Then

      If Me.CheckBoxSheets.Checked = True Then

        ' Get Sheets
        _s.GetSheets()

      End If

      If Me.CheckBoxViews.Checked = True Then

        ' Get Views
        _s.GetViews()

      End If

      ' Prep Progress Bar
      With Me.ProgressBar1
        .Minimum = 0
        .Maximum = _s.Sheets.Count + _s.Views.Count
        .Value = 0
      End With

      ' Delete Views
      DeleteItems()

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
  Private Sub ButtonCancel_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCancel.Click
    Close()
  End Sub

  ''' <summary>
  ''' Launch Case Site
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
    Process.Start("http://www.case-inc.com")
  End Sub

#End Region

End Class
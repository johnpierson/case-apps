Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports System.Diagnostics

Imports [Case].ModeledRoomTags.Data

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

    ' Setup
    doSetup()

  End Sub

  ''' <summary>
  ''' General Setup
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub doSetup()

    Try

      ' Form configurations
      Text = "Place 3D Room Tags v" & My.Application.Info.Version.ToString

      ' Valid Symbol?
      If Not _s.TagSymbol Is Nothing Then

        ' Continue
        SetFormViz(formViz.isStandby)

        ' Bind to Grid
        DataGridViewRooms.DataSource = _s.RoomTags

      Else

        ' Fail
        MsgBox("Required '3D_RoomTag' Family Does Not Exist in Generic Model Category!", MsgBoxStyle.Critical, "Cannot Continue!")
        Close()

      End If

    Catch 
    End Try

  End Sub

#Region "Form Viz"

  Private Enum formViz
    isStandby
    isProcessing
  End Enum

  ''' <summary>
  ''' Set the Form Viz
  ''' </summary>
  ''' <param name="v"></param>
  ''' <remarks></remarks>
  Private Sub SetFormViz(v As formViz)

    Select Case v
      Case formViz.isProcessing
        ProgressBar1.Show()
        ButtonOk.Hide()
        ButtonCancel.Hide()
        ButtonHelp.Hide()

      Case formViz.isStandby
        ProgressBar1.Hide()
        ButtonOk.Show()
        ButtonCancel.Show()
        ButtonHelp.Show()

    End Select

  End Sub

#End Region

  ''' <summary>
  ''' Place the Tags
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub PlaceTags()

    ' Replace Existing?
    If _s.MatchingInstances.Count > 0 Then

      ' Delete All Current Instances?
      If Not MsgBox("All " & _s.MatchingInstances.Count.ToString & " Instances of Family:" & vbCr &
                    "'3D_RoomTag' Will be Deleted and Replaced!",
                    MsgBoxStyle.YesNo,
                    "Continue?") = MsgBoxResult.Yes Then

        ' Only Continue On a Yes
        Exit Sub

      End If

    End If

    ' Progress
    With Me.ProgressBar1
      .Minimum = 0
      .Maximum = _s.RoomTags.Count
      .Value = 0
    End With

    ' Delete the Elements
    Using t As New Transaction(_s.Doc, "3D Model Room Tag Deletion")
      If t.Start() Then

        Try
          _s.Doc.Delete(_s.MatchingInstances)
          t.Commit()
        Catch
          t.RollBack()
        End Try
        Try
          ' Refresh the Model
          _s.Doc.Regenerate()
        Catch
        End Try

      End If
    End Using

    ' Counts
    Dim iNo As Integer = 0
    Dim iYes As Integer = 0

    ' Place Each Tag
    For Each x As clsRoomTagInfo In _s.RoomTags

      Try
        ' Progress
        ProgressBar1.Increment(1)
      Catch 
      End Try

      ' Placement
      If x.PlaceTagElement(_s.TagSymbol) = True Then
        iYes += 1
      Else
        iNo += 1
      End If

    Next

    ' Inform User
    Using td As New Autodesk.Revit.UI.TaskDialog("3D Room Tag Placement Results")
      Dim m_top As String = ""
      Dim m_msg As String = ""
      If iYes > 0 And iNo = 0 Then
        m_top = "No Failures!"
        m_msg = iYes.ToString & " tags placed!"
      End If
      If iYes > 0 And iNo > 0 Then
        m_top = "****  Some Failures!  ****"
        m_msg = iYes.ToString & " tags placed!" & vbCr
        m_msg += iNo.ToString & " tags failed to place!"
      End If
      If iYes = 0 And iNo > 0 Then
        m_top = "******   All Failed!   ******"
        m_msg = iNo.ToString & " tags failed to place!"
      End If
      td.MainContent = m_msg
      td.MainInstruction = m_top
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
    Process.Start("http://apps.case-inc.com/content/free-place-3d-room-tags")
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

    ' Commit
    PlaceTags()

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
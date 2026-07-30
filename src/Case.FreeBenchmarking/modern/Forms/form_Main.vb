Public Class form_Main

  Private _s As clsSettings

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="p_s"></param>
  ''' <remarks></remarks>
  Public Sub New(p_s As clsSettings)
    InitializeComponent()
    ' Widen Scope
    _s = p_s
  End Sub

#Region "Form Controls & Events"

  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    ' Start Time
    Dim m_time As String = Now().ToString

    ' Run Test
    Dim t As New clsAutomationTest(_s)

    If t.Test1 = True Then

      ' Success
      AppendLog("Command 1", m_time, Now().ToString, "")

    Else

      ' Failure
      AppendLog("Command 1 - Failed", m_time, Now().ToString, "X")

    End If

  End Sub

  Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    ' Start Time
    Dim m_time As String = Now().ToString

    ' Run Test
    Dim t As New clsAutomationTest(_s)

    If t.Test2 = True Then

      ' Success
      AppendLog("Command 2", m_time, Now().ToString, "")

    Else

      ' Failure
      AppendLog("Command 2 - Failed", m_time, Now().ToString, "X")

    End If

  End Sub

  Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    ' Start Time
    Dim m_time As String = Now().ToString

    ' Run Test
    Dim t As New clsAutomationTest(_s)

    If t.Test3 = True Then

      ' Success
      AppendLog("Command 3", m_time, Now().ToString, "")

    Else

      ' Failure
      AppendLog("Command 3 - Failed", m_time, Now().ToString, "X")

    End If

  End Sub

  Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

    ' Start Time
    Dim m_time As String = Now().ToString

    ' Run Test
    Dim t As New clsAutomationTest(_s)

    If t.Test4 = True Then

      ' Success
      AppendLog("Command 4", m_time, Now().ToString, "")

    Else

      ' Failure
      AppendLog("Command 4 - Failed", m_time, Now().ToString, "X")

    End If

  End Sub

  Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

    ' Start Time
    Dim m_time As String = Now().ToString

    ' Run Test
    Dim t As New clsAutomationTest(_s)

    If t.Test5 = True Then

      ' Success
      AppendLog("Command 5", m_time, Now().ToString, "")

    Else

      ' Failure
      AppendLog("Command 5 - Failed", m_time, Now().ToString, "X")

    End If

  End Sub

#End Region

End Class
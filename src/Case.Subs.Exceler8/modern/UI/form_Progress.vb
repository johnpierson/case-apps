Imports System.Windows.Forms

Public Class form_Progress

  ''' <summary>
  ''' Progress Form
  ''' </summary>
  ''' <param name="t">Progress Title</param>
  ''' <param name="m">Max Value</param>
  ''' <remarks></remarks>
  Public Sub New(t As String, m As Integer)
    InitializeComponent()

    ' Widen Scope
    Text = t & " v" & My.Application.Info.Version.ToString()
    With ProgressBar1
      .Minimum = 0
      .Maximum = m
      .Value = 0
    End With

  End Sub

  ''' <summary>
  ''' Update Maximums, etc
  ''' </summary>
  ''' <param name="mVal">New Max Value</param>
  ''' <param name="cVal">New Value</param>
  ''' <remarks></remarks>
  Public Sub UpdateMaxAndValue(mVal As Integer, cVal As Integer)
    Try
      ProgressBar1.Maximum = mVal
    Catch
    End Try
    Try
      ProgressBar1.Value = cVal
    Catch
    End Try
    Application.DoEvents()
  End Sub

  ''' <summary>
  ''' Step the Progress Bar
  ''' </summary>
  ''' <param name="t">Optional Progress Title</param>
  ''' <remarks></remarks>
  Public Sub StepProgress(Optional t As String = "")

    Try

      ' Update Title
      If Not String.IsNullOrEmpty(t) Then Text = t & " v" & My.Application.Info.Version.ToString()

      ' Update Progress
      ProgressBar1.Increment(1)
      Application.DoEvents()

    Catch
    End Try

  End Sub

End Class
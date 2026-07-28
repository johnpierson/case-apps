Imports System.Windows.Forms

Public Class form_Progress

  Private _kind As String = ""

  ''' <summary>
  ''' Setup the Progress Bar
  ''' </summary>
  ''' <param name="max"></param>
  ''' <remarks></remarks>
  Public Sub New(max As Integer, pkind As String)
    InitializeComponent()

    ' Setup
    _kind = pkind
    With Me.ProgressBar1
      .Minimum = 0
      .Value = 0
      .Maximum = max
    End With

  End Sub

  ''' <summary>
  ''' Increment
  ''' </summary>
  ''' <param name="p"></param>
  ''' <remarks></remarks>
  Public Sub Inc(p As String)

    Try
      Me.Text = "Loading " & _kind & " Parameter: " & p
      Me.ProgressBar1.Increment(1)
      Application.DoEvents()
    Catch
    End Try

  End Sub

End Class
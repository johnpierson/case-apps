Namespace UI
  Public Class form_Progress

    ''' <summary>
    ''' Main Progress Form
    ''' </summary>
    ''' <param name="valMax">Maximum Progress Goal Value</param>
    ''' <param name="titleText">Form Title - Full</param>
    ''' <remarks></remarks>
    Public Sub New(valMax As Integer,
                   titleText As String)
      InitializeComponent()

      ' Title
      Text = titleText

      ' Progress
      With ProgressBar1
        .Minimum = 0
        .Maximum = valMax
        .Value = 0
      End With

    End Sub

    ''' <summary>
    ''' Step the Progress Bar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StepProgress()

      Try

        ' Step the Value
        ProgressBar1.Value += 1

        ' Force the Updates
        System.Windows.Forms.Application.DoEvents()

      Catch
      End Try

    End Sub

  End Class
End Namespace
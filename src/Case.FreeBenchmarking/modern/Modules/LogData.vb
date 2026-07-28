Imports System.IO

Module LogData

  ' time	Machine	User	EventName	TimeStart	TimeEnd	Comments

  ''' <summary>
  ''' Append a Line to the Log
  ''' </summary>
  ''' <param name="eName"></param>
  ''' <param name="eStart"></param>
  ''' <param name="eEnd"></param>
  ''' <param name="eComments"></param>
  ''' <remarks></remarks>
  Public Sub AppendLog(eName As String,
                       eStart As String,
                       eEnd As String,
                       eComments As String)

    Try

      Dim _path As String
      _path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
      _path += "\Revit2012Benchmarking"

      ' Verify Path Exists
      If Not Directory.Exists(_path) Then

        Try

          ' Create it
          Directory.CreateDirectory(_path)

        Catch ex As Exception

          MsgBox("Error Creating Revit2012Benchmarking Directory!" & vbCr & ex.Message, MsgBoxStyle.Critical, "Cannot Create Directory!")

        End Try

      End If

      _path += "\MyBenchmarkData.csv"

      ' Writer
      Using sw As New StreamWriter(_path, True)

        ' Write the Line
        sw.WriteLine(Environment.MachineName & vbTab &
                     Environment.UserName & vbTab &
                     eName & vbTab &
                     eStart & vbTab &
                     eEnd & vbTab &
                     eComments)
      End Using

    Catch ex As Exception

      MsgBox("Error Writing to Log file!" & vbCr & ex.Message, MsgBoxStyle.Critical, "Cannot Write to Log!")

    End Try

  End Sub

End Module
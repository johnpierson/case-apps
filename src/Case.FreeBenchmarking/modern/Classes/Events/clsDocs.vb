Imports System.IO

Public Class clsDocs

#Region "Public Properties"

  Public Property FilePath As String
  Public Property TimeStart As Date
  Public Property FileSizeMb As Double

#End Region

  ''' <summary>
  ''' Sync With Central Data Tracker
  ''' </summary>
  ''' <param name="fPath"></param>
  ''' <remarks></remarks>
  Public Sub New(fPath As String)

    ' Widen Scope
    FilePath = fPath
    TimeStart = Now()

    ' Starting File Size
    Try
      Dim MyFile As New FileInfo(FilePath)
      FileSizeMb = MyFile.Length / 1024
    Catch ex As Exception
      FileSizeMb = -1
    End Try

  End Sub

End Class
Imports System.IO

Public Class clsSWC

  Private _s As clsSettings

#Region "Public Properties"

  Public Property Comment As String
  Public Property SWCstart As Date
  Public Property FileSizeMbStart As Double
  Public Property FileSizeMbEnd As Double

#End Region

  ''' <summary>
  ''' Sync With Central Data Tracker
  ''' </summary>
  ''' <param name="s"></param>
  ''' <param name="cmt"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings, cmt As String)

    ' Widen Scope
    _s = s
    SWCstart = Now()
    If Not String.IsNullOrEmpty(cmt) Then
      Comment = cmt
    Else
      Comment = ""
    End If

    ' Starting File Size
    Try
      Dim MyFile As New FileInfo(_s.DocumentName)
      FileSizeMbStart = MyFile.Length / 1024
    Catch ex As Exception
      FileSizeMbStart = -1
    End Try

  End Sub

  ''' <summary>
  ''' End Data
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub EndSwc()

    ' Ending File Size
    Try
      Dim MyFile As New FileInfo(_s.DocumentName)
      FileSizeMbEnd = MyFile.Length / 1024
    Catch ex As Exception
      FileSizeMbEnd = -1
    End Try

  End Sub

End Class
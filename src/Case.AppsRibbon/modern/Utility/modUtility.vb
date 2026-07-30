Imports System.IO

Namespace Utility

  Module ModUtility

    ''' <summary>
    ''' Get or Create the Master CASE Temp Path
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetCaseRoamingPath() As String
      Try
        ' Main CASE Roaming AppData Folder
        Dim m_master As String = Path.Combine(
           Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
           "CASE Design, Inc.")
        If Not Directory.Exists(m_master) Then Directory.CreateDirectory(m_master)
        If Directory.Exists(m_master) Then Return m_master
      Catch
      End Try
      Return ""
    End Function

    ''' <summary>
    ''' Log Stuff
    ''' </summary>
    ''' <param name="logData"></param>
    ''' <remarks></remarks>
    Friend Sub PostToLog(logData As String)
      Try
        Dim m_dirName As String = GetCaseRoamingPath()
        If Not Directory.Exists(m_dirName) Then Return

        Dim m_fileName As String = Path.Combine(m_dirName,
                                                String.Format("CASE APPS_{0}.log", DateTime.Now.ToString("yyMMdd")))
        Using sr As New StreamWriter(m_fileName, True)
          sr.WriteLine(logData)
          sr.WriteLine()
        End Using
      Catch
      End Try
    End Sub

  End Module
End Namespace
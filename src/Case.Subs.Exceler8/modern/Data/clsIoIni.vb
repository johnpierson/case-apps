Imports System.IO

Namespace Data

  Public Class clsIoIni

    Private _path As String = ""

    Public Property FileSettings As SortedDictionary(Of String, Dictionary(Of String, List(Of clsDirection)))
    Public Property SyncAsNumeric As Boolean

    ' Find the File and read it

    ' Collection by File Name(C:\path.xlsx[worksheetName])
    ' - Worksheet
    ' - - Inst Param Settings[INST]
    ' - - - This Parameter Name [To Revit]
    ' - - - This Parameter Name [To Excel]
    ' - - - This Parameter Name [Ignore]
    ' - - Type Param Settings[TYPE]
    ' - - - This Parameter Name [To Revit]
    ' - - - This Parameter Name [To Excel]
    ' - - - This Parameter Name [Ignore]

    ''' <summary>
    ''' Configuration Helper
    ''' </summary>
    ''' <param name="p"></param>
    ''' <remarks></remarks>
    Public Sub New(p As String)

      ' Widen Scope
      _path = p

      ' Setup
      Setup()

    End Sub

    ''' <summary>
    ''' Setup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Setup()

      ' Setup
      FileSettings = New SortedDictionary(Of String, Dictionary(Of String, List(Of clsDirection)))
      SyncAsNumeric = False

      Try

        ' ParamKind
        Dim m_kind As String = ""

        ' File
        Dim m_file As String = ""

        ' Read the File
        If File.Exists(_path) Then
          Using sr As New StreamReader(_path)
            Do Until sr.EndOfStream

              ' Line From File
              Dim m_txt As String = sr.ReadLine

              ' Any Value?
              If String.IsNullOrEmpty(m_txt) Then Continue Do

              ' Excel File?
              If m_txt.ToLower.Contains(".xls") Then

                ' File
                m_file = m_txt.ToLower

                Try
                  ' This is a New File
                  FileSettings.Add(m_file, New Dictionary(Of String, List(Of clsDirection)))
                  FileSettings(m_file).Add("inst", New List(Of clsDirection))
                  FileSettings(m_file).Add("type", New List(Of clsDirection))
                Catch
                End Try

              Else

                ' Numeric
                If m_txt.ToLower.StartsWith("numeric=") Then
                  If m_txt.ToLower = "Numeric=true" Then
                    SyncAsNumeric = True
                  Else
                    SyncAsNumeric = False
                  End If
                  Continue Do
                End If

                ' Add Data to Existing
                If m_txt.ToLower = "[inst]" Then
                  m_kind = "i"
                  Continue Do
                End If
                If m_txt.ToLower = "[type]" Then
                  m_kind = "t"
                  Continue Do
                End If

                ' Need a File
                If String.IsNullOrEmpty(m_file) Then Continue Do

                ' Add to Set
                If m_kind = "i" Then

                  ' Direction
                  If m_txt.ToLower.StartsWith("[toexcel]") Then
                    FileSettings(m_file)("inst").Add(New clsDirection(Replace(m_txt, "[toExcel]", "", , , CompareMethod.Text), EnumSyncDir.toExcel))
                  End If
                  If m_txt.ToLower.StartsWith("[torevit]") Then
                    FileSettings(m_file)("inst").Add(New clsDirection(Replace(m_txt, "[toRevit]", "", , , CompareMethod.Text), EnumSyncDir.toRevit))
                  End If
                  If m_txt.ToLower.StartsWith("[isignore]") Then
                    FileSettings(m_file)("inst").Add(New clsDirection(Replace(m_txt, "[isIgnore]", "", , , CompareMethod.Text), EnumSyncDir.isIgnore))
                  End If

                End If
                If m_kind = "t" Then

                  ' Direction
                  If m_txt.ToLower.StartsWith("[toexcel]") Then
                    FileSettings(m_file)("type").Add(New clsDirection(Replace(m_txt, "[toExcel]", "", , , CompareMethod.Text), EnumSyncDir.toExcel))
                  End If
                  If m_txt.ToLower.StartsWith("[torevit]") Then
                    FileSettings(m_file)("type").Add(New clsDirection(Replace(m_txt, "[toRevit]", "", , , CompareMethod.Text), EnumSyncDir.toRevit))
                  End If
                  If m_txt.ToLower.StartsWith("[isignore]") Then
                    FileSettings(m_file)("type").Add(New clsDirection(Replace(m_txt, "[isIgnore]", "", , , CompareMethod.Text), EnumSyncDir.isIgnore))
                  End If

                End If

              End If

            Loop
          End Using
        End If

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Add or Update a Sync Setting
    ''' </summary>
    ''' <param name="fPath">Path to Excel Doc</param>
    ''' <param name="wsName">Worksheet Tab Name</param>
    ''' <param name="tList">List of Types</param>
    ''' <param name="iList">List of Instances</param>
    ''' <param name="isNumeric">Sync as Numeric</param>
    ''' <remarks></remarks>
    Public Sub UpdateSetting(fPath As String,
                             wsName As String,
                             tList As List(Of clsDirection),
                             iList As List(Of clsDirection),
                             isNumeric As Boolean)

      Try

        ' Numeric
        SyncAsNumeric = isNumeric

        ' File Name as Key
        Dim m_key As String = fPath.ToLower & "[" & wsName.ToLower & "]"

        ' Does it Exist?
        If Not FileSettings.ContainsKey(m_key) Then

          ' Insert
          FileSettings.Add(m_key, New Dictionary(Of String, List(Of clsDirection)))
          FileSettings(m_key).Add("inst", New List(Of clsDirection))
          FileSettings(m_key).Add("type", New List(Of clsDirection))

        Else

          ' Fresh Lists
          FileSettings(m_key)("inst") = New List(Of clsDirection)
          FileSettings(m_key)("type") = New List(Of clsDirection)

        End If

        ' Post Updated Data
        For Each x In tList
          FileSettings(m_key)("type").Add(x)
        Next
        For Each x In iList
          FileSettings(m_key)("inst").Add(x)
        Next

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Update the INI File
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub WriteIniFile()

      Try

        ' Stream Writer
        Using sw As New StreamWriter(_path, False)

          ' Process All
          For Each x In FileSettings

            ' File
            sw.WriteLine(x.Key)

            ' Type Parameters
            If x.Value("type").Count > 0 Then
              sw.WriteLine("[TYPE]")
              For Each p In x.Value("type")
                sw.WriteLine("[" & p.Direction.ToString & "]" & p.NameAndGroup)
              Next
            End If

            ' Instance Parameters
            If x.Value("inst").Count > 0 Then
              sw.WriteLine("[INST]")
              For Each p In x.Value("inst")
                sw.WriteLine("[" & p.Direction.ToString & "]" & p.NameAndGroup)
              Next
            End If

            ' Empty Line
            sw.WriteLine("")

          Next

          ' Numeric
          sw.WriteLine("Numeric=" & SyncAsNumeric.ToString)

        End Using

      Catch
      End Try

    End Sub

  End Class
End Namespace
Imports System.IO

Namespace Data

  Public Class clsIoConfig

    Private _path As String = ""

#Region "Public Properties"

    ''' <summary>
    ''' Filename without extension or path
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ConfigName As String
      Get
        Try
          Return Path.GetFileNameWithoutExtension(_path)
        Catch
        End Try
        Return _path
      End Get
    End Property

    Public Property IsExport As Boolean
    Public Property IsMultifile As Boolean
    Public Property IsNumeric As Boolean
    Public Property IsNumEid As Boolean
    Public Property Categories As List(Of String)
    Public Property IsInstWithTypes As Boolean
    Public Property IsTypesOnly As Boolean
    Public Property IsInstOnly As Boolean

    Public Property CatList As List(Of clsRvtCategory)

#End Region

    ''' <summary>
    ''' Configuration Helper
    ''' </summary>
    ''' <param name="p"></param>
    ''' <remarks></remarks>
    Public Sub New(p As String)

      ' Widen Scope
      _path = p
      Categories = New List(Of String)
      CatList = New List(Of clsRvtCategory)

    End Sub

#Region "Public Members"

    ''' <summary>
    ''' Read the File and Assign Stored Values
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReadData()

      Try

        ' Fresh List
        Categories = New List(Of String)

        ' Read the File
        Using sr As New StreamReader(_path)

          ' Read the Lines
          Dim m_isCats As Boolean = False
          Do Until sr.EndOfStream
            Dim m_line = sr.ReadLine
            If String.IsNullOrEmpty(m_line) Then Continue Do
            If m_line.ToLower = "[categories]" Then
              m_isCats = True
              Continue Do
            End If
            If m_isCats = True Then
              Categories.Add(m_line)
            End If
            If m_line.StartsWith("isExport") Then
              If m_line.ToLower.Contains("=t") Then
                isExport = True
              Else
                isExport = False
              End If
            End If
            If m_line.StartsWith("isMultifile") Then
              If m_line.ToLower.Contains("=t") Then
                isMultifile = True
              Else
                isMultifile = False
              End If
            End If
            If m_line.StartsWith("isNumEid") Then
              If m_line.ToLower.Contains("=t") Then
                isNumEid = True
              Else
                isNumEid = False
              End If
            End If
            If m_line.StartsWith("isNumeric") Then
              If m_line.ToLower.Contains("=t") Then
                IsNumeric = True
              Else
                IsNumeric = False
              End If
            End If
            If m_line.StartsWith("isTypesOnly") Then
              If m_line.ToLower.Contains("=t") Then
                isTypesOnly = True
              Else
                isTypesOnly = False
              End If
            End If
            If m_line.StartsWith("isInstOnly") Then
              If m_line.ToLower.Contains("=t") Then
                isInstOnly = True
              Else
                isInstOnly = False
              End If
            End If
            If m_line.StartsWith("isInstWithTypes") Then
              If m_line.ToLower.Contains("=t") Then
                isInstWithTypes = True
              Else
                isInstWithTypes = False
              End If
            End If
          Loop

        End Using

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Write Stored Values to File
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub WriteData()

      Try

        ' Write the File
        Using sw As New StreamWriter(_path, False)

          sw.WriteLine("isExport=" & isExport.ToString)
          sw.WriteLine("isMultifile=" & isMultifile.ToString)
          sw.WriteLine("isNumeric=" & IsNumeric.ToString)
          sw.WriteLine("isNumEid=" & isNumEid.ToString)
          sw.WriteLine("")
          sw.WriteLine("isInstOnly=" & isInstOnly.ToString)
          sw.WriteLine("isTypesOnly=" & isTypesOnly.ToString)
          sw.WriteLine("isInstWithTypes=" & isInstWithTypes.ToString)
          sw.WriteLine("")
          sw.WriteLine("[Categories]")
          For Each x In Categories
            sw.WriteLine(x)
          Next

        End Using

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Delete the File
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteFile() As Boolean

      Try

        ' Delete It
        File.Delete(_path)

        ' Success
        Return True

      Catch
      End Try

      ' Failed
      Return False

    End Function

#End Region

  End Class
End Namespace
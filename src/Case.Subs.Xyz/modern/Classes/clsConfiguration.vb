Imports System.IO

''' <summary>
''' A class to handle parameter settings via external file
''' </summary>
''' <remarks></remarks>
Public Class clsConfiguration

  Private _filePath As String = ""

#Region "Public Properties"

  ''' <summary>
  ''' Category is KEY
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property DictParamMaps As SortedDictionary(Of String, clsParamMap)

#End Region

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="fPath">Path to File</param>
  ''' <remarks></remarks>
  Public Sub New(fPath As String)

    ' Widen Scope
    _filePath = fPath

    ' Setup
    doSetup()

  End Sub

  ''' <summary>
  ''' Setup
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub doSetup()

    Try

      ' Fresh List
      DictParamMaps = New SortedDictionary(Of String, clsParamMap)

      ' Verify File Existence
      If File.Exists(_filePath) Then

        ' Read the File
        ReadFile()

      End If

    Catch
    End Try

  End Sub

#Region "Private Members"

  ''' <summary>
  ''' Read the Data
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub ReadFile()

    ' Mapper
    Dim m_map As clsParamMap = Nothing

    ' Current Category
    Dim m_catName As String = ""

    ' Stream Reader
    Using sr As New StreamReader(_filePath)
      Do While sr.Peek() >= 0

        ' Read a Line
        Dim m_s As String = sr.ReadLine()
        If String.IsNullOrEmpty(m_s) Then GoTo NextLine

        ' Is this a category header?
        If m_s.ToLower.StartsWith("category:") Then

          ' Add the Map Item if Second
          If Not m_map Is Nothing Then
            Try
              DictParamMaps.Add(m_catName, m_map)
            Catch ex As Exception

            End Try
          End If

          ' Get the Category
          m_catName = Mid(m_s, 10)

          ' Create a Map Object
          m_map = New clsParamMap(m_catName)

        End If

        ' Next Line if no Category
        If m_map.CategoryName Is Nothing Then GoTo NextLine

        If m_s.ToLower.StartsWith("p:") Then
          m_map.Param_P = Mid(m_s, 3)
        End If
        If m_s.ToLower.StartsWith("n:") Then
          m_map.Param_N = Mid(m_s, 3)
        End If
        If m_s.ToLower.StartsWith("e:") Then
          m_map.Param_E = Mid(m_s, 3)
        End If
        If m_s.ToLower.StartsWith("z:") Then
          m_map.Param_Z = Mid(m_s, 3)
        End If
        If m_s.ToLower.StartsWith("d:") Then
          m_map.Param_D = Mid(m_s, 3)
        End If
        If m_s.ToLower.StartsWith("1:") Then
          m_map.Param_1 = Mid(m_s, 3)
        End If
        If m_s.ToLower.StartsWith("2:") Then
          m_map.Param_2 = Mid(m_s, 3)
        End If
        If m_s.ToLower.StartsWith("3:") Then
          m_map.Param_3 = Mid(m_s, 3)
        End If
        If m_s.ToLower.StartsWith("4:") Then
          m_map.Param_4 = Mid(m_s, 3)
        End If
        If m_s.ToLower.StartsWith("5:") Then
          m_map.Param_5 = Mid(m_s, 3)
        End If

NextLine:
      Loop
    End Using

    ' Add the Last Item
    If Not m_map Is Nothing Then
      Try
        DictParamMaps.Add(m_catName, m_map)
      Catch
      End Try
    End If

  End Sub

#End Region

#Region "Public Members"

  ''' <summary>
  ''' Append a New Category
  ''' </summary>
  ''' <param name="p_name">Category Name</param>
  ''' <remarks></remarks>
  Public Sub AddNewCategoryItem(p_name As String)

    ' Don't Allow Duplicates
    For Each x As clsParamMap In Me.DictParamMaps.Values
      If x.CategoryName.ToLower = p_name.ToLower Then Exit Sub
    Next

    ' Add the New Item
    Me.DictParamMaps.Add(p_name, New clsParamMap(p_name))

    ' Write the Update
    WriteFile()

  End Sub

  ''' <summary>
  ''' Write the Data
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub WriteFile()

    ' Stream Writer
    Using sw As New StreamWriter(_filePath, False)

      ' Iterate Each Map
      For Each x As clsParamMap In DictParamMaps.Values

        ' Category
        sw.WriteLine("category:" & x.CategoryName)

        ' PNEZD
        sw.WriteLine("p:" & x.Param_P)
        sw.WriteLine("n:" & x.Param_N)
        sw.WriteLine("e:" & x.Param_E)
        sw.WriteLine("z:" & x.Param_Z)
        sw.WriteLine("d:" & x.Param_D)

        ' 1 thru 5
        sw.WriteLine("1:" & x.Param_1)
        sw.WriteLine("2:" & x.Param_2)
        sw.WriteLine("3:" & x.Param_3)
        sw.WriteLine("4:" & x.Param_4)
        sw.WriteLine("5:" & x.Param_5)

        ' Blank Line
        sw.WriteLine("")

      Next

    End Using
  End Sub

#End Region

End Class
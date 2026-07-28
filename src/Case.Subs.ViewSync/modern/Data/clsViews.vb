Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Namespace Data

  Public Class clsViews

    Private _path As String = ""

#Region "Public Properties - No jSON"

    ''' <summary>
    ''' View Ports from File
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <JsonIgnore>
    Public Property ViewPorts As Dictionary(Of String, clsVP)

    ''' <summary>
    ''' Valid File Format
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <JsonIgnore>
    Public Property IsValid As Boolean

#End Region

#Region "Public Properties - jSON"

    ''' <summary>
    ''' View Data
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Views As List(Of clsVP)
      Get
        Dim m_views As New List(Of clsVP)
        Try
          For Each x In ViewPorts.Values
            m_views.Add(x)
          Next
        Catch
        End Try
        Return m_views
      End Get
    End Property

#End Region

    ''' <summary>
    ''' View Sync Data Helper
    ''' </summary>
    ''' <param name="p">Path to Data File</param>
    ''' <remarks></remarks>
    Public Sub New(p As String)

      ' Widen Scope
      _path = p

      ' Setup
      isValid = False
      ReadData()

    End Sub

#Region "Friends - Data File IO"

    ''' <summary>
    ''' View Sync Data from jSON
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadData()

      ' Fresh List
      ViewPorts = New Dictionary(Of String, clsVP)
      If String.IsNullOrEmpty(_path) Then Return

      Try

        ' Read jSON Nodes
        Dim m_j = JObject.Parse(ViewsToJson)

        ' Valid?
        If Not m_j Is Nothing Then
          isValid = True

          ' Read Data
          If Not m_j("Views") Is Nothing Then

            ' Views
            For Each x In m_j("Views")

              Try

                ' Add the Item
                ViewPorts.Add(x("GUID"), New clsVP(x))

              Catch
              End Try

            Next

          End If

        Else
          isValid = False
        End If

      Catch
        isValid = False
      End Try

    End Sub

    ''' <summary>
    ''' Views to JSON
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ViewsToJson() As String

      ' Json Response
      Dim m_json As String = ""

      Try

        ' Check for Errors
        Using sw As New StreamReader(_path)
          Do Until sw.EndOfStream
            m_json += sw.ReadLine
          Loop
        End Using

      Catch
      End Try

      ' Return
      Return m_json

    End Function

    ''' <summary>
    ''' Update the Views File
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function UpdateViewsFile() As Boolean

      Try

        ' To jSon and Update File
        Dim m_jSon As String = JsonConvert.SerializeObject(Me)
        If Not String.IsNullOrEmpty(m_jSon) Then
          Using sw As New StreamWriter(_path, False)
            sw.WriteLine(m_jSon)
          End Using
        End If

      Catch
      End Try

      ' Failed
      Return False

    End Function

#End Region

  End Class
End Namespace
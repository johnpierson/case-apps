Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData

    Public ReadOnly Property Doc As Document
      Get
        Try
          Return _cmd.Application.ActiveUIDocument.Document
        Catch ex As Exception
          Return Nothing
        End Try
      End Get
    End Property
    Public ReadOnly Property Version As String
      Get
        Return "v" & My.Application.Info.Version.ToString
      End Get
    End Property

    ''' <summary>
    ''' Constructor 
    ''' </summary>
    ''' <param name="c"></param>
    ''' <remarks></remarks>
    Public Sub New(c As ExternalCommandData)

      ' Widen Scope
      _cmd = c

    End Sub

#Region "Public Members - Get Element Stuff"

    ''' <summary>
    ''' Element Count
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetElementCount() As Integer
      Return GetElements.ToElements().Count
    End Function

    ''' <summary>
    ''' Get Elements
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetElements() As FilteredElementCollector
      Dim collector As New FilteredElementCollector(Doc)
      Return collector.WhereElementIsNotElementType()
    End Function

    ''' <summary>
    ''' Return all database elements after the given number n
    ''' </summary>
    ''' <param name="eInt"></param>
    ''' <param name="rvtDoc"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetElementsAfter(ByVal eInt As Integer, ByVal rvtDoc As Document) As List(Of Element)
      Dim elems As New List(Of Element)
      Dim fec As FilteredElementCollector = GetElements()
      Dim i As Integer = 0
      For Each e As Element In fec
        i += 1
        If eInt < i Then
          elems.Add(e)
        End If
      Next
      Return elems
    End Function

#End Region

  End Class
End Namespace
Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsRvtWorksets

    Private _w As Workset

#Region "Public Properties"

    ''' <summary>
    ''' Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Name As String
      Get
        Try
          Return _w.Name
        Catch
          Return "{error}"
        End Try
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Get Workset Data
    ''' </summary>
    ''' <param name="w"></param>
    ''' <remarks></remarks>
    Public Sub New(w As Workset)

      ' Widen Scope
      _w = w

    End Sub

  End Class
End Namespace
Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsLineSubCategory

    Public Property LinePat As Category

    ''' <summary>
    ''' Line Pattern Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Name As String
      Get
        Try
          Return LinePat.Name
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Line Pattern Category
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ElementId As ElementId
      Get
        Try
          Return LinePat.Id
        Catch ex As Exception
          Return Nothing
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="c"></param>
    ''' <remarks></remarks>
    Public Sub New(c As Category)

      ' Widen Scope
      LinePat = c

    End Sub

  End Class
End Namespace
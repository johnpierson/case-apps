Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsCategory

    Private _c As Category

#Region "Public Properties"

    ''' <summary>
    ''' Checked for Inclusion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsChecked As Boolean

    ''' <summary>
    ''' Category Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CatName As String
      Get
        Try
          Return _c.Name
        Catch
          Return ""
        End Try
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Category Helper
    ''' </summary>
    ''' <param name="c"></param>
    ''' <remarks></remarks>
    Public Sub New(c As Category)

      ' Widen Scope
      _c = c
      isChecked = False

    End Sub

    ''' <summary>
    ''' Get the Category without being a property
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCategory() As Category
      Return _c
    End Function

  End Class
End Namespace
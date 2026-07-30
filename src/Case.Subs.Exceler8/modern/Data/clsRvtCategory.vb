Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsRvtCategory

    Private _c As Category

#Region "Public Properties"

    ''' <summary>
    ''' Inclusion 
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
    Public ReadOnly Property CategoryName As String
      Get
        Try

          If Not _c.Parent Is Nothing Then
            Return _c.Parent.Name & ": " & _c.Name
          Else
            Return _c.Name
          End If
        Catch
        End Try
        Return ""
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
      isChecked = False
      _c = c

    End Sub

    ''' <summary>
    ''' Get the Category
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetCategory() As Category
      Return _c
    End Function

  End Class
End Namespace
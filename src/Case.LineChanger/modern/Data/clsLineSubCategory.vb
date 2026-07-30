Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsLineSubCategory

    Private _cat As Category

    Public ReadOnly Property LinePat As Category
      Get
        Return _cat
      End Get
    End Property
    Public ReadOnly Property Name As String
      Get
        Return _cat.Name
      End Get
    End Property
    Public ReadOnly Property Eid As ElementId
      Get
        Return _cat.Id
      End Get
    End Property

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="cat"></param>
    ''' <remarks></remarks>
    Public Sub New(cat As Category)

      ' Widen Scope
      _cat = cat
     
    End Sub

  End Class
End Namespace
Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsParameterDescription

    Public Property Name As String
    Public Property Kind As StorageType

    ''' <summary>
    ''' Parameter Mappings
    ''' </summary>
    ''' <param name="pFormat"></param>
    ''' <param name="n"></param>
    ''' <remarks></remarks>
    Public Sub New(pFormat As StorageType, n As String)

      ' Widen Scope
      Kind = pFormat
      Name = n

    End Sub

  End Class
End Namespace
Namespace Data
  Public Class clsMapping

    Public Property SourceName As String
    Public Property DestinationName As String

    ''' <summary>
    ''' Parameter Mappings
    ''' </summary>
    ''' <param name="s">Source Parameter (Rooms)</param>
    ''' <param name="d">Destination Parameter (Mass)</param>
    ''' <remarks></remarks>
    Public Sub New(s As String, d As String)

      ' Widen Scope
      SourceName = s
      DestinationName = d

    End Sub

  End Class
End Namespace
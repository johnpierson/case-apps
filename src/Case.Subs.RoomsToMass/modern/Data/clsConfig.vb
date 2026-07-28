Namespace Data
  Public Class clsConfig

    Public Property ModelPath As String
    Public Property Mappings As List(Of clsMapping)

    Public Sub New()
      Mappings = New List(Of clsMapping)
    End Sub

    ''' <summary>
    ''' Configuration Helper
    ''' </summary>
    ''' <param name="p"></param>
    ''' <param name="m"></param>
    ''' <remarks></remarks>
    Public Sub New(p As String, m As List(Of clsMapping))

      ' Widen Scope
      ModelPath = p
      Mappings = m

    End Sub

  End Class
End Namespace
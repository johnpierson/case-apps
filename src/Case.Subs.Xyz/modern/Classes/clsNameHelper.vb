Public Class clsNameHelper

  Public Property Name As String

  ''' <summary>
  ''' Simple Singleton List Naming Helper
  ''' </summary>
  ''' <param name="n"></param>
  ''' <remarks></remarks>
  Public Sub New(n As String)
    Name = n
  End Sub

End Class
Namespace Charts
  Public Class clsChartKind

    Private _kind As String = ""

#Region "Public Properties"

    ''' <summary>
    ''' Category Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Kind As String
      Get
        Return _kind
      End Get
    End Property
    Public Property Quantity As Integer

#End Region

    ''' <summary>
    ''' Class Kinds
    ''' </summary>
    ''' <param name="n"></param>
    ''' <remarks></remarks>
    Public Sub New(n As String)

      ' Widen Scope
      _kind = n
      Quantity = 0

    End Sub

  End Class
End Namespace
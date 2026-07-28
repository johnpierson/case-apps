Namespace Charts
  Public Class clsChartCategory

    Private _name As String = ""

#Region "Public Properties"

    ''' <summary>
    ''' Category Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Category As String
      Get
        Return _name
      End Get
    End Property
    Public Property Types As Integer
    Public Property Instances As Integer

#End Region

    ''' <summary>
    ''' Category
    ''' </summary>
    ''' <param name="n"></param>
    ''' <remarks></remarks>
    Public Sub New(n As String)

      ' Widen Scope
      _name = n
      Types = 0
      Instances = 0

    End Sub

  End Class
End Namespace
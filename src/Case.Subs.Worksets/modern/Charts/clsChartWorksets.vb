Namespace Charts
  Public Class clsChartWorksets

    Private _ws As String = ""

#Region "Public Properties"

    Public ReadOnly Property Workset As String
      Get
        Return _ws
      End Get
    End Property
    Public Property Instances As Integer
    'Public Property UniqueLevels As Integer
    'Public Property UniqueCategories As Integer

#End Region

    ''' <summary>
    ''' Worksets
    ''' </summary>
    ''' <param name="ws"></param>
    ''' <remarks></remarks>
    Public Sub New(ws As String, i As Integer)

      ' Widen Scope
      _ws = ws
      Instances = i

    End Sub

  End Class
End Namespace
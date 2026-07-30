Imports Autodesk.Revit.DB

Public Class clsLevel

  Private _l As Level

#Region "Public Properties"

  ''' <summary>
  ''' Name of Level
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property LevelName As String
    Get
      Try
        Return _l.Name
      Catch ex As Exception
        Return "<All>"
      End Try
    End Get
  End Property

  ''' <summary>
  ''' Long Name
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property LevelLongName As String
    Get
      Try
        Dim m_p As String = _l.Parameter(BuiltInParameter.LEVEL_ELEV).AsValueString
        Return _l.Name & " [" & m_p & "]"
      Catch ex As Exception
        Return "<All>"
      End Try
    End Get
  End Property

  ''' <summary>
  ''' The Level
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property LevelElement As Level
    Get
      Return _l
    End Get
  End Property

#End Region

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="l"></param>
  ''' <remarks></remarks>
  Public Sub New(l As Element)

    ' Widen Scope
    _l = TryCast(l, Level)

  End Sub

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="n"></param>
  ''' <remarks></remarks>
  Public Sub New(n As String)
  End Sub

End Class
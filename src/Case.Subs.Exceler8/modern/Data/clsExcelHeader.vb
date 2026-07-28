Namespace Data
  ''' <summary>
  ''' Header Name and Kind
  ''' </summary>
  ''' <remarks></remarks>
  Public Class clsExcelHeader

#Region "Public Properties"

    ''' <summary>
    ''' Column (Parameter) Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Name As String

    ''' <summary>
    ''' Kind of Item (E, I, T)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Kind As EnumExcelHeaderKind

    ''' <summary>
    ''' Revit Data Type (Normal, Read-Only, Complex)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataType As EnumCellDataType

    ''' <summary>
    ''' Group Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GroupName As String

    ''' <summary>
    ''' Sync Direction
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Direction As EnumSyncDir

#End Region

    ''' <summary>
    ''' Name and Kind
    ''' </summary>
    ''' <param name="n">Name</param>
    ''' <param name="k">Kind</param>
    ''' <param name="g">Group Name (for duplicates)</param>
    ''' <remarks></remarks>
    Public Sub New(n As String,
                   k As EnumExcelHeaderKind,
                   g As String,
                   dt As EnumCellDataType)

      ' Widen Scope
      Direction = EnumSyncDir.isIgnore
      Name = n
      Kind = k
      GroupName = g
      DataType = dt

    End Sub

  End Class
End Namespace
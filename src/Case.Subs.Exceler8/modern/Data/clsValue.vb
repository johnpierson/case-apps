Namespace Data

  ''' <summary>
  ''' Name and Value Helper
  ''' </summary>
  ''' <remarks></remarks>
  Public Class clsValue

#Region "Public Properties"

    ''' <summary>
    ''' Parameter Group Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Group As String

    ''' <summary>
    ''' Parameter Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Name As String

    ''' <summary>
    ''' Existing Parameter Value
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Value As String

    ''' <summary>
    ''' New Parameter Value
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NewValue As String

    ''' <summary>
    ''' Used During Synchronization
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Direction As EnumSyncDir

#End Region

    ''' <summary>
    ''' Name and Value
    ''' </summary>
    ''' <param name="g">Group</param>
    ''' <param name="n">Name</param>
    ''' <param name="v">Value</param>
    ''' <remarks></remarks>
    Public Sub New(g As String, n As String, v As String)

      ' Widen Scope
      Group = g
      Name = n
      Value = v
      NewValue = "{}"
      Direction = EnumSyncDir.isIgnore

    End Sub

  End Class
End Namespace
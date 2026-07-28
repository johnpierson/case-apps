Namespace Data
  Public Class clsDirection

#Region "Public Properties"

    ''' <summary>
    ''' Field Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameAndGroup As String

    ''' <summary>
    ''' Data Direction Intention
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Direction As EnumSyncDir

#End Region

    ''' <summary>
    ''' Sync Direction
    ''' </summary>
    ''' <param name="n">Parameter Name | Parameter Group Name</param>
    ''' <param name="d">Direction of Sync Intent</param>
    ''' <remarks></remarks>
    Public Sub New(n As String, d As EnumSyncDir)

      ' Widen Scope
      NameAndGroup = n
      Direction = d

    End Sub

  End Class
End Namespace
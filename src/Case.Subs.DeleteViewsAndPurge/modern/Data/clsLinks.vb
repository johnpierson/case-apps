Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsLinks

    Private _link As RevitLinkType

#Region "Public Properties"

    Public Property IsChecked As Boolean

    Public ReadOnly Property Link As String
      Get
        Try
          Return "[" & _link.AttachmentType.ToString & "] " & _link.Name
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Link Helper
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub New(e As Element)

      ' Widen Scope
      _link = TryCast(e, RevitLinkType)
      isChecked = True

    End Sub

    ''' <summary>
    ''' The ElementID
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetId() As ElementId
      Try
        Return _link.Id
      Catch
        Return Nothing
      End Try
    End Function

  End Class
End Namespace
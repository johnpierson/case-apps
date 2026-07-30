Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsSharedParam

    Private _d As Definition

#Region "Public Properties"

    ''' <summary>
    ''' For Combo Box
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DisplayName As String
      Get
        Try
          Return _d.Name
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Shared Definition Helper
    ''' </summary>
    ''' <param name="d"></param>
    ''' <remarks></remarks>
    Public Sub New(d As Definition)

      ' Widen Scope
      _d = d

    End Sub

    ''' <summary>
    ''' The Definition Object
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetDefinition() As Definition
      Return _d
    End Function

  End Class
End Namespace
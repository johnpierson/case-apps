Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsViewTemplate

    Private _view As View = Nothing

#Region "Public Properties"

    ''' <summary>
    ''' Inclusion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsChecked As Boolean

    ''' <summary>
    ''' View Template Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ViewTemplateName As String
      Get
        Try
          Return _view.Name
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' View Template Kind
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ViewTemplateKind As String
      Get
        Try
          Return _view.ViewType.ToString
        Catch
        End Try
        Return ""
      End Get
    End Property

    ''' <summary>
    ''' Number of times used by Views
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Assignments As Integer

#End Region

    ''' <summary>
    ''' View Template Helper
    ''' </summary>
    ''' <param name="v"></param>
    ''' <remarks></remarks>
    Public Sub New(v As View)

      ' Widen Scope
      _view = v
      isChecked = False
      Assignments = 0

    End Sub

    ''' <summary>
    ''' Return the View Object
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTemplate() As View
      Return _view
    End Function

    ''' <summary>
    ''' Delete the View Template
    ''' </summary>
    ''' <param name="d"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteVt(d As Document) As Boolean

      Try

        ' Delete it
        d.Delete(_view.Id)

        ' Success
        Return True

      Catch
      End Try

      ' Final Value
      Return False

    End Function

  End Class
End Namespace
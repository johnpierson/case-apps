Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsViewTemplates

    ''' <summary>
    ''' View Template
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewTemplate As View

    ''' <summary>
    ''' View Template Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ViewTemplateName As String
      Get
        Try
          Return ViewTemplate.Name
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' View Template Helper
    ''' </summary>
    ''' <param name="v"></param>
    ''' <remarks></remarks>
    Public Sub New(v As View)

      ' Widen Scope
      ViewTemplate = v

    End Sub

  End Class
End Namespace
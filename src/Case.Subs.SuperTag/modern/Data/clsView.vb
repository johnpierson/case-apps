Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsView

    Private _v As View

#Region "Public Properties"

    ''' <summary>
    ''' Check Include
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsChecked As Boolean

    ''' <summary>
    ''' View Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ViewName As String
      Get
        Try
          Return _v.Name
        Catch
          Return "{error}"
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Generation Level
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ViewLevel As String
      Get
        Try
          If Not _v.GenLevel Is Nothing Then
            Return _v.GenLevel.Name
          End If
        Catch
        End Try
        Return ""
      End Get
    End Property

    ''' <summary>
    ''' View Type
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ViewType As String
      Get
        Try
          Return _v.ViewType.ToString
        Catch
          Return "{error}"
        End Try
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="v"></param>
    ''' <remarks></remarks>
    Public Sub New(v As View)

      ' Widen Scope
      _v = v

      ' Defaults 
      isChecked = False

    End Sub

    ''' <summary>
    ''' Get the View
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetViewElement() As View
      Return _v
    End Function

  End Class
End Namespace
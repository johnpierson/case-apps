Imports Autodesk.Revit.DB

Public Class clsView

  Private _v As View

#Region "Public Properties"

  ''' <summary>
  ''' Inclusion
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property isChecked As Boolean

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
      End Try
      Return "{error}"
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
      End Try
      Return ""
    End Get
  End Property

  ''' <summary>
  ''' Level Name
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
  ''' View Element
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property ViewElement As View
    Get
      Return _v
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

    ' General 
    isChecked = False

  End Sub

End Class
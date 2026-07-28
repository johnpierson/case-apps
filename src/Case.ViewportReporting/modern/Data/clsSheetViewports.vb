Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSheetViewports

    ''' <summary>
    ''' The Viewport Element
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewportElement As Viewport
    Public Property ViewportLocation As XYZ

    Public ReadOnly Property SheetNumber As String
      Get
        Try
          Return ViewportElement.Parameter(BuiltInParameter.VIEWPORT_SHEET_NUMBER).AsString
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property SheetName As String
      Get
        Try
          Return ViewportElement.Parameter(BuiltInParameter.VIEWPORT_SHEET_NAME).AsString
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property ViewNumber As Integer
      Get
        Try
          Return ViewportElement.Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER).AsString
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property ViewScale As String
      Get
        Try
          Return ViewportElement.Parameter(BuiltInParameter.VIEWPORT_SCALE).AsString
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property ViewTitle As String
      Get
        Try
          Return ViewportElement.Parameter(BuiltInParameter.VIEWPORT_VIEW_NAME).AsString
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property ViewPortType As String
      Get
        Try
          Dim m_e As Element = ViewportElement.Document.GetElement(ViewportElement.GetTypeId)
          Return m_e.Name
        Catch ex As Exception
          Return Nothing
        End Try
      End Get
    End Property
    Public ReadOnly Property ViewDiscipline As ViewDiscipline
      Get
        Try

          Return ViewportElement.Parameter(BuiltInParameter.VIEW_DISCIPLINE).AsInteger
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property ViewDependency As String
      Get
        Try
          Return ViewportElement.Parameter(BuiltInParameter.VIEW_DEPENDENCY).AsString
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property ViewPhase As String
      Get
        Try
          Dim m_p As Phase = ViewportElement.Document.GetElement(ViewportElement.Parameter(BuiltInParameter.VIEW_PHASE).AsElementId)

          Return m_p.Name
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Viewport Data Helper
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub New(e As Element)

      ' Widen Scope
      ViewportElement = TryCast(e, Viewport)

    End Sub

  End Class
End Namespace
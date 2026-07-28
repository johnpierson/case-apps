Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsRvtElemInst

    Private _e As Element
    Private _typeName As String
    Private _typeId As String
    Private _userWs As New SortedDictionary(Of Integer, clsRvtWorksets)

#Region "Public Properties"

    ''' <summary>
    ''' Type Element Reference
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UniqueId As String
      Get
        Try
          Return _e.UniqueId.ToString()
        Catch
        End Try
        Return ""
      End Get
    End Property

    ''' <summary>
    ''' ElementID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Eid As String
      Get
        Try
          Return _e.Id.ToString
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' ElementID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TypeEid As String
      Get
        Return _typeId
      End Get
    End Property

    ''' <summary>
    ''' ElementID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TypeName As String
      Get
        Return _typeName
      End Get
    End Property

    ''' <summary>
    ''' Category Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CategoryName As String
      Get
        Try
          If Not _e.Category Is Nothing Then
            Return _e.Category.Name
          Else
            Return " n/a"
          End If
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' Object Class Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ObjectClass As String
      Get
        Try
          Dim o As Object = _e
          Dim m_ret As String = Replace(o.GetType.ToString, "Autodesk.Revit.DB.", "", , , CompareMethod.Text)
          Return Replace(m_ret, ".", " ", , , CompareMethod.Text)
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' Workset Name (If a User Workset)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WorksetName As String
      Get
        Try
          If _userWs.ContainsKey(_e.WorksetId.IntegerValue) Then
            Return _userWs(_e.WorksetId.IntegerValue).Name
          End If
        Catch
        End Try
        Return ""
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Instance Element
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub New(e As Element, uws As SortedDictionary(Of Integer, clsRvtWorksets))

      ' Widen Scope
      _e = e
      _userWs = uws

      Try
        If _e.GetTypeId.Value < 1 Then
          _typeName = CategoryName
        Else
          Dim m_eid As ElementId = _e.GetTypeId
          If Not m_eid Is Nothing Then
            Dim m_element As Element = _e.Document.GetElement(m_eid)
            _typeId = m_element.UniqueId.ToString
            _typeName = m_element.Name
          End If
        End If
      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get the Element
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetElement() As Element
      Return _e
    End Function

  End Class
End Namespace
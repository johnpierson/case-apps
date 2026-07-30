Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsRvtElemType

    Private _e As Element

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
          Return _e.UniqueId.ToString
        Catch
        End Try
        Return ""
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
          If _e.Category Is Nothing Then
            Return " n/a"
          Else
            Return _e.Category.Name
          End If
        Catch
        End Try
        Return ""
      End Get
    End Property

    ''' <summary>
    ''' ElementID as String
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
    ''' Element Family Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FamilyName As String
      Get
        Try
          Dim p As Parameter = _e.Parameter(BuiltInParameter.SYMBOL_FAMILY_NAME_PARAM)
          If Not p Is Nothing Then Return p.AsString
        Catch
        End Try
        ' No Matches
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' Element Name (Type Name))
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TypeName As String
      Get
        Try
          Dim p As Parameter = _e.Parameter(BuiltInParameter.SYMBOL_NAME_PARAM)
          If Not p Is Nothing Then Return p.AsString
        Catch
        End Try
        ' No Matches
        Return ""
      End Get
    End Property

    ''' <summary>
    ''' Get the Class Object Name
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
          Return "{error}"
        End Try
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Type Element
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub New(e As Element)

      ' Widen Scope
      _e = e

    End Sub

#Region "Public and Friend Members"

    ''' <summary>
    ''' Get the Element
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetElement() As Element
      Return _e
    End Function

#End Region

  End Class
End Namespace
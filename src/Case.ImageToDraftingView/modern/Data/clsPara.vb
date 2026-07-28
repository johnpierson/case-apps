Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsPara

    Private _p As Parameter

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="parameter"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal parameter As Parameter)
      _p = parameter
    End Sub

    ''' <summary>
    ''' Value Property
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Value() As String
      Get
        Try
          Return GetParameterValue(_p)
        Catch
          Return Nothing
        End Try
      End Get
      Set(ByVal value As String)
        Try
          SetParameterValue(_p, value)
        Catch
        End Try
      End Set
    End Property

    ''' <summary>
    ''' Get the Value
    ''' </summary>
    ''' <param name="parameter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetParameterValue(ByVal parameter As Parameter) As String
      Select Case parameter.StorageType
        Case StorageType.Double
          Return parameter.AsDouble
        Case StorageType.ElementId
          Return parameter.AsElementId.ToString
        Case StorageType.Integer
          Return parameter.AsInteger
        Case StorageType.None
          Return parameter.AsValueString()
        Case StorageType.String
          Return parameter.AsString()
        Case Else
          Return ""
      End Select
    End Function

    ''' <summary>
    ''' Set the Value
    ''' </summary>
    ''' <param name="parameter"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Shared Sub SetParameterValue(ByVal parameter As Parameter, ByVal value As Object)
      If parameter.IsReadOnly Then
        Exit Sub
      End If
      Select Case parameter.StorageType
        Case StorageType.Double
          parameter.SetValueString(TryCast(value, String))
          Exit Select
        Case StorageType.ElementId
          Dim myElementId As ElementId = DirectCast((value), ElementId)
          parameter.Set(myElementId)
          Exit Select
        Case StorageType.Integer
          parameter.SetValueString(TryCast(value, String))
          Exit Select
        Case StorageType.None
          parameter.SetValueString(TryCast(value, String))
          Exit Select
        Case StorageType.String
          parameter.Set(TryCast(value, String))
          Exit Select
        Case Else
          Exit Select
      End Select
    End Sub

  End Class
End Namespace
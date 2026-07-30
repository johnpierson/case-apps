Imports Autodesk.Revit.DB

Namespace Data

  ''' <summary>
  ''' A simple class used to work with parameters 
  ''' </summary>
  ''' <remarks></remarks>
  Public Class clsPara

    Private _p As Parameter

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="p"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal p As Parameter)
      _p = p
    End Sub

    ''' <summary>
    ''' The display unit type
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DisplayUnitType() As String
      Get
        Try
            #If Revit2020 Or Revit2021 
                     Return _p.DisplayUnitType.ToString
#End If
            #If Revit2022 Or Revit2023 Or Revit2025 Or Revit2026 Or Revit2027
            Return _p.GetUnitTypeId.ToString
#End If
         
        Catch
          Return Nothing
        End Try
      End Get
    End Property

    ''' <summary>
    ''' The parameter reference.
    ''' The element the parameter belongs to is accessible from this object
    ''' </summary>
    ''' <value></value>
    ''' <returns>DB.Parameter of para, to gain access to its parent element</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ParameterObject() As Parameter
      Get
        Return _p
      End Get
    End Property

    ''' <summary>
    ''' Is it a read only parameter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ParameterIsReadOnly() As Boolean
      Get
        Try
          Return _p.IsReadOnly
        Catch
          Return Nothing
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Is this a shared parameter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ParameterIsShared() As Boolean
      Get
        Try
          Return _p.IsShared
        Catch
          Return Nothing
        End Try
      End Get
    End Property

    ''' <summary>
    ''' The type of parameter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Type() As String
      Get
        Try
          Return _p.GetType.Name
        Catch
          Return Nothing
        End Try
      End Get
    End Property

    ''' <summary>
    ''' The name of the parameter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Name() As String
      Get
        Try
          Return _p.Definition.Name
        Catch
          Return Nothing
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Gets the parameter format
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Format() As String
      Get
        Try
          Return _p.StorageType.ToString
        Catch
          Return Nothing
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Returns value of the parameter
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
    ''' Get the value of a parameter
    ''' </summary>
    ''' <param name="parameter">DB.Parameter</param>
    ''' <returns>String representing the value</returns>
    ''' <remarks></remarks>
    Public Shared Function GetParameterValue(ByVal parameter As Parameter) As String
      'public static Object GetParameterValue(Parameter parameter) 
      Select Case parameter.StorageType
        Case StorageType.[Double]
          Return parameter.AsDouble
        Case StorageType.ElementId
          Return parameter.AsElementId.Value
        Case StorageType.[Integer]
          Return parameter.AsInteger
        Case StorageType.None
          Return parameter.AsValueString()
        Case StorageType.[String]
          Return parameter.AsString()
        Case Else
          Return ""
      End Select
    End Function

    ''' <summary>
    ''' Set a value to a parameter
    ''' </summary>
    ''' <param name="parameter">DB.Parameter</param>
    ''' <param name="value">Value does not have to be a string, Object</param>
    ''' <remarks></remarks>
    Public Shared Sub SetParameterValue(ByVal parameter As Parameter, ByVal value As Object)
      'first,check whether this parameter is read only 
      If parameter.IsReadOnly Then
        Exit Sub
      End If
      Select Case parameter.StorageType
        Case StorageType.[Double]
          parameter.SetValueString(TryCast(value, String))
          Exit Select
        Case StorageType.ElementId
          Dim myElementId As ElementId = DirectCast((value), ElementId)
          parameter.[Set](myElementId)
          Exit Select
        Case StorageType.[Integer]
          parameter.SetValueString(TryCast(value, String))
          Exit Select
        Case StorageType.None
          parameter.SetValueString(TryCast(value, String))
          Exit Select
        Case StorageType.[String]
          parameter.[Set](TryCast(value, String))
          Exit Select
        Case Else
          Exit Select
      End Select
    End Sub

  End Class
End Namespace
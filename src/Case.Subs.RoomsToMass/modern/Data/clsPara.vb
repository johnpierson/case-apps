Imports Autodesk.Revit.DB

Namespace Data

  ''' <summary>
  ''' A simple class used to work with parameters 
  ''' </summary>
  ''' <remarks></remarks>
  Public Class clsPara

    Private _p As Parameter

#Region "Public Properties"

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
        End Try
        Return Nothing
      End Get
    End Property

    ''' <summary>
    ''' Returns value of the parameter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Value() As String
      Get
        Try
          Return GetParameterValue(_p)
        Catch
        End Try
        Return Nothing
      End Get
    End Property

    ''' <summary>
    ''' Get the value of a parameter 
    ''' </summary>
    ''' <param name="parameter">DB.Parameter</param>
    ''' <returns>String representing the value</returns>
    ''' <remarks></remarks>
    Public Shared Function GetParameterValue(ByVal parameter As Parameter) As String
      Select Case parameter.StorageType
        Case StorageType.Double
          Return parameter.AsValueString
        Case StorageType.Integer
          Return parameter.AsInteger
        Case StorageType.String
          Return parameter.AsString()
      End Select
      Return ""
    End Function

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="parameter"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal parameter As Parameter)
      _p = parameter
    End Sub

  End Class
End Namespace
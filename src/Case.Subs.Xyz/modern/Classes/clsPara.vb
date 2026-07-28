Imports Autodesk.Revit.DB

''' <summary>
''' A simple class used to work with parameters 
''' </summary>
''' <remarks></remarks>
Public Class clsPara

  Private _p As Parameter

#Region "Public Properties"

  Public Property ErrorMessage As String
  Public Property SameValue As Boolean

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
  ''' Returns value of the parameter
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property ValueString As String
    Get
      Try
        Return GetParameterValueString(_p)
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

#End Region

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="p"></param>
  ''' <remarks></remarks>
  Public Sub New(ByVal p As Parameter)

    ' Widen Scope
    _p = p
    ErrorMessage = ""

  End Sub

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="p">Parameter Name to Set</param>
  ''' <param name="v">Value to Set to the Parameter</param>
  ''' <param name="e">Element</param>
  ''' <remarks></remarks>
  Public Sub New(ByVal p As String, v As String, e As Element)
    ' Get the Parameter
    ErrorMessage = ""
    If String.IsNullOrEmpty(p) Then Exit Sub
    Try
      Dim m_p As Parameter = e.LookupParameter(p)
      If Not m_p Is Nothing Then
        _p = m_p
        Try
          If Me.Value = v Then
            SameValue = True
            Exit Sub
          End If
        Catch ex As Exception

        End Try
        Try
          Me.Value = v
        Catch ex As Exception
          ErrorMessage = "Failed to set value for " & p & " to: " & v
        End Try
      Else
        ErrorMessage = "Failed to get parameter " & p
      End If
    Catch ex As Exception
      ErrorMessage = "Failed to get parameter " & p
    End Try
  End Sub

#Region "Public Members"

  ''' <summary>
  ''' Get the value of a parameter
  ''' </summary>
  ''' <param name="p">DB.Parameter</param>
  ''' <returns>String representing the value</returns>
  ''' <remarks></remarks>
  Public Shared Function GetParameterValueString(ByVal p As Parameter) As String
    Select Case p.StorageType
      Case StorageType.Double
        Return p.AsValueString
      Case StorageType.ElementId
        ' Get the Element's Name
        Dim m_eid As New ElementId(p.AsElementId.Value)
        Dim m_obj As Element = p.Element.Document.GetElement(m_eid)
        Return m_obj.Name
      Case StorageType.Integer
        Return p.AsInteger
      Case StorageType.None
        Return p.AsValueString
      Case StorageType.String
        Return p.AsString
      Case Else
        Return ""
    End Select
  End Function

  ''' <summary>
  ''' Get the value of a parameter
  ''' </summary>
  ''' <param name="p">DB.Parameter</param>
  ''' <returns>String representing the value</returns>
  ''' <remarks></remarks>
  Public Shared Function GetParameterValue(ByVal p As Parameter) As String
    'public static Object GetParameterValue(Parameter parameter) 
    Select Case p.StorageType
      Case StorageType.Double
        Return p.AsValueString
      Case StorageType.ElementId
        ' Get the Element's Name
        Dim m_eid As New ElementId(p.AsElementId.Value)
        Dim m_obj As Element = p.Element.Document.GetElement(m_eid)
        Return m_obj.Name
      Case StorageType.Integer
        Return p.AsInteger
      Case StorageType.None
        Return p.AsValueString
      Case StorageType.String
        Return p.AsString
      Case Else
        Return ""
    End Select
  End Function

  ''' <summary>
  ''' Set a value to a parameter
  ''' </summary>
  ''' <param name="p">DB.Parameter</param>
  ''' <param name="v">Value does not have to be a string, Object</param>
  ''' <remarks></remarks>
  Public Shared Sub SetParameterValue(ByVal p As Parameter, ByVal v As Object)
    'first,check whether this parameter is read only 
    If p.IsReadOnly Then
      Exit Sub
    End If
    Select Case p.StorageType
      Case StorageType.Double
        p.SetValueString(TryCast(v, String))
        Exit Select
      Case StorageType.ElementId
        Dim myElementId As ElementId = DirectCast(v, ElementId)
        p.Set(myElementId)
        Exit Select
      Case StorageType.Integer
        p.SetValueString(TryCast(v, String))
        Exit Select
      Case StorageType.None
        p.SetValueString(TryCast(v, String))
        Exit Select
      Case StorageType.String
        p.Set(TryCast(v, String))
        Exit Select
      Case Else
        Exit Select
    End Select
  End Sub

#End Region

End Class
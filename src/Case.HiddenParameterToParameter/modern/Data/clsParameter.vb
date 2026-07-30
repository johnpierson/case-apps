Imports Autodesk.Revit.DB

Namespace Data

  ''' <summary>
  ''' Helper class used to work with parameters 
  ''' </summary>
  ''' <remarks></remarks>
  Public Class clsParameter

    Private _p As Parameter

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="p"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal p As Parameter)

      ' Widen Scope
      _p = p

    End Sub

    ''' <summary>
    ''' The parameter reference
    ''' </summary>
    ''' <value></value>
    ''' <returns>Parameter Object</returns>
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
    Public ReadOnly Property Format() As StorageType
      Get
        Try
          Return _p.StorageType
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
          Dim v As String = GetValue(False)
          If Not String.IsNullOrEmpty(v) Then
            Return v
          Else
            Return ""
          End If
        Catch
          Return Nothing
        End Try
      End Get
      Set(ByVal v As String)
        Try
          SetValue(v, False)
        Catch
        End Try
      End Set
    End Property

    ''' <summary>
    ''' Returns value of the parameter
    ''' as a string
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValueString As String
      Get
        Try
          Dim v As String = GetValue(True)
          If Not String.IsNullOrEmpty(v) Then
            Return v
          Else
            Return ""
          End If
        Catch
          Return Nothing
        End Try
      End Get
      Set(ByVal v As String)
        Try
          SetValue(v, True)
        Catch
        End Try
      End Set
    End Property

    ''' <summary>
    ''' Set a value to a parameter
    ''' </summary>
    ''' <param name="value">
    '''  Value does not have to be a string
    ''' </param>
    ''' <param name="asString">As string?</param>
    ''' <remarks></remarks>
    Private Sub SetValue(ByVal value As Object,
                         asString As Boolean)

      ' Cannot edit readonly
      If _p.IsReadOnly Then Exit Sub

      Try
        ' Storage Type
        Select Case _p.StorageType
          Case StorageType.Double
            If asString = True Then
              _p.SetValueString _
                (TryCast(value, String))
            Else
              _p.Set(value)
            End If

          Case StorageType.ElementId
            Dim m_eid As ElementId
            m_eid = DirectCast((value), ElementId)
            _p.Set(m_eid)

          Case StorageType.Integer
            _p.SetValueString _
              (TryCast(value, String))

          Case StorageType.None
            _p.SetValueString _
              (TryCast(value, String))

          Case StorageType.String
            _p.Set(TryCast(value, String))
            Exit Select

        End Select
      Catch

      End Try

    End Sub

    ''' <summary>
    ''' Get the value of a parameter
    ''' </summary>
    ''' <param name="asString">Return as String?</param>
    ''' <returns>String representing the value</returns>
    ''' <remarks></remarks>
    Private Function GetValue(asString As Boolean) As String

      ' Return the Value
      Select Case _p.StorageType
        Case StorageType.Double
          If asString = True Then
            Return _p.AsValueString
          Else
            Return _p.AsDouble.ToString
          End If

        Case StorageType.ElementId
          If asString = True Then
            ' Get the Element's Name
            Dim m_eid As New ElementId(_p.AsElementId.Value)
            Dim m_obj As Element
            m_obj = _p.Element.Document.GetElement(m_eid)
            Return m_obj.Name
          Else
            Return _p.AsElementId.ToString
          End If

        Case StorageType.Integer
          Return _p.AsInteger.ToString

        Case StorageType.None
          Return _p.AsValueString

        Case StorageType.String
          Return _p.AsString

        Case Else
          Return ""

      End Select

    End Function

  End Class
End Namespace
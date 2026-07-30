Imports Autodesk.Revit.DB

Namespace Data

  ''' <summary>
  ''' Helper class used to work with parameters 
  ''' </summary>
  ''' <remarks></remarks>
  Public Class clsRvtParameter

    Private _parameter As Parameter

#Region "Public Properties"

    ''' <summary>
    ''' The name of the parameter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Name() As String
      Get
        Try
          Return _parameter.Definition.Name
        Catch
          Return Nothing
        End Try
      End Get
    End Property

    ''' <summary>
    ''' The parameter reference
    ''' </summary>
    ''' <value></value>
    ''' <returns>Parameter Object</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ParameterObject() As Parameter
      Get
        Return _parameter
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
          End If
        Catch
        End Try
        Return ""
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
          End If
        Catch
        End Try
        Return ""
      End Get
      Set(ByVal v As String)
        Try
          SetValue(v, True)
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
      _parameter = p

    End Sub

#Region "Private Members"

    ''' <summary>
    ''' Set a value to a parameter
    ''' </summary>
    ''' <param name="val"></param>
    ''' <param name="asString"></param>
    ''' <remarks></remarks>
    Private Sub SetValue(ByVal val As Object,
                         asString As Boolean)

      ' Cannot edit readonly
      If _parameter.IsReadOnly Then Exit Sub

      Try
        ' Storage Type
        Select Case _parameter.StorageType
          Case StorageType.Double
            If asString = True Then
              _parameter.SetValueString(CStr(val))
            Else
              _parameter.Set(CDbl(val))
            End If

          Case StorageType.ElementId
            ' No Real String Support for Setting
            Dim m_eid As ElementId = DirectCast(val, ElementId)
            _parameter.Set(m_eid)

          Case StorageType.Integer
            _parameter.Set(CInt(val))

          Case StorageType.None
            _parameter.Set(CStr(val))

          Case StorageType.String
            _parameter.Set(CStr(val))
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

      Try

        ' Return the Value
        Select Case _parameter.StorageType
          Case StorageType.Double
            If asString = True Then
              Return _parameter.AsValueString
            Else
              Return _parameter.AsDouble.ToString
            End If

          Case StorageType.ElementId
            If asString = True Then
              ' Get the Element's Name
              Dim m_eid As New ElementId(_parameter.AsElementId.Value)
              Dim m_obj As Element = _parameter.Element.Document.GetElement(m_eid)
              If Not m_obj Is Nothing Then
                Return m_obj.Name
              End If
            Else
              Return _parameter.AsElementId.ToString
            End If

          Case StorageType.Integer
            Return _parameter.AsInteger.ToString

          Case StorageType.None
            Return _parameter.AsValueString

          Case StorageType.String
            Return _parameter.AsString

        End Select

      Catch
      End Try

      ' Last Resort
      Return ""

    End Function

#End Region

  End Class
End Namespace
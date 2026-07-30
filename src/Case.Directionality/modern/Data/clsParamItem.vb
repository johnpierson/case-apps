Namespace Data

  ''' <summary>
  ''' A very generic class for capturing the name, format and sharing scope of a parameter
  ''' without capturing and storing an actual parameter object
  ''' </summary>
  ''' <remarks></remarks>
  Public Class clsParamItem

    Public Property Name As String
    Public Property Format As String
    Public Property isShared As Boolean
    Public Property SharedGUID As String
    Public Property ConstantCustomValue As String
    Public Property CreatedFromParameter As Boolean
    Public Property isReadOnly As Boolean
    Public Property DisplayUnitType As String

    ''' <summary>
    ''' Class constructor using a para class
    ''' </summary>
    ''' <param name="para"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal para As clsPara)
      Name = para.Name

      ' Switched to be text for Doubles
      If para.Format.ToLower = "double" Then
        Format = "String"
      Else
        Format = para.Format
      End If

      isShared = para.ParameterIsShared
      DisplayUnitType = para.DisplayUnitType
      ConstantCustomValue = ""
      CreatedFromParameter = True
      isReadOnly = para.ParameterIsReadOnly
      If isShared = True Then
        SharedGUID = para.ParameterObject.GUID.ToString
      Else
        SharedGUID = ""
      End If
    End Sub

    ''' <summary>
    ''' Class constructor using a parameter name and format
    ''' </summary>
    ''' <param name="p_Name"></param>
    ''' <param name="p_Format"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal p_Name As String, ByVal p_Format As String)
      Name = p_Name
      Format = p_Format
      isShared = False
      ConstantCustomValue = ""
      SharedGUID = ""
      CreatedFromParameter = False
      isReadOnly = False
      DisplayUnitType = ""
    End Sub

    ''' <summary>
    ''' Class constructor using a parameter name, format and value
    ''' </summary>
    ''' <param name="p_Name"></param>
    ''' <param name="p_Format"></param>
    ''' <param name="p_Value"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal p_Name As String, ByVal p_Format As String, ByVal p_Value As String)
      Name = p_Name
      Format = p_Format
      isShared = False
      ConstantCustomValue = p_Value
      SharedGUID = ""
      CreatedFromParameter = False
      isReadOnly = False
      DisplayUnitType = ""
    End Sub

  End Class
End Namespace
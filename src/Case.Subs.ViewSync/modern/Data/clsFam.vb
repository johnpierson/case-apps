Imports Autodesk.Revit.DB
Imports Newtonsoft.Json

Namespace Data

  Public Class clsFam

    Private _famName As String = ""
    Private _typName As String = ""
    Private _fs As FamilySymbol

#Region "Public Properties"

    ''' <summary>
    ''' Found ElementID Integer
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property IdFamily As Integer

    ''' <summary>
    ''' Family Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FamilyName As String
      Get
        Return _famName
      End Get
    End Property

    ''' <summary>
    ''' Type Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TypeName As String
      Get
        Return _typName
      End Get
    End Property

    ''' <summary>
    ''' For Combobox
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <JsonIgnore>
    Public ReadOnly Property DisplayName As String
      Get
        Try
          Return FamilyName & " (" & TypeName & ")"
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' Instances in the Model
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <JsonIgnore>
    Public Property InstQty As Integer

#End Region

    ''' <summary>
    ''' Family Symbol
    ''' </summary>
    ''' <param name="fs"></param>
    ''' <remarks></remarks>
    Public Sub New(fs As FamilySymbol)

      ' Widen Scope
      _fs = fs
      InstQty = 0
      idFamily = -1
      _famName = fs.Family.Name
      _typName = fs.Name

    End Sub

    ''' <summary>
    ''' Family Item
    ''' </summary>
    ''' <param name="f">Family Name</param>
    ''' <param name="t">Type Name</param>
    ''' <remarks></remarks>
    Public Sub New(f As String, t As String)

      Try
        _famName = f
        _typName = t
      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get the Family Symbol
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetFamily() As FamilySymbol
      Return _fs
    End Function

  End Class
End Namespace
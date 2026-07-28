Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsFormulas

    Private _fs As FamilySymbol

    Public Property ParamName As String
    Public Property IsTypeParameter As String
    Public Property ParamFormula As String

    Public ReadOnly Property FamilyName As String
      Get
        Try
          Return _fs.Family.Name
        Catch
          Return "{error}"
        End Try
      End Get
    End Property

    Public ReadOnly Property FamilyCategory As String
      Get
        Try
          Return _fs.Category.Name
        Catch
          Return "{error}"
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Basic Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(fs As FamilySymbol,
                   pName As String,
                   pFormula As String,
                   isType As Boolean)

      ' Widen Scope
      _fs = fs
      ParamName = pName
      ParamFormula = pFormula
      If isType = True Then
        isTypeParameter = "True"
      Else
        isTypeParameter = "False"
      End If

    End Sub

  End Class
End Namespace
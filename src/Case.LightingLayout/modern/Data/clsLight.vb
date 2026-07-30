Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsLight

    Private _symb As FamilySymbol

#Region "Public Properties"

    ''' <summary>
    ''' Display Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DisplayName As String
      Get
        Return "(" & _symb.Family.Name & ") " & _symb.Name
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Lighting Family
    ''' </summary>
    ''' <param name="s"></param>
    ''' <remarks></remarks>
    Public Sub New(s As FamilySymbol)

      ' Widen Scope
      _symb = s

    End Sub

    ''' <summary>
    ''' Get the Symbol
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSymbol() As FamilySymbol
      Return _symb
    End Function

  End Class
End Namespace
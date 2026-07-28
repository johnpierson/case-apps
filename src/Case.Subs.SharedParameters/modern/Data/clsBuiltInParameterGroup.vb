Imports Autodesk.Revit.DB

Namespace Data

  ''' <summary>
  ''' Parameter group helper (ForgeTypeId / GroupTypeId for Revit 2025+).
  ''' </summary>
  Public Class clsBuiltInParameterGroup

    Private _pg As ForgeTypeId

    Public ReadOnly Property DisplayName As String
      Get
        Try
          Return LabelUtils.GetLabelForGroup(_pg)
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    Public ReadOnly Property ParameterGroup As ForgeTypeId
      Get
        Return _pg
      End Get
    End Property

    Public Sub New(pg As ForgeTypeId)
      _pg = pg
    End Sub

  End Class
End Namespace

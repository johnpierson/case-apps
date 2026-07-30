Imports Autodesk.Revit.DB

Namespace Data

  ''' <summary>
  ''' Parameter group helper (ForgeTypeId / GroupTypeId for Revit 2025+).
  ''' </summary>
  Public Class clsBuiltInParameterGroup

    Public ReadOnly Property DisplayName As String
      Get
        Try
          Return LabelUtils.GetLabelForGroup(ParameterGroup)
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    Public Property ParameterGroup As ForgeTypeId

    Public Sub New(pg As ForgeTypeId)
      ParameterGroup = pg
    End Sub

  End Class
End Namespace

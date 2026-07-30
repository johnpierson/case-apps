Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsViewType

    Private _vt As ViewFamilyType = Nothing

#Region "Public Properties"

    ''' <summary>
    ''' Inclusion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsChecked As Boolean

    ''' <summary>
    ''' Type Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Type As String

    ''' <summary>
    ''' Count of Views in Type
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Count As Integer

    ''' <summary>
    ''' The Kind of View
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewKind As String

#End Region

    ''' <summary>
    ''' ViewFamilyType Helper
    ''' </summary>
    ''' <param name="vt"></param>
    ''' <remarks></remarks>
    Public Sub New(vt As ViewFamilyType)

      ' Widen Scope
      _vt = vt
      isChecked = True

      Try
        Type = _vt.Name
      Catch
      End Try

      Count = 0

      Try
        ViewKind = _vt.ViewFamily.ToString
      Catch
      End Try

    End Sub

    ''' <summary>
    ''' ElementID Integer
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetId() As Integer
      Try
        Return _vt.Id.Value
      Catch
      End Try
      Return -1
    End Function

  End Class
End Namespace
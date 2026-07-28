Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsViews

    Private _v As View

#Region "Public Properties"

    Public Property IsChecked As Boolean

    Public ReadOnly Property Name As String
      Get
        Try
          Return _v.Parameter(BuiltInParameter.VIEW_NAME).AsString
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property

    Public ReadOnly Property Type As String
      Get
        Try
          If _v.IsTemplate = True Then Return "View Template!"
        Catch
        End Try
        Try
          Return _v.Parameter(BuiltInParameter.VIEW_TYPE).AsString
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property

    Public ReadOnly Property Level As String
      Get
        Try
          If Not _v.GenLevel Is Nothing Then
            Return _v.GenLevel.Name
          Else
            Return "N/A"
          End If
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub New(e As Element)

      ' Widen Scope
      _v = TryCast(e, View)
      isChecked = True

    End Sub

    ''' <summary>
    ''' The ElementID
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetId() As ElementId
      Try
        Return _v.Id
      Catch
        Return Nothing
      End Try
    End Function

    ''' <summary>
    ''' The ViewFamilyType ElementID
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetViewFamilyTypeId() As ElementId
      Try
        Return _v.GetTypeId
      Catch
        Return Nothing
      End Try
    End Function

  End Class
End Namespace
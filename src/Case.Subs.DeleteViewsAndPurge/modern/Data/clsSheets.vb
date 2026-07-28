Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsSheets

    Private _sht As ViewSheet

#Region "Public Properties"

    Public Property IsChecked As Boolean

    Public ReadOnly Property Number As String
      Get
        Try
          Return _sht.Parameter(BuiltInParameter.SHEET_NUMBER).AsString
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property

    Public ReadOnly Property Name As String
      Get
        Try
          Return _sht.Parameter(BuiltInParameter.SHEET_NAME).AsString
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property

#End Region

    ''' <summary>
    ''' A Sheet Helper Class
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub New(e As Element)

      ' Widen Scope
      _sht = TryCast(e, ViewSheet)
      isChecked = True

    End Sub

    ''' <summary>
    ''' The ElementID
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetId() As ElementId
      Try
        Return _sht.Id
      Catch
        Return Nothing
      End Try
    End Function

  End Class
End Namespace
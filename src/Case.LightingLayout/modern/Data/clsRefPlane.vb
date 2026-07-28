Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsRefPlane

    Private _plane As ReferencePlane

#Region "Public Properties"

    ''' <summary>
    ''' Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Name As String
      Get
        Try
          Return _plane.Name
        Catch
        End Try
        Return ""
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Reference Plane
    ''' </summary>
    ''' <param name="rp"></param>
    ''' <remarks></remarks>
    Public Sub New(rp As ReferencePlane)

      ' Widen Scope
      _plane = rp

    End Sub

    ''' <summary>
    ''' Get the Plane
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPlane() As ReferencePlane
      Return _plane
    End Function

  End Class
End Namespace
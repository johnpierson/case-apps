Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Public Class clsSettings

  Private _cmd As ExternalCommandData

#Region "Public Properties - Revit Document and Application"

  ''' <summary>
  ''' Current Document
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property ActiveDoc As Document
    Get
      Try
        Return _cmd.Application.ActiveUIDocument.Document
      Catch ex As Exception
        Return Nothing
      End Try
    End Get
  End Property

  ''' <summary>
  ''' UI Application
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property UIApplication As Autodesk.Revit.UI.UIApplication
    Get
      Try
        Return _cmd.Application
      Catch ex As Exception
        Return Nothing
      End Try
    End Get
  End Property

  ''' <summary>
  ''' Document Path
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property ActiveDocPath As String
    Get
      Try
        If ActiveDoc.IsWorkshared = True Then
          If Not String.IsNullOrEmpty(ActiveDoc.GetWorksharingCentralModelPath().ToString) Then
            Return ActiveDoc.GetWorksharingCentralModelPath().ToString
          Else
            Return ActiveDoc.PathName
          End If
        Else
          Return ActiveDoc.PathName
        End If
      Catch ex As Exception
        Return ""
      End Try
    End Get
  End Property

#End Region

#Region "Public Properties - Import Settings"

  Public Property ModelScale As Double
  Public Property PrecisionSpline As Integer
  Public Property PrecisionSurface As Integer

  Public Property ImportPoints As Boolean
  Public Property ImportLines As Boolean
  Public Property ImportArcs As Boolean
  Public Property ImportCircles As Boolean
  Public Property ImportPolyLines As Boolean
  Public Property ImportPolyCurves As Boolean
  Public Property ImportNURBSCurves2D As Boolean
  Public Property ImportClosedSplines2D As Boolean
  Public Property ImportSplines3D As Boolean
  Public Property ImportSurfaces As Boolean
  Public Property ImportCornerSrf As Boolean
  Public Property ImportTrimSrf As Boolean
  Public Property ImportPolySrf As Boolean

  Public Property ModelTolerance As Double

#End Region

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="CmdData"></param>
  ''' <remarks></remarks>
  Friend Sub New(CmdData As ExternalCommandData)

    ' Widen Scope
    _cmd = CmdData

  End Sub

End Class
Imports System.Reflection
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData
    Private _eSet As ElementSet

#Region "Public Properties"

    ''' <summary>
    ''' UI Application Object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property uiApp As UIApplication
      Get
        Try
          Return _cmd.Application
        Catch ex As Exception
          Return Nothing
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Active Document
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
    ''' The Active UI Document
    ''' </summary>
    ''' <value>Document object</value>
    ''' <returns>a UIDocument</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ActiveUiDoc As UIDocument
      Get
        Try
          Return _cmd.Application.ActiveUIDocument
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
    Public ReadOnly Property DocName As String
      Get
        Try
          If ActiveDoc.IsWorkshared = True Then
            If Not String.IsNullOrEmpty(ActiveDoc.GetWorksharingCentralModelPath.CentralServerPath) Then
              Return ActiveDoc.GetWorksharingCentralModelPath.CentralServerPath
            Else
              Return "Detached Model"
            End If
          Else
            Return ActiveDoc.PathName
          End If
        Catch ex As Exception
          Return ""
        End Try
      End Get
    End Property

    ''' <summary>
    ''' App Version
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Version As String
      Get
        Return Assembly.GetExecutingAssembly.GetName.Version.ToString
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <param name="eSet"></param>
    ''' <remarks></remarks>
    Public Sub New(cmd As ExternalCommandData, eSet As ElementSet)

      ' Widen Scope
      _cmd = cmd
      _eSet = eSet

    End Sub

  End Class
End Namespace
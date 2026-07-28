Imports System.Reflection
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

#Region "Public Properties"

    Private _cmd As ExternalCommandData
    Private _eSet As ElementSet

    ''' <summary>
    ''' The Active Document
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Doc As Document
      Get
        Try
          Return _cmd.Application.ActiveUIDocument.Document
        Catch
          Return Nothing
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Path to Active Doc
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DocName As String
      Get
        Try
          If Doc.IsWorkshared = True Then
            If Not String.IsNullOrEmpty(Doc.GetWorksharingCentralModelPath.CentralServerPath) Then
              Return Doc.GetWorksharingCentralModelPath.CentralServerPath
            Else
              Return Doc.PathName
            End If
          Else
            Return Doc.PathName
          End If
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' App Version
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AppVersion As String
      Get
        Return Assembly.GetExecutingAssembly.GetName.Version.ToString
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="c"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub New(c As ExternalCommandData, e As ElementSet)

      ' Widen Scope
      _cmd = c
      _eSet = e

    End Sub

  End Class
End Namespace
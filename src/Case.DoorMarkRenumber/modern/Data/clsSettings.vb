Imports System.Reflection
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  ''' <summary>
  ''' A class to manage general application settings
  ''' </summary>
  ''' <remarks></remarks>
  Public Class clsSettings

    Private _cmd As ExternalCommandData

#Region "Public Properties"

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

    Public ReadOnly Property AppVersion As String
      Get
        Return "v" & Assembly.GetExecutingAssembly.GetName.Version.ToString
      End Get
    End Property
    Public ReadOnly Property Doc As Document
      Get
        Try
          Return _cmd.Application.ActiveUIDocument.Document
        Catch
          Return Nothing
        End Try
      End Get
    End Property
    Public ReadOnly Property ActiveUIdoc As UIDocument
      Get
        Try
          Return _cmd.Application.ActiveUIDocument
        Catch
          Return Nothing
        End Try
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Class Constructor
    ''' </summary>
    ''' <param name="cmdData">Revit ExternalCommandData Reference</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal cmdData As ExternalCommandData)

      ' Widen Scope
      _cmd = cmdData

    End Sub

  End Class
End Namespace
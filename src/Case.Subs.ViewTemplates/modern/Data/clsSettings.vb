Imports System.Reflection
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData
    Private _appVersion As String = ""

#Region "Public Properties"

    ''' <summary>
    ''' Views
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewElements As List(Of clsView)

    ''' <summary>
    ''' View Templates
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewTemplates As Dictionary(Of Integer, clsViewTemplate)

    ''' <summary>
    ''' Active Document
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Doc As Document
      Get
        Try
          Return _cmd.Application.ActiveUIDocument.Document
        Catch
        End Try
        Return Nothing
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
          If Doc.IsWorkshared = True Then
            If Not String.IsNullOrEmpty(Doc.GetWorksharingCentralModelPath.CentralServerPath) Then
              Return Doc.GetWorksharingCentralModelPath.CentralServerPath
            Else
              Return "Detached Model"
            End If
          Else
            Return Doc.PathName
          End If
        Catch
        End Try
        Return ""
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
        Return _appVersion
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Public Sub New(cmd As ExternalCommandData)

      ' Widen Scope
      _cmd = cmd
      _appVersion = Assembly.GetExecutingAssembly.GetName.Version.ToString

    End Sub

#Region "Public Members"

    ''' <summary>
    ''' Get all views
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetViews()

      Try

        ' Fresh Lists
        ViewTemplates = New Dictionary(Of Integer, clsViewTemplate)
        ViewElements = New List(Of clsView)

        ' Collect All Views
        Using col As New FilteredElementCollector(Doc)
          col.OfClass(GetType(View))
          For Each e In col.ToElements

            ' Helper
            Dim m_v As View = TryCast(e, View)
            If Not m_v Is Nothing Then

              ' Skip Types:
              If m_v.ViewType = ViewType.CostReport Then Continue For
              If m_v.ViewType = ViewType.DrawingSheet Then Continue For
              If m_v.ViewType = ViewType.Internal Then Continue For
              If m_v.ViewType = ViewType.Legend Then Continue For
              If m_v.ViewType = ViewType.LoadsReport Then Continue For
              #If REVIT2027
              If m_v.ViewType = ViewType.PressureLossReport Then Continue For
              #Else
              If m_v.ViewType = ViewType.PresureLossReport Then Continue For
              #End If
              If m_v.ViewType = ViewType.Rendering Then Continue For
              If m_v.ViewType = ViewType.Report Then Continue For
              If m_v.ViewType = ViewType.Undefined Then Continue For
              If m_v.ViewType = ViewType.Walkthrough Then Continue For

              ' Is it a Template?
              If m_v.IsTemplate = True Then
                ViewTemplates.Add(m_v.Id.Value, New clsViewTemplate(m_v))
              Else
                ViewElements.Add(New clsView(m_v))
              End If

            End If

          Next
        End Using

      Catch
      End Try

    End Sub

#End Region

  End Class
End Namespace
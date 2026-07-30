Imports System.Reflection
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData
    Private _eSet As ElementSet

#Region "Public Properties"

    Public Property ViewElements As List(Of clsViews)
    Public Property ViewTemplates As List(Of clsViewTemplates)

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
          If Doc.IsWorkshared = True Then
            If Not String.IsNullOrEmpty(Doc.GetWorksharingCentralModelPath.CentralServerPath) Then
              Return Doc.GetWorksharingCentralModelPath.CentralServerPath
            Else
              Return "Detached Model"
            End If
          Else
            Return Doc.PathName
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
    Public ReadOnly Property AppVersion As String
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

      ' Get Views
      GetViews()

    End Sub

    ''' <summary>
    ''' Get all views
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetViews()

      Try

        ' Fresh Lists
        ViewTemplates = New List(Of clsViewTemplates)
        ViewElements = New List(Of clsViews)

        ' Collect All Views
        Using col As New FilteredElementCollector(Doc)
          col.OfClass(GetType(View))
          For Each e In col.ToElements
            Dim m_v As View = TryCast(e, View)
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

            If Not m_v Is Nothing Then
              If m_v.IsTemplate = True Then
                ViewTemplates.Add(New clsViewTemplates(m_v))
              Else
                ViewElements.Add(New clsViews(m_v))
              End If
            End If
          Next
        End Using

      Catch
      End Try

    End Sub

  End Class
End Namespace
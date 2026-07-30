Imports System.Linq
Imports System.Reflection
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData
    Private _vTypes As New SortedDictionary(Of String, String)
    Private _vKinds As New SortedDictionary(Of String, String)

#Region "Public Properties"

    ''' <summary>
    ''' View Helpers
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Views As SortableBindingList(Of clsView)

    ''' <summary>
    ''' List of View Type Names
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewTypes As List(Of String)

    ''' <summary>
    ''' List of View Kinds
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewKinds As List(Of String)

    ''' <summary>
    ''' Views, Organized by Kind
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewNames As Dictionary(Of String, Dictionary(Of String, String))

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
        Return Assembly.GetExecutingAssembly.GetName.Version.ToString
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

      ' Setup
      Setup()

    End Sub

#Region "Private Members"
    
    ''' <summary>
    ''' Class Setup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Setup()

      ' Fresh List
      Views = New SortableBindingList(Of clsView)
      ViewTypes = New List(Of String)
      ViewKinds = New List(Of String)
      ViewNames = New Dictionary(Of String, Dictionary(Of String, String))

      Try

        ' Query
#If REVIT2027
        Dim skipPressure As ViewType = ViewType.PressureLossReport
#Else
        Dim skipPressure As ViewType = ViewType.PresureLossReport
#End If
        Dim m_views = From e In New FilteredElementCollector(Doc) _
              .OfClass(GetType(View))
              Let v = TryCast(e, View)
              Where v.IsTemplate = False
              Where Not v.ViewType = ViewType.DrawingSheet
              Where Not v.ViewType = ViewType.Legend
              Where Not v.ViewType = ViewType.SystemBrowser
              Where Not v.ViewType = skipPressure
              Where Not v.ViewType = ViewType.ProjectBrowser
              Where Not v.ViewType = ViewType.LoadsReport
              Where Not v.ViewType = ViewType.Rendering
              Where Not v.ViewType = ViewType.Report
              Where Not v.ViewType = ViewType.Undefined
              Select v

        ' Process Results
        If Not m_views Is Nothing Then
          For Each x In m_views

            ' Helper
            Dim m_viewHelper = New clsView(x)

            Try
              ' Add Names
              If Not ViewNames.ContainsKey(m_viewHelper.ViewKind.ToLower) Then
                ViewNames.Add(m_viewHelper.ViewKind.ToLower, New Dictionary(Of String, String))
              End If
              ViewNames(m_viewHelper.ViewKind.ToLower).Add(m_viewHelper.Name.ToLower, m_viewHelper.Name)
            Catch
            End Try

            Try
              ' Types
              If Not _vTypes.ContainsKey(m_viewHelper.ViewType) Then
                _vTypes.Add(m_viewHelper.ViewType, m_viewHelper.ViewType)
                If Not String.IsNullOrEmpty(m_viewHelper.ViewType) Then
                  ViewTypes.Add(m_viewHelper.ViewType)
                End If
              End If
            Catch
            End Try

            Try
              ' Kinds
              If Not _vKinds.ContainsKey(m_viewHelper.ViewKind) Then
                _vKinds.Add(m_viewHelper.ViewKind, m_viewHelper.ViewKind)
                If Not String.IsNullOrEmpty(m_viewHelper.ViewKind) Then
                  ViewKinds.Add(m_viewHelper.ViewKind)
                End If
              End If
            Catch
            End Try

            ' Add as Helper
            Views.Add(m_viewHelper)

          Next
        End If

      Catch
      End Try

      ' Sort Lists
      ViewKinds.Sort()
      ViewTypes.Sort()

    End Sub

#End Region

  End Class
End Namespace
Imports System.Reflection
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData
    Private _eSet As ElementSet
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

    Public Property ViewTypes As List(Of String)
    Public Property ViewKinds As List(Of String)
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

      ' Setup
      doSetup()

    End Sub

    ''' <summary>
    ''' Class Setup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub doSetup()

      ' Fresh List
      Views = New SortableBindingList(Of clsView)
      ViewTypes = New List(Of String)
      ViewKinds = New List(Of String)
      ViewNames = New Dictionary(Of String, Dictionary(Of String, String))

      Try

        ' Get the Views
        Using col As New FilteredElementCollector(Doc)
          col.OfClass(GetType(View))
          For Each x In col.ToElements

            ' Cast
            Dim m_v As View = TryCast(x, View)
            If m_v Is Nothing Then Continue For

            ' Skip Templates
            If m_v.IsTemplate = True Then Continue For

            ' Helper
            Dim m_viewHelper = New clsView(m_v)

            Try

              ' Ignore Sheets, Legends & Browsers
              If m_v.ViewType = ViewType.DrawingSheet Then Continue For
              If m_v.ViewType = ViewType.Legend Then Continue For
              If m_v.ViewType = ViewType.SystemBrowser Then Continue For
              If m_v.ViewType = ViewType.ProjectBrowser Then Continue For
              If m_v.ViewType = ViewType.LoadsReport Then Continue For
              #If REVIT2027
              If m_v.ViewType = ViewType.PressureLossReport Then Continue For
              #Else
              If m_v.ViewType = ViewType.PresureLossReport Then Continue For
              #End If
              If m_v.ViewType = ViewType.Rendering Then Continue For
              If m_v.ViewType = ViewType.Report Then Continue For
              If m_v.ViewType = ViewType.Undefined Then Continue For

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

            Catch
            End Try

          Next
        End Using

      Catch
      End Try

      ' Sort Lists
      ViewKinds.Sort()
      ViewTypes.Sort()

    End Sub

  End Class
End Namespace
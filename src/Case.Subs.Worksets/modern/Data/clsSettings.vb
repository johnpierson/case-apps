Imports System.Reflection
Imports System.Linq
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData
    Private _eSet As ElementSet
    Private _appVersion As String = ""

#Region "Public Properties"

    ''' <summary>
    ''' UI Application, this is the base point for all document data
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UiApp As UIApplication
      Get
        Try
          If Not _cmd Is Nothing Then Return _cmd.Application
        Catch
        End Try
        Return Nothing
      End Get
    End Property

    ''' <summary>
    ''' UI Document
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UiDoc As UIDocument
      Get
        Try
          Return UiApp.ActiveUIDocument
        Catch
        End Try
        Return Nothing
      End Get
    End Property

    ''' <summary>
    ''' Activate Selections in the Model
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SelectedElements As List(Of ElementId)

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
    ''' Document Name And Path
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DocPath As String
      Get
        Try

          ' Workshared?
          If Doc.IsWorkshared Then

            Return ModelPathUtils.ConvertModelPathToUserVisiblePath(Doc.GetWorksharingCentralModelPath)

          Else

            ' Use the document title
            Return Doc.PathName

          End If

        Catch
        End Try

        ' Failure
        Return ""

      End Get

    End Property

    ''' <summary>
    ''' User Kind Worksets
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserWorksets As SortedDictionary(Of Integer, clsRvtWorksets)

    ''' <summary>
    ''' All Type Elements for Category
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ElementsType As SortedDictionary(Of Integer, clsRvtElemType)

    ''' <summary>
    ''' All Instance Elements for Category
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ElementsInst As SortedDictionary(Of Integer, clsRvtElemInst)

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

    ''' <summary>
    ''' The Revit Build
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property RevitBuild As String
      Get
        Try
          If Not UiApp Is Nothing Then Return UiApp.Application.VersionBuild
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' The Revit Build Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property RevitBuildName As String
      Get
        Try
          If Not UiApp Is Nothing Then Return UiApp.Application.VersionName
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' The Revit Build Number
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property RevitBuildNumber As String
      Get
        Try
          If Not UiApp Is Nothing Then Return UiApp.Application.VersionBuild
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Public Sub New(cmd As ExternalCommandData, eSet As ElementSet)

      ' Widen Scope
      _cmd = cmd
      _eSet = eSet
      SelectedElements = UiDoc.Selection.GetElementIds()
      _appVersion = Assembly.GetExecutingAssembly.GetName.Version.ToString

    End Sub

#Region "Public Members"

    ''' <summary>
    ''' Get Model Data
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetData()

      Try

        ' Get Worksets
        UserWorksets = New SortedDictionary(Of Integer, clsRvtWorksets)
        Using col As New FilteredWorksetCollector(Doc)
          col.OfKind(WorksetKind.UserWorkset)
          For Each x In col.ToWorksets
            UserWorksets.Add(x.Id.IntegerValue, New clsRvtWorksets(x))
          Next
        End Using

        ' Default List
        ElementsType = New SortedDictionary(Of Integer, clsRvtElemType)
        ElementsInst = New SortedDictionary(Of Integer, clsRvtElemInst)

        Try

          ' Get Types
          Dim m_typeElements = From e In New FilteredElementCollector(Doc) _
                               .WhereElementIsElementType()

          For Each x As Element In m_typeElements.ToList()

            ' Store the Element
            ElementsType.Add(CInt(x.Id.Value), New clsRvtElemType(x))

          Next

        Catch
        End Try

        Try

          ' Get Instances
          Dim m_instanceElements = From e In New FilteredElementCollector(Doc) _
                                   .WhereElementIsNotElementType()

          For Each x As Element In m_instanceElements.ToList()

            ' Store the Element
            ElementsInst.Add(CInt(x.Id.Value), New clsRvtElemInst(x, UserWorksets))

          Next

        Catch
        End Try

      Catch
      End Try

    End Sub

#End Region

  End Class
End Namespace
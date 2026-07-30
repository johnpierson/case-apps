Imports System.IO
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Namespace Data

  Public Class clsConfig

    Private _doc As Document
    Private _pData As String = ""
    Private _families As New List(Of clsFam)

#Region "Public Properties - Non jSON"

    ''' <summary>
    ''' Valid jSON?
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <JsonIgnore>
    Public Property IsValid As Boolean

    ''' <summary>
    ''' Configuration File Path
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <JsonIgnore>
    Public Property ConfigPath As String

    ''' <summary>
    ''' State of Config
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <JsonIgnore>
    Public Property ConfigDataState As EnumCfgState

    ''' <summary>
    ''' View Sync Data File Path
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <JsonIgnore>
    Public ReadOnly Property ViewSyncPath As String
      Get
        Try
          If Not String.IsNullOrEmpty(ConfigPath) Then
            If File.Exists(ConfigPath) Then
              Return Replace(ConfigPath, ".ini", "_views.ini", , , CompareMethod.Text)
            End If
          End If
        Catch
        End Try
        Return ""
      End Get
    End Property

    ''' <summary>
    ''' View Sync Data
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <JsonIgnore>
    Public Property ViewData As clsViews

    ''' <summary>
    ''' Log File Path
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <JsonIgnore>
    Public ReadOnly Property LogPath As String
      Get
        Try
          If Not String.IsNullOrEmpty(ConfigPath) Then
            If File.Exists(ConfigPath) Then
              Return Replace(ConfigPath, ".ini", ".log", , , CompareMethod.Text)
            End If
          End If
        Catch
        End Try
        Return ""
      End Get
    End Property

    ''' <summary>
    ''' INI Parameter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <JsonIgnore>
    Public ReadOnly Property ParamData As String
      Get
        Return "Case View Sync INI Path"
      End Get
    End Property

    ''' <summary>
    ''' GUID Parameter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <JsonIgnore>
    Public ReadOnly Property ParamGuid As String
      Get
        Return "Case View Sync GUID"
      End Get
    End Property

#End Region

#Region "Public Properties - jSON"

    ''' <summary>
    ''' For temporary locking - to block multi edits
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LockedBy As String

    ''' <summary>
    ''' Master Document Model Path
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentationModelPath As String

    ''' <summary>
    ''' View Number Parameter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ParamView As String

    ''' <summary>
    ''' Sheet Number Parameter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ParamShtn As String

    ''' <summary>
    ''' Log All Placement Data
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LogPlacements As Boolean

    ''' <summary>
    ''' Log All Sync Data
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LogSyncs As Boolean

    ''' <summary>
    ''' Log All Config Data
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LogConfig As Boolean

    ''' <summary>
    ''' Family Tags
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Families As List(Of clsFam)
      Get
        Return _families
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

      ' Defaults
      _families = New List(Of clsFam)
      DocumentationModelPath = ""
      LogConfig = True
      LogPlacements = True
      LogSyncs = True
      LockedBy = Environment.MachineName & ": " & Environment.UserName
      ConfigDataState = EnumCfgState.isMissingParameter
      ParamView = "Case View Sync View Number"
      ParamShtn = "Case View Sync Sheet Number"

      Try
        ' View Data
        ViewData = New clsViews(ViewSyncPath)
      Catch
      End Try

    End Sub

#Region "Friends - Family Checking"

    ''' <summary>
    ''' Find the Family
    ''' </summary>
    ''' <param name="t">Key text</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function HasFamily(t As String) As Boolean

      Try

        ' Find the Family
        For Each x In Families
          If t.ToLower = (x.FamilyName & "|" & x.TypeName).ToLower Then Return True
        Next

      Catch
      End Try

      ' False
      Return False

    End Function

#End Region

#Region "Friends - Project Info"

    ''' <summary>
    ''' Read the Path and Create a Fresh File
    ''' </summary>
    ''' <param name="d">Document</param>
    ''' <param name="pData">Name of Parameter for INI Path</param>
    ''' <remarks></remarks>
    Friend Sub GetProjectInfo(d As Document, pData As String)

      ' Widen Scope
      _doc = d
      _pData = pData

      Try

        ' Project Information
        ConfigPath = ""
        Using col As New FilteredElementCollector(d)
          col.OfCategory(BuiltInCategory.OST_ProjectInformation)
          For Each x In col.ToElements

            ' Does Sync Path Parameter Exist
            Dim p As Parameter = x.LookupParameter(pData)
            If Not p Is Nothing Then

              ' Parameter Exists - Check for NULL
              ConfigDataState = EnumCfgState.isNullValue
              ConfigPath = p.AsString

              ' Value?
              If Not String.IsNullOrEmpty(ConfigPath) Then
                ConfigDataState = EnumCfgState.isPathNotFound

                ' Path Found?
                If File.Exists(ConfigPath) Then
                  ConfigDataState = EnumCfgState.isOK

                  ' Deserialize jSon
                  ReadFile()

                Else

                  ' Path Not Found
                  ConfigDataState = EnumCfgState.isPathNotFound

                End If

              End If

            End If

            ' Only One
            Exit For

          Next
        End Using

      Catch
        ConfigDataState = EnumCfgState.IsError
      End Try

    End Sub

    ''' <summary>
    ''' Add the INI Parameter
    ''' </summary>
    ''' <param name="d">Document</param>
    ''' <param name="uiApp">UI Application</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function AddIniParameter(d As Document,
                                    uiApp As UIApplication) As Boolean

      ' Result
      Dim m_result As Boolean = False

      Try

        ' Open the Default File
        Dim m_sharedDef As New clsSharedParameters(uiApp)
        If m_sharedDef.LoadSharedParameterFile() = False Then Return False

        ' Get the Category
        Dim m_catList As New List(Of Category)
        For Each x As Category In d.Settings.Categories
          If x.Name.ToLower.Contains("project") Then
            If x.Name.ToLower.Contains("info") Then
              m_catList.Add(x)
              Exit For
            End If
          End If
        Next

        ' Get the Definition
        Dim m_defList As New List(Of Definition)
        For Each x In m_sharedDef.DefinitionsByGroup
          For Each xx As Definition In x.Value
            If xx.Name.ToLower = "case view sync ini path" Then
              m_defList.Add(xx)
              Exit For
            End If
          Next
          If m_defList.Count > 0 Then Exit For
        Next

        ' Add the Parameter
        If m_defList.Count > 0 And m_catList.Count > 0 Then
          m_sharedDef.BindDefinitionsToCategories(m_defList, m_catList, GroupTypeId.Text)

          ' Success
          m_result = True

        End If

      Catch
      End Try

      ' Final
      Return m_result

    End Function

#End Region

#Region "Friends - Data File IO"

    ''' <summary>
    ''' Config to JSON
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfigToJson() As String

      ' Json Response
      Dim m_json As String = ""

      Try

        ' Check for Errors
        Using sw As New StreamReader(ConfigPath)
          Do Until sw.EndOfStream
            m_json += sw.ReadLine
          Loop
        End Using

      Catch
      End Try

      ' Return
      Return m_json

    End Function

    ''' <summary>
    ''' Config File Locked
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsConfigLocked() As Boolean

      Try

        ' Write Fresh File
        Dim m_cfgTemp As New clsConfig()
        m_cfgTemp.GetProjectInfo(_doc, _pData)
        m_cfgTemp.ReadFile()
        If Not m_cfgTemp Is Nothing Then
          If String.IsNullOrEmpty(m_cfgTemp.LockedBy) Then Return False
          If m_cfgTemp.LockedBy.ToLower <> (Environment.MachineName & ": " & Environment.UserName).ToLower Then
            Return True
          Else
            Return False
          End If
        End If

      Catch
      End Try

      ' Failed
      Return True

    End Function

    ''' <summary>
    ''' Update the Config File
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function UpdateConfigFile() As Boolean

      Try

        ' To jSon and Update File
        Dim m_jSon As String = JsonConvert.SerializeObject(Me)
        If Not String.IsNullOrEmpty(m_jSon) Then
          Using sw As New StreamWriter(ConfigPath, False)
            sw.WriteLine(m_jSon)
          End Using
        End If

      Catch
      End Try

      ' Failed
      Return False

    End Function

    ''' <summary>
    ''' Write a Line to the Log
    ''' </summary>
    ''' <param name="l">Line to Write</param>
    ''' <param name="p">Path to Model</param>
    ''' <param name="lk">Log Kind</param>
    ''' <remarks></remarks>
    Friend Sub WriteLogLine(l As String,
                            p As String,
                            lk As EnumLogKind)

      Try

        If String.IsNullOrEmpty(LogPath) Then Return

        ' Log Kind
        Select Case lk
          Case EnumLogKind.isOther
            Exit Sub
          Case EnumLogKind.isConfig
            If LogConfig = False Then Exit Sub
          Case EnumLogKind.isSync
            If LogSyncs = False Then Exit Sub
          Case EnumLogKind.isTag
            If LogPlacements = False Then Exit Sub
        End Select

        ' Line to Write
        Dim m_text As String = l & vbTab &
                               Environment.UserName & vbTab &
                               Environment.MachineName & vbTab &
                               Now.ToString & vbTab & p

        ' Append Line
        Using sw As New StreamWriter(LogPath, True)
          sw.WriteLine(m_text)
        End Using

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Read the jSon
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ReadFile()

      ' Fresh Dictionary
      _families = New List(Of clsFam)

      Try

        ' Read jSON Nodes
        Dim m_j = JObject.Parse(ConfigToJson)

        ' Valid?
        If Not m_j Is Nothing Then
          isValid = True

          ' Read Data
          If Not String.IsNullOrEmpty(m_j("LockedBy")) Then
            LockedBy = m_j("LockedBy")
          End If
          DocumentationModelPath = m_j("DocumentationModelPath")
          LogConfig = m_j("LogConfig")
          LogPlacements = m_j("LogPlacements")
          LogSyncs = m_j("LogSyncs")
          ParamView = m_j("ParamView")
          ParamShtn = m_j("ParamShtn")

          ' Families
          For Each x In m_j("Families")

            Try

              ' Add the Item
              Families.Add(New clsFam(x("FamilyName"), x("TypeName")))

            Catch
            End Try

          Next

        Else
          isValid = False
        End If

      Catch
        isValid = False
      End Try

    End Sub

#End Region

  End Class
End Namespace
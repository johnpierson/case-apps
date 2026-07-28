Imports System
Imports System.IO
Imports System.Reflection
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.UI
Imports [Case].AppsRibbon.Utility

Namespace Entry

  ''' <summary>
  ''' Revit Ribbon Implementation
  ''' </summary>
  ''' <remarks></remarks>
  <Transaction(TransactionMode.Manual)>
  Public Class AppMain

    Implements IExternalApplication

    Private Const CTabNamePro = "CASE Apps #2"
    Private Const CTabNameFree = "CASE Apps #1"

    Private _path As String
    Private _uiApp As UIControlledApplication
    Private _toolsExistsSubsc As Boolean = False
    Private _toolsExistsFree As Boolean = False

#Region "Public Members - Revit IExternalApplication Implementation"

    ''' <summary>
    ''' Startup
    ''' </summary>
    ''' <param name="a"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function OnStartup(a As UIControlledApplication) As Result Implements IExternalApplication.OnStartup

      Try
        ' Path
        _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location)
        ' UI App
        _uiApp = a
        ' General Buttons
        LoadItemsGeneral()
        Try
                    ' Load the Ribbon Controls, etc.
                    LoadItemsTwo()
                Catch ex As Exception
                    PostToLog(String.Format("OnStartup PRO Loading Exception:" & vbCr & ex.ToString()))
                End Try

                Try

                    ' Load Free Items
                    LoadItemsOne()

                Catch ex As Exception
                    PostToLog(String.Format("OnStartup FREE Loading Exception:" & vbCr & ex.ToString()))
                End Try

                ' Success
                Return Result.Succeeded

            Catch ex As Exception
                PostToLog(String.Format("OnStartup Loading Exception:" & vbCr & ex.ToString()))
                Return Result.Failed
            End Try

        End Function

        ''' <summary>
        ''' Shut Down
        ''' </summary>
        ''' <param name="a"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function OnShutdown(a As UIControlledApplication) As Result Implements IExternalApplication.OnShutdown
            Return Result.Succeeded
        End Function

#End Region

#Region "Private Members - Ribbon Functions"

        ''' <summary>
        ''' Get a Pushbutton Object
        ''' </summary>
        ''' <param name="cmdName"></param>
        ''' <param name="cmdText"></param>
        ''' <param name="filePath"></param>
        ''' <param name="className"></param>
        ''' <param name="img16"></param>
        ''' <param name="img32"></param>
        ''' <param name="tooltipText"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetPushButtonData(cmdName As String,
                                       cmdText As String,
                                       filePath As String,
                                       className As String,
                                       img16 As String,
                                       img32 As String,
                                       tooltipText As String,
                                       cmdAvail As String,
                                       Optional toolType As String = "") As PushButtonData

            Try

                ' Return Nothing if File Not Exist
                If File.Exists(filePath) Then

                    If toolType.ToLower() = "subsc" Then _toolsExistsSubsc = True
                    If toolType.ToLower() = "free" Then _toolsExistsFree = True

                    ' Pushbutton Data
                    Dim m_pb As New PushButtonData(cmdName, cmdText, filePath, className)
                    With m_pb
                        .Image = LoadPngImgSource(img16)
                        .LargeImage = LoadPngImgSource(img32)
                        .ToolTip = tooltipText
                    End With

                    ' Availability?
                    If Not String.IsNullOrEmpty(cmdAvail) Then m_pb.AvailabilityClassName = cmdAvail

                    ' Success
                    Return m_pb

                End If

            Catch ex As Exception
                PostToLog(String.Format("GetPushButtonData Exception:" & vbCr & ex.ToString()))
            End Try
            Return Nothing
        End Function

        ''' <summary>
        ''' Adds a set of PushbuttonData to the ribbon on a specified panel as stacked and/or buttons
        ''' </summary>
        ''' <param name="panelName"></param>
        ''' <param name="buttons"></param>
        ''' <remarks></remarks>
        Private Sub AddAsStackedAndButtons(tabName As String,
                                       panelName As String,
                                       buttons As Dictionary(Of String, List(Of PushButtonData)))

            Try

                ' General Panel
                Dim m_panelSubsc As RibbonPanel = GetRibbonPanelByTabName(tabName, panelName)
                Dim m_iCnt As Integer = 0

                ' Process The Buttons into Stacks or Buttons
                For Each kvp In buttons

                    Try

                        ' Calculate for Separators
                        If kvp.Value.Count > 0 Then
                            If m_iCnt > 0 Then m_panelSubsc.AddSeparator()
                            m_iCnt += 1
                        End If

                        Select Case kvp.Value.Count

                            Case 12
                                m_panelSubsc.AddStackedItems(kvp.Value(0), kvp.Value(1), kvp.Value(2))
                                m_panelSubsc.AddStackedItems(kvp.Value(3), kvp.Value(4), kvp.Value(5))
                                m_panelSubsc.AddStackedItems(kvp.Value(6), kvp.Value(7), kvp.Value(8))
                                m_panelSubsc.AddStackedItems(kvp.Value(9), kvp.Value(10), kvp.Value(11))

                            Case 11
                                m_panelSubsc.AddStackedItems(kvp.Value(9), kvp.Value(10))
                                m_panelSubsc.AddStackedItems(kvp.Value(0), kvp.Value(1), kvp.Value(2))
                                m_panelSubsc.AddStackedItems(kvp.Value(3), kvp.Value(4), kvp.Value(5))
                                m_panelSubsc.AddStackedItems(kvp.Value(6), kvp.Value(7), kvp.Value(8))

                            Case 10
                                m_panelSubsc.AddItem(kvp.Value(9))
                                m_panelSubsc.AddStackedItems(kvp.Value(0), kvp.Value(1), kvp.Value(2))
                                m_panelSubsc.AddStackedItems(kvp.Value(3), kvp.Value(4), kvp.Value(5))
                                m_panelSubsc.AddStackedItems(kvp.Value(6), kvp.Value(7), kvp.Value(8))

                            Case 9
                                m_panelSubsc.AddStackedItems(kvp.Value(0), kvp.Value(1), kvp.Value(2))
                                m_panelSubsc.AddStackedItems(kvp.Value(3), kvp.Value(4), kvp.Value(5))
                                m_panelSubsc.AddStackedItems(kvp.Value(6), kvp.Value(7), kvp.Value(8))

                            Case 8
                                m_panelSubsc.AddItem(kvp.Value(0))
                                m_panelSubsc.AddItem(kvp.Value(1))
                                m_panelSubsc.AddStackedItems(kvp.Value(2), kvp.Value(3), kvp.Value(4))
                                m_panelSubsc.AddStackedItems(kvp.Value(5), kvp.Value(6), kvp.Value(7))

                            Case 7
                                m_panelSubsc.AddItem(kvp.Value(0))
                                m_panelSubsc.AddStackedItems(kvp.Value(1), kvp.Value(2), kvp.Value(3))
                                m_panelSubsc.AddStackedItems(kvp.Value(4), kvp.Value(5), kvp.Value(6))

                            Case 6
                                m_panelSubsc.AddStackedItems(kvp.Value(0), kvp.Value(1), kvp.Value(2))
                                m_panelSubsc.AddStackedItems(kvp.Value(3), kvp.Value(4), kvp.Value(5))

                            Case 5
                                m_panelSubsc.AddItem(kvp.Value(0))
                                m_panelSubsc.AddItem(kvp.Value(1))
                                m_panelSubsc.AddStackedItems(kvp.Value(2), kvp.Value(3), kvp.Value(4))

                            Case 4
                                m_panelSubsc.AddItem(kvp.Value(0))
                                m_panelSubsc.AddStackedItems(kvp.Value(1), kvp.Value(2), kvp.Value(3))

                            Case 3
                                m_panelSubsc.AddStackedItems(kvp.Value(0), kvp.Value(1), kvp.Value(2))

                            Case 2
                                m_panelSubsc.AddStackedItems(kvp.Value(0), kvp.Value(1))

                            Case 1
                                m_panelSubsc.AddItem(kvp.Value(0))

                        End Select

                    Catch ex As Exception
                        PostToLog(String.Format("AddAsStackedAndButtons Inner Exception:" & vbCr & ex.ToString()))
                    End Try

                Next

            Catch ex As Exception
                PostToLog(String.Format("AddAsStackedAndButtons Exception:" & vbCr & ex.ToString()))
            End Try
        End Sub

        ''' <summary>
        ''' Get the Ribbon Panel by Tab and Panel Name
        ''' </summary>
        ''' <param name="tabName">Tab Name</param>
        ''' <param name="panelName">Panel Name</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetRibbonPanelByTabName(tabName As String, panelName As String) As RibbonPanel

            Dim m_panel As RibbonPanel = Nothing

            Try

                ' Does it already exist?
                For Each x In _uiApp.GetRibbonPanels(tabName)
                    If x.Title.ToLower = panelName.ToLower Then Return x
                Next

            Catch ex As Exception
                PostToLog(String.Format("GetRibbonPanelByTabName Exception:" & vbCr & ex.ToString()))
            End Try

            Try

                ' Add the Panel
                m_panel = _uiApp.CreateRibbonPanel(tabName, panelName)

            Catch ex As Exception
                PostToLog(String.Format("GetRibbonPanelByTabName Exception:" & vbCr & ex.ToString()))
            End Try
            Return m_panel
        End Function

        ''' <summary>
        ''' Load an Image Source from File
        ''' </summary>
        ''' <param name="sourceName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function LoadPngImgSource(sourceName As String) As ImageSource

            Try

                ' Assembly
                Dim m_assembly As Assembly = Assembly.GetExecutingAssembly

                ' Stream
                Dim m_icon As Stream = m_assembly.GetManifestResourceStream(sourceName)

                ' Decoder
                Dim m_decoder As New PngBitmapDecoder(m_icon,
                                              BitmapCreateOptions.PreservePixelFormat,
                                              BitmapCacheOption.Default)

                ' Source
                Dim m_source As ImageSource = m_decoder.Frames(0)
                Return (m_source)

            Catch ex As Exception
                PostToLog(String.Format("LoadPngImgSource Exception:" & vbCr & ex.ToString()))
            End Try

            ' Fail
            Return Nothing

        End Function

        ''' <summary>
        ''' Add a button to a Ribbon Tab
        ''' </summary>
        ''' <param name="rpanel">The ribbon panel</param>
        ''' <param name="buttonName">The Name of the Button</param>
        ''' <param name="buttonText">Command Text</param>
        ''' <param name="imagePath16">Small Image</param>
        ''' <param name="imagePath32">Large Image</param>
        ''' <param name="dllPath">Path to the DLL file</param>
        ''' <param name="dllClass">Full qualified class descriptor</param>
        ''' <param name="tooltip">Tooltip to add to the button</param>
        ''' <param name="pbAvail">Pushbutton availability class, blank if none</param>
        ''' <remarks></remarks>
        Private Sub AddButton(rpanel As RibbonPanel,
                          buttonName As String,
                          buttonText As String,
                          imagePath16 As String,
                          imagePath32 As String,
                          dllPath As String,
                          dllClass As String,
                          tooltip As String,
                          pbAvail As String)

            Try

                ' PB Data
                Dim m_pbData As PushButtonData = GetPushButtonData(buttonName,
                                                           buttonText,
                                                           dllPath,
                                                           dllClass,
                                                           imagePath16,
                                                           imagePath32,
                                                           tooltip,
                                                           pbAvail)

                ' Add the button to the tab
                rpanel.AddItem(m_pbData)

            Catch ex As Exception
                PostToLog(String.Format("AddButton Exception:" & vbCr & ex.ToString()))
            End Try
        End Sub

#End Region

#Region "Private Members - Buttons (General)"

        ''' <summary>
        ''' Load the Controls
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadItemsGeneral()

            Try

                ' No Vasari
                If _uiApp.ControlledApplication.VersionName.ToLower.Contains("vasari") Then Return

                Try
                    ' First Create the Tab
                    _uiApp.CreateRibbonTab(CTabNameFree)
                Catch
                End Try

                ' Add the Stacked Items and Setup the Tab
                Dim _panel As RibbonPanel = GetRibbonPanelByTabName(CTabNameFree, "Resources v" & My.Application.Info.Version.ToString)

                AddButton(_panel,
                  "CaseAppsSite",
                  "Apps Page",
                  "Case.AppsRibbon.case_16.png",
                  "Case.AppsRibbon.case_blue_32.png",
                  GetType(AppMain).Assembly.Location,
                  "Case.AppsRibbon.Entry.CmdWebApps",
                  "Visit the main CASE Apps Site",
                  "")

            Catch ex As Exception
                PostToLog(String.Format("LoadItemsGeneral Exception:" & vbCr & ex.ToString()))
            End Try

        End Sub

#End Region

#Region "Private Members - Buttons (Apps2)"

        ''' <summary>
        ''' Load the Controls
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadItemsTwo()

            Try

                If _uiApp.ControlledApplication.VersionName.ToLower.Contains("vasari") Then Return

                Try
                    ' First Create the Tab
                    _uiApp.CreateRibbonTab(CTabNamePro)
                Catch
                End Try

                ' Excerl8 - Large Buttons
                Dim m_exceler8Path As String = Path.Combine(_path, "Case.Subs.Exceler8.dll")
                If File.Exists(m_exceler8Path) Then

                    Try

                        ' New RibbonPanel
                        Dim m_panelExceler8 As RibbonPanel = GetRibbonPanelByTabName(CTabNamePro, "Exceler8")

                        m_panelExceler8.AddItem(GetPushButtonData("CaseSubsExceler8Export",
                                                      "Export" & vbCr & "Category",
                                                      m_exceler8Path,
                                                      "Case.Subs.Exceler8.Entry.DE309158ACE741799F90E9210DEEF5F1",
                                                      "Case.AppsRibbon.Exceler8_Out_32.png",
                                                      "Case.AppsRibbon.Exceler8_Out_32.png",
                                                      "Excel Export Categories to Exceler8",
                                                      ""))

                        m_panelExceler8.AddItem(GetPushButtonData("CaseSubsExceler8ExportSched",
                                                      "Export" & vbCr & "Schedule",
                                                      m_exceler8Path,
                                                      "Case.Subs.Exceler8.Entry.DE309158ACE741799F90E9210DEEF5F3",
                                                      "Case.AppsRibbon.Exceler8_Out_32.png",
                                                      "Case.AppsRibbon.Exceler8_Out_32.png",
                                                      "Excel Export Schedule(s) to Exceler8",
                                                      ""))

                        m_panelExceler8.AddSeparator()

                        m_panelExceler8.AddItem(GetPushButtonData("CaseSubsExceler8Import",
                                                      "Import" & vbCr & "(Sync)",
                                                      m_exceler8Path,
                                                      "Case.Subs.Exceler8.Entry.DE309158ACE741799F90E9210DEEF5F2",
                                                      "Case.AppsRibbon.Exceler8_In_32.png",
                                                      "Case.AppsRibbon.Exceler8_In_32.png",
                                                      "Excel Import from Exceler8",
                                                      ""))

                    Catch ex As Exception
                        PostToLog(String.Format("LoadItemsTwo Exceler8 Exception:" & vbCr & ex.ToString()))
                    End Try

                End If

                ' List of Stacked Button Tools
                Dim m_subscTools As New Dictionary(Of String, List(Of PushButtonData))
                Dim m_pbd As PushButtonData

                ' Views
                m_subscTools.Add("views", New List(Of PushButtonData))
                m_pbd = GetPushButtonData("Case.Subs.WorksetBrowser",
                                  "Workset" & vbCr & "Browser",
                                  Path.Combine(_path, "Case.Subs.Worksets.dll"),
                                  "Case.Subs.Worksets.Entry.CEB749CFCF844AE8B76F40E3EBAA2FF7",
                                  "Case.AppsRibbon.Worksets_16.png",
                                  "Case.AppsRibbon.Worksets_32.png",
                                  "Browse elements by workset with some sweet charting...",
                                  "",
                                  "subsc")
                If Not m_pbd Is Nothing Then m_subscTools("views").Add(m_pbd)
                m_pbd = GetPushButtonData("Case.Subs.MultiViewDuplicate",
                                  "View" & vbCr & "Duplicator",
                                  Path.Combine(_path, "Case.Subs.multiviewduplicate.dll"),
                                  "Case.Subs.MultiViewDuplicate.Entry.A330EB8E54CA46B3A28E41DA9E77F9F6",
                                  "Case.AppsRibbon.Duplicate_16.png",
                                  "Case.AppsRibbon.Duplicate_32.png",
                                  "Multiple View Duplication...",
                                  "",
                                  "subsc")
                If Not m_pbd Is Nothing Then m_subscTools("views").Add(m_pbd)
                m_pbd = GetPushButtonData("CaseSubsViewTemplates",
                                  "View" & vbCr & "Templates",
                                  Path.Combine(_path, "Case.Subs.ViewTemplates.dll"),
                                  "Case.Subs.ViewTemplates.Entry.CEB749CFCF844AE8B76F40E3EBAA2FF7",
                                  "Case.AppsRibbon.SubsViewTemplate_16.png",
                                  "Case.AppsRibbon.SubsViewTemplate_32.png",
                                  "Manage view template assignments... and cleanup view templates...",
                                  "",
                                  "subsc")
                If Not m_pbd Is Nothing Then m_subscTools("views").Add(m_pbd)
                m_pbd = GetPushButtonData("CaseSubsDeleteViewsAndPurge",
                                  "Delete" & vbCr & "Views and Links",
                                  Path.Combine(_path, "Case.Subs.deleteviewsandpurge.dll"),
                                  "Case.Subs.DeleteViewsAndPurge.Entry.B31EA3F20509489BB52A0381E8838AFC",
                                  "Case.AppsRibbon.DeleteViews_16.png",
                                  "Case.AppsRibbon.DeleteViews_32.png",
                                  "Remove Sheets, Views, and Revit Links from the Model...",
                                  "",
                                  "subsc")
                If Not m_pbd Is Nothing Then m_subscTools("views").Add(m_pbd)

                m_subscTools.Add("manage", New List(Of PushButtonData))
                m_pbd = GetPushButtonData("Case.Subs.SharedParameters",
                                  "Shared" & vbCr & "Parameters",
                                  Path.Combine(_path, "Case.Subs.SharedParameters.dll"),
                                  "Case.Subs.SharedParameters.Entry.BC456E773F0B4A04A764DE16ECDA69C9",
                                  "Case.AppsRibbon.SharedParam_16.png",
                                  "Case.AppsRibbon.SharedParam_32.png",
                                  "An improved tool for loading Shared Parameters from a file... Supports Family Documents!",
                                  "",
                                  "subsc")
                If Not m_pbd Is Nothing Then m_subscTools("manage").Add(m_pbd)
                m_pbd = GetPushButtonData("Case.Subs.Compare",
                                  "CaseSubsCompare",
                                  Path.Combine(_path, "Case.Subs.Compare.dll"),
                                  "Case.Subs.Compare.Entry.A12745A39FCA40B49E32C51C0F9A26E5",
                                  "",
                                  "",
                                  "Compare Stuff...",
                                  "",
                                  "subsc")
                If Not m_pbd Is Nothing Then m_subscTools("manage").Add(m_pbd)
                m_pbd = GetPushButtonData("Case.Subs.Linestyles",
                                  "Change/Replace" & vbCr & "Line Styles",
                                  Path.Combine(_path, "Case.Subs.Linestyles.dll"),
                                  "Case.Subs.Linestyles.Entry.AB0844C654F34178B365D9C83BA84D7D",
                                  "Case.AppsRibbon.LineStyles_16.png",
                                  "Case.AppsRibbon.LineStyles_32.png",
                                  "Change and Replace Line Styles",
                                  "",
                                  "subsc")
                If Not m_pbd Is Nothing Then m_subscTools("manage").Add(m_pbd)
                m_pbd = GetPushButtonData("Case.Subs.Renamer",
                                  "Rename" & vbCr & "Stuff",
                                  Path.Combine(_path, "Case.Subs.Renamer.dll"),
                                  "Case.Subs.Renamer.Entry.DD108DD4BDB24224BFB1848AEEA2582D",
                                  "Case.AppsRibbon.Rename_16.png",
                                  "Case.AppsRibbon.Rename_32.png",
                                  "Rename things (Excel optional)",
                                  "",
                                  "subsc")
                If Not m_pbd Is Nothing Then m_subscTools("manage").Add(m_pbd)

                m_subscTools.Add("misc", New List(Of PushButtonData))
                m_pbd = GetPushButtonData("Case.Subs.SuperTag",
                                  "Super" & vbCr & "Tag",
                                  Path.Combine(_path, "Case.Subs.SuperTag.dll"),
                                  "Case.Subs.SuperTag.Entry.A01A0A226EBE4CD89D655C55B997650F",
                                  "Case.AppsRibbon.TagView_16.png",
                                  "Case.AppsRibbon.TagView_32.png",
                                  "Tag specific family types across multiple views",
                                  "",
                                  "subsc")
                If Not m_pbd Is Nothing Then m_subscTools("misc").Add(m_pbd)
                m_pbd = GetPushButtonData("Case.Subs.OpenNURBS",
                                  "Import" & vbCr & "openNURBS",
                                  Path.Combine(_path, "Case.Subs.OpenNURBS.dll"),
                                  "Case.Subs.OpenNURBS.Entry.D4E12C09AFB84891A24F3AF186B13DD3",
                                  "Case.AppsRibbon.OpenNURBS_16.png",
                                  "Case.AppsRibbon.OpenNURBS_32.png",
                                  "Import openNURBS into Revit",
                                  "",
                                  "subsc")
                If Not m_pbd Is Nothing Then m_subscTools("misc").Add(m_pbd)
                m_pbd = GetPushButtonData("Case.Subs.RoomsToMass",
                                  "3D Mass" & vbCr & "from Rooms",
                                  Path.Combine(_path, "Case.Subs.RoomsToMass.dll"),
                                  "Case.Subs.RoomsToMass.Entry.AF73BA45B4B94C7585CB3E425D423F5E",
                                  "Case.AppsRibbon.Rm3d_16.png",
                                  "Case.AppsRibbon.Rm3d_32.png",
                                  "Generate Mass Families from Rooms",
                                  "",
                                  "subsc")
                If Not m_pbd Is Nothing Then m_subscTools("misc").Add(m_pbd)

                ' Add to Ribbon
                If _toolsExistsSubsc = True Then
                    AddAsStackedAndButtons(CTabNamePro, "Tools", m_subscTools)
                Else
                    PostToLog(String.Format("LoadItemsTwo General Failure"))
                End If

            Catch ex As Exception
                PostToLog(String.Format("LoadItemsTwo Exception:" & vbCr & ex.ToString()))
            End Try

        End Sub

#End Region

#Region "Private Members - Buttons (Apps1)"

        ''' <summary>
        ''' Load the Free Items 
        ''' </summary>
        ''' <remarks></remarks>
        Private Function LoadItemsOne()

            Try
                ' First Create the Tab
                _uiApp.CreateRibbonTab(CTabNameFree)
            Catch
            End Try

            Try

                ' List of Stacked Button Tools
                Dim m_freeStuff As New Dictionary(Of String, List(Of PushButtonData))
                Dim m_pbd As PushButtonData

                ' Groups
                m_freeStuff.Add("views", New List(Of PushButtonData))
                m_freeStuff.Add("report", New List(Of PushButtonData))
                m_freeStuff.Add("data", New List(Of PushButtonData))
                m_freeStuff.Add("manage", New List(Of PushButtonData))
                m_freeStuff.Add("model", New List(Of PushButtonData))

                ' Delete Views and Links
                m_pbd = GetPushButtonData("Case.DeleteViewsAndPurge",
                                  "Delete" & vbCr & "Views & Links",
                                  Path.Combine(_path, "Case.DeleteViewsAndPurge.dll"),
                                  "Case.DeleteViewsAndPurge.Entry.CmdMain",
                                  "Case.AppsRibbon.DeleteViews_16.png",
                                  "Case.AppsRibbon.DeleteViews_32.png",
                                  "Delete Sheets, Views and Revit Links",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("views").Add(m_pbd)

                ' View Templates
                m_pbd = GetPushButtonData("Case.ViewTemplates",
                                  "View" & vbCr & "Templates",
                                  Path.Combine(_path, "Case.ViewTemplates.dll"),
                                  "Case.ViewTemplates.Entry.CmdMain",
                                  "Case.AppsRibbon.ViewTemplate_16.png",
                                  "Case.AppsRibbon.ViewTemplate_32.png",
                                  "View Template Controller... Drag and Drop from within a Tree View",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("views").Add(m_pbd)

                ' Images to Drafting Views
                m_pbd = GetPushButtonData("Case.ImageToDraftingView",
                                  "Images to" & vbCr & "Drafting Views",
                                  Path.Combine(_path, "Case.ImageToDraftingView.dll"),
                                  "Case.ImageToDraftingView.Entry.CmdMain",
                                  "Case.AppsRibbon.Images_16.png",
                                  "Case.AppsRibbon.Images_32.png",
                                  "Import Images to Drafting Views",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("views").Add(m_pbd)

                ' View Duplicator
                m_pbd = GetPushButtonData("Case.MultiViewDuplicate",
                                  "View" & vbCr & "Duplicator",
                                  Path.Combine(_path, "Case.MultiViewDuplicate.dll"),
                                  "Case.MultiViewDuplicate.Entry.CmdMain",
                                  "Case.AppsRibbon.Duplicate_16.png",
                                  "Case.AppsRibbon.Duplicate_32.png",
                                  "Multiple View Duplication...",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("views").Add(m_pbd)

                ' View Creator
                m_pbd = GetPushButtonData("Case.ViewCreator",
                                  "View" & vbCr & "Creator",
                                  Path.Combine(_path, "Case.ViewCreator.dll"),
                                  "Case.ViewCreator.Entry.CmdMain",
                                  "Case.AppsRibbon.ViewCreator_16.png",
                                  "Case.AppsRibbon.ViewCreator_32.png",
                                  "Create New Views",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("views").Add(m_pbd)

                ' Export Object Styles
                m_pbd = GetPushButtonData("Case.ObjectStyles",
                                  "Export" & vbCr & "Styles",
                                  Path.Combine(_path, "Case.ObjectStyles.dll"),
                                  "Case.ObjectStyles.Entry.CmdMain",
                                  "Case.AppsRibbon.objstyle_16.png",
                                  "Case.AppsRibbon.objstyle_32.png",
                                  "Export object styles to txt file...",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("report").Add(m_pbd)

                ' Basic Reporting
                m_pbd = GetPushButtonData("Case.BasicReporting",
                                  "Basic" & vbCr & "Reporting",
                                  Path.Combine(_path, "Case.BasicReporting.dll"),
                                  "Case.BasicReporting.Entry.CmdMain",
                                  "Case.AppsRibbon.BasicReporting_16.png",
                                  "Case.AppsRibbon.BasicReporting_32.png",
                                  "Export formula and hosting data",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("report").Add(m_pbd)

                ' External Wall Direction
                m_pbd = GetPushButtonData("Case.Directionality",
                                  "External Wall" & vbCr & "Direction",
                                  Path.Combine(_path, "Case.Directionality.dll"),
                                  "Case.Directionality.Entry.CmdMain",
                                  "Case.AppsRibbon.WallDir_16.png",
                                  "Case.AppsRibbon.WallDir_32.png",
                                  "Record the facing direction for External Walls",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("report").Add(m_pbd)

                ' Report Groups by View
                m_pbd = GetPushButtonData("Case.ReportGroupsByView",
                                  "Report Groups" & vbCr & "By View",
                                  Path.Combine(_path, "Case.ReportGroupsByView.dll"),
                                  "Case.ReportGroupsByView.Entry.CmdMain",
                                  "Case.AppsRibbon.report_groups_16.png",
                                  "Case.AppsRibbon.report_groups_32.png",
                                  "Report to CSV file all group instances by the views they are visible...",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("report").Add(m_pbd)

                ' Revision Cloud Reporting
                m_pbd = GetPushButtonData("Case.RevClouds",
                                  "Revision Cloud" & vbCr & "Reporting",
                                  Path.Combine(_path, "Case.RevClouds.dll"),
                                  "Case.RevClouds.Entry.CmdMain",
                                  "Case.AppsRibbon.RevCloud_16.png",
                                  "Case.AppsRibbon.RevCloud_32.png",
                                  "Export Revision Cloud Data to TXT File",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("report").Add(m_pbd)

                ' Viewport Reporting
                m_pbd = GetPushButtonData("Case.ViewportReporting",
                                  "Viewport" & vbCr & "Reporting",
                                  Path.Combine(_path, "Case.ViewportReporting.dll"),
                                  "Case.ViewportReporting.Entry.CmdMain",
                                  "Case.AppsRibbon.report_viewport_16.png",
                                  "Case.AppsRibbon.report_viewport_32.png",
                                  "Report what views are where on what sheets to txt file...",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("report").Add(m_pbd)

                ' MEP Sys. Orientation
                m_pbd = GetPushButtonData("Case.ApplySysOrient",
                                  "MEP Sys." & vbCr & "Orientation",
                                  Path.Combine(_path, "Case.ApplySysOrient.dll"),
                                  "Case.ApplySysOrient.Entry.CmdMain",
                                  "Case.AppsRibbon.pipe_16.png",
                                  "Case.AppsRibbon.pipe_32.png",
                                  "Record orientation for pipe, duct, conduit, and cable tray pieces (vertical, horizontal or sloped)",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("report").Add(m_pbd)

                ' Param to Param
                m_pbd = GetPushButtonData("Case.HiddenParameterToParameter",
                                  "Param to" & vbCr & "Param",
                                  Path.Combine(_path, "Case.HiddenParameterToParameter.dll"),
                                  "Case.HiddenParameterToParameter.Entry.CmdMain",
                                  "Case.AppsRibbon.ParamParam_16.png",
                                  "Case.AppsRibbon.ParamParam_32.png",
                                  "Slide data from one parameter to another within a selected category and parameter selection...",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("data").Add(m_pbd)

                ' Rail Length to Param
                m_pbd = GetPushButtonData("Case.RailingLengthToParameter",
                                  "Rail Length" & vbCr & "to Param",
                                  Path.Combine(_path, "Case.RailingLengthToParameter.dll"),
                                  "Case.RailingLengthToParameter.Entry.CmdMain",
                                  "Case.AppsRibbon.ParamParam_16.png",
                                  "Case.AppsRibbon.ParamParam_32.png",
                                  "Send the length of railings to a specified parameter of your choosing for tagging Egress",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("data").Add(m_pbd)

                ' Change/Replace Line Styles
                m_pbd = GetPushButtonData("Case.RoomSync",
                                  "Sync Linked" & vbCr & "Model Rooms",
                                  Path.Combine(_path, "Case.RoomSync.dll"),
                                  "Case.RoomSync.Entry.CmdMain",
                                  "Case.AppsRibbon.roomsync_16.png",
                                  "Case.AppsRibbon.roomsync_32.png",
                                  "Grab missing rooms and sync data for existing rooms from a linked model...",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("manage").Add(m_pbd)

                ' Change/Replace Line Styles
                m_pbd = GetPushButtonData("Case.UngroupAll",
                                  "Ungroup All",
                                  Path.Combine(_path, "Case.UngroupAll.dll"),
                                  "Case.UngroupAll.Entry.CmdMain",
                                  "Case.AppsRibbon.ungroupall_16.png",
                                  "Case.AppsRibbon.ungroupall_32.png",
                                  "Ungroup all groups in model",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("manage").Add(m_pbd)

                ' Change/Replace Line Styles
                m_pbd = GetPushButtonData("Case.ChangeLineStyles",
                                  "Change/Replace" & vbCr & "Line Styles",
                                  Path.Combine(_path, "Case.ChangeLineStyles.dll"),
                                  "Case.ChangeLineStyles.Entry.CmdMain",
                                  "Case.AppsRibbon.LineStyles_16.png",
                                  "Case.AppsRibbon.LineStyles_32.png",
                                  "Change and Replace Line Styles",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("manage").Add(m_pbd)

                ' Change/Replace Names
                m_pbd = GetPushButtonData("Case.ChangeReplaceFamTypeNames",
                                  "Change/Replace" & vbCr & "Names",
                                  Path.Combine(_path, "Case.ChangeReplaceFamTypeNames.dll"),
                                  "Case.ChangeReplaceFamTypeNames.Entry.CmdMain",
                                  "Case.AppsRibbon.Rename_16.png",
                                  "Case.AppsRibbon.Rename_32.png",
                                  "Change and replace text in family and/or type names",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("manage").Add(m_pbd)

                ' Door Mark Renumbering
                m_pbd = GetPushButtonData("Case.DoorMarkRenumber",
                                  "Door Mark" & vbCr & "Renumbering",
                                  Path.Combine(_path, "Case.DoorMarkRenumber.dll"),
                                  "Case.DoorMarkRenumber.Entry.CmdMain",
                                  "Case.AppsRibbon.renumber_16.png",
                                  "Case.AppsRibbon.renumber_32.png",
                                  "Renumber Door Marks by Adjacent Room Numbers",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("manage").Add(m_pbd)

                ' Export Families to RFA
                m_pbd = GetPushButtonData("Case.Export.Families",
                                  "Export Families" & vbCr & "to RFA",
                                  Path.Combine(_path, "Case.Export.Families.dll"),
                                  "Case.Export.Families.Entry.CmdMain",
                                  "Case.AppsRibbon.Fams_16.png",
                                  "Case.AppsRibbon.Fams_32.png",
                                  "Export Component Families to RFA",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("manage").Add(m_pbd)

                ' Room Insertion Points
                m_pbd = GetPushButtonData("Case.RoomInsertionPoint",
                                  "Room" & vbCr & "Insertion Points",
                                  Path.Combine(_path, "Case.RoomInsertionPoint.dll"),
                                  "Case.RoomInsertionPoint.Entry.CmdMain",
                                  "Case.AppsRibbon.point_16.png",
                                  "Case.AppsRibbon.point_32.png",
                                  "Move space and room insertion points to their calculated centroid...",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("manage").Add(m_pbd)

                ' Selection Sets
                m_pbd = GetPushButtonData("Case.SelectionSets",
                                  "Selection" & vbCr & "Sets",
                                  Path.Combine(_path, "Case.SelectionSets.dll"),
                                  "Case.SelectionSets.Entry.CmdMain",
                                  "Case.AppsRibbon.Sel_16.png",
                                  "Case.AppsRibbon.Sel_32.png",
                                  "Share and/or Apply Externally Saved Selection Sets",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("manage").Add(m_pbd)

                ' Param Loader
                m_pbd = GetPushButtonData("Case.SharedParameters",
                                  "Param" & vbCr & "Loader",
                                  Path.Combine(_path, "Case.SharedParameters.dll"),
                                  "Case.SharedParameters.Entry.CmdMain",
                                  "Case.AppsRibbon.SharedParam_16.png",
                                  "Case.AppsRibbon.SharedParam_32.png",
                                  "An improved tool for loading Shared Parameters from a file",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("manage").Add(m_pbd)

                ' 3D Mass from Rooms
                m_pbd = GetPushButtonData("Case.ExtrudeRoomsToMass",
                                  "3D Mass" & vbCr & "from Rooms",
                                  Path.Combine(_path, "Case.ExtrudeRoomsToMass.dll"),
                                  "Case.ExtrudeRoomsToMass.Entry.CmdMain",
                                  "Case.AppsRibbon.Rm3d_16.png",
                                  "Case.AppsRibbon.Rm3d_32.png",
                                  "Generate Mass Families from Rooms",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("model").Add(m_pbd)

                ' Wall Parallize
                m_pbd = GetPushButtonData("Case.ParallelWalls",
                                  "Parallel" & vbCr & "Walls",
                                  Path.Combine(_path, "Case.ParallelWalls.dll"),
                                  "Case.ParallelWalls.Entry.CmdMain",
                                  "Case.AppsRibbon.parallelwalls_16.png",
                                  "Case.AppsRibbon.parallelwalls_32.png",
                                  "Fix wall angles by making them parallel to another...",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("model").Add(m_pbd)

                ' Lighting Layouts
                m_pbd = GetPushButtonData("Case.LightingLayout",
                                  "Lighting" & vbCr & "Layout",
                                  Path.Combine(_path, "Case.LightingLayout.dll"),
                                  "Case.LightingLayout.Entry.CmdMain",
                                  "Case.AppsRibbon.light_16.png",
                                  "Case.AppsRibbon.light_32.png",
                                  "Lighting Layouts by Room...",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("model").Add(m_pbd)

                ' 3D Room Tags
                m_pbd = GetPushButtonData("Case.ModeledRoomTags",
                                  "3D" & vbCr & "Room Tags",
                                  Path.Combine(_path, "Case.ModeledRoomTags.dll"),
                                  "Case.ModeledRoomTags.Entry.CmdMain",
                                  "Case.AppsRibbon.RoomTags_16.png",
                                  "Case.AppsRibbon.RoomTags_32.png",
                                  "Place 3D Room Tags at Centroid for All Rooms...",
                                  "",
                                  "free")
                If Not m_pbd Is Nothing Then m_freeStuff("model").Add(m_pbd)

                ' Add to Ribbon
                If _toolsExistsFree = True Then
                    AddAsStackedAndButtons(CTabNameFree, "CASE Free Apps", m_freeStuff)
                Else
                    PostToLog(String.Format("LoadItemsOne General Failure"))
                End If

                ' Success
                Return True
            Catch ex As Exception
                PostToLog(String.Format("LoadItemsOne Exception:" & vbCr & ex.ToString()))
            End Try

            ' Failure
            Return False

        End Function

#End Region

    End Class
End Namespace
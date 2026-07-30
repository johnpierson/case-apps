'' ''Imports Autodesk.Revit.ApplicationServices
'' ''Imports Autodesk.Revit.Attributes
'' ''Imports Autodesk.Revit.UI
'' ''Imports System.Windows.Media.Imaging
'' ''Imports System.IO
'' ''Imports System.Reflection

'' ''<Transaction(TransactionMode.Manual)>
'' ''Class app

'' ''  Implements IExternalApplication

'' ''  Private _path As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location)
'' ''  Private _uiApp As UIControlledApplication

'' ''  ''' <summary>
'' ''  ''' Add the Ribbon Item and Panel
'' ''  ''' </summary>
'' ''  ''' <param name="a"></param>
'' ''  ''' <remarks></remarks>
'' ''  Public Sub AddRibbonPanel(ByVal a As UIControlledApplication)

'' ''    ' The Execution Path
'' ''    Dim m_Path As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location) & "\"

'' ''    Try
'' ''      ' First Create the Tab
'' ''      a.CreateRibbonTab("CASE")
'' ''    Catch ex As Exception
'' ''      ' Might already exist...
'' ''    End Try

'' ''    ' The Ribbon Panel
'' ''    Dim m_RibbonPanel As RibbonPanel = Nothing
'' ''    Try
'' ''      m_RibbonPanel = _uiApp.CreateRibbonPanel("CASE", "CASE Point Management")
'' ''    Catch ex As Exception

'' ''    End Try

'' ''    Try ' Add a Stacked Button Pair for Links
'' ''      Dim m_pb_stack1 As New PushButtonData("ExportPts",
'' ''                                            "Report Points",
'' ''                                            m_Path & "CASE.PointManagement.dll",
'' ''                                            "CASE.PointManagement.cmdReporting")
'' ''      With m_pb_stack1
'' ''        '.Image = New BitmapImage(New Uri(m_Path & "CASE.PointManagement.png"))
'' ''        '.LargeImage = New BitmapImage(New Uri(m_Path & "CASE.PointManagement.png"))
'' ''        .ToolTip = "Report Points for Elements"
'' ''      End With
'' ''      Dim m_pb_stack2 As New PushButtonData("TagViews",
'' ''                                            "Tag Multi Views",
'' ''                                            m_Path & "CASE.PointManagement.dll",
'' ''                                            "CASE.PointManagement.cmdTagging")
'' ''      With m_pb_stack2
'' ''        '.Image = New BitmapImage(New Uri(m_Path & "CASE.PointManagement.png"))
'' ''        '.LargeImage = New BitmapImage(New Uri(m_Path & "CASE.PointManagement.png"))
'' ''        .ToolTip = "Tag Families in Multiple Selected Views"
'' ''      End With
'' ''      ' Add it to the Ribbon
'' ''      m_RibbonPanel.AddStackedItems(m_pb_stack1, m_pb_stack2)

'' ''    Catch ex As Exception

'' ''    End Try

'' ''    ' Add a Separator
'' ''    m_RibbonPanel.AddSeparator()

'' ''    Try ' Add a Stacked Button Pair for Links
'' ''      Dim m_pb_stack1 As New PushButtonData("DataManager",
'' ''                                            "Data Manager",
'' ''                                            m_Path & "CASE.PointManagement.dll",
'' ''                                            "CASE.PointManagement.cmdDataManager")
'' ''      With m_pb_stack1
'' ''        '.Image = New BitmapImage(New Uri(m_Path & "CASE.PointManagement.png"))
'' ''        '.LargeImage = New BitmapImage(New Uri(m_Path & "CASE.PointManagement.png"))
'' ''        .ToolTip = "Manage Data from External File"
'' ''      End With
'' ''      Dim m_pb_stack2 As New PushButtonData("PlaceFromFile",
'' ''                                            "Points From File",
'' ''                                            m_Path & "CASE.PointManagement.dll",
'' ''                                            "CASE.PointManagement.cmdPlacePointsFromFile")
'' ''      With m_pb_stack2
'' ''        '.Image = New BitmapImage(New Uri(m_Path & "CASE.PointManagement.png"))
'' ''        '.LargeImage = New BitmapImage(New Uri(m_Path & "CASE.PointManagement.png"))
'' ''        .ToolTip = "Place Points from External File"
'' ''      End With
'' ''      ' Add it to the Ribbon
'' ''      m_RibbonPanel.AddStackedItems(m_pb_stack1, m_pb_stack2)

'' ''    Catch ex As Exception

'' ''    End Try

'' ''    ' Add a Separator
'' ''    m_RibbonPanel.AddSeparator()

'' ''    Try ' Add a Stacked Button Pair for Links
'' ''      Dim m_pb_stack1 As New PushButtonData("ParamManager",
'' ''                                            "Parameter Manager",
'' ''                                            m_Path & "CASE.PointManagement.dll",
'' ''                                            "CASE.PointManagement.cmdManageParameters")
'' ''      With m_pb_stack1
'' ''        '.Image = New BitmapImage(New Uri(m_Path & "CASE.PointManagement.png"))
'' ''        '.LargeImage = New BitmapImage(New Uri(m_Path & "CASE.PointManagement.png"))
'' ''        .ToolTip = "Manage PNEZD Parameter Assignments by Category"
'' ''      End With
'' ''      Dim m_pb_stack2 As New PushButtonData("PlacePoint",
'' ''                                            "Manual Points",
'' ''                                            m_Path & "CASE.PointManagement.dll",
'' ''                                            "CASE.PointManagement.cmdPlacePoints")
'' ''      With m_pb_stack2
'' ''        '.Image = New BitmapImage(New Uri(m_Path & "CASE.PointManagement.png"))
'' ''        '.LargeImage = New BitmapImage(New Uri(m_Path & "CASE.PointManagement.png"))
'' ''        .ToolTip = "Place points by base points of family or categories. Place by XYZ or Selected point."
'' ''      End With
'' ''      ' Add it to the Ribbon
'' ''      m_RibbonPanel.AddStackedItems(m_pb_stack1, m_pb_stack2)

'' ''    Catch ex As Exception

'' ''    End Try


'' ''  End Sub

'' ''  ''' <summary>
'' ''  ''' Add a button to a Ribbon Tab
'' ''  ''' </summary>
'' ''  ''' <param name="Rpanel">The name of the ribbon panel</param>
'' ''  ''' <param name="ButtonName">The Name of the Button</param>
'' ''  ''' <param name="ButtonText">Command Text</param>
'' ''  ''' <param name="ImagePath16">Small Image</param>
'' ''  ''' <param name="ImagePath32">Large Image</param>
'' ''  ''' <param name="dllPath">Path to the DLL file</param>
'' ''  ''' <param name="dllClass">Full qualified class descriptor</param>
'' ''  ''' <param name="Tooltip">Tooltip to add to the button</param>
'' ''  ''' <returns></returns>
'' ''  ''' <remarks></remarks>
'' ''  Private Function AddButton(ByVal Rpanel As String,
'' ''                             ByVal ButtonName As String,
'' ''                             ByVal ButtonText As String,
'' ''                             ByVal ImagePath16 As String,
'' ''                             ByVal ImagePath32 As String,
'' ''                             ByVal dllPath As String,
'' ''                             ByVal dllClass As String,
'' ''                             ByVal Tooltip As String) As Boolean
'' ''    Try
'' ''      ' The Ribbon Panel
'' ''      Dim m_RibbonPanel As RibbonPanel = Nothing

'' ''      ' Find the Panel within the Case Tab
'' ''      Dim m_RP As New List(Of RibbonPanel)
'' ''      m_RP = _uiApp.GetRibbonPanels("CASE")
'' ''      For Each x As RibbonPanel In m_RP
'' ''        If x.Name.ToUpper = Rpanel.ToUpper Then
'' ''          m_RibbonPanel = x
'' ''        End If
'' ''      Next

'' ''      ' Create the Panel if it doesn't Exist
'' ''      If m_RibbonPanel Is Nothing Then
'' ''        m_RibbonPanel = _uiApp.CreateRibbonPanel("CASE", Rpanel)
'' ''      End If

'' ''      ' Create the Pushbutton Data
'' ''      Dim m_PushButtonData As New PushButtonData(ButtonName, ButtonText, dllPath, dllClass)
'' ''      If ImagePath16 <> "" Then
'' ''        m_PushButtonData.Image = New BitmapImage(New Uri(ImagePath16))
'' ''      End If
'' ''      If ImagePath32 <> "" Then
'' ''        m_PushButtonData.LargeImage = New BitmapImage(New Uri(ImagePath32))
'' ''      End If
'' ''      m_PushButtonData.ToolTip = Tooltip

'' ''      ' Add the button to the tab
'' ''      Dim m_PushButtonData_Add As PushButton = m_RibbonPanel.AddItem(m_PushButtonData)
'' ''    Catch
'' ''    End Try
'' ''    Return True
'' ''  End Function

'' ''  ''' <summary>
'' ''  ''' Implement the external application
'' ''  ''' </summary>
'' ''  ''' <param name="a"></param>
'' ''  ''' <returns></returns>
'' ''  ''' <remarks></remarks>
'' ''  Public Function OnStartup(ByVal a As UIControlledApplication) As Result Implements IExternalApplication.OnStartup
'' ''    ' Load the Ribbon Item
'' ''    Try
'' ''      ' The Shared uiApp variable
'' ''      _uiApp = a
'' ''      ' Add the Ribbon Panel!!
'' ''      AddRibbonPanel(a)
'' ''      ' Return Success
'' ''      Return Result.Succeeded
'' ''    Catch
'' ''      Return Result.Failed
'' ''    End Try
'' ''  End Function

'' ''  ''' <summary>
'' ''  ''' Implement the external application
'' ''  ''' </summary>
'' ''  ''' <param name="a"></param>
'' ''  ''' <returns></returns>
'' ''  ''' <remarks></remarks>
'' ''  Public Function OnShutdown(ByVal a As UIControlledApplication) As Result Implements IExternalApplication.OnShutdown
'' ''    ' Return Success
'' ''    Return Result.Succeeded
'' ''  End Function

'' ''End Class
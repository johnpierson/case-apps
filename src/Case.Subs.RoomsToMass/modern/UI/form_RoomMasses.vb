Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].Subs.RoomsToMass.Data
Imports Newtonsoft.Json

Public Class form_RoomMasses

    Private _s As clsSettings
    Private _appCreate As Autodesk.Revit.Creation.Application
    Private _stopWatches As New List(Of Stopwatch)
    Private _famFactory As Autodesk.Revit.Creation.FamilyItemFactory
    Private _path As String = ""
    Private _checked As Boolean = False
    Private _iMasses As Integer = 0

    ''' <summary>
    ''' General Constructor 
    ''' </summary>
    ''' <param name="Settings">Settings Object</param>
    ''' <remarks></remarks>
    Public Sub New(Settings As clsSettings)
        InitializeComponent()

        ' Widen Scope
        _s = Settings

    End Sub

#Region "Private Members"

    ''' <summary>
    ''' Generate masses for rooms 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GenerateMasses()

        ' Reset Count
        _iMasses = 0

        ' Prime the Progressbar
        With Me.ProgressBar1
            .Minimum = 0
            .Value = 0
            .Maximum = _s.Rooms.Count
        End With

        ' Purge Unused?
        If Me.CheckBoxPurge.Checked = True Then
            Try
                If _s.DeleteOldMassInstances = True Then
                    _s.DeleteOldMassTypes()
                End If
            Catch
            End Try
        End If

        ' Save Files?
        If Me.CheckBoxSaveMass.Checked = True Then

            ' Get a Path
            If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then

                ' Was a Path Returned
                If Not String.IsNullOrEmpty(FolderBrowserDialog1.SelectedPath) Then

                    ' Save the Path
                    _path = Me.FolderBrowserDialog1.SelectedPath & "\"

                Else

                    ' Failure Message
                    MsgBox("Invalid or Missing Path, Terminating", MsgBoxStyle.Critical, ":(")
                    Exit Sub

                End If

            End If

        End If

        ' Close all Families
        _s.CloseAllFamilyDocuments()

        ' Random Object - Material Color
        Dim m_r As New Random

        ' Iterate the Rooms
        For Each x As clsRoomFamily In Me.DataGridViewRooms.DataSource

            Try
                ' Step the Progress
                Me.ProgressBar1.Increment(1)
            Catch
            End Try

            ' Is it Checked
            If Not x.IsChecked = True Then Continue For

            ' Create a new Family
            _s.FamilyDoc = _s.UiApp.Application.NewFamilyDocument(_s.FamilyTemplatePath)

            ' Start a new Family Transaction
            Using t As New Transaction(_s.FamilyDoc, "New Mass Family Document")
                If t.Start() Then

                    Try

                        ' Subcategory Named by Parameter Value
                        Dim m_Subcategory As Category = Nothing

                        ' Get the Material Parameter Value
                        Dim m_SubCatName As String = "Mass_Rooms"
                        Dim m_valueName As String = ""
                        Try
                            Dim m_mapper As clsParameterDescription = Me.ComboBoxMaterial.SelectedItem
                            Dim m_materialValuePara As clsPara = New clsPara(x.RoomElement.LookupParameter(m_mapper.Name))
                            m_valueName = m_materialValuePara.Value.ToString
                        Catch
                        End Try

                        If Not String.IsNullOrEmpty(m_valueName) Then
                            m_SubCatName = "Mass_Rooms_" & m_valueName
                        End If

                        ' Get the subcategory
                        Dim m_NameMap As CategoryNameMap = _s.Familycategory.SubCategories
                        For Each x1 As Category In m_NameMap
                            If x1.Name.ToLower = m_SubCatName.ToLower Then
                                m_Subcategory = x1
                                Exit For
                            End If
                        Next

                        ' Create if Missing
                        If m_Subcategory Is Nothing Then

                            Try

                                ' Create the subcategory
                                m_Subcategory = _s.FamilyDoc.Settings.Categories.NewSubcategory(_s.Familycategory, m_SubCatName)

                            Catch
                            End Try

                        End If

                        ' Material named by Parameter Value
                        Dim m_Material As Material = Nothing
                        Dim m_colMat As New FilteredElementCollector(_s.FamilyDoc)
                        m_colMat.OfCategory(BuiltInCategory.OST_Materials)

                        ' Find the Material by Name
                        For Each m As Material In m_colMat.ToElements
                            If m.Name = m_SubCatName Then
                                m_Material = m
                                Exit For
                            End If
                        Next

                        ' Create the Material if Not Found
                        If m_Material Is Nothing Then

                            Try

                                ' Name as Subcategory
                                Dim m_matid As ElementId = Material.Create(_s.FamilyDoc, m_SubCatName)
                                m_Material = TryCast(_s.FamilyDoc.GetElement(m_matid), Material)

                                ' Random Color
                                Dim m_color As New Color(m_r.Next(1, 255), m_r.Next(1, 255), m_r.Next(1, 255))

                                ' Apply the Color to the Material
                                m_Material.Color = m_color

                                ' Set Transparency
                                m_Material.Transparency = Me.NumericUpDown1.Value

                                ' Assign the Material to the Subcategory
                                m_Subcategory.Material = m_Material

                            Catch
                            End Try

                        End If

                        ' Apply the material to the subcategory
                        m_Subcategory.Material = m_Material

                        ' What are the upp bounds or height of the room?
                        Dim m_RmHeight As Double = x.RoomElement.UnboundedHeight
                        Dim m_RmElev As Double = x.RoomElement.Level.Elevation
                        If m_RmHeight < 1 Then m_RmHeight = 8
                        Dim m_RmUpLevel As Level = x.RoomElement.UpperLimit

                        ' Get the room boundary data
                        Dim m_Boundary As IList(Of IList(Of BoundarySegment)) =
                                    x.RoomElement.GetBoundarySegments(New SpatialElementBoundaryOptions)

                        ' Make Sure we Have Something
                        If m_Boundary Is Nothing Then Continue For

                        ' Flat Sketchplane - Solid
                        Dim m_plane As Plane = Plane.CreateByNormalAndOrigin(New XYZ(0, 0, 1), New XYZ(0, 0, m_RmElev))
                        Dim m_skplane As SketchPlane = SketchPlane.Create(_s.FamilyDoc, m_plane)

                        ' The extrusion form direction
                        Dim m_dir As New XYZ(0, 0, m_RmHeight)
                        Dim m_dirVoid As New XYZ(0, 0, -m_RmHeight)

                        ' Iterate to gather the reference array objects
                        For i = 0 To m_Boundary.Count - 1

                            ' The Reference Array - Moved
                            Dim m_refArray As New ReferenceArray

                            Dim il As IList(Of BoundarySegment) = m_Boundary(i)

                            ' Segments Array
                            For ii = 0 To il.Count - 1

                                Dim m_Seg As BoundarySegment = il.Item(ii)

                                ' Get the Model Curve
                                Dim m_mc As ModelCurve = Nothing

                                If i = 0 Then

                                    ' Curve is Line
                                    If TypeOf m_Seg.GetCurve() Is Line Then
                                        Dim m_cLine As Line = TryCast(m_Seg.GetCurve(), Line)
                                        m_mc = _s.FamilyDoc.FamilyCreate.NewModelCurve(m_cLine, m_skplane)
                                    End If

                                    ' Curve is Arc
                                    If TypeOf m_Seg.GetCurve() Is Arc Then
                                        Dim m_cLine As Arc = TryCast(m_Seg.GetCurve(), Arc)
                                        m_mc = _s.FamilyDoc.FamilyCreate.NewModelCurve(m_cLine, m_skplane)
                                    End If

                                    ' Curve is NurbSpline
                                    If TypeOf m_Seg.GetCurve() Is NurbSpline Then
                                        Dim m_cLine As NurbSpline = TryCast(m_Seg.GetCurve(), NurbSpline)
                                        m_mc = _s.FamilyDoc.FamilyCreate.NewModelCurve(m_cLine, m_skplane)
                                    End If

                                Else

                                    ' Recreate All > 0

                                    ' Curve is Line
                                    If TypeOf m_Seg.GetCurve() Is Line Then
                                        ' New Curve
                                        Dim m_cLine As Line = m_Seg.GetCurve().Clone
                                        m_mc = _s.FamilyDoc.FamilyCreate.NewModelCurve(m_cLine, m_skplane)
                                    End If

                                    ' Curve is Arc
                                    If TypeOf m_Seg.GetCurve() Is Arc Then
                                        ' New Curve
                                        Dim m_cLine As Arc = m_Seg.GetCurve().Clone
                                        m_mc = _s.FamilyDoc.FamilyCreate.NewModelCurve(m_cLine, m_skplane)
                                    End If

                                    ' Curve is NurbSpline
                                    If TypeOf m_Seg.GetCurve() Is NurbSpline Then
                                        ' New Curve
                                        Dim m_cLine As NurbSpline = m_Seg.GetCurve().Clone
                                        m_mc = _s.FamilyDoc.FamilyCreate.NewModelCurve(m_cLine, m_skplane)
                                    End If

                                End If

                                Try
                                    m_refArray.Append(m_mc.GeometryCurve.Reference)
                                Catch
                                End Try

                            Next

                            Try

                                ' First is Solid, Second and Beyond is Void
                                If i = 0 Then

                                    ' Set Extrusion Form
                                    Dim m_form As Autodesk.Revit.DB.Form = _s.FamilyDoc.FamilyCreate.NewExtrusionForm(True, m_refArray, m_dir)
                                    m_form.Subcategory = m_Subcategory

                                Else

                                    ' Void
                                    Dim m_form As Autodesk.Revit.DB.Form = _s.FamilyDoc.FamilyCreate.NewExtrusionForm(False, m_refArray, m_dir)
                                    m_form.Subcategory = m_Subcategory

                                End If

                                _s.FamilyDoc.Regenerate()

                            Catch
                            End Try

                        Next

                        ' Commit the Family Transaction
                        t.Commit()

                    Catch exFam As Exception

                        ' Commit the Family Transaction
                        t.RollBack()

                        ' Cannot Continue to Place Mass Family
                        Continue For

                    End Try

                End If

            End Using ' Family Transaction

            ' Family Reference
            Dim m_family As Family = Nothing

            ' Start a new Model Transaction
            Using t As New Transaction(_s.Doc, "Extrude Rooms to Masses")
                If t.Start() Then

                    Dim m_FamilyFilePath As String = ""

                    ' Load from Family to Doc or File to Doc
                    If Me.CheckBoxSaveMass.Checked = True Then

                        ' From Permanent File
                        m_FamilyFilePath = _path & x.RoomElement.UniqueId.ToString & ".rfa"

                    Else

                        ' From Temp File
                        m_FamilyFilePath = Path.GetTempPath & x.RoomElement.UniqueId.ToString & ".rfa"

                    End If

                    ' Save and Close
                    Dim m_opt As New SaveAsOptions
                    m_opt.OverwriteExistingFile = True
                    _s.FamilyDoc.SaveAs(m_FamilyFilePath, m_opt)
                    _s.FamilyDoc.Close()

                    ' Load the File
                    _s.Doc.LoadFamily(m_FamilyFilePath,
                                      New clsFamOptions,
                                      m_family)

                    ' Get the Default Symbol from the Family
                    For Each fs In m_family.GetFamilySymbolIds()

                        Dim m_famSymbol As FamilySymbol = TryCast(_s.Doc.GetElement(fs), FamilySymbol)

                        ' Place the Family at 0,0,0 
                        ' If Not placed yet, place it

                        ' Find the Element
                        Dim m_col As New FilteredElementCollector(_s.Doc)
                        m_col.WhereElementIsNotElementType()
                        m_col.OfCategory(BuiltInCategory.OST_Mass)
                        Dim m_foundItem As Boolean = False
                        For Each e In m_col.ToElements
                            If e.Name.ToLower.StartsWith(x.RoomElement.UniqueId.ToString.ToLower) Then
                                m_foundItem = True
                            End If
                        Next

                        ' Check if family symbol is active, if not activate it. Since Revit 2014.
                        If Not m_famSymbol.IsActive Then
                            m_famSymbol.Activate()
                        End If
                        ' Only Place if Not In Place Already
                        If m_foundItem = False Then
                            Dim m_finst As FamilyInstance = _s.Doc.Create.NewFamilyInstance(New XYZ(0, 0, 0),
                                                                                            m_famSymbol,
                                                                                            [Structure].StructuralType.NonStructural)

                            ' Count if Placed
                            If Not m_finst Is Nothing Then _iMasses += 1

                        End If

                        ' Delete if Temp
                        If Not CheckBoxSaveMass.Checked = True Then

                            Try
                                ' Delete the Temp File
                                File.Delete(m_FamilyFilePath)
                            Catch
                            End Try
                        End If

                    Next

                    ' Commit 
                    t.Commit()

                    ' Close all Families
                    _s.CloseAllFamilyDocuments()

                End If

            End Using

        Next

    End Sub

    ''' <summary>
    ''' Load List of Parameters to Use as Material Targets
    ''' Default is Department
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadRoomParameters()

        Try

            ' Load them into the Control
            For Each x In _s.RoomParameters.Values
                Me.ComboBoxMaterial.Items.Add(x)
            Next
            Me.ComboBoxMaterial.DisplayMember = "Name"

        Catch
        End Try

        Try

            ' Set Default to Department
            For Each x As clsParameterDescription In Me.ComboBoxMaterial.Items
                If x.Name = "Department" Then
                    Me.ComboBoxMaterial.SelectedItem = x
                    Exit For
                End If
            Next

        Catch
        End Try

    End Sub

    ''' <summary>
    ''' Load Matching Room Data into Data View
    ''' </summary>
    ''' <param name="allTrue"></param>
    ''' <remarks></remarks>
    Private Sub UpdateRoomsDatagrid(Optional allTrue As Boolean = True)

        Try

            ' Fresh List
            Dim m_final As New SortableBindingList(Of clsRoomFamily)

            ' Iterate Collection
            For Each x As clsRoomFamily In _s.Rooms.Values

                ' Checked?
                x.IsChecked = allTrue
                m_final.Add(x)

            Next

            ' Bind the Results
            Me.DataGridViewRooms.DataSource = m_final

        Catch
        End Try

    End Sub

#End Region

#Region "Form Controls & Events"

    ''' <summary>
    ''' Startup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub form_RoomMasses_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            ' Form Stuff
            Me.Text = "Subscription Extrude Rooms to Mass v" & My.Application.Info.Version.ToString
            SetFormViz(formViz.isStandby)

            ' App creator
            _appCreate = _s.UiApp.Application.Create

            ' Rooms
            UpdateRoomsDatagrid()

            ' Room Parameters
            LoadRoomParameters()

            ' Parameters Button
            If _s.MassRooms.Count > 0 Then
                Me.ButtonParameters.Enabled = True
            Else
                Me.ButtonParameters.Enabled = False
            End If

        Catch
        End Try

    End Sub

#Region "Form Viz"

    Private Enum formViz
        isStandby
        isProcessing
    End Enum

    ''' <summary>
    ''' Set the Form Visuals
    ''' </summary>
    ''' <param name="v"></param>
    ''' <remarks></remarks>
    Private Sub SetFormViz(v As formViz)

        Select Case v

            Case formViz.isProcessing
                Me.ProgressBar1.Show()
                Me.ButtonCancel.Hide()
                Me.ButtonHelp.Hide()
                Me.ButtonGenerateMasses.Hide()
                Me.CheckBoxPurge.Hide()
                Me.CheckBoxSaveMass.Hide()
                Me.Label2.Enabled = False
                Me.ComboBoxMaterial.Enabled = False
                Me.ButtonAll.Enabled = False
                Me.ButtonNone.Enabled = False
                Me.NumericUpDown1.Enabled = False
                Me.Label1.Enabled = False
                Me.DataGridViewRooms.Enabled = False

            Case formViz.isStandby
                Me.ProgressBar1.Hide()

        End Select

        ' Update the Form
        Application.DoEvents()

    End Sub

#End Region

    ''' <summary>
    ''' Parameter Mapping
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonParameters_Click(sender As Object, e As EventArgs) Handles ButtonParameters.Click

        ' Update Mass Data
        _s.GetRoomMasses()

        ' Saved Config
        Dim m_config As New clsConfig
        For Each x In _s.ModelConfig
            If x.ModelPath.ToLower = _s.DocName.ToLower Then
                m_config = x
                Exit For
            End If
        Next

        ' Parameter Mapping
        Using d As New form_Parameters(_s.MassParameters, _s.RoomParameters, m_config, _s.DocName)
            d.ShowDialog()
            If Not d.MapConfig Is Nothing Then

                ' Update Config Item
                Dim m_existing As Boolean = False
                For Each x In _s.ModelConfig
                    If x.ModelPath.ToLower = _s.DocName.ToLower Then

                        ' Overwrite Current
                        x = d.MapConfig

                        ' Found
                        m_existing = True
                        Exit For

                    End If
                Next

                ' Add if Necessary
                If m_existing = False Then _s.ModelConfig.Add(d.MapConfig)

                Try

                    ' Save to File
                    Dim m_json As String = JsonConvert.SerializeObject(_s.ModelConfig)
                    Using w As New StreamWriter(_s.SavedSettingsPath, False)
                        w.WriteLine(m_json)
                    End Using

                Catch
                End Try

                Try

                    ' Update Mass Elements
                    _s.UpdateMassParameters(d.MapConfig.Mappings, Me.ProgressBar1)

                Catch
                End Try

            End If
        End Using

    End Sub

    ''' <summary>
    ''' Help
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click
        Process.Start("http://apps.case-inc.com/content/subscription-extrude-rooms-3d-mass")
    End Sub

    ''' <summary>
    ''' Start building the mass families
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonGenerateMasses_Click(ByVal sender As System.Object,
                                           ByVal e As System.EventArgs) Handles ButtonGenerateMasses.Click

        ' Close All Families
        If MsgBox("Running this Command Will Close All Open Families!",
                   MsgBoxStyle.YesNo,
                   "Do You Want to Continue?") = MsgBoxResult.Yes Then

            ' Form Visibility
            SetFormViz(formViz.isProcessing)

            ' Generate the Extrusions
            GenerateMasses()

            ' Parameters
            If _iMasses > 0 Then
                If MsgBox("Do you want to port parameters from your rooms into the Room Mass elements?",
                          MsgBoxStyle.YesNo, "Optional") = MsgBoxResult.Yes Then

                    ' Button for Parameters
                    ButtonParameters_Click(Nothing, Nothing)

                End If
            End If

            ' Close the Form when Done
            Me.Close()

        End If

    End Sub

    ''' <summary>
    ''' Close and Cancel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonCancel_Click(ByVal sender As System.Object,
                                   ByVal e As System.EventArgs) Handles ButtonCancel.Click

        ' Close
        Me.Close()

    End Sub

    ''' <summary>
    ''' Case Site Launch
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
        Process.Start("http://apps.case-inc.com/")
    End Sub

    ''' <summary>
    ''' Check All
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonAll_Click(sender As System.Object, e As System.EventArgs) Handles ButtonAll.Click
        UpdateRoomsDatagrid(True)
    End Sub

    ''' <summary>
    ''' Check None
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonNone_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNone.Click
        UpdateRoomsDatagrid(False)
    End Sub

#End Region

End Class
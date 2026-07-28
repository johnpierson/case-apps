Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData

    Public ReadOnly Property Doc As Document
      Get
        Try
          Return _cmd.Application.ActiveUIDocument.Document
        Catch
          Return Nothing
        End Try
      End Get
    End Property
    Public ReadOnly Property ActiveUiDoc As UIDocument
      Get
        Try
          Return _cmd.Application.ActiveUIDocument
        Catch
          Return Nothing
        End Try
      End Get
    End Property

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

    Friend Lines As New List(Of Element)
    Friend LineTypesDict As New Dictionary(Of String, String)
    Friend LineTypes As New List(Of String)
    Friend LineTypeStyles As New List(Of GraphicsStyle)
    Friend FailureMessage As String = ""

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="p_cmd"></param>
    ''' <remarks></remarks>
    Public Sub New(p_cmd As ExternalCommandData)

      ' Widen Scope
      _cmd = p_cmd

      ' Get all lines
      GetLines()

      ' Get the Line Styles
      GetGraphicStyles()

    End Sub

    ''' <summary>
    ''' Get All Lines
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetLines()
      'Dim m_fltr As New ElementClassFilter(GetType(Line))
      Dim m_col As New FilteredElementCollector(Doc)
      m_col.OfCategory(BuiltInCategory.OST_Lines)
      ' m_col.WherePasses(m_fltr)
      Lines = m_col.ToElements
    End Sub

    ''' <summary>
    ''' Get the Graphical Styles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetGraphicStyles()
      ' The Filter Object
      Dim m_fltr As ElementClassFilter
      m_fltr = New ElementClassFilter(GetType(GraphicsStyle))
      ' Build the Collector
      Dim m_col As FilteredElementCollector
      m_col = New FilteredElementCollector(Doc)
      m_col.WherePasses(m_fltr)
      ' Get the List of Elements
      Dim m_gs As New List(Of Element)
      m_gs = m_col.ToElements
      ' Get the Linestyles
      For Each x As GraphicsStyle In m_gs
        Try
          If x.GraphicsStyleCategory.Parent.Name.ToLower = "lines" Then
            If Not LineTypesDict.ContainsKey(x.Name) Then
              LineTypesDict.Add(x.Name, x.Name)
              LineTypes.Add(x.Name)
              LineTypeStyles.Add(x)
            End If
          End If
        Catch
        End Try
      Next
      ' Sort the List
      LineTypes.Sort()
    End Sub

    ''' <summary>
    ''' Change and Replace Linestyle
    ''' </summary>
    ''' <param name="p_findStyle"></param>
    ''' <param name="p_replaceStyle"></param>
    ''' <param name="p_useSelection"></param>
    ''' <remarks></remarks>
    Public Sub ReplaceStyles(p_findStyle As String,
                             p_replaceStyle As String,
                             p_useSelection As Boolean)
      ' Transaction
      Dim m_trans As New Transaction(Doc, "Line Style Change")
      m_trans.Start()

      ' Get the Element of the Old Style
      Dim m_FindStyleElement As Element = Nothing
      For Each x As GraphicsStyle In LineTypeStyles
        If x.Name = p_findStyle Then
          m_FindStyleElement = x
          Exit For
        End If
      Next
      ' Make Sure We Have the Element
      If m_FindStyleElement Is Nothing Then
        MsgBox("Error Retrieving 'Find' Graphic Style Element", MsgBoxStyle.Critical, "Stopping")
        Exit Sub
      End If

      ' Get the Element of the New Style
      Dim m_ReplaceStyleElement As Element = Nothing
      For Each x As GraphicsStyle In LineTypeStyles
        If x.Name = p_replaceStyle Then
          m_ReplaceStyleElement = x
          Exit For
        End If
      Next
      ' Make Sure We Have the Element
      If m_ReplaceStyleElement Is Nothing Then
        MsgBox("Error Retrieving 'Replace' Graphic Style Element", MsgBoxStyle.Critical, "Stopping")
        Exit Sub
      End If

      ' Iterate the Elements
      For Each x As Element In Lines
        ' Selection Scope
        If p_useSelection = True Then
          ' ElementID Match or Continue For
          If ActiveUiDoc.Selection.GetElementIds().Count > 0 Then
            For Each e As ElementId In ActiveUiDoc.Selection.GetElementIds()
              If e.ToString.ToUpper = x.Id.ToString.ToUpper Then
                GoTo IsValidElement
              End If
            Next
          End If
          ' Element Not Valid
          Continue For
        End If
IsValidElement:

        ' Change On Match
        ChangeIfMatch(x, m_FindStyleElement, m_ReplaceStyleElement)

      Next

      ' Commit the Changes
      m_trans.Commit()

    End Sub

    ''' <summary>
    ''' Change the Element if all Matches
    ''' </summary>
    ''' <param name="p_e"></param>
    ''' <param name="p_find"></param>
    ''' <param name="p_replace"></param>
    ''' <remarks></remarks>
    Private Sub ChangeIfMatch(p_e As Element, p_find As GraphicsStyle, p_replace As GraphicsStyle)
      ' What kind of element is it?
      Dim m_da As DetailArc = Nothing
      Dim m_de As DetailEllipse = Nothing
      Dim m_dl As DetailLine = Nothing
      Dim m_dn As DetailNurbSpline = Nothing
      Dim m_ma As ModelArc = Nothing
      Dim m_me As ModelEllipse = Nothing
      Dim m_ml As ModelLine = Nothing
      Dim m_mn As ModelNurbSpline = Nothing

      ' Detail Arc
      If TypeOf p_e Is DetailArc Then
        m_da = TryCast(p_e, DetailArc)
        If m_da.LineStyle.Name = p_find.Name Then
          m_da.LineStyle = p_replace
        End If
      End If
      ' Detail Ellipse
      If TypeOf p_e Is DetailEllipse Then
        m_de = TryCast(p_e, DetailEllipse)
        If m_de.LineStyle.Name = p_find.Name Then
          m_de.LineStyle = p_replace
        End If
      End If
      ' Detail Line
      If TypeOf p_e Is DetailLine Then
        m_dl = TryCast(p_e, DetailLine)
        If m_dl.LineStyle.Name = p_find.Name Then
          m_dl.LineStyle = p_replace
        End If
      End If
      ' Detail NurbSpline
      If TypeOf p_e Is DetailNurbSpline Then
        m_dn = TryCast(p_e, DetailNurbSpline)
        If m_dn.LineStyle.Name = p_find.Name Then
          m_dn.LineStyle = p_replace
        End If
      End If
      ' Model Arc
      If TypeOf p_e Is ModelArc Then
        m_ma = TryCast(p_e, ModelArc)
        If m_ma.LineStyle.Name = p_find.Name Then
          m_ma.LineStyle = p_replace
        End If
      End If
      ' Model Ellipse
      If TypeOf p_e Is ModelEllipse Then
        m_me = TryCast(p_e, ModelEllipse)
        If m_me.LineStyle.Name = p_find.Name Then
          m_me.LineStyle = p_replace
        End If
      End If
      ' Model Line
      If TypeOf p_e Is ModelLine Then
        m_ml = TryCast(p_e, ModelLine)
        If m_ml.LineStyle.Name = p_find.Name Then
          m_ml.LineStyle = p_replace
        End If
      End If
      ' Model NurbSpline
      If TypeOf p_e Is ModelNurbSpline Then
        m_mn = TryCast(p_e, ModelNurbSpline)
        If m_mn.LineStyle.Name = p_find.Name Then
          m_mn.LineStyle = p_replace
        End If
      End If
    End Sub

  End Class
End Namespace
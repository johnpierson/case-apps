Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

#Region "Public and Friend Properties"

    Friend ExtCmdData As ExternalCommandData
    Friend Lines As New List(Of Element)
    Friend LineTypesDict As New Dictionary(Of String, String)
    Friend LineTypes As New List(Of String)
    Friend LineTypeStyles As New List(Of GraphicsStyle)
    Friend FailureMessage As String = ""

    ''' <summary>
    ''' Document
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Doc As Document
      Get
        Try
          Return ExtCmdData.Application.ActiveUIDocument.Document
        Catch ex As Exception
          Return Nothing
        End Try
      End Get
    End Property

    ''' <summary>
    ''' UI Document
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ActiveUiDoc As UIDocument
      Get
        Try
          Return ExtCmdData.Application.ActiveUIDocument
        Catch ex As Exception
          Return Nothing
        End Try
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
      ExtCmdData = cmd

      ' Get all lines
      GetLines()

      ' Get the Line Styles
      GetGraphicStyles()

    End Sub

#Region "Private Members"
    
    ''' <summary>
    ''' Get All Lines
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetLines()

      ' Collector
      Using col As New FilteredElementCollector(Doc)
        col.OfCategory(BuiltInCategory.OST_Lines)
        For Each x In col.ToElements
          If x.GroupId.Value < 1 Then Lines.Add(x)
        Next
      End Using

    End Sub

    ''' <summary>
    ''' Get the Graphical Styles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetGraphicStyles()

      ' Collector
      Using col As New FilteredElementCollector(Doc)
        col.OfClass(GetType(GraphicsStyle))

        ' Get the Linestyles
        For Each x As GraphicsStyle In col.ToElements
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

      End Using

    End Sub

    ''' <summary>
    ''' Change the Element if all Matches
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="gFind"></param>
    ''' <param name="gReplace"></param>
    ''' <remarks></remarks>
    Private Sub ChangeIfMatch(e As Element,
                              gFind As GraphicsStyle,
                              gReplace As GraphicsStyle)

      ' What kind of element is it?
      Dim m_da As DetailArc
      Dim m_de As DetailEllipse
      Dim m_dl As DetailLine
      Dim m_dn As DetailNurbSpline
      Dim m_ma As ModelArc
      Dim m_me As ModelEllipse
      Dim m_ml As ModelLine
      Dim m_mn As ModelNurbSpline

      ' Detail Arc
      Dim m_detailArc = TryCast(e, DetailArc)
      If (m_detailArc IsNot Nothing) Then
        m_da = m_detailArc
        If m_da.LineStyle.Name = gFind.Name Then
          m_da.LineStyle = gReplace
        End If
      End If
      ' Detail Ellipse
      Dim m_detailEllipse = TryCast(e, DetailEllipse)
      If (m_detailEllipse IsNot Nothing) Then
        m_de = m_detailEllipse
        If m_de.LineStyle.Name = gFind.Name Then
          m_de.LineStyle = gReplace
        End If
      End If
      ' Detail Line
      Dim m_detailLine = TryCast(e, DetailLine)
      If (m_detailLine IsNot Nothing) Then
        m_dl = m_detailLine
        If m_dl.LineStyle.Name = gFind.Name Then
          m_dl.LineStyle = gReplace
        End If
      End If
      ' Detail NurbSpline
      Dim m_detailNurbSpline = TryCast(e, DetailNurbSpline)
      If (m_detailNurbSpline IsNot Nothing) Then
        m_dn = m_detailNurbSpline
        If m_dn.LineStyle.Name = gFind.Name Then
          m_dn.LineStyle = gReplace
        End If
      End If
      ' Model Arc
      Dim m_modelArc = TryCast(e, ModelArc)
      If (m_modelArc IsNot Nothing) Then
        m_ma = m_modelArc
        If m_ma.LineStyle.Name = gFind.Name Then
          m_ma.LineStyle = gReplace
        End If
      End If
      ' Model Ellipse
      Dim m_modelEllipse = TryCast(e, ModelEllipse)
      If (m_modelEllipse IsNot Nothing) Then
        m_me = m_modelEllipse
        If m_me.LineStyle.Name = gFind.Name Then
          m_me.LineStyle = gReplace
        End If
      End If
      ' Model Line
      Dim m_modelLine = TryCast(e, ModelLine)
      If (m_modelLine IsNot Nothing) Then
        m_ml = m_modelLine
        If m_ml.LineStyle.Name = gFind.Name Then
          m_ml.LineStyle = gReplace
        End If
      End If
      ' Model NurbSpline
      Dim m_modelNurbSpline = TryCast(e, ModelNurbSpline)
      If (m_modelNurbSpline IsNot Nothing) Then
        m_mn = m_modelNurbSpline
        If m_mn.LineStyle.Name = gFind.Name Then
          m_mn.LineStyle = gReplace
        End If
      End If
    End Sub

#End Region

#Region "Public Members"

    ''' <summary>
    ''' Change and Replace Linestyle
    ''' </summary>
    ''' <param name="styleOld"></param>
    ''' <param name="styleNew"></param>
    ''' <param name="bySelection"></param>
    ''' <remarks></remarks>
    Public Sub ReplaceStyles(styleOld As String,
                             styleNew As String,
                             bySelection As Boolean)
      ' Transaction
      Using t As New Transaction(Doc, "Line Style Change")
        If t.Start() Then

          ' Get the Element of the Old Style
          Dim m_findStyleElement As Element
          For Each x As GraphicsStyle In LineTypeStyles
            If x.Name = styleOld Then
              m_findStyleElement = x
              Exit For
            End If
          Next

          ' Make Sure We Have the Element
          If m_findStyleElement Is Nothing Then
            MsgBox("Error Retrieving 'Find' Graphic Style Element", MsgBoxStyle.Critical, "Stopping")
            Exit Sub
          End If

          ' Get the Element of the New Style
          Dim m_replaceStyleElement As Element
          For Each x As GraphicsStyle In LineTypeStyles
            If x.Name = styleNew Then
              m_replaceStyleElement = x
              Exit For
            End If
          Next

          ' Make Sure We Have the Element
          If m_replaceStyleElement Is Nothing Then
            MsgBox("Error Retrieving 'Replace' Graphic Style Element", MsgBoxStyle.Critical, "Stopping")
            Exit Sub
          End If

          ' Iterate the Elements
          For Each x As Element In Lines
            ' Selection Scope
            If bySelection = True Then
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
            ChangeIfMatch(x, m_findStyleElement, m_replaceStyleElement)

          Next

          ' Commit the Changes
          t.Commit()

        End If

      End Using

    End Sub

#End Region

  End Class
End Namespace
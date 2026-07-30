Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _c As ExternalCommandData

    Public ReadOnly Property Doc As Document
      Get
        Try
          Return _c.Application.ActiveUIDocument.Document
        Catch ex As Exception
          Return Nothing
        End Try
      End Get
    End Property
    Public ReadOnly Property DocPath As String
      Get
        Try
          If Doc.IsWorkshared = True Then
            Dim m_mp As ModelPath = Doc.GetWorksharingCentralModelPath
            If Not String.IsNullOrEmpty(m_mp.CentralServerPath) Then
              Return m_mp.CentralServerPath
            Else
              Return Doc.PathName
            End If
          Else
            Return Doc.PathName
          End If
        Catch
          If Not String.IsNullOrEmpty(Doc.PathName) Then
            Return Doc.PathName
          Else
            Return "No File Path"
          End If
        End Try
      End Get
    End Property
    Public Property ViewPortInstances As List(Of clsSheetViewports)

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="p_c"></param>
    ''' <param name="p_e"></param>
    ''' <remarks></remarks>
    Public Sub New(p_c As ExternalCommandData, p_e As ElementSet)

      ' Widen Scope
      _c = p_c

    End Sub

    ''' <summary>
    ''' Get all Viewports
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetViewports()

      ' Fresh Lists
      ViewPortInstances = New List(Of clsSheetViewports)

      ' Collect Instances
      Using m_col As New FilteredElementCollector(Doc)
        m_col.OfCategory(BuiltInCategory.OST_Viewports)
        For Each x As Element In m_col.ToElements
          ViewPortInstances.Add(New clsSheetViewports(x))
        Next
      End Using

    End Sub

  End Class
End Namespace
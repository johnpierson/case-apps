Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Public Class clsSettings

  Private _doc As Document

#Region "Public Properties"

  Public Property CommandData As ExternalCommandData
  Public Property ElementSet As ElementSet
  Public Property Levels As SortedDictionary(Of Double, Level)

  ''' <summary>
  ''' The Active Document
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property Doc As Document
    Get
      Try
        Return CommandData.Application.ActiveUIDocument.Document
      Catch ex As Exception
        Return _doc
      End Try
    End Get
  End Property

  ''' <summary>
  ''' Document Name And Path
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property DocumentName As String
    Get
      Try
        If Doc.IsWorkshared Then
          ' Test for a valid file name
          If Not String.IsNullOrEmpty(ModelPathUtils.ConvertModelPathToUserVisiblePath(doc.GetWorksharingCentralModelPath())) Then
            ' Only use central file name if the model has been saved
            Return ModelPathUtils.ConvertModelPathToUserVisiblePath(doc.GetWorksharingCentralModelPath())
          Else
            Return ""
          End If
        Else
          ' Use the document title
          Return Doc.PathName
        End If
      Catch ex As Exception
        Return ""
      End Try
    End Get
  End Property

#End Region

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="cmdData"></param>
  ''' <param name="eset"></param>
  ''' <remarks></remarks>
  Public Sub New(cmdData As ExternalCommandData, eset As ElementSet)

    ' Widen Scope
    CommandData = cmdData
    ElementSet = eset

  End Sub

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="d"></param>
  ''' <remarks></remarks>
  Public Sub New(d As Document)

    ' Widen Scope
    _doc = d

  End Sub

  '' '' ''' <summary>
  '' '' ''' Generate Levels
  '' '' ''' </summary>
  '' '' ''' <param name="cnt">How many levels to create</param>
  '' '' ''' <param name="sp">Spacing between Levels</param>
  '' '' ''' <returns></returns>
  '' '' ''' <remarks></remarks>
  '' ''Public Function BenchLevelCreator(cnt As Integer, sp As Integer)

  '' ''End Function

  ''' <summary>
  ''' Get All Levels in the Model
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub GetLevels()
    ' Fresh List
    Levels = New SortedDictionary(Of Double, Level)
    ' Collector
    Using col As New FilteredElementCollector(Doc)
      col.OfClass(GetType(Level))
      For Each x In col.ToElements
        Try
          Dim l As Level = TryCast(x, Level)
          Levels.Add(l.Elevation, l)
        Catch ex As Exception
        End Try
      Next
    End Using
  End Sub

  ''' <summary>
  ''' Return the First Wall type
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function FirstWallType() As WallType
    ' Collect Types
    Using col As New FilteredElementCollector(Doc)
      col.OfClass(GetType(WallType))
      For Each x In col.ToElements
        Return x
      Next
    End Using
    Return Nothing
  End Function

End Class
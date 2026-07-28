Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  ''' <summary>
  ''' A class to hold general settings in memory
  ''' </summary>
  ''' <remarks></remarks>
  Public Class clsSettings

    Private _cmd As ExternalCommandData

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

    Public ReadOnly Property AppVersion As String
      Get
        Return "v" & My.Application.Info.Version.ToString
      End Get
    End Property
    Public Property Categories As SortedDictionary(Of String, Category)
    Public Property SymbolElements As List(Of FamilySymbol)

    ''' <summary>
    ''' General Class Constructor
    ''' </summary>
    ''' <param name="c"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal c As ExternalCommandData)

      ' Widen Scope
      _cmd = c

      ' Fresh List
      SymbolElements = New List(Of FamilySymbol)

      ' Scan
      ScanData()

    End Sub

    ''' <summary>
    ''' Get a Sorted List of Categories in Use
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ScanData()

      ' Category Data
      Categories = New SortedDictionary(Of String, Category)

      ' Filter for FamilySymbol
      Using col As New FilteredElementCollector(Doc)
        col.OfClass(GetType(FamilySymbol))

        ' Process Each Element
        For Each e In col.ToElements

          Try
            Dim m_fs As FamilySymbol = TryCast(e, FamilySymbol)
            If m_fs Is Nothing Then Continue For

            ' Add the Symbol
            SymbolElements.Add(m_fs)

            ' Category Present
            If Not e.Category Is Nothing Then

              ' Avoid Tags and Weird Categories
              If e.Category.Name.StartsWith("<") Then Continue For

              ' Category Name
              If Not Categories.ContainsKey(e.Category.Name) Then

                ' Add to the List
                Categories.Add(e.Category.Name, e.Category)

              End If

            End If

          Catch
          End Try

        Next

      End Using

    End Sub

  End Class
End Namespace
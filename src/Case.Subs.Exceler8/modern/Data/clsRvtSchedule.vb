Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsRvtSchedule

    Private _category As Category = Nothing
    Private _schedule As ViewSchedule
    Private _schDef As ScheduleDefinition
    Private _name As String = ""
    Private _fields As New Dictionary(Of Integer, ScheduleField)

#Region "Public Properties"

    ''' <summary>
    ''' Inclusion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsChecked As Boolean

    ''' <summary>
    ''' Schedule Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Name As String
      Get
        Return _name
      End Get
    End Property

    ''' <summary>
    ''' Category
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CategoryName As String
      Get
        Try
          Return _category.Name
        Catch
        End Try
        Return ""
      End Get
    End Property

    ''' <summary>
    ''' Field Count
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FieldCount As Integer
      Get
        Try
          Return _fields.Values.Count
        Catch
        End Try
        Return 0
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Schedule Helper
    ''' </summary>
    ''' <param name="s"></param>
    ''' <remarks></remarks>
    Public Sub New(s As ViewSchedule)

      ' Widen Scope
      _schedule = s

      ' Setup
      Setup()

    End Sub

#Region "Private Members"

    ''' <summary>
    ''' Basic Setup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Setup()

      ' Defaults
      isChecked = False

      Try
        _schDef = _schedule.Definition
      Catch
      End Try

      Try
        _name = _schedule.Name
      Catch
      End Try

      Try
        ' Get the Category Name
        If Not _schDef Is Nothing Then
          Dim m_id As ElementId = _schDef.CategoryId

          ' Multi?
          If m_id.Value = -1 Then
            ' Multi Schedule...

          End If

          ' Find the Category
          For Each x As Category In _schedule.Document.Settings.Categories
            If x.Id.Value = m_id.Value Then
              _category = x
              Exit For
            End If
          Next

          ' Any Category?
          If _category Is Nothing Then
            Using col As New FilteredElementCollector(_schedule.Document)
              col.OfCategoryId(m_id)
              For Each x In col.ToElements
                _category = x.Category
                Exit For
              Next
            End Using
          End If

        End If
      Catch
      End Try

      Try
        ' Fields
        GetFieldNames()
      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Fields
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetFieldNames()

      ' Value
      _fields = New Dictionary(Of Integer, ScheduleField)

      If Not _schDef Is Nothing Then
        Dim m_iFld As Integer = _schDef.GetFieldCount
        For i = 0 To m_iFld - 1
          Try
            ' Field
            Dim m_sf As ScheduleField = _schDef.GetField(i)
            If Not m_sf Is Nothing Then

              Try

                ' Store the Field
                _fields.Add(m_sf.FieldIndex, m_sf)

              Catch
              End Try

            End If
          Catch
          End Try

        Next
      End If

    End Sub

#End Region

#Region "Public Members"

    ''' <summary>
    ''' Fields in Schedule
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFields() As Dictionary(Of Integer, ScheduleField)
      Return _fields
    End Function

    ''' <summary>
    ''' Category
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCategory() As Category
      Return _category
    End Function

    ''' <summary>
    ''' Category
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetViewSchedule() As ViewSchedule
      Return _schedule
    End Function

#End Region

  End Class
End Namespace
Imports Autodesk.Revit.DB
Imports Newtonsoft.Json.Linq

Namespace Data

  Public Class clsVp

    Private _e As Element
    Private _guid As String = ""
    Private _eid As String = ""
    Private _shtNumber As String = ""
    Private _shtName As String = ""
    Private _detailNumber As String = ""
    Private _vName As String = ""
    Private _titleOnSheet As String = ""

#Region "Public Properties"

    ''' <summary>
    ''' Primary Key ; GUID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Guid As String
      Get
        Return _guid
      End Get
    End Property

    Public ReadOnly Property Eid As String
      Get
        Return _eid
      End Get
    End Property

    Public ReadOnly Property SheetNumber As String
      Get
        Return _shtNumber
      End Get
    End Property

    Public ReadOnly Property SheetName As String
      Get
        Return _shtName
      End Get
    End Property

    Public ReadOnly Property DetailNumber As String
      Get
        Return _detailNumber
      End Get
    End Property

    Public ReadOnly Property ViewName As String
      Get
        Return _vName
      End Get
    End Property

    Public ReadOnly Property TitleOnSheet As String
      Get
        Return _titleOnSheet
      End Get
    End Property

    Public Property ViewState As String

#End Region

    ''' <summary>
    ''' Constructor from Element
    ''' </summary>
    ''' <param name="e">Element - Viewport</param>
    ''' <remarks></remarks>
    Public Sub New(e As Element)

      Try

        ' Widen Scope
        _e = e

        ' GUID
        _guid = e.UniqueId.ToString.ToLower

        ' State
        ViewState = "Active"

        ' Eid
        _eid = e.Id.ToString

        Try
          ' Sheet Number
          Dim m_p As Parameter = _e.Parameter(BuiltInParameter.VIEWPORT_SHEET_NUMBER)
          _shtNumber = m_p.AsString
        Catch
        End Try

        Try
          ' Sheet Name
          Dim m_p As Parameter = _e.Parameter(BuiltInParameter.VIEWPORT_SHEET_NAME)
          _shtName = m_p.AsString
        Catch
        End Try

        Try
          ' View Name
          Dim m_p As Parameter = _e.Parameter(BuiltInParameter.VIEW_NAME)
          _vName = m_p.AsString
        Catch
        End Try

        Try
          ' View/Detail Number
          Dim m_p As Parameter = _e.Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER)
          _detailNumber = m_p.AsString
        Catch
        End Try

        Try
          ' View Name on Sheet
          Dim m_p As Parameter = _e.Parameter(BuiltInParameter.VIEW_DESCRIPTION)
          _titleOnSheet = m_p.AsString
        Catch
        End Try

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Construct from jSON
    ''' </summary>
    ''' <param name="j">jSON Object</param>
    ''' <remarks></remarks>
    Public Sub New(j As JToken)

      ' Process Each Node

      Try
        _guid = j("GUID")
      Catch
        _guid = ""
      End Try

      Try
        _eid = j("Eid")
      Catch
        _eid = ""
      End Try

      Try
        _shtNumber = j("SheetNumber")
      Catch
        _shtNumber = ""
      End Try

      Try
        _shtName = j("SheetName")
      Catch
        _shtName = ""
      End Try

      Try
        _detailNumber = j("DetailNumber")
      Catch
        _detailNumber = ""
      End Try

      Try
        _vName = j("ViewName")
      Catch
        _vName = ""
      End Try

      Try
        _titleOnSheet = j("TitleOnSheet")
      Catch
        _titleOnSheet = ""
      End Try

      Try
        ViewState = j("ViewState")
      Catch
        ViewState = ""
      End Try

    End Sub

    ''' <summary>
    ''' Get the Element - Avoid Properties
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetElement() As Element
      Return _e
    End Function

  End Class
End Namespace
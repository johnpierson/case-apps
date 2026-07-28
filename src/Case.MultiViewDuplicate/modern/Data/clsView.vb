Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsView

    Private _v As View
    Private _name As String = ""
    Private _kind As String = ""
    Private _type As String = ""

#Region "Public Properties"

    ''' <summary>
    ''' If the View is Selected for Processing
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property isChecked As Boolean

    ''' <summary>
    ''' The View Element
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ViewElement As View
      Get
        Return _v
      End Get
    End Property

    ''' <summary>
    ''' View Name
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
    ''' View Type
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ViewType As String
      Get
        Return _type
      End Get
    End Property

    ''' <summary>
    ''' View Type
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ViewKind As String
      Get
        Return _kind
      End Get
    End Property

#End Region

    ''' <summary>
    ''' View Helper
    ''' </summary>
    ''' <param name="v"></param>
    ''' <remarks></remarks>
    Public Sub New(v As View)

      ' Widen Scope
      _v = v

      ' Setup
      doSetup()

    End Sub

    ''' <summary>
    ''' Setup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub doSetup()

      Try
        _name = _v.Parameter(BuiltInParameter.VIEW_NAME).AsString
      Catch
      End Try
      Try
        If String.IsNullOrEmpty(_name) Then _name = _v.Name
      Catch
      End Try
      Try
        Dim m_vt As ViewFamilyType = _v.Document.GetElement(_v.GetTypeId)
        _type = m_vt.Name
      Catch
      End Try
      Try
        _kind = _v.ViewType.ToString
      Catch
      End Try

    End Sub

  End Class
End Namespace
Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsTagInstance

    Private _fi As FamilyInstance
    Private _vp As clsVP
    Private _paramNameGuid As String
    Private _paramNameView As String
    Private _paramNameShtn As String

#Region "Public Properties"

    ''' <summary>
    ''' Fam Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property FamilyName As String
      Get
        Try
          Dim m_symb As FamilySymbol = _fi.Document.GetElement(_fi.GetTypeId)
          Return m_symb.Family.Name
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' Type Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property TypeName As String
      Get
        Try
          Return _fi.Name
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' Sync GUID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Must run inside a transaction</remarks>
    Friend Property SyncGuid As String
      Get
        Try
          ' Sync GUID
          Dim m_p As Parameter = _fi.LookupParameter(_paramNameGuid)
          If Not m_p Is Nothing Then
            Return m_p.AsString
          End If
        Catch
        End Try
        Return ""
      End Get
      Set(value As String)
        Try
          Dim m_p As Parameter = _fi.LookupParameter(_paramNameGuid)
          If Not m_p Is Nothing Then
            m_p.Set(value)
          End If
        Catch
        End Try
      End Set
    End Property

    ''' <summary>
    ''' Detail Number
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Must run inside a transaction</remarks>
    Public Property DetailNumber As String
      Get
        Try
          ' Drawing Number (Detail Number)
          Dim m_p As Parameter = _fi.LookupParameter(_paramNameView)
          If Not m_p Is Nothing Then
            Return m_p.AsString
          Else
            Return ""
          End If
        Catch
        End Try
        Return "{error}"
      End Get
      Set(value As String)
        Try
          Dim m_p As Parameter = _fi.LookupParameter(_paramNameView)
          If Not m_p Is Nothing Then
            m_p.Set(value)
          End If
        Catch
        End Try
      End Set
    End Property

    ''' <summary>
    ''' Sheet Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Must run inside a transaction</remarks>
    Public Property SheetNumber As String
      Get
        Try
          ' Sheet Number
          Dim m_p As Parameter = _fi.LookupParameter(_paramNameShtn)
          If Not m_p Is Nothing Then
            Return m_p.AsString
          Else
            Return ""
          End If
        Catch
        End Try
        Return "{error}"
      End Get
      Set(value As String)
        Try
          Dim m_p As Parameter = _fi.LookupParameter(_paramNameShtn)
          If Not m_p Is Nothing Then
            m_p.Set(value)
          End If
        Catch
        End Try
      End Set
    End Property

    ''' <summary>
    ''' Fam and Type Names
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FamType As String
      Get
        Try
          Return FamilyName & " (" & TypeName & ")"
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' Element Comments
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Comments As String
      Get
        Try
          Return _fi.Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).AsString
        Catch
        End Try
        Return ""
      End Get
    End Property

    ''' <summary>
    ''' View Name Tag Placed In
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ViewPlaced As String
      Get
        Try
          Dim m_view As View = TryCast(_fi.Document.GetElement(_fi.OwnerViewId), View)
                    Return m_view.Name
                Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' Sheet Name comes from a VP Object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SheetName As String
      Get
        Try
          Return _vp.SheetName
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' View Name comes from a VP Object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ViewName As String
      Get
        Try
          Return _vp.ViewName
        Catch
          Return "{error}"
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Title on Sheet Comes from a VP Object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TitleOnSheet As String
      Get
        Try
          Return _vp.TitleOnSheet
        Catch
          Return "{error}"
        End Try
      End Get
    End Property

#End Region

    ''' <summary>
    ''' View Tag Helper
    ''' </summary>
    ''' <param name="f">Family Instance (Tag)</param>
    ''' <param name="pG">Parameter name for viewport GUID</param>
    ''' <param name="pV">Parameter name for view number</param>
    ''' <param name="pS">Parameter name for sheet number</param>
    ''' <remarks></remarks>
    Public Sub New(f As FamilyInstance,
                   pG As String,
                   pV As String,
                   pS As String)

      ' Widen Scope
      _fi = f
      _paramNameGuid = pG
      _paramNameShtn = pS
      _paramNameView = pV

    End Sub

#Region "Friend Members"

    ''' <summary>
    ''' Set the View Object
    ''' </summary>
    ''' <param name="vp"></param>
    ''' <remarks></remarks>
    Friend Sub SetVp(vp As clsVP)

      ' Widen Scope
      _vp = vp

    End Sub

    ''' <summary>
    ''' Get the Element - Avoid Property
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFamilyInstance() As Element
      Return _fi
    End Function

    ''' <summary>
    ''' Set the Values from the Viewport Object
    ''' </summary>
    ''' <param name="vp">View Contains Data to Send to Tag Instance</param>
    ''' <remarks></remarks>
    Friend Sub SetValues(vp As clsVP)

      Try
        SyncGUID = vp.GUID
      Catch
      End Try

      Try
        DetailNumber = vp.DetailNumber
      Catch
      End Try

      Try
        SheetNumber = vp.SheetNumber
      Catch
      End Try

    End Sub

#End Region

  End Class
End Namespace
Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsFamilyType

    Private _e As Element

#Region "Public Properties"

    ''' <summary>
    ''' ElementID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ElementId As String
      Get
        Try
          Return _e.Id.ToString
        Catch
          Return "{error}"
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Category Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CategoryName As String
      Get
        Try
          Return _e.Category.Name
        Catch
          Return "{error}"
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Current Family Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CurrentFamilyName As String
      Get
        Try
          Return _e.Parameter(BuiltInParameter.SYMBOL_FAMILY_NAME_PARAM).AsString
        Catch
          Return "{error}"
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Current Type Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CurrentTypeName As String
      Get
        Try
          Return _e.Parameter(BuiltInParameter.SYMBOL_NAME_PARAM).AsString
        Catch
          Return "{error}"
        End Try
      End Get
    End Property

    Public Property NewFamilyName As String
    Public Property NewTypeName As String

    ''' <summary>
    ''' Type Name Change for Datagrid
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TypeNameChange As String
      Get
        Try
          If CurrentTypeName = NewTypeName Then
            Return CurrentTypeName & " --> [Same]"
          Else
            Return CurrentTypeName & " --> [" & NewTypeName & "]"
          End If
        Catch
          Return "{error}"
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Family Name Change for Datagrid
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FamilyNameChange As String
      Get
        Try
          If CurrentFamilyName = NewFamilyName Then
            Return CurrentFamilyName & " --> [Same]"
          Else
            Return CurrentFamilyName & " --> [" & NewFamilyName & "]"
          End If
        Catch
          Return "{error}"
        End Try
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Manage a Family 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="famName"></param>
    ''' <param name="tName"></param>
    ''' <remarks></remarks>
    Public Sub New(e As Element, Optional famName As String = "", Optional tName As String = "")

      ' Widen Scope
      _e = e
      NewFamilyName = CurrentFamilyName
      NewTypeName = CurrentTypeName

      ' Naming
      If Not String.IsNullOrEmpty(FamName) Then NewFamilyName = FamName
      If Not String.IsNullOrEmpty(tName) Then NewTypeName = tName

    End Sub

#Region "Public Members"
    
    ''' <summary>
    ''' Rename the Element
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RenameElement() As clsRenameResult

      ' Return Value
      Dim m_return As New clsRenameResult

      Try

        ' Anything to Rename?
        If CurrentFamilyName = NewFamilyName And CurrentTypeName = NewTypeName Then

          ' Nothing to Do
          m_return.famResult = "Same"
          m_return.typResult = "Same"

        Else

          ' Start a New Transaction
          Using t As New Transaction(_e.Document, "Renaming: " & CurrentTypeName & "(" & CurrentFamilyName & ") to: " &
                                                  NewFamilyName & "(" & NewTypeName & ")")
            If t.Start Then

              Try

                Try

                  ' Family
                  If Not CurrentFamilyName = NewFamilyName Then
                    If TypeOf _e Is FamilySymbol Then
                      Dim m_famsymbol As FamilySymbol = TryCast(_e, FamilySymbol)
                      If Not m_famsymbol Is Nothing Then
                        m_famsymbol.Family.Name = NewFamilyName
                      End If
                    Else
                      m_return.famResult = "Cannot Rename Family for this Kind of Element (" & CategoryName & ")"
                    End If
                  Else
                    m_return.famResult = "Same"
                  End If

                Catch ex As Exception
                  m_return.famResult = "Failed to Rename Family (" & CurrentFamilyName & ": " & ex.Message & ") "
                End Try

                Try

                  ' Type
                  If Not CurrentTypeName = NewTypeName Then
                    _e.Name = NewTypeName
                  Else
                    m_return.typResult = "Same"
                  End If

                Catch ex As Exception
                  m_return.typResult = "Failed to Rename Type (" & CurrentTypeName & ": " & ex.Message & ") "
                End Try

                ' Success
                t.Commit()

              Catch ex As Exception

                ' Failure Message
                m_return.famResult = ex.Message

              End Try

            End If

          End Using

        End If

      Catch ex As Exception

        ' Failure Message
        m_return.famResult = ex.Message

      End Try

      ' Final Result
      Return m_return

    End Function

    ''' <summary>
    ''' UniqueID
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUid() As String
      Try
        Return _e.UniqueId.ToString
      Catch
        Return ""
      End Try
    End Function

#End Region

  End Class
End Namespace
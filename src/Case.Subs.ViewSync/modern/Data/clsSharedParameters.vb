Imports System.IO
Imports System.Reflection
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSharedParameters

    Private _uiApp As UIApplication
    Private _df As DefinitionFile
    Private _defOriginal As String = ""

#Region "Public Properties"

    ''' <summary>
    ''' By Group
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DefinitionsByGroup As SortedDictionary(Of String, List(Of Definition))

#End Region

    ''' <summary>
    ''' Shared Parameter File 
    ''' </summary>
    ''' <param name="uiApp"></param>
    ''' <remarks></remarks>
    Public Sub New(uiApp As UIApplication)

      ' Widen Scope
      _uiApp = uiApp

      Try

        ' Valid Definition File
        _df = _uiApp.Application.OpenSharedParameterFile

        ' Store the Reset File Path
        If Not _df Is Nothing Then

          ' Store Path to Their Definition File for Reset
          If Not String.IsNullOrEmpty(_df.Filename) Then
            _defOriginal = _df.Filename
          End If

        End If

      Catch
      End Try

      ' Load Params
      GetParameters()

    End Sub

#Region "Private Members"

    ''' <summary>
    ''' Update the Parameter Lists 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParameters()

      ' Fresh Lists
      DefinitionsByGroup = New SortedDictionary(Of String, List(Of Definition))

      ' Definition File Exists?
      If _df Is Nothing Then Exit Sub

      Try

        ' Iterate All Groups
        For Each g As DefinitionGroup In _df.Groups
          For Each d As Definition In g.Definitions

            ' By Groups
            If Not DefinitionsByGroup.ContainsKey(g.Name) Then

              ' Group and Fresh List
              DefinitionsByGroup.Add(g.Name, New List(Of Definition))

            End If

            ' Add the Definition
            DefinitionsByGroup(g.Name).Add(d)

          Next
        Next

      Catch
      End Try

    End Sub

#End Region

#Region "Public Members"

    ''' <summary>
    ''' Load Original Definition File
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ResestOriginalDefinitionFile() As Boolean
      Try
        LoadSharedParameterFile(_defOriginal)
        Return True
      Catch
      End Try
      Return False
    End Function

    ''' <summary>
    ''' Load the Shared Parameter File
    ''' </summary>
    ''' <param name="f"></param>
    ''' <remarks></remarks>
    Public Function LoadSharedParameterFile(Optional f As String = "") As Boolean

      ' Reset Definition
      _df = Nothing

      Try

        ' NULL = Default CASE File
        If String.IsNullOrEmpty(f) Then

          ' Open the ViewSync Parameter File
          Dim m_defPath As String = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location), "Case.Subs.ViewSync.txt")
          If File.Exists(m_defPath) Then

            ' Load Called Path
            _uiApp.Application.SharedParametersFilename = m_defPath

          Else

            ' Missing Default File
            MsgBox("Missing Shared Parameter File" & vbCr & vbCr &
                   m_defPath,
                   MsgBoxStyle.Exclamation,
                   "CASE View Sync Error:")
            Return False

          End If

        Else

          ' Custom Path
          If File.Exists(f) Then

            ' Load Called Path
            _uiApp.Application.SharedParametersFilename = f

          Else

            ' Missing Default File
            MsgBox("Missing Shared Parameter File" & vbCr & f, MsgBoxStyle.Critical, "Missing File")
            Return False

          End If

        End If

        ' Load the File
        _df = _uiApp.Application.OpenSharedParameterFile

        ' Get the Parameters
        GetParameters()

        ' Success
        Return True

      Catch
      End Try

      ' Failure
      Return False

    End Function

    ''' <summary>
    ''' Bind Definitions to Multiple Categories 
    ''' </summary>
    ''' <param name="defList"></param>
    ''' <param name="catList"></param>
    ''' <param name="bipGroup"></param>
    ''' <remarks></remarks>
    Public Sub BindDefinitionsToCategories(defList As List(Of Definition),
                                           catList As List(Of Category),
                                           bipGroup As ForgeTypeId)

      ' Category Set
      Dim m_catSet As New CategorySet
      For Each cat As Category In catList
        m_catSet.Insert(cat)
      Next

      Dim m_namesFailed As New List(Of String)
      Dim m_namesSuc As New List(Of String)

      ' Process Each Definition 
      For Each def As Definition In defList

        ' Transaction
        Using t As New Transaction(_uiApp.ActiveUIDocument.Document, "Add Parameter Binding: " & def.Name)
          If t.Start = TransactionStatus.Started Then

            Try

              ' Family?
              If _uiApp.ActiveUIDocument.Document.IsFamilyDocument = True Then

                Try

                  ' Family Manager
                  Dim m_famMan As FamilyManager = _uiApp.ActiveUIDocument.Document.FamilyManager

                  m_famMan.AddParameter(def, bipGroup, True)

                  ' Count Success
                  m_namesSuc.Add(def.Name)

                Catch ex As Exception

                  m_namesFailed.Add(def.Name & "(" & ex.Message & ")")

                End Try

              Else

                ' Bind to Category as Instance
                Dim m_bind As InstanceBinding
                m_bind = _uiApp.Application.Create.NewInstanceBinding(m_catSet)
                _uiApp.ActiveUIDocument.Document.ParameterBindings.Insert(def, m_bind, bipGroup)

                ' Count Success
                m_namesSuc.Add(def.Name)

              End If

              ' Commit
              t.Commit()

            Catch ex As Exception

              m_namesFailed.Add(def.Name)

            End Try

          End If
        End Using

      Next

      ' Report to User
      Using td As New Autodesk.Revit.UI.TaskDialog("View Sync Parameter Loading")
        td.MainInstruction = "Attempted to Load " & defList.Count.ToString & " Parameters"

        ' Success
        Dim m_results As String = """"
        If m_namesSuc.Count > 0 Then
          m_results = m_namesSuc.Count & " Succeeded to Load..."
          For Each x As String In m_namesSuc
            m_results += vbCr & x & vbCr
          Next
          td.MainContent += m_results
        End If
        td.MainContent = m_results

        ' Failures
        If m_namesFailed.Count > 0 Then
          Dim m_msg As String = vbCr & vbCr & "Failed to Load These Guys:"
          For Each x As String In m_namesFailed
            m_msg += vbCr & x & vbCr
          Next
          td.MainContent += m_msg
        End If
        td.Show()
      End Using

    End Sub

#End Region

  End Class
End Namespace
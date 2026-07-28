Imports System.IO
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSharedParameters

    Private _uiApp As UIApplication
    Private _df As DefinitionFile

    Public ReadOnly Property FileName As String
      Get
        Try
          Return _df.Filename
        Catch ex As Exception
          Return "Load a Shared Parameter File"
        End Try
      End Get
    End Property
    Public Property DefinitionsByGroup As SortedDictionary(Of String, List(Of Definition))
    Public Property DefinitionsByType As SortedDictionary(Of String, List(Of Definition))

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

      Catch
      End Try

      ' Get the Definitions
      GetParameters()

    End Sub

    ''' <summary>
    ''' Update the Parameter Lists 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParameters()

      ' Fresh Lists
      DefinitionsByGroup = New SortedDictionary(Of String, List(Of Definition))
      DefinitionsByType = New SortedDictionary(Of String, List(Of Definition))

      ' Definition File Exists?
      If _df Is Nothing Then Exit Sub

      Try

        ' Iterate All Groups
        For Each g As DefinitionGroup In _df.Groups
          For Each d As Definition In g.Definitions

            ' By Groups
            If Not DefinitionsByGroup.ContainsKey(g.Name) Then
              Dim m_dList As New List(Of Definition)
              m_dList.Add(d)
              DefinitionsByGroup.Add(g.Name, m_dList)
            Else
              DefinitionsByGroup(g.Name).Add(d)
            End If

            ' By Type
              #If Revit2020 or Revit2021 Or Revit2022
 If Not DefinitionsByType.ContainsKey(d.ParameterType.ToString) Then
              Dim m_dList As New List(Of Definition)
              m_dList.Add(d)
              DefinitionsByType.Add(d.ParameterType.ToString, m_dList)
            Else
              DefinitionsByType(d.ParameterType.ToString).Add(d)
            End If
#End If
#If Revit2023 Or Revit2024 Or Revit2025 Or Revit2026 Or Revit2027
 If Not DefinitionsByType.ContainsKey(d.GetDataType.ToString) Then
              Dim m_dList As New List(Of Definition)
              m_dList.Add(d)
              DefinitionsByType.Add(d.GetDataType.ToString, m_dList)
            Else
              DefinitionsByType(d.GetDataType.ToString).Add(d)
            End If
#End If  

          Next
        Next

      Catch ex As Exception

      End Try

    End Sub

    ''' <summary>
    ''' Load the Shared Parameter File
    ''' </summary>
    ''' <param name="f"></param>
    ''' <remarks></remarks>
    Public Sub LoadSharedParameterFile(f As String)

      Try

        ' Load the File
        _uiApp.Application.SharedParametersFilename = f
        _df = _uiApp.Application.OpenSharedParameterFile

        ' Get Parameters List
        GetParameters()

      Catch ex As Exception

        ' Failure
        _df = Nothing

      End Try

    End Sub

    ''' <summary>
    ''' Bind Definitions to Multiple Categories 
    ''' </summary>
    ''' <param name="defList"></param>
    ''' <param name="catList"></param>
    ''' <param name="bipGroup"></param>
    ''' <param name="isInstance"></param>
    ''' <remarks></remarks>
    Public Sub BindDefinitionsToCategories(defList As List(Of Definition),
                                           catList As List(Of Category),
                                           bipGroup As ForgeTypeId,
                                           isInstance As Boolean)

      ' Category Set
      Dim m_catSet As New CategorySet
      For Each cat As Category In catList
        m_catSet.Insert(cat)
      Next

      ' Process Each Definition
      For Each def As Definition In defList

        ' Transaction
        Using t As New Transaction(_uiApp.ActiveUIDocument.Document,
                                   "Add Parameter Binding")
          If t.Start Then

            Try

              ' Bind to Category
              If isInstance = True Then

                ' Instance
                Dim m_bind As InstanceBinding
                m_bind = _uiApp.Application.Create.NewInstanceBinding(m_catSet)
                _uiApp.ActiveUIDocument.Document.ParameterBindings.Insert(def, m_bind, bipGroup)

              Else

                ' Type
                Dim m_bind As TypeBinding
                m_bind = _uiApp.Application.Create.NewTypeBinding(m_catSet)
                _uiApp.ActiveUIDocument.Document.ParameterBindings.Insert(def, m_bind, bipGroup)

              End If

              ' Commit
              t.Commit()

            Catch
            End Try

          End If
        End Using

      Next

    End Sub

  End Class
End Namespace
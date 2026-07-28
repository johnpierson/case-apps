Imports System.Data
Imports System.Reflection
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData

#Region "Public Properties"

    ''' <summary>
    ''' The Active Document
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Doc As Document
      Get
        Try
          Return _cmd.Application.ActiveUIDocument.Document
        Catch ex As Exception
          Return Nothing
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Document Path
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
              Return "Detached Model"
            End If
          Else
            Return Doc.PathName
          End If
        Catch ex As Exception
          Return ""
        End Try
      End Get
    End Property

    ''' <summary>
    ''' The UI Application
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UiApp As UIApplication
      Get
        Try
          Return _cmd.Application
        Catch ex As Exception
          Return Nothing
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Family Types
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FamilyItems As New SortedDictionary(Of String, Dictionary(Of String, clsFamilyType))

    ''' <summary>
    ''' Categories
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CategoryListing As List(Of clsCategory)

    ''' <summary>
    ''' Path to the Active Excel Document
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExcelPath As String

    ''' <summary>
    ''' Version of this Addin - Set in the Application Assembly Information
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Version As String
      Get
        Return "v" & Assembly.GetExecutingAssembly.GetName.Version.ToString
      End Get
    End Property

#End Region

    ''' <summary>
    ''' The main memory management class
    ''' </summary>
    ''' <param name="c"></param>
    ''' <remarks></remarks>
    Public Sub New(c As ExternalCommandData)

      ' Widen Scope
      _cmd = c
      Setup()

    End Sub

    ''' <summary>
    ''' Class Setup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Setup()

      Try

        ' Fresh Lists
        FamilyItems = New SortedDictionary(Of String, Dictionary(Of String, clsFamilyType))
        CategoryListing = New List(Of clsCategory)

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get Elements from Data
    ''' </summary>
    ''' <param name="dt">Datatable listing all elements to find and possibly rename</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ImportData(dt As DataTable) As Boolean

      Try

        ' Read the Data
        For Each dr As DataRow In dt.Rows

          Try

            ' Does the Element Exist in the Model?
            Dim m_uid As String = dr("UniqueID")
            Dim m_element As Element = Doc.GetElement(m_uid)
            If Not m_element Is Nothing Then

              ' Found it
              Dim m_catName As String = dr("Category Name")
              Dim m_newFam As String = dr("New Family Name")
              Dim m_newType As String = dr("New Type Name")
              Dim m_curFam As String = dr("Current Family Name")
              Dim m_curType As String = dr("Current Type Name")
              Dim m_eId As String = dr("ElementID")

              ' Is it in the master?
              If Not FamilyItems.ContainsKey(m_catName) Then

                Try

                  ' Add It
                  Dim m_dict As New Dictionary(Of String, clsFamilyType)
                  m_dict.Add(m_uid, New clsFamilyType(m_element, m_newFam, m_newType))
                  FamilyItems.Add(m_catName, m_dict)

                Catch
                End Try

              Else

                Try

                  ' Does this Element Exist in it?
                  If FamilyItems(m_catName).ContainsKey(m_uid) Then

                    ' Update the New Names
                    FamilyItems(m_catName)(m_uid).NewFamilyName = m_newFam
                    FamilyItems(m_catName)(m_uid).NewTypeName = m_newType

                  Else

                    ' Add it to the existing
                    FamilyItems(m_catName).Add(m_uid, New clsFamilyType(m_element, m_newFam, m_newType))

                  End If

                Catch
                End Try

              End If

            Else

            End If

          Catch
          End Try

        Next

        ' Success
        Return True

      Catch
      End Try

      ' Failure
      Return False

    End Function

  End Class
End Namespace
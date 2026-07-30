Imports System.IO
Imports System.Reflection
Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].ModeledRoomTags.Data

Namespace Entry

  <Transaction(TransactionMode.Manual)>
  Public Class CmdMain

    Implements IExternalCommand

    ''' <summary>
    ''' Place 3D Model Text Room Tags in All Rooms
    ''' </summary>
    ''' <param name="commandData"></param>
    ''' <param name="message"></param>
    ''' <param name="elements"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Execute(ByVal commandData As ExternalCommandData,
                            ByRef message As String,
                            ByVal elements As ElementSet) As Result Implements IExternalCommand.Execute

      Try
        ' Settings
        Dim m_s As New clsSettings(commandData, elements)
        m_s.GetFamilySymbol()

        ' Family Loaded?
        If m_s.TagSymbol Is Nothing Then

          ' Does the File Exist?
          Dim m_path As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location) & "\" & m_s.TagFamilyName & ".rfa"
          If Not File.Exists(m_path) Then

            ' Missing File Error
            message = "Required Family File Not Found! Cannot continue..."
            Return Result.Failed

          Else

            ' Load it
            Dim m_fam As Family = Nothing
            m_fam = m_s.LoadFamily(m_path, m_s.Doc, True)
            If Not m_fam Is Nothing Then

              ' Update Reference
              m_s.GetFamilySymbol()

            Else

              ' Failure
              message = "Failed to load required tag family... cannot continue"
              Return Result.Failed

            End If

          End If

        End If

        ' Construct and Display the Form
        Using d As New form_Main(m_s)
          d.ShowDialog()
        End Using

        ' Success
        Return Result.Succeeded

      Catch ex As Exception

        ' Failure
        message = ex.Message
        Return Result.Failed

      End Try

    End Function

  End Class
End Namespace
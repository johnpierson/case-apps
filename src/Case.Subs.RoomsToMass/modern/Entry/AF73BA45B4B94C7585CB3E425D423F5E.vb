Imports System
Imports System.IO
Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].Subs.RoomsToMass.Data

Namespace Entry

  ''' <summary>
  ''' Updates for Paid Version!:
  ''' 
  ''' 1.) Detect Missing and Show List
  '''       GUID File Names in Target Location
  '''       GUID Named Mass Families that Match a Room GUID
  '''        - Ask to Update?
  ''' 2.) Delete all Placed and Place Updated Selections Only
  ''' 3.) ByPass Warning to Replace on New Load
  ''' 4.) Only Add/Update Checked
  ''' 
  ''' </summary>
  ''' <remarks></remarks>
  <Transaction(TransactionMode.Manual)>
  Public Class AF73BA45B4B94C7585CB3E425D423F5E

    Implements IExternalCommand

    ''' <summary>
    ''' Main entry point for every external command
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
          Dim m_s As New clsSettings(commandData)

          ' Any Rooms?
          If m_s.Rooms.Count = 0 Then
            message = "No rooms found in current model..."
            Return Result.Failed
          End If

          ' Required Template File?
          If Not File.Exists(m_s.FamilyTemplatePath) Then

            ' Error, close
            message = "Mass family template not found: " & m_s.FamilyTemplatePath
            Return Result.Failed

          End If

          ' Construct and Display the Form
          Using d As New form_RoomMasses(m_s)

            ' Show It
            d.ShowDialog()

            ' Success
            Return Result.Succeeded

          End Using

     

      Catch ex As Exception

        ' Failure
        message = ex.Message
        Return Result.Failed

      End Try

    End Function

   
  End Class
End Namespace
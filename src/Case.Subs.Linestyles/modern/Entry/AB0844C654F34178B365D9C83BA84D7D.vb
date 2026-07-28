Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].Subs.Linestyles.Data

Namespace Entry

  ''' <summary>
  ''' Change and Replace Line Styles
  ''' </summary>
  <Transaction(TransactionMode.Manual)>
  Public Class AB0844C654F34178B365D9C83BA84D7D

    Implements IExternalCommand

    ''' <summary>
    ''' Command Entry Point
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
        ' Construct and Display the Form
        Dim d As New form_Main(New clsSettings(commandData))
          ' Show It
          d.ShowDialog()

          ' Success
          Return Result.Succeeded

      
      Catch ex As Exception

        ' Failure
        message = "Change and Replace Line Style Failed..."
        Return Result.Failed

      End Try

    End Function

 

  End Class
End Namespace
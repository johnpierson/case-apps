using System;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Case.ViewCreator.Entry
{

  /// <summary>
  /// Revit 2021 Command Class
  /// </summary>
  /// <remarks></remarks>
  [Transaction(TransactionMode.Manual)]
  public class CmdMain : IExternalCommand
  {

    /// <summary>
    /// Command Entry Point
    /// </summary>
    /// <param name="commandData">Input argument providing access to the Revit application and documents</param>
    /// <param name="message">Return message to the user in case of error or cancel</param>
    /// <param name="elements">Return argument to highlight elements on the graphics screen if Result is not Succeeded.</param>
    /// <returns>Cancelled, Failed or Succeeded</returns>
    public Result Execute(ExternalCommandData commandData,
                            ref string message,
                            ElementSet elements)
    {
      try
      {

        // Construct and Display the form
        form_Main frm = new form_Main(commandData);
        frm.ShowDialog();

        // Return Success
        return Result.Succeeded;
      }
      catch (Exception ex)
      {

        // Failure Message
        message = ex.Message;
        return Result.Failed;

      }
    }
  }
}
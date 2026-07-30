using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Case.ReportGroupsByView.Data;
using Case.ReportGroupsByView.UI;

namespace Case.ReportGroupsByView.Entry
{

  [Transaction(TransactionMode.Manual)]
  public class CmdMain : IExternalCommand
  {

    /// <summary>
    /// Report Groups by View
    /// </summary>
    /// <param name="commandData"></param>
    /// <param name="message"></param>
    /// <param name="elements"></param>
    /// <returns></returns>
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {

      try
      {
          // Settings
        clsSettings m_s = new clsSettings(commandData);

        // Form
        using (form_Main d = new form_Main(m_s))
        {
          d.ShowDialog();
        }

        // Success
        return Result.Succeeded;

      }
      catch (Exception ex)
      {

        // Failure
        message = ex.Message;
        return Result.Failed;

      }

    }
  }
}

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Case.ApplySysOrient.Data;
using Case.ApplySysOrient.UI;
using System;

namespace Case.ApplySysOrient.Entry
{

  [Transaction(TransactionMode.Manual)]
  public class CmdMain : IExternalCommand
  {

    /// <summary>
    /// Duct Command
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

        // Main Category Selection Form
        using (form_Orient dlg = new form_Orient())
        {
          dlg.ShowDialog();

          if (!dlg.DoConduit && !dlg.DoDuct && !dlg.DoPipe & !dlg.DoTray) return Result.Cancelled;

          // Process Data
          if (dlg.DoConduit) m_s.ProcessConduit();
          if (dlg.DoDuct) m_s.ProcessDuct();
          if (dlg.DoPipe) m_s.ProcessPipe();
          if (dlg.DoTray) m_s.ProcessTray();

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
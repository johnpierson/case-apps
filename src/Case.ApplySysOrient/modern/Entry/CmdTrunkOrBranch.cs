using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Case.ApplySysOrient.Data;
using Case.ApplySysOrient.UI;

namespace Case.ApplySysOrient.Entry
{

  [Transaction(TransactionMode.Manual)]
  public class CmdTrunkOrBranch : IExternalCommand
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

		  // Version
		  if (!commandData.Application.Application.VersionName.Contains("2021"))
		  {

			 // Failure
			 message = "";
			 return Result.Failed;

		  }

		  // Settings
		  clsSettings m_s = new clsSettings(commandData);

		  // User Form
		  using (form_MaxSize dlg = new form_MaxSize(m_s))
		  {
			 dlg.ShowDialog();
			 if (!string.IsNullOrEmpty(dlg.ErrorMsg))
			 {
				message = dlg.ErrorMsg;
				return Result.Failed;
			 }
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
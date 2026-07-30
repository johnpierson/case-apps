using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Case.ApplySysOrient.Data
{
  class Util
  {

	 /// <summary>
	 /// Orientation Parameter
	 /// </summary>
	 /// <param name="e"></param>
	 /// <returns></returns>
	 internal static Parameter GetElementOrientationParameter(Element e)
	 {
		Parameter p = e.LookupParameter("CurveOrientation");
		return p;
	 }

	 /// <summary>
	 /// Ask User
	 /// </summary>
	 /// <returns></returns>
	 internal static bool ShowMessage()
	 {
		using (Autodesk.Revit.UI.TaskDialog td = new Autodesk.Revit.UI.TaskDialog("Explanation"))
		{
		  td.TitleAutoPrefix = false;
		  td.MainInstruction = "Update 'CurveOrientation' Parameters";
		  td.MainContent += "This command requires that an instance text parameter named 'CurveOrientation' exist on all elements of the target category.\n\n";
		  td.MainContent += "A text string of HORIZONTAL, VERTICAL, or SLOPED will then get placed into each element's 'CurveOrientation' parameter for the primary purpose of clash detection filtering in an external program.";
        td.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "Update 'CurveOrientation' Parameters");
		  td.AddCommandLink(TaskDialogCommandLinkId.CommandLink2, "Cancel and Do Nothing");
		  if (td.Show() == Autodesk.Revit.UI.TaskDialogResult.CommandLink1)
			 return true;
		  else
			 return false;

		}
	 }



    /// <summary>
    /// Find the Direction Based on Points and Length
    /// </summary>
    /// <param name="length"></param>
    /// <param name="pt1"></param>
    /// <param name="pt2"></param>
    /// <returns></returns>
    internal static string GetDirection(double length, double pt1, double pt2)
    {
      try
      {

        // Slope Test
        double m_pt1Rounded = Math.Round(pt1, 1);
        double m_pt2Rounded = Math.Round(pt2, 1);
        double m_length = Math.Round(length, 1);

        // Horizontal?
        if (m_pt1Rounded == m_pt2Rounded) return "HORIZONTAL";

        // Vertical?
        if (pt1 > pt2)
        {
          double m_resA = m_pt1Rounded - m_pt2Rounded;
          if (m_resA > m_length * 0.999 && m_resA < m_length * 1.001) return "VERTICAL";
        }
        else
        {
          double m_resB = m_pt2Rounded - m_pt1Rounded;
          if (m_resB > m_length * 0.999 && m_resB < m_length * 1.001) return "VERTICAL";
        }

      }
      catch { }

      // Not Vertical Nor Horizontal
      return "SLOPED";

    }

  }
}
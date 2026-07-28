using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Case.ParallelWalls.Data;
using Case.ParallelWalls.UI;
using Microsoft.Win32;

namespace Case.ParallelWalls.Entry
{
  [Transaction(TransactionMode.Manual)]
  public class CmdMain : IExternalCommand
  {
    
    /// <summary>
    /// Command
    /// </summary>
    /// <param name="commandData"></param>
    /// <param name="message"></param>
    /// <param name="elements"></param>
    /// <returns></returns>
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
      try
      {
          Version m_version = typeof(CmdMain).Assembly.GetName().Version;
        
        RegistryKey m_key = Registry.CurrentUser
          .CreateSubKey(@"Software\CASE\Case.ParallelWalls\");

        if (m_key != null && m_key.GetValue(m_version.ToString()) == null)
        {
          form_SplashScreen m_splash = new form_SplashScreen(m_key);
          
          m_splash.ShowDialog();
        }

        clsElementSelection m_selection = new clsElementSelection(commandData.Application.ActiveUIDocument);

        clsVectors m_vectors = new clsVectors(
          m_selection.RefElement,
          m_selection.WallElement);

        clsWallTransform m_transform = new clsWallTransform(
          m_selection.WallElement,
          m_vectors.AxisLine,
          m_vectors.DeltaAngle);


        // Success
        return Result.Succeeded;
      }
      catch (Exception m_ex)
      {
        // Failure
        message = m_ex.Message;
        return Result.Failed;
      }
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Case.ExportSharedParameters.UI;

namespace Case.ExportSharedParameters.Entry
{

  /// <summary>
  /// Shared Parameter Export
  /// </summary>
  [Transaction(TransactionMode.Manual)]
  public class cmd : IExternalCommand
  {

    private Dictionary<string, string> _sharedParams = new Dictionary<string, string>();

    /// <summary>
    /// Command Entry Point
    /// </summary>
    /// <param name="commandData"></param>
    /// <param name="message"></param>
    /// <param name="elements"></param>
    /// <returns></returns>
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {

      try
      {

        // Document
        Document m_doc = commandData.Application.ActiveUIDocument.Document;

        // Session Guid
        string m_guid = Guid.NewGuid().ToString();

        //// New Schedules
        //List<ViewSchedule> m_schedules = new List<ViewSchedule>();

        //// Project Information
        //// Wires
        //// Roads
        //// Rebar Shape
        //// Areas
        //// Materials
        //// Shaft Openings
        //// Views
        //// Sheets

        //// Start a New Transaction
        //using (Transaction t = new Transaction(m_doc))
        //{
        //  if (t.Start("Not Saved") == TransactionStatus.Started)
        //  {

        //    try
        //    {

        //      // Find Categories that Support Custom Parameter Bindings
        //      foreach (Category x in m_doc.Settings.Categories)
        //      {
        //        if (x.AllowsBoundParameters)
        //        {

        //          try
        //          {
        //            // New Schedule
        //            ViewSchedule m_vs = ViewSchedule.CreateSchedule(m_doc, x.Id);
        //            m_vs.Name = x.Name;
        //            m_schedules.Add(m_vs);

        //            if (x.Id.Value == (int)BuiltInCategory.OST_Rooms)
        //            {

        //              foreach (SchedulableField sf in m_vs.Definition.GetSchedulableFields())
        //              {

                        

        //                m_vs.Definition.AddField(sf);

        //                //// Shared Parameters will have a Positive ID
        //                //if (sf.ParameterId.Value > 0)
        //                //{
        //                //  Element m_elem = m_doc.GetElement(sf.ParameterId);
        //                //}

        //              }

        //            }

        //          }
        //          catch { }

        //        }

        //      }

        //      // Commit
        //      t.Commit();
              
        //    }
        //    catch { }
        //  }
        //}


        
        // Show the Progressbar Form
        form_Main m_dlg = new form_Main(m_doc.ParameterBindings.Size + 1);
        m_dlg.Show();

        BindingMap m_map = m_doc.ParameterBindings;

        try
        {

          // Iterate
          DefinitionBindingMapIterator m_iter = m_map.ForwardIterator();
          while (m_iter.MoveNext())
          {

            ElementBinding m_eb = m_iter.Current as ElementBinding;
            Definition m_def = m_iter.Key;

            try
            {

              ExternalDefinition m_extDev = m_def as ExternalDefinition;
              if (m_extDev != null)
              {
                string m_todo = string.Empty;
              }

            }
            catch { }


            //if (m_eb != null)
            //{
            //  CategorySet m_set = m_eb.Categories;
            //}

          }

        }
        catch { }

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
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Electrical;

namespace Case.ApplySysOrient.Data
{
  public class clsSettings
  {

	 ExternalCommandData _cmd;
	 List<Level> _levels;

	 #region Public Properties
	 	 
	 public Autodesk.Revit.ApplicationServices.Application App
	 {
		get
		{
		  try
		  {
			 return _cmd.Application.Application;
		  }
		  catch { }
		  return null;
		}
	 }

	 public UIApplication UiApplication
	 {
		get
		{
		  try
		  {
			 return _cmd.Application;
		  }
		  catch { }
		  return null;
		}
	 }

	 public UIDocument UiDoc
	 {
		get
		{
		  try
		  {
			 return _cmd.Application.ActiveUIDocument;
		  }
		  catch { }
		  return null;
		}
	 }
	 public Document Document
	 {
		get
		{
		  try
		  {
			 return _cmd.Application.ActiveUIDocument.Document;
		  }
		  catch { }
		  return null;
		}
	 }

    /// <summary>
    /// Get all Levels
    /// </summary>
	 public List<Level> Levels
	 {
		get
		{
		  try
		  {
			 if (_levels != null)
				if (_levels.Count > 0) return _levels;

			 // Query
			 _levels = new List<Level>();
			 IEnumerable<Level> m_l = from e in new FilteredElementCollector(Document)
											  .OfClass(typeof(Level))
											  let l = e as Level
											  select l;

			 // Valid Results?
			 _levels = m_l.ToList();

		  }
		  catch { }

		  // New List of Levels
		  return _levels;

		}
	 }

	 #endregion

	 /// <summary>
	 /// Constructor
	 /// </summary>
	 /// <param name="cmd"></param>
	 public clsSettings(ExternalCommandData cmd)
	 {

		// Widen Scope
		_cmd = cmd;

	 }

    #region Internal Members

    /// <summary>
	 /// Get all Duct and Fittings
	 /// </summary>
	 internal void ProcessDuct()
	 {

		try
		{

        int m_iCnt = 0;

        // Query
        IEnumerable<Duct> m_c = from e in new FilteredElementCollector(Document)
                                .OfClass(typeof(Duct))
                                let c = e as Duct
                                select c;

        // Start a New Transaction 
        using (Transaction t = new Transaction(Document))
        {
          if (t.Start("CASE Ductwork Orientation") != TransactionStatus.Started) return;
          try
          {

            foreach (Duct x in m_c.ToList())
            {

              // These are the values we are testing for
              double m_eOff = x.LookupParameter("End Offset").AsDouble();
              double m_sOff = x.LookupParameter("Start Offset").AsDouble();
              double m_length = x.LookupParameter("Length").AsDouble();

              // Choose which Paramter we will write to
              Parameter p = Util.GetElementOrientationParameter(x);
              if (p == null) continue;

              // Get the Value
              p.Set(Util.GetDirection(m_length, m_eOff, m_sOff));

              // Add Counter
              m_iCnt += 1;

            }
          }
          catch { }

          // Commit
          t.Commit();

          // Summary
          using (Autodesk.Revit.UI.TaskDialog td = new Autodesk.Revit.UI.TaskDialog("Duct Orientation"))
          {
            td.TitleAutoPrefix = false;
            td.MainInstruction = "Success!";
            td.MainContent = "Updated " + m_iCnt + " Ducts";
            td.Show();
          }
        }

		}
		catch { }

	 }

	 /// <summary>
	 /// Get all Piping and Fittings
	 /// </summary>
    internal void ProcessPipe()
	 {

		try
		{

        int m_iCnt = 0;

        // Query
        IEnumerable<Pipe> m_c = from e in new FilteredElementCollector(Document)
                                .OfClass(typeof(Pipe))
                                let c = e as Pipe
                                select c;

        // Start a New Transaction 
        using (Transaction t = new Transaction(Document))
        {
          if (t.Start("CASE Piping Orientation") != TransactionStatus.Started) return;
          try
          {
            foreach (Pipe x in m_c.ToList())
            {

              // These are the values we are testing for
              double m_eOff = x.LookupParameter("End Offset").AsDouble();
              double m_sOff = x.LookupParameter("Start Offset").AsDouble();
              double m_length = x.LookupParameter("Length").AsDouble();

              // Choose which Paramter we will write to
              Parameter p = Util.GetElementOrientationParameter(x);
              if (p == null) continue;

              // Get the Value
              p.Set(Util.GetDirection(m_length, m_eOff, m_sOff));

              // Add Counter
              m_iCnt += 1;

            }

            // Commit
            t.Commit();

            // Summary
            using (Autodesk.Revit.UI.TaskDialog td = new Autodesk.Revit.UI.TaskDialog("Pipe Orientation"))
            {
              td.TitleAutoPrefix = false;
              td.MainInstruction = "Success";
              td.MainContent = "Updated " + m_iCnt + " Pipes";
              td.Show();
            }

          }
          catch { }
        }
		}
		catch { }

	 }

    /// <summary>
    /// Process Cable Tray
    /// </summary>
    internal void ProcessTray()
    {

      try
      {

        int m_iCnt = 0;

        // Query
        IEnumerable<CableTray> m_c = from e in new FilteredElementCollector(Document)
                                   .OfClass(typeof(CableTray))
                                     let c = e as CableTray
                                     select c;

        // Start a New Transaction 
        using (Transaction t = new Transaction(Document))
        {
          if (t.Start("CASE Cable Tray Orientation") != TransactionStatus.Started) return;
          try
          {
            foreach (CableTray x in m_c.ToList())
            {

              // These are the values we are testing for
              double m_eOff = x.LookupParameter("End Offset").AsDouble();
              double m_sOff = x.LookupParameter("Start Offset").AsDouble();
              double m_length = x.LookupParameter("Length").AsDouble();

              // Choose which Paramter we will write to
              Parameter p = Util.GetElementOrientationParameter(x);
              if (p == null) continue;

              // Get the Value
              p.Set(Util.GetDirection(m_length, m_eOff, m_sOff));

              // Add Counter
              m_iCnt += 1;

            }
          }
          catch { }

          // Commit
          t.Commit();

          // Summary
          using (Autodesk.Revit.UI.TaskDialog td = new Autodesk.Revit.UI.TaskDialog("Cable Tray Orientation"))
          {
            td.TitleAutoPrefix = false;
            td.MainInstruction = "Success!";
            td.MainContent = "Updated " + m_iCnt + " Cable Trays";
            td.Show();
          }
        }
      }
      catch { }
    }

    /// <summary>
    /// Process Conduits
    /// </summary>
    internal void ProcessConduit()
    {
      
      try
      {



        int m_iCnt = 0;

        // Query
        IEnumerable<Conduit> m_c = from e in new FilteredElementCollector(Document)
                                   .OfClass(typeof(Conduit))
                                   let c = e as Conduit
                                   select c;

        // Start a New Transaction 
        using (Transaction t = new Transaction(Document))
        {
          if (t.Start("CASE Conduit Orientation") != TransactionStatus.Started) return;
          try
          {

            foreach (Conduit x in m_c.ToList())
            {

              // These are the values we are testing for
              double m_eOff = x.LookupParameter("End Offset").AsDouble();
              double m_sOff = x.LookupParameter("Start Offset").AsDouble();
              double m_length = x.LookupParameter("Length").AsDouble();

              // Choose which Paramter we will write to
              Parameter p = Util.GetElementOrientationParameter(x);
              if (p == null) continue;

              // Get the Value
              p.Set(Util.GetDirection(m_length, m_eOff, m_sOff));

              // Add Counter
              m_iCnt += 1;

            }

          }
          catch { }

          // Commit
          t.Commit();

          // Summary
          using (Autodesk.Revit.UI.TaskDialog td = new Autodesk.Revit.UI.TaskDialog("Conduit Orientation"))
          {
            td.TitleAutoPrefix = false;
            td.MainInstruction = "Success!";
            td.MainContent = "Updated " + m_iCnt + " Conduits";
            td.Show();
          }
        }
        
      }
      catch { }

    }

    #endregion

  }
}

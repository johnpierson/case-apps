using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Case.ReportGroupsByView.Data
{
  
  public class clsSettings
  {

    private ExternalCommandData _cmd = null;
    private List<clsData> _report = new List<clsData>();

    #region Public Properties

    /// <summary>
    /// Report Rows
    /// </summary>
    public List<clsData> Report
    {
      get
      {
        try
        {
          if (_report.Count < 1)
          {
            foreach (KeyValuePair<long, List<Group>> x in GroupsByView)
            {
              foreach (Group g in x.Value)
              {
                _report.Add(new clsData(g, x.Key));
              }
            }
          }
        }
        catch { }
        return _report;
      }
    }

    public List<Autodesk.Revit.DB.View> Views = new List<Autodesk.Revit.DB.View>();
    public Dictionary<long, List<Group>> GroupsByView;

    /// <summary>
    /// Active UIDocument
    /// </summary>
    public UIDocument ActiveUIDoc
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

    /// <summary>
    /// Active Document
    /// </summary>
    public Document ActiveDoc
    {
      get
      {
        try
        {
          return ActiveUIDoc.Document;
        }
        catch { }
        return null;
      }
    }

    #endregion

    /// <summary>
    /// Consturctor
    /// </summary>
    /// <param name="cmd"></param>
    public clsSettings(ExternalCommandData cmd)
    {

      // Widen Scope
      _cmd = cmd;

      // Get Views
      GroupsByView = new Dictionary<long, List<Group>>();
      GetViews();

    }

    #region Public Members

    /// <summary>
    /// Save the Data to File
    /// </summary>
    /// <param name="filePath"></param>
    public void SaveDataToCSV(string filePath)
    {
      try
      {
        bool m_first = true;
        using (StreamWriter x = new StreamWriter(filePath, false))
        {
          foreach (clsData d in Report)
          {
            if (m_first)
            {
              x.WriteLine(d.HeaderComma);
              m_first = false;
            }
            x.WriteLine(d.ReportComma);
          }
        }
      }
      catch { }
    }

    /// <summary>
    /// Get a List of Groups for a Given View
    /// </summary>
    /// <param name="v">View to Query</param>
    /// <param name="keepDetail">Keey Detail Groups</param>
    /// <param name="keepModel">Keep Model Groups</param>
    /// <returns></returns>
    public List<Group> GetGroupsByView(Autodesk.Revit.DB.View v, bool keepDetail, bool keepModel)
    {

      // Default
      List<Group> m_groups = new List<Group>();

      try
      {
        if (!keepDetail || !keepModel)
        {
          if (!keepDetail)
          {
            // Query
            IEnumerable<Group> m_g = from e in new FilteredElementCollector(ActiveDoc, v.Id)
                                     .WhereElementIsNotElementType()
                                     .OfClass(typeof(Group))
                                     let g = e as Group
                                     where g.Category.Name.ToLower().Contains("model")
                                     select g;

            // Validate Results
            if (m_g != null)
            {
              m_groups = m_g.ToList();
            }
          }
          else
          {
            // Query
            IEnumerable<Group> m_g = from e in new FilteredElementCollector(ActiveDoc, v.Id)
                                     .WhereElementIsNotElementType()
                                     .OfClass(typeof(Group))
                                     let g = e as Group
                                     where g.Category.Name.ToLower().Contains("detail")
                                     select g;

            // Validate Results
            if (m_g != null)
            {
              m_groups = m_g.ToList();
            }
          }
        }
        else
        {
          // Query
          IEnumerable<Group> m_g = from e in new FilteredElementCollector(ActiveDoc, v.Id)
                                   .WhereElementIsNotElementType()
                                   .OfClass(typeof(Group))
                                   let g = e as Group
                                   select g;

          // Validate Results
          if (m_g != null)
          {
            m_groups = m_g.ToList();
          }
        }

      }
      catch { }

      // Result
      return m_groups;

    }

    #endregion

    #region Private Members

    /// <summary>
    /// Get All Views
    /// </summary>
    private void GetViews()
    {
      try
      {

        // Query
        IEnumerable<Autodesk.Revit.DB.View> m_views = from e in new FilteredElementCollector(ActiveDoc)
                                                      .WhereElementIsNotElementType()
                                                      .OfClass(typeof(Autodesk.Revit.DB.View))
                                                      let v = e as Autodesk.Revit.DB.View
                                                      where (v.ViewType == ViewType.AreaPlan ||
                                                             v.ViewType == ViewType.CeilingPlan ||
                                                             v.ViewType == ViewType.Elevation ||
                                                             v.ViewType == ViewType.FloorPlan ||
                                                             v.ViewType == ViewType.Section ||
                                                             v.ViewType == ViewType.ThreeD)
                                                      where v.IsTemplate == false
                                                      select v;

        // Validate Results
        if (m_views != null)
        {
          Views = m_views.ToList();
        }

      }
      catch { }
    }

    #endregion

  }
}
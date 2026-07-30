using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Case.UngroupAll.Data
{
  
  public class clsSettings
  {

    private ExternalCommandData _cmd = null;

    #region Public Properties

    public List<Group> ModelGroups;
    public List<Group> DetailGroups;

    /// <summary>
    /// Active UIDocument
    /// </summary>
    public UIDocument ActiveUiDoc
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
          return ActiveUiDoc.Document;
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
      _cmd = cmd;
      ModelGroups = new List<Group>();
      DetailGroups = new List<Group>();

      try
      {

        // Query
        IEnumerable<Group> m_query = from e in new FilteredElementCollector(ActiveDoc)
                                       .WhereElementIsNotElementType()
                                       .OfClass(typeof(Group))
                                     let g = e as Group
                                     select g;

        foreach (var x in m_query.ToList())
        {
          if (x.Category.Name.ToLower().Contains("model"))
          {
            ModelGroups.Add(x);
          }
          if (x.Category.Name.ToLower().Contains("detail"))
          {
            DetailGroups.Add(x);
          }
        }

      }
      catch { }
    }

  }
}
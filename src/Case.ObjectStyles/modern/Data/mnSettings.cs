using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Case.ObjectStyles.Data
{
  public class clsSettings
  {
    //Private Property - Keep Out
    private readonly ExternalCommandData _cmd;
    private readonly List<clsData> _report = new List<clsData>();

    //Category List Breakout
    public readonly List<clsFamData> FamReport = new List<clsFamData>();
    public List<Category> AnalyticalCatSet = new List<Category>();
    public List<Category> AnnoCatSet = new List<Category>();
    public List<Category> ModelCatSet = new List<Category>();

    #region Public Properties

    /// <summary>
    ///   Active UIDocument
    /// </summary>
    public UIDocument ActiveUiDoc
    {
      get
      {
        try
        {
          return _cmd.Application.ActiveUIDocument;
        }
        catch (Exception exception)
        {
          Debug.WriteLine(exception.Message);
        }
        return null;
      }
    }

    /// <summary>
    ///   Active Document
    /// </summary>
    public Document ActiveDoc
    {
      get
      {
        try
        {
          return ActiveUiDoc.Document;
        }
        catch (Exception exception)
        {
          Debug.WriteLine(exception.Message);
        }
        return null;
      }
    }

    /// <summary>
    ///   Report Rows
    /// </summary>
    public List<clsData> Report(List<Category> cats)
    {
      try
      {
        if (_report.Count < 1)
        {
          foreach (Category c in cats)
          {
            _report.Add(new clsData(c, false));

            CategoryNameMap map = c.SubCategories;

            if (map != null && map.Size > 0)
            {
              IOrderedEnumerable<Category> orderedMap = from Category cm
                in map
                orderby cm.Name ascending
                select cm;

              foreach (Category subCat in orderedMap)
              {
                _report.Add(new clsData(subCat, true));
              }
            }
          }
        }
      }
      catch (Exception exception)
      {
        Debug.WriteLine(exception.Message);
      }
      return _report;
    }

    #endregion

    #region Public Members

    public void LogData(string logText, Exception ex)
    {
     
    }

    /// <summary>
    ///   Get the Document's Title without the RVT extension
    /// </summary>
    /// <returns></returns>
    public string DocTitle()
    {
      string docTitle = ActiveDoc.Title;
      return docTitle.Remove(docTitle.Length - 4, 4);
    }

    /// <summary>
    ///   Grab all the Current Documents Categories and Split them into 3 lists
    ///   to match UI Tabs.
    /// </summary>
    public void OrganizeCategorySets()
    {
      Categories cats = ActiveDoc.Settings.Categories;
      
      if (cats.IsEmpty) return;

      IOrderedEnumerable<Category> orderedCat = from Category c in cats orderby c.Name ascending select c;

      foreach (Category cat in orderedCat.Where(cat => cat.get_AllowsVisibilityControl(Default3DView())))
      {
        if(cat.Id == ElementId.InvalidElementId) continue;
        
        switch (cat.CategoryType)
        {
          case CategoryType.AnalyticalModel:
            AnalyticalCatSet.Add(cat);
            break;
          case CategoryType.Model:
            ModelCatSet.Add(cat);
            break;
          case CategoryType.Annotation:
            AnnoCatSet.Add(cat);
            break;
        }
      }
    }

    /// <summary>
    ///   Get All Families based on Category
    /// </summary>
    /// <param name="cat">Category Input</param>
    /// <returns></returns>
    public List<Family> FamiliesByCategory(Category cat)
    {
      var famList = new List<Family>();
      try
      {
        famList.AddRange(
          from Family f
            in new FilteredElementCollector(ActiveDoc)
              .OfClass(typeof (Family))
          where f.FamilyCategory.Name == cat.Name && f.IsEditable
          select f
          );
        return famList;
      }
      catch
      {
        return famList;
      }
    }

    /// <summary>
    ///   Save the Data to File
    /// </summary>
    /// <param name="filePath">Where we going?</param>
    /// <param name="cats">List of Categories to Process</param>
    public void SaveDataToCsv(string filePath, List<Category> cats)
    {
      try
      {
        bool mFirst = true;
        using (var x = new StreamWriter(filePath, false))
        {
          foreach (clsData d in Report(cats))
          {
            if (mFirst)
            {
              x.WriteLine(d.HeaderTabs);
              mFirst = false;
            }
            x.WriteLine(d.ReportTabs);
          }
        }
        _report.Clear();
      }
      catch (Exception exception)
      {
        Debug.WriteLine(exception.Message);
      }
    }

    /// <summary>
    ///   Save Family Data to File
    /// </summary>
    /// <param name="filePath">Where we going?</param>
    public void SaveFamDataToCsv(string filePath)
    {
      try
      {
        bool mFirst = true;
        using (var x = new StreamWriter(filePath, false))
        {
          foreach (clsFamData d in FamReport)
          {
            if (mFirst)
            {
              x.WriteLine(d.HeaderTabs);
              mFirst = false;
            }
            x.WriteLine(d.ReportTabs);
          }
        }
        FamReport.Clear();
      }
      catch (Exception exception)
      {
        Debug.WriteLine(exception.Message);
      }
    }

    #endregion

    #region Constructor

    public clsSettings(ExternalCommandData cmd)
    {
      // Widen Scope
      _cmd = cmd;
    }

    #endregion

    #region Private Members

    /// <summary>
    ///   Gets a 3D View (Simplest View Form for all Categories)
    /// </summary>
    /// <returns>View</returns>
    private View Default3DView()
    {
      return new FilteredElementCollector(ActiveDoc)
        .OfClass(typeof (View3D))
        .Cast<View3D>()
        .FirstOrDefault();
    }

    #endregion

    //Family Reports
  }
}
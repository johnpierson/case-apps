using System;
using System.Diagnostics;
using System.Linq;
using Autodesk.Revit.DB;

namespace Case.ObjectStyles.Data
{
  public class clsFamData
  {
    private readonly Family _f; // The Family

    /// <summary>
    /// Get the Family Document Cateory based on the family Category Name
    /// </summary>
    private Category _c
    {
      get { return _fd.Settings.Categories.get_Item(FamCat); }
    }

    private readonly Document _fd; // Passed in Family Document 

    #region Public Properties

    /// <summary>
    /// Get the Family Category Name
    /// </summary>
    public string FamCat
    {
      get { return _f.FamilyCategory.Name; }
    }

    /// <summary>
    /// Get the Family Name
    /// </summary>
    public string FamName
    {
      get { return _f.Name; }
    }

    /// <summary>
    /// Get the Family Path if possible
    /// </summary>
    public string FamSource
    {
      get
      {
        return !string.IsNullOrEmpty(_fd.PathName) ? _fd.PathName : "Off Network";
      }
    }

    /// <summary>
    /// Get the Projection Lineweight
    /// </summary>
    public string FamPrjWeight
    {
      get
      {
        var lineWeight = _c
          .GetLineWeight(GraphicsStyleType.Projection);

        if (lineWeight != null)
          return lineWeight
            .Value
            .ToString();

        return "-";
      }
    }

    /// <summary>
    ///   Get the Cut Lineweight
    ///   Error Checking due to null value issue.
    /// </summary>
    public string FamCutWeight
    {
      get
      {
        if (_c.IsCuttable)
        {
          var lineWeight = _c.GetLineWeight(GraphicsStyleType.Cut);

          if (lineWeight != null)
            return lineWeight
              .Value
              .ToString();

          return "-";
        }
        return "-";
      }
    }

    /// <summary>
    ///   Get the Category Line Color
    /// </summary>
    public string FamLineColor
    {
      get
      {
        string clr = "Invalid Color";
        if (!_c.LineColor.IsValid) return clr;

        try{
          clr = _c.LineColor.Red + ",";
          clr += _c.LineColor.Green + ",";
          clr += _c.LineColor.Blue;
        }
        catch (Exception exception){
          Debug.WriteLine(exception.Message);
        }
        return clr;
      }
    }

    /// <summary>
    ///   Get Material If Available
    /// </summary>
    public string FamMaterial
    {
      get
      {
        return _c.Material == null ? "-" : _c.Material.Name;
      }
    }

    /// <summary>
    ///   Tab Separated Report Line
    /// </summary>
    public string ReportTabs
    {
      get
      {
        string v = string.Empty;

        v += FamCat + "\t" +
             FamName + "\t" +
             FamSource + "\t" +
             FamPrjWeight + "\t" +
             FamCutWeight + "\t" +
             FamLineColor + "\t" +
             FamMaterial + "\t";

        try
        {
          CategoryNameMap subCatList = _c.SubCategories;

          if (subCatList == null || subCatList.Size <= 0) return v;
          v = subCatList.Cast<Category>()
            .Aggregate(v, (current, c) => current + (c.Name + "\t"));
        }
        catch (Exception exception)
        {
          Debug.WriteLine(exception.Message);
        }

        return v;
      }
    }

    /// <summary>
    ///   Tab Separated Report Line
    /// </summary>
    public string HeaderTabs
    {
      get
      {
        return "Category\tFamily\tSource\tProjection Lineweight\tCut Lineweight\tLine Color\tMaterial\tSub-Categories";
      }
    }

    #endregion

    /// <summary>
    ///   Constructor
    /// </summary>
    /// <param name="fd">Family Document</param>
    /// <param name="f">Family Element</param>
    public clsFamData(Document fd, Family f)
    {
      // Widen Scope     
      _f = f;
      _fd = fd;
    }
  }
}
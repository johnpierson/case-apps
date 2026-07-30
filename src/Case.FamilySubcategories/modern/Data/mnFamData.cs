using System;
using System.Diagnostics;
using System.Linq;
using Autodesk.Revit.DB;

namespace Case.FamilySubcategories.Data
{
  public class clsFamData
  {
    
    #region Private Properties

    private readonly Document _fd; // Passed in Family Document 

    /// <summary>
    ///   Get the Family Document Cateory based on the family Category Name
    /// </summary>
    private Category _c
    {
      get { return _fd.OwnerFamily.FamilyCategory; }
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Get the Family Category Name
    /// </summary>
    /// <summary>
    ///   Get the Family Name
    /// </summary>
    public string FamName
    {
      get { return _fd.Title; }
    }

    /// <summary>
    ///   Get the Family Path if possible
    /// </summary>
    public string FamSource
    {
      get { return !string.IsNullOrEmpty(_fd.PathName) ? _fd.PathName : "Off Network"; }
    }

    /// <summary>
    ///   Get the Projection Lineweight
    /// </summary>
    public string FamPrjWeight
    {
      get
      {
        int? lineWeight = _c
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
          int? lineWeight = _c.GetLineWeight(GraphicsStyleType.Cut);

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

        try
        {
          clr = _c.LineColor.Red + ",";
          clr += _c.LineColor.Green + ",";
          clr += _c.LineColor.Blue;
        }
        catch (Exception exception)
        {
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
      get { return _c.Material == null ? "-" : _c.Material.Name; }
    }

    /// <summary>
    ///   Tab Separated Report Line
    /// </summary>
    public string ReportTabs
    {
      get
      {
        string v = null;

        v += _c.Name + "\t" +
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

    #endregion

    #region Constructor
    
    /// <summary>
    ///   Constructor
    /// </summary>
    /// <param name="fd">Family Document</param>
    public clsFamData(Document fd)
    {
      // Widen Scope     
      _fd = fd;
    }
    
    #endregion

  }
}
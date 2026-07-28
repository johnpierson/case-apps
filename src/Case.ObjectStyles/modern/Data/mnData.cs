using System;
using System.Diagnostics;
using Autodesk.Revit.DB;

namespace Case.ObjectStyles.Data
{
  public class clsData
  {
    private readonly Category _c; //The Passed Category
    private readonly bool _sc; // Are we looking at a Sub-Category

    #region Public Properties

    public string CatName
    {
      get { return _c.Name; }
    }

    /// <summary>
    ///   Get the Projection Lineweight
    /// </summary>
    public string CatPrjWeight
    {
      get
      {
        int? lineWeight = _c.GetLineWeight(GraphicsStyleType.Projection);
        return lineWeight != null ? lineWeight.Value.ToString() : "-";
      }
    }

    /// <summary>
    ///   Get the Cut Lineweight
    ///   Error Checking due to null value issue.
    /// </summary>
    public string CatCutWeight
    {
      get
      {
        if (!_c.IsCuttable) return "-";
        int? lineWeight = _c.GetLineWeight(GraphicsStyleType.Cut);
        return lineWeight != null ? lineWeight.Value.ToString() : "-";
      }
    }

    /// <summary>
    ///   Get the Category Line Color
    /// </summary>
    public string CatLineColor
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
    ///   Get Category Material If Available
    /// </summary>
    public string CatMaterial
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

        if (_sc)
        {
          v += "  ";
        }

        v += CatName + "\t" + CatPrjWeight + "\t" + CatCutWeight + "\t" + CatLineColor + "\t" + CatMaterial;
        return v;
      }
    }

    /// <summary>
    ///   Tab Separated Report Line
    /// </summary>
    public string HeaderTabs
    {
      get { return "Category\tProjection LineWeight\tCut Lineweight\tLine Color\tMaterial"; }
    }

    #endregion

    /// <summary>
    ///   Constructor
    /// </summary>
    /// <param name="c">Category</param>
    /// <param name="subCat">is SubCategory</param>
    public clsData(Category c, bool subCat)
    {
      // Widen Scope
      _c = c;
      _sc = subCat;
    }
  }
}
using Autodesk.Revit.DB;

namespace Case.ReportGroupsByView.Data
{
  
  public class clsData
  {

    private Group _g;
    private View _v;

    #region Public Properties

    public string ViewName { get { return _v.Name; } }
    public string ViewKind { get { return _v.ViewType.ToString(); } }
    public string GroupName { get { return _g.Name; } }
    public string GroupKind { get { return _g.Category.Name; } }

    /// <summary>
    /// Tab Separated Report Line
    /// </summary>
    public string ReportTabs 
    { 
      get 
      {
        return ViewName + "\t" + ViewKind + "\t" + GroupName + "\t" + GroupKind + "\t" + _g.Id.Value.ToString();
      } 
    }

    /// <summary>
    /// Tab Comma Report Line
    /// </summary>
    public string ReportComma
    {
      get
      {
        return "\"" + ViewName + "\";\"" + ViewKind + "\";\"" + GroupName + "\";\"" + GroupKind + "\";\"" + _g.Id.Value.ToString() + "\"";
      }
    }

    /// <summary>
    /// Tab Separated Report Line
    /// </summary>
    public string HeaderTabs
    {
      get
      {
        return "View Name\tView Kind\tGroup Name\tGroup Kind\tGroup ElementId";
      }
    }

    /// <summary>
    /// Tab Comma Report Line
    /// </summary>
    public string HeaderComma
    {
      get
      {
        return "\"View Name\";\"View Kind\";\"Group Name\";\"Group Kind\";\"Group ElementId\"";
      }
    }

    #endregion


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="g"></param>
    /// <param name="v"></param>
    public clsData(Group g, long v)
    {

      // Widen Scope
      _g = g;
      _v = _g.Document.GetElement(new ElementId(v)) as View;

    }

  }

}

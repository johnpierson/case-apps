using Autodesk.Revit.DB;

namespace Case.ApplySysOrient.Data
{
  public class clsDuct
  {

	 private Element _e;

	 #region Public Properties

	 public string Kind { get; set; }
	 public double SizeWidth { get; set; }
	 public double SizeHeight { get; set; }

	 #endregion

	 public clsDuct(Element e)
	 {

		// Widen Scope
		_e = e;

	 }

	 /// <summary>
	 /// Get the Element
	 /// </summary>
	 /// <returns></returns>
	 public Element GetElement()
	 {
		return _e;
	 }

  }
}
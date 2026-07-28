using Autodesk.Revit.DB;

namespace Case.ApplySysOrient.Data
{
  public class clsPipe
  {

	 private Element _e;

	 #region Public Properties

	 public string Kind { get; set; }
	 public double Size { get; set; }

	 #endregion

	 public clsPipe(Element e)
	 {

		// Widen Scope
		_e = e;

	 }

	 /// <summary>
	 /// Get the Element
	 /// </summary>
	 /// <returns></returns>
	 internal Element GetElement()
	 {
		return _e;
	 }

  }
}
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace Case.ParallelWalls.Data
{
  /// <summary>
  ///   Class clsReferenceFilter.
  /// </summary>
  public class clsReferenceFilter : ISelectionFilter
  {
    /// <summary>
    ///   Gets or sets the _ document.
    /// </summary>
    /// <value>The _ document.</value>
    internal Document _Doc { get; set; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="clsReferenceFilter" /> class.
    /// </summary>
    /// <param name="document">The document.</param>
    public clsReferenceFilter(Document document)
    {
      _Doc = document;
    }

    /// <summary>
    ///   Allows the element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public bool AllowElement(Element element)
    {
      BuiltInCategory m_cat = (BuiltInCategory) element.Category.Id.Value;

      return m_cat == BuiltInCategory.OST_Lines
             || m_cat == BuiltInCategory.OST_Walls;
    }

    /// <summary>
    ///   Allows the reference.
    /// </summary>
    /// <param name="refer">The refer.</param>
    /// <param name="point">The point.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public bool AllowReference(Reference refer, XYZ point)
    {
      return false;
    }
  }
}
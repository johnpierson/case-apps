using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace Case.ParallelWalls.Data
{
  /// <summary>
  ///   Class clsElementSelection.
  /// </summary>
  internal class clsElementSelection
  {
    /// <summary>
    ///   Gets or sets the _ UI document.
    /// </summary>
    /// <value>The _ UI document.</value>
    internal UIDocument _UiDoc { get; set; }

    /// <summary>
    ///   Gets or sets the _ document.
    /// </summary>
    /// <value>The _ document.</value>
    internal Document _Doc { get; set; }

    /// <summary>
    ///   Gets the reference element.
    /// </summary>
    /// <value>The reference element.</value>
    public Element RefElement { get; private set; }

    /// <summary>
    ///   Gets the wall element.
    /// </summary>
    /// <value>The wall element.</value>
    public Wall WallElement { get; private set; }


    /// <summary>
    ///   Initializes a new instance of the <see cref="clsElementSelection" /> class.
    /// </summary>
    /// <param name="uidoc">The uidoc.</param>
    public clsElementSelection(UIDocument uidoc)
    {
      _UiDoc = uidoc;
      _Doc = _UiDoc.Document;

      GetReferenceElement();
      GetWallElement();
    }

    /// <summary>
    ///   Gets the reference element.
    /// </summary>
    /// <exception cref="Exception">Selected Reference Element was found in Document</exception>
    private void GetReferenceElement()
    {
      clsReferenceFilter m_filter = new clsReferenceFilter(_Doc);

      Reference m_ref = _UiDoc.Selection.PickObject(
        ObjectType.Element,
        m_filter,
        "Select the Reference Element. [Wall,Lines]");

      RefElement = _Doc.GetElement(m_ref);

      if (RefElement == null)
      {
        throw new Exception("Selected Reference Element was found in Document");
      }
    }

    /// <summary>
    ///   Gets the wall element.
    /// </summary>
    /// <exception cref="Exception">Selected Wall Element was found in Document</exception>
    private void GetWallElement()
    {
      clsWallFilter m_filter = new clsWallFilter();

      Reference m_ref = _UiDoc.Selection.PickObject(
        ObjectType.Element,
        m_filter,
        "Select the target Wall. [Wall]");

      WallElement = _Doc.GetElement(m_ref) as Wall;

      if (WallElement == null)
      {
        throw new Exception("Selected Wall Element was found in Document");
      }
    }
  }
}
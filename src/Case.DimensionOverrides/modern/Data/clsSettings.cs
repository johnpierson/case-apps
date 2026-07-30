using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Settings = Case.DimensionOverrides.Properties.Settings;

namespace Case.DimensionOverrides.Data
{
  public class clsSettings
  {
    private readonly ExternalCommandData _cmd;
    public IList<Dimension> Dimensions { get; private set; }
    public int SegmentCount { get; private set; }

    #region Public Properties

    /// <summary>
    ///   Active UIDocument
    /// </summary>
    public UIDocument ActiveUiDoc
    {
      get { return _cmd.Application.ActiveUIDocument; }
    }

    /// <summary>
    ///   Active Document
    /// </summary>
    public Document ActiveDoc
    {
      get { return ActiveUiDoc.Document; }
    }

    private IEnumerable<ElementId> _selCollection
    {
      get { return ActiveUiDoc.Selection.GetElementIds(); }
    }

    private IList<Dimension> _dimensions { get; set; }

    #endregion

    /// <summary>
    ///   Constructor
    /// </summary>
    /// <param name="cmd"></param>
    public clsSettings(ExternalCommandData cmd)
    {
      _cmd = cmd;
      _dimensions = new List<Dimension>();

      try
      {
        if (_selCollection.Any())
        {
          _dimensions = (from m_x in _selCollection
            let m_y = ActiveDoc.GetElement(m_x)
            where new DimensionFilter().AllowElement(m_y)
            select m_y as Dimension)
            .ToList();
        }
        else
        {
          IList<Reference> m_selection = ActiveUiDoc.Selection.PickObjects(
            ObjectType.Element,
            new DimensionFilter(),
            Settings.Default.Prompt_String);

          _dimensions = (from m_x in m_selection
            let m_y = ActiveDoc.GetElement(m_x)
            let m_z = m_y as Dimension
            select m_z)
            .ToList();
        }

        Dimensions = _dimensions;
        SegmentCounter();
      }
      catch
      {
        throw new Exception(Settings.Default.Error_String);
      }
    }

    private void SegmentCounter()
    {
      SegmentCount = 0;
      foreach (Dimension m_dimension 
        in _dimensions.Where(m_dimension => !m_dimension.Segments.IsEmpty))
      {
        SegmentCount += m_dimension.Segments.Size;
      }
    }
  }

  public class DimensionFilter : ISelectionFilter
  {
    public bool AllowElement(Element elem)
    {
      return elem.Category.Id.Value == (int) BuiltInCategory.OST_Dimensions;
    }

    public bool AllowReference(Reference reference, XYZ position)
    {
      return false;
    }
  }
}
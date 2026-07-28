using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Autodesk.Revit.DB;

namespace Case.ParallelWalls.Data
{
  /// <summary>
  ///   Class clsWallTransform.
  /// </summary>
  public class clsWallTransform
  {
    /// <summary>
    ///   The transaction name
    /// </summary>
    internal const string TransactionName = " <CASE-Parallized>";

    /// <summary>
    ///   Gets or sets the _ document.
    /// </summary>
    /// <value>The _ document.</value>
    private Document _Doc { get; set; }

    /// <summary>
    ///   Gets the _ wall.
    /// </summary>
    /// <value>The _ wall.</value>
    public Wall Wall { get; private set; }

    /// <summary>
    ///   Gets the _ axis.
    /// </summary>
    /// <value>The _ axis.</value>
    public Line Axis { get; private set; }

    /// <summary>
    ///   Gets the _ delta.
    /// </summary>
    /// <value>The _ delta.</value>
    public double Delta { get; private set; }


    /// <summary>
    ///   Initializes a new instance of the <see cref="clsWallTransform" /> class.
    /// </summary>
    /// <param name="wall">The wall.</param>
    /// <param name="axis">The axis.</param>
    /// <param name="angle">The angle.</param>
    /// <exception cref="Exception">
    ///   No Wall was present. Must Exit.  +
    ///   [ElementId:  + wall.Id.Value + ]
    /// </exception>
    public clsWallTransform(
      Element wall,
      Line axis,
      double angle)
    {
      Wall = wall as Wall;

      if (Wall == null)
      {
        throw new Exception(
          "No Wall was present. Must Exit. " +
          "[ElementId: " + wall.Id.Value + "]");
      }

      _Doc = Wall.Document;
      Axis = axis.ApproximateLength > 0 ? axis : null;
      Delta = angle;

      RotateWallTransaction();
    }

    /// <summary>
    ///   Rotates the wall transaction.
    /// </summary>
    /// <exception cref="Exception">
    ///   Could Not Checkout Elements. Sync.
    ///   or
    ///   Could not Execute Rotation.
    /// </exception>
    private void RotateWallTransaction()
    {
      using (Transaction m_t = new Transaction(_Doc, TransactionName))
      {
        m_t.Start();

        try
        {
          if (_Doc.IsWorkshared)
          {
            ICollection<ElementId> m_walls = CheckOutElements();

            if (!m_walls.Any())
            {
              throw new Exception("Could Not Checkout Elements. Sync.");
            }

            RotateWall(m_walls.FirstOrDefault());
          }
          else
          {
            RotateWall(Wall.Id);
          }
        }
        catch (Exception m_e)
        {
          Debug.Write(m_e.Message);

          m_t.RollBack();

          throw new Exception("Could not Execute Rotation.");
        }

        m_t.Commit();
      }
    }

    /// <summary>
    ///   Checks the out elements.
    /// </summary>
    /// <returns>ICollection&lt;ElementId&gt;.</returns>
    private ICollection<ElementId> CheckOutElements()
    {
      ICollection<ElementId> m_collection = new Collection<ElementId> {Wall.Id};

      return WorksharingUtils.CheckoutElements(_Doc, m_collection);
    }

    /// <summary>
    ///   Rotates the wall.
    /// </summary>
    /// <param name="wallId">The wall identifier.</param>
    /// <exception cref="Exception">Axis line was not valid.</exception>
    private void RotateWall(ElementId wallId)
    {
      if (Axis == null)
      {
        throw new Exception("Axis line was not valid.");
      }

      ElementTransformUtils.RotateElement(
        _Doc,
        wallId,
        Axis,
        Delta
        );

      SetCommentsForAffectedWall();
    }

    /// <summary>
    ///   By Request of the Internal CASE Projects Group, they want to track
    ///   the affected walls using the Comments Parameter. Adding the value to
    ///   the end of the Existing Comments to ensure that no data loss ensue`s.
    /// </summary>
    private void SetCommentsForAffectedWall()
    {
      Parameter m_param = Wall.get_Parameter(
        BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);

      string m_value = m_param.AsString();

      m_param.Set(m_value + TransactionName);
    }
  }
}
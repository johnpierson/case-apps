using System;
using Autodesk.Revit.DB;

namespace Case.ParallelWalls.Data
{
  /// <summary>
  ///   Class clsVectors.
  /// </summary>
  public class clsVectors
  {
    /// <summary>
    ///   Gets or sets the _ wall.
    /// </summary>
    /// <value>The _ wall.</value>
    private Wall _Wall { get; set; }

    /// <summary>
    ///   Gets or sets the _ reference.
    /// </summary>
    /// <value>The _ reference.</value>
    private Element _Ref { get; set; }

    /// <summary>
    ///   Gets the _ document.
    /// </summary>
    /// <value>The _ document.</value>
    internal Document _Doc
    {
      get { return _Wall.Document; }
    }

    /// <summary>
    ///   Gets the _ wall location.
    /// </summary>
    /// <value>The _ wall location.</value>
    internal LocationCurve _WallLocation
    {
      get { return _Wall.Location as LocationCurve; }
    }

    /// <summary>
    ///   Gets the _ wall curve.
    /// </summary>
    /// <value>The _ wall curve.</value>
    internal Curve _WallCurve
    {
      get { return _WallLocation.Curve; }
    }

    /// <summary>
    ///   Gets the _ reference location.
    /// </summary>
    /// <value>The _ reference location.</value>
    internal LocationCurve _RefLocation
    {
      get { return _Ref.Location as LocationCurve; }
    }

    /// <summary>
    ///   Gets the _ reference curve.
    /// </summary>
    /// <value>The _ reference curve.</value>
    internal Curve _RefCurve
    {
      get { return _RefLocation.Curve; }
    }

    /// <summary>
    ///   Gets the reference line.
    /// </summary>
    /// <value>The reference line.</value>
    public Line RefLine { get; private set; }

    /// <summary>
    ///   Gets the wall line.
    /// </summary>
    /// <value>The wall line.</value>
    public Line WallLine { get; private set; }

    /// <summary>
    ///   Gets the reference angle.
    /// </summary>
    /// <value>The reference angle.</value>
    public double RefAngle { get; private set; }

    /// <summary>
    ///   Gets the wall angle.
    /// </summary>
    /// <value>The wall angle.</value>
    public double WallAngle { get; private set; }

    /// <summary>
    ///   Gets the delta angle.
    /// </summary>
    /// <value>The delta angle.</value>
    public double DeltaAngle { get; private set; }

    /// <summary>
    ///   Gets the axis line.
    /// </summary>
    /// <value>The axis line.</value>
    public Line AxisLine { get; private set; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="clsVectors" /> class.
    /// </summary>
    /// <param name="refElement">The reference element.</param>
    /// <param name="wall">The wall.</param>
    /// <exception cref="Exception">No Locations were found.</exception>
    public clsVectors(
      Element refElement,
      Wall wall)
    {
      _Wall = wall;
      _Ref = refElement;

      if (_WallLocation == null || _RefLocation == null)
      {
        throw new Exception("No Locations were found.");
      }

      GetOriginalAngles();
      GetDeltaAngle();
      CreateRotationAxis();
    }

    /// <summary>
    ///   Gets the original angles.
    /// </summary>
    private void GetOriginalAngles()
    {
      XYZ m_r0 = _RefCurve.GetEndPoint(0);
      XYZ m_r1 = _RefCurve.GetEndPoint(1);

      XYZ m_w0 = _WallCurve.GetEndPoint(0);
      XYZ m_w1 = _WallCurve.GetEndPoint(1);

      WallLine = Line.CreateBound(m_w0, m_w1);
      RefLine = Line.CreateBound(m_r0, m_r1);

      RefAngle = XYZ.BasisX.AngleTo(RefLine.Direction);
      WallAngle = XYZ.BasisX.AngleTo(WallLine.Direction);
    }

    /// <summary>
    ///   Gets the delta angle.
    /// </summary>
    private void GetDeltaAngle()
    {
      NegativeAngleTest();

      DeltaAngle = RefAngle - WallAngle;

      if (DeltaAngle >= Math.PI/2) DeltaAngle -= Math.PI;
      if (DeltaAngle <= -Math.PI/2) DeltaAngle += Math.PI;
    }

    /// <summary>
    ///   Negatives the angle test.
    /// </summary>
    private void NegativeAngleTest()
    {
      if (RefLine.Direction.Y < 0) RefAngle = -RefAngle;
      if (WallLine.Direction.Y < 0) WallAngle = -WallAngle;
    }

    /// <summary>
    ///   Creates the rotation axis.
    /// </summary>
    private void CreateRotationAxis()
    {
      XYZ m_wMid = _WallCurve.Evaluate(0.5, true);
      XYZ m_wMidTop = new XYZ(m_wMid.X, m_wMid.Y, m_wMid.Z + 1);

      AxisLine = Line.CreateBound(m_wMid, m_wMidTop);
    }
  }
}
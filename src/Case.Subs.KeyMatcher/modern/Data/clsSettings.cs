using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Newtonsoft.Json;

namespace Case.Subs.KeyMatcher.Data
{

  public class clsSettings
  {

    private ExternalCommandData _cmd;

    #region Public Properties
    
    #endregion

    #region Internal Properties

    /// <summary>
    /// Active UIDocument
    /// </summary>
    internal UIDocument ActiveUiDoc
    {
      get
      {
        try
        {
          return _cmd.Application.ActiveUIDocument;
        }
        catch { }
        return null;
      }
    }

    /// <summary>
    /// Active Document
    /// </summary>
    internal Document ActiveDoc
    {
      get
      {
        try
        {
          return ActiveUiDoc.Document;
        }
        catch { }
        return null;
      }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Consturctor
    /// </summary>
    /// <param name="cmd"></param>
    public clsSettings(ExternalCommandData cmd)
    {

      // Widen Scope
      _cmd = cmd;

      // Views & Data

    }

    #endregion

    #region Private Members

    #endregion

    #region Internal Members

    #endregion

  }
}
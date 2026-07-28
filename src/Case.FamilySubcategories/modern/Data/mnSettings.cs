using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Case.FamilySubcategories.Data
{
  public class clsSettings
  {
    #region Private Properties

    private readonly ExternalCommandData _cmd;

    #endregion

    #region Public Properties

    /// <summary>
    ///   The Active Application from the UI
    /// </summary>
    public Application ActiveApp
    {
      get
      {
        try
        {
          return _cmd.Application.Application;
        }
        catch (Exception exception)
        {
          Debug.WriteLine(exception.Message);
        }
        return null;
      }
    }

    #endregion

    #region Public Members

    /// <summary>
    ///   Just a List of FamilyPaths
    /// </summary>
    public List<string> FamilyPathList = new List<string>();

    /// <summary>
    ///   Get a List of the Family Paths to Process
    /// </summary>
    /// <param name="path"></param>
    /// <param name="subdir"></param>
    /// <returns></returns>
    public List<string> FamiliesToProcess(String path, bool subdir)
    {
      string[] pathList = Directory.GetFiles(path);

      foreach (string fn in pathList)
      {
        if (!fn.EndsWith(".rfa")) continue;
        FamilyPathList.Add(fn);
      }
      if (!subdir) return FamilyPathList;

      string[] subDirPathList = Directory.GetDirectories(path);

      foreach (string sd in subDirPathList)
      {
        FamiliesToProcess(sd, true);
      }
      return FamilyPathList;
    }


    /// <summary>
    ///   Process Family from Path
    /// </summary>
    /// <param name="fp">Family Path</param>
    /// <returns>String of Report Row</returns>
    public string ProcessFamily(string fp)
    {
      Document fd = ActiveApp.OpenDocumentFile(fp);

      string rt = null;

      if (fd != null)
      {
        var famData = new clsFamData(fd);
        if (famData.ReportTabs == null) rt = famData.ReportTabs;
        fd.Close(false);
      }

      return rt ?? "error";
    }

    #endregion

    #region Constructor

    /// <summary>
    ///   Constructor
    /// </summary>
    /// <param name="cmd"></param>
    public clsSettings(ExternalCommandData cmd)
    {
      // Widen Scope
      _cmd = cmd;
    }

    #endregion
  }
}
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Case.Subs.KeyMatcher.Utility
{
  public static class clsGlobals
  {

    private static string _assemblyPath;
    private static string _assemblyFullName;
    private static string _assemblyVersion;

    public static string RevitYear { get; set; } = "2023";
    public const string ClientName = "CASE";
    public const string ProjectName = "Key Matcher";
    public const string NotApplicable = "<n/a>";

    /// <summary>
    /// Revit Style Messagebox
    /// </summary>
    /// <param name="headerText"></param>
    /// <param name="largeText"></param>
    /// <param name="smallText"></param>
    internal static void ShowTaskDialog(string headerText, string largeText, string smallText)
    {
      using (Autodesk.Revit.UI.TaskDialog td = new Autodesk.Revit.UI.TaskDialog(headerText))
      {
        td.CommonButtons = Autodesk.Revit.UI.TaskDialogCommonButtons.Ok;
        td.DefaultButton = Autodesk.Revit.UI.TaskDialogResult.Ok;
        td.TitleAutoPrefix = false;
        td.MainInstruction = largeText;
        td.MainContent = smallText;
        td.Show();
      }
    }

    /// <summary>
    /// Sort an Observable Collection
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="source"></param>
    /// <param name="keyItem1"></param>
    public static void Sort<TSource, TKey>(this Collection<TSource> source, Func<TSource, TKey> keyItem1)
    {
      var m_sortedList = source.OrderBy(keyItem1).ToList();
      source.Clear();
      foreach (var m_sortedItem in m_sortedList)
      {
        source.Add(m_sortedItem);
      }
    }

    /// <summary>
    /// Sort an Observable Collection
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="source"></param>
    /// <param name="keyItem1"></param>
    public static void SortBackwards<TSource, TKey>(this Collection<TSource> source, Func<TSource, TKey> keyItem1)
    {
      var m_sortedList = source.OrderByDescending(keyItem1).ToList();
      source.Clear();
      foreach (var m_sortedItem in m_sortedList)
      {
        source.Add(m_sortedItem);
      }
    }

    /// <summary>
    /// Logging
    /// </summary>
    /// <param name="message"></param>
    /// <param name="level"></param>
    internal static void LogMessage(string message, int level = 0)
    {
      Console.WriteLine(message);
      Debug.WriteLine(message);
      if (level > 0) Autodesk.Revit.UI.TaskDialog.Show(ClientName, message);
    }

    /// <summary>
    /// Current Assembly Path
    /// </summary>
    /// <returns></returns>
    public static String GetAssemblyPath()
    {
      if (string.IsNullOrEmpty(_assemblyPath))
        _assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      return _assemblyPath;
    }

    /// <summary>
    /// Assembly Name
    /// </summary>
    /// <returns></returns>
    public static String GetAssemblyFullName()
    {
      if (string.IsNullOrEmpty(_assemblyFullName))
        _assemblyFullName = Assembly.GetExecutingAssembly().Location;
      return _assemblyFullName;
    }

    /// <summary>
    /// Assembly Name
    /// </summary>
    /// <returns></returns>
    public static String GetAssemblyVersion()
    {
      if (string.IsNullOrEmpty(_assemblyVersion))
      {
        Assembly assembly = Assembly.GetExecutingAssembly();
        FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
        _assemblyVersion = fvi.FileVersion;
      }
      return _assemblyVersion;
    }

    #region Internal Members - Parameter Values - Set

    /// <summary>
    /// Set a string parameter value
    /// </summary>
    /// <param name="p"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    internal static void SetValueParameter(Parameter p, string s)
    {
      try
      {
        if (p != null)
        {
          string m_log;
          if (p.StorageType == StorageType.String)
          {
            m_log = p.Definition.Name + "\t" + p.AsString() + "\t" + s;
            p.Set(s);
          }
          else
          {
            m_log = p.Definition.Name + "\t" + p.AsValueString() + "\t" + s;
            p.SetValueString(s);
          }

          LogData(m_log);
        }
        else
        {
          // Not Found
          LogData("(Not Found)");
        }
      }
      catch { LogData("FAILURE"); }
    }

    /// <summary>
    /// Set a double parameter value
    /// </summary>
    /// <param name="p"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    internal static void SetValueParameter(Parameter p, double d)
    {
      try
      {
        if (p != null)
        {
          string m_log = p.Definition.Name + "\t" + p.AsDouble() + "\t" + d;
          LogData(m_log);
          p.Set(d);
        }
        else
        {
          // Not Found
          LogData("(Not Found)");
        }
      }
      catch { LogData("FAILURE"); }
    }

    /// <summary>
    /// Set an integer parameter value
    /// </summary>
    /// <param name="p"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    internal static void SetValueParameter(Parameter p, int i)
    {
      try
      {
        if (p != null)
        {
          string m_log = p.Definition.Name + "\t" + p.AsInteger() + "\t" + i;
          LogData(m_log);
          p.Set(i);
        }
        else
        {
          // Not Found
          LogData("(Not Found)");
        }
      }
      catch { LogData("FAILURE"); }
    }

    #endregion

    #region Internal Members - Parameter Values - Get

    /// <summary>
    /// Get a string parameter value as a string
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    internal static string GetValueString(Parameter p)
    {
      try
      {
        if (p != null)
        {
          if (p.StorageType == StorageType.None) return "";
          return p.StorageType == StorageType.String ? p.AsString() : p.AsValueString();
        }

      }
      catch { }
      return "";
    }

    /// <summary>
    /// Get a string parameter value as a double
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    internal static double GetValueDouble(Parameter p)
    {
      try
      {
        if (p != null && p.HasValue)
          return p.AsDouble();
      }
      catch { }
      return 0.0;
    }

    /// <summary>
    /// Get a string parameter value as a integer
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    internal static Int32 GetValueInteger(Parameter p)
    {
      try
      {
        if (p != null && p.HasValue)
          return p.AsInteger();
      }
      catch { }
      return 0;
    }

    /// <summary>
    /// Get a string parameter value as a ElementID
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    internal static ElementId GetValueElementId(Parameter p)
    {
      try
      {
        if (p != null && p.HasValue)
          return p.AsElementId();
      }
      catch { }
      return ElementId.InvalidElementId;
    }

    #endregion

    /// <summary>
    /// Write text to Log
    /// </summary>
    /// <param name="s"></param>
    internal static void LogData(string s)
    {
      //try
      //{
      //  using (StreamWriter sw = new StreamWriter(@"C:\Temp\SomeLogName.log", true))
      //  {
      //    sw.WriteLine(s);
      //  }
      //}
      //catch { }
    }

  }
}
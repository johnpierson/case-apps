using System;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System.Linq;

namespace Case.ViewCreator
{
  public partial class form_Main : System.Windows.Forms.Form
  {
    // Private Variables
    private Document _doc;
    private ExternalCommandData _cmd;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="p_commandData"></param>
    public form_Main(ExternalCommandData p_commandData)
    {
      InitializeComponent();

      // Set form title
      this.Text = "Create Views by Discipline v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
      this.progressBar1.Hide();

      // Widen Scope
      _cmd = p_commandData;
      _doc = _cmd.Application.ActiveUIDocument.Document;

      // Get Revit Product type
      if (app.myProduct == "NotMEP")
      {
        this.textSubDiscipline.ReadOnly = true;
      }

      // Execute
      populateViewType();
      populateViewDiscipline();
      populateCheckListBox(_doc);

    }

    /// <summary>
    /// Cancel Button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnCancel_Click(object sender, EventArgs e)
    {
      // Close Main Form
      this.Close();
    }

    /// <summary>
    /// Commit View Creations
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnCreate_Click(object sender, EventArgs e)
    {

      // Progressbar and Controls Visibility
      this.progressBar1.Minimum = 0;
      this.progressBar1.Value = 0;
      this.progressBar1.Maximum = this.checkedListBox1.CheckedItems.Count;
      this.progressBar1.Show();
      this.btnCreate.Hide();
      this.btnCancel.Hide();
      this.ButtonHelp.Hide();
      System.Windows.Forms.Application.DoEvents();

      try
      {
        // Record Usage
        string m_path = "";
        if (_doc.IsWorkshared == true)
        {

          // Model Path Object
          ModelPath m_mp = _doc.GetWorksharingCentralModelPath();
          m_path = m_mp.CentralServerPath;

        }
        else
        {
          m_path = _doc.PathName.ToString();
        }

        if (string.IsNullOrEmpty(m_path))
        {
          m_path = "Detached Model";
        }
        
      }
      // Catch Exception
      catch
      {
      }

      try
      {
        // Execute function to create views and confirm success
        if (makeViews(_doc) == true)
        {

          // Moved this into function to show what happened
          // MessageBox.Show("View Creation Complete!", "Revit");
        }
      }
      // Catch Exception
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Revit");
      }

      // Close the Form
      this.Close();

    }

    public class ViewTypeName
    {
      public string Name { get; set; }
      public string Value { get; set; }
    }

    public class ViewDisciplineName
    {
      public string Name { get; set; }
      public string Value { get; set; }
    }

    /// <summary>
    /// Populate the View types
    /// </summary>
    private void populateViewType()
    {

      // Start with a clean list
      this.cbViewType.Items.Clear();

      // Hardcoded list
      var dataSource = new List<ViewTypeName>();
      dataSource.Add(new ViewTypeName() { Name = "Floor Plan", Value = "Floor Plan" });
      dataSource.Add(new ViewTypeName() { Name = "Reflected Ceiling Plan", Value = "Ceiling Plan" });

      // Bind to list displaying name
      this.cbViewType.DataSource = dataSource;
      this.cbViewType.DisplayMember = "Name";
      this.cbViewType.ValueMember = "Value";

    }

    /// <summary>
    /// Populate the View Disciplines
    /// </summary>
    private void populateViewDiscipline()
    {

      // Start with a clean list
      this.cbViewDiscipline.Items.Clear();

      // Hardcoded list
      var dataSource = new List<ViewDisciplineName>();
      dataSource.Add(new ViewDisciplineName() { Name = "Architectural", Value = "Architectural" });
      dataSource.Add(new ViewDisciplineName() { Name = "Structural", Value = "Structural" });
      dataSource.Add(new ViewDisciplineName() { Name = "Mechanical", Value = "Mechanical" });
      dataSource.Add(new ViewDisciplineName() { Name = "Electrical", Value = "Electrical" });
      dataSource.Add(new ViewDisciplineName() { Name = "Coordination", Value = "Coordination" });

      // Bind to list displaying name
      this.cbViewDiscipline.DataSource = dataSource;
      this.cbViewDiscipline.DisplayMember = "Name";
      this.cbViewDiscipline.ValueMember = "Value";

    }

    /// <summary>
    /// Populate the checked list box with all views in current document
    /// </summary>
    /// <param name="doc"></param>
    private void populateCheckListBox(Document doc)
    {

      // Start with a clean list
      this.checkedListBox1.Items.Clear();

      // Collect Levels
      FilteredElementCollector collector = new FilteredElementCollector(doc);
      ICollection<Element> collection = collector.OfClass(typeof(Level)).ToElements();

      // Bind to list displaying name
      this.checkedListBox1.DataSource = collection;
      this.checkedListBox1.DisplayMember = "Name";
    }

    /// <summary>
    /// Function to Select-Deselect all elements in list
    /// </summary>
    /// <param name="bSelected"></param>
    private void SelectDeselectAll(bool bSelected)
    {
      for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
      {
        if (bSelected)
          this.checkedListBox1.SetItemChecked(i, true);
        else
          this.checkedListBox1.SetItemChecked(i, false);
      }
    }

    /// <summary>
    /// Bind Select-Deselect function to check box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
    {
      if (this.checkBoxAll.Checked)
        SelectDeselectAll(true);
      else
        SelectDeselectAll(false);
    }

    /// <summary>
    /// Main function to create all views
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    private bool makeViews(Document doc)
    {

      // Get all View Type Families for Floor Plans
      IEnumerable<Element> viewFTypesFP =
        from e in new FilteredElementCollector(_doc).OfClass(typeof(ViewFamilyType))
        let type = e as ViewFamilyType
        where type.ViewFamily == ViewFamily.FloorPlan
        select type;

      // Get all View Type Families for Floor Plans
      IEnumerable<Element> viewFTypesCP =
        from e in new FilteredElementCollector(_doc).OfClass(typeof(ViewFamilyType))
        let type = e as ViewFamilyType
        where type.ViewFamily == ViewFamily.CeilingPlan
        select type;

      // Failures and Success
      string m_fails = "";
      string m_sucess = "";

      // Check that atleast on level is checked
      if (this.checkedListBox1.CheckedItems.Count == 0)
      {
        MessageBox.Show("Please choose at least one Level to create a view.", "Hold On Silly...");
        return false;
      }
      else
      {

        // Loop through each item checked
        foreach (Object obj in this.checkedListBox1.CheckedItems)
        {

          // Step Progress
          this.progressBar1.Increment(1);
          System.Windows.Forms.Application.DoEvents();

          // Get the level
          Level level = obj as Level;
          string viewdiscipline = this.cbViewDiscipline.SelectedValue.ToString();
          string viewtype = this.cbViewType.SelectedValue.ToString();
          string viewtypename = this.cbViewType.SelectedValue.ToString();
          string subdiscipline = this.textSubDiscipline.Text;
          string viewname = "";

          if (app.myProduct == "MEP")
          {
            viewname = subdiscipline + " " + level.Name + " " + viewtypename;
          }
          else
          {
            viewname = viewdiscipline + " " + level.Name + " " + viewtypename;
          }

          viewname = viewname.ToUpper();

          // Check that sub-discipline box has been filled
          if (app.myProduct == "MEP" && subdiscipline.Trim() == "")
          {
            MessageBox.Show("Sub-Discipline can not be blank.", "Hold On ...");
            return false;
          }
          else
          {
            // Start Transaction
            Transaction t = new Transaction(doc, "Create " + viewdiscipline + " " + level.Name + " " + viewtype);
            t.Start();

            // View Name (for errors)
            string m_viewName = viewdiscipline + " " + level.Name + " " + viewtype;

            try
            {
              // Chcek viewtype
              if (viewtype == "Floor Plan")
              {
                // Create the view
                ViewPlan viewplan = ViewPlan.Create(_doc, viewFTypesFP.First().Id, level.Id);

                // Set the discipline based on combobox selection
                if (viewdiscipline == "Architectural")
                {
                  viewplan.get_Parameter(BuiltInParameter.VIEW_DISCIPLINE).Set(1);
                }
                else if (viewdiscipline == "Structural")
                {
                  viewplan.get_Parameter(BuiltInParameter.VIEW_DISCIPLINE).Set(2);
                }
                else if (viewdiscipline == "Mechanical")
                {
                  viewplan.get_Parameter(BuiltInParameter.VIEW_DISCIPLINE).Set(4);
                }
                else if (viewdiscipline == "Electrical")
                {
                  viewplan.get_Parameter(BuiltInParameter.VIEW_DISCIPLINE).Set(8);
                }
                else if (viewdiscipline == "Coordination")
                {
                  viewplan.get_Parameter(BuiltInParameter.VIEW_DISCIPLINE).Set(4095);
                }
                else
                {
                  MessageBox.Show("Invalid Discipline", "Error");
                  break;
                }

                if (app.myProduct == "MEP")
                {
                  // Set Sub-Discipline based on text box entry
                  clsParameterData.GetParameterByName(viewplan, "Sub-Discipline").Set(subdiscipline);
                }
              }
              else
              {
                // Check viewtype
                ViewPlan viewplan = ViewPlan.Create(_doc, viewFTypesCP.First().Id, level.Id);

                // Set the discipline based on combobox selection
                if (viewdiscipline == "Architectural")
                {
                  viewplan.get_Parameter(BuiltInParameter.VIEW_DISCIPLINE).Set(1);
                }
                else if (viewdiscipline == "Structural")
                {
                  viewplan.get_Parameter(BuiltInParameter.VIEW_DISCIPLINE).Set(2);
                }
                else if (viewdiscipline == "Mechanical")
                {
                  viewplan.get_Parameter(BuiltInParameter.VIEW_DISCIPLINE).Set(4);
                }
                else if (viewdiscipline == "Electrical")
                {
                  viewplan.get_Parameter(BuiltInParameter.VIEW_DISCIPLINE).Set(8);
                }
                else if (viewdiscipline == "Coordination")
                {
                  viewplan.get_Parameter(BuiltInParameter.VIEW_DISCIPLINE).Set(4095);
                }
                else
                {
                  MessageBox.Show("Invalid Discipline", "Error");
                  break;
                }

                if (app.myProduct == "MEP")
                {
                  // Set the discipline based on combobox selection
                  clsParameterData.GetParameterByName(viewplan, "Sub-Discipline").Set(subdiscipline);
                }
              }

              // Commit
              m_sucess += "\n" + m_viewName.ToUpper();
              t.Commit();

            }
            catch
            {

              // Rollback on Failure
              t.RollBack();
              m_fails += "\n" + m_viewName.ToUpper();
              // return false;

            }
          }
        }
      }

      // Report to the user
      Autodesk.Revit.UI.TaskDialog m_td = new Autodesk.Revit.UI.TaskDialog("Here's What Happend:");
      m_td.MainContent = "Successful Views:" + m_sucess + "\n\nFailed Views:" + m_fails;
      m_td.MainInstruction = "Created Some Views... or Not:";
      m_td.Show();
      return true;

    }

    /// <summary>
    /// Help Button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonHelp_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://apps.case-inc.com/content/create-plan-views-discipline");
    }
  }
}

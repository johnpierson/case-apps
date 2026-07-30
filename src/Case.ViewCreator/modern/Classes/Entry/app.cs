using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Case.ViewCreator
{
    /// <summary>
    /// Revit 2021 API Application Class
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class app : IExternalApplication
    {
        // ExternalCommands assembly path
        static string m_Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        // Button icons directory
        static string ButtonIconsFolder = Path.GetDirectoryName(m_Path);

        //Cached Variables
        public static UIControlledApplication m_uiApp;
        public static string myProduct = "";

        /// <summary>
        /// Fires off when Revit Session Starts
        /// </summary>
        /// <param name="application">An object that is passed to the external application which contains the controlled application.</param>
        /// <returns>Return the status of the external application. A result of Succeeded means that the external application successfully started. Cancelled can be used to signify that the user cancelled the external operation at some point. If false is returned then Revit should inform the user that the external application failed to load and the release the internal reference.</returns>
        public Result OnStartup(UIControlledApplication a)
        {
            try
            {
                ProductType pt = a.ControlledApplication.Product;

                if (ProductType.MEP == pt)
                {
                    myProduct = "MEP";
                }
                else
                {
                    myProduct = "NotMEP";
                }

                m_uiApp = a;
                // Add the Ribbon Panel
                CreateRibbonPanel(a);
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ribbon");

                // Return Failure
                return Result.Failed;
            }
        }

        /// <summary>
        /// Fires off when Revit Session Ends
        /// </summary>
        /// <param name="application">An object that is passed to the external application which contains the controlled application.</param>
        /// <returns>Return the status of the external application. A result of Succeeded means that the external application successfully shutdown. Cancelled can be used to signify that the user cancelled the external operation at some point. If false is returned then the Revit user should be warned of the failure of the external application to shut down correctly.</returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            try
            {
                //TODO: add you code below.
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return Result.Failed;
            }
        }

        /// <summary>
        /// Create the Panel
        /// </summary>
        /// <param name="uiapp"></param>
        public void CreateRibbonPanel(UIControlledApplication uiapp)
        {
            try
            {
                // Create a custom ribbon tab
                uiapp.CreateRibbonTab("Case Design Inc.");
            }
            catch
            {
                // Might Already Exist
            }
            // Tools
            AddButton(
                "Free Tools",
                "ViewCreator",
                "View\nCreator",
                m_Path + "\\Case.ViewCreator.16.png",
                m_Path + "\\Case.ViewCreator.32.png",
                m_Path + "\\Case.ViewCreator.dll",
                "Case.ViewCreator.cmd",
                "Create New Views");
        }

        /// <summary>
        /// Add a button to a Ribbon Tab
        /// </summary>
        /// <param name="Rpanel">The name of the ribbon panel</param>
        /// <param name="ButtonName">The Name of the Button</param>
        /// <param name="ButtonText">Command Text</param>
        /// <param name="ImagePath16">Small Image</param>
        /// <param name="ImagePath32">Large Image</param>
        /// <param name="dllPath">Path to the DLL file</param>
        /// <param name="dllClass">Full qualified class descriptor</param>
        /// <param name="Tooltip">Tooltip to add to the button</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool AddButton(
            string Rpanel,
            string ButtonName,
            string ButtonText,
            string ImagePath16,
            string ImagePath32,
            string dllPath,
            string dllClass,
            string Tooltip)
        {
            try
            {
                // The Ribbon Panel
                RibbonPanel m_RibbonPanel = null;

                // Find the Panel within the Case Tab                    
                List<RibbonPanel> m_RP = new List<RibbonPanel>();
                m_RP = m_uiApp.GetRibbonPanels("Case Design Inc.");
                foreach (RibbonPanel x in m_RP)
                {
                    if (x.Name.ToUpper() == Rpanel.ToUpper())
                    {
                        m_RibbonPanel = x;
                    }
                }
                // Create the panel if it doesn't exist
                if (m_RibbonPanel == null)
                {
                    m_RibbonPanel = m_uiApp.CreateRibbonPanel("Case Design Inc.", Rpanel);
                }
                // Create the Pushbutton Data
                PushButtonData m_PushButtonData = new PushButtonData(ButtonName, ButtonText, dllPath, dllClass);
                if (!string.IsNullOrEmpty(ImagePath16))
                {
                    m_PushButtonData.Image = new BitmapImage(new Uri(ImagePath16));
                }
                if (!string.IsNullOrEmpty(ImagePath32))
                {
                    m_PushButtonData.LargeImage = new BitmapImage(new Uri(ImagePath32));
                }
                m_PushButtonData.ToolTip = Tooltip;

                // Add the button to the tab
                PushButton m_PushButtonData_Add = m_RibbonPanel.AddItem(m_PushButtonData) as PushButton;
            }
            catch (Exception)
            {
                // Quiet Fail
            }
            return true;
        }
    }
}

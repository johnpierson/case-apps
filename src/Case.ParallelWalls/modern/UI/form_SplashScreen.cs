using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Case.ParallelWalls.UI
{
  public partial class form_SplashScreen : Form
  {

    private readonly RegistryKey _key;
    private string _version;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="key"></param>
    public form_SplashScreen(RegistryKey key)
    {
      InitializeComponent();
      _key = key;
    }

    /// <summary>
    /// Form Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void form_SplashScreen_Load(object sender, EventArgs e)
    {
      _version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
      Text = string.Format("Free Parallize Walls v{0}", _version);
    }

    /// <summary>
    /// Start
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button1_Click(object sender, EventArgs e)
    {
      _key.SetValue(_version, "false");
    }

    /// <summary>
    /// Logo Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void pictureBox1_Click(object sender, EventArgs e)
    {
      Process.Start("http://apps.case-inc.com/");
    }

  }
}
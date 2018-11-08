using System;

namespace YZX.WINCC.Controls
{
  public partial class TIAIronPythonConsoleForm : Siemens.Automation.UI.Controls.Form
  {
    public TIAIronPythonConsoleForm()
    {
      InitializeComponent();
      Load += PythonConsoleForm_Load;
      Resize += PythonConsoleForm_Resize;
    }

    private void PythonConsoleForm_Load(object sender, System.EventArgs e)
    {
      UpdatePCVSize();
      PCV.Pad.Host.ConsoleCreated += Host_ConsoleCreated;
    }


    private void Host_ConsoleCreated(object sender, EventArgs e)
    {
      PCV.Pad.Host.Console.ConsoleInitialized += Console_ConsoleInitialized;
    }
    private void Console_ConsoleInitialized(object sender, EventArgs e)
    {
      PCV.SetVariable("self", this);

      PCV.UpdateVariables();

      var console = PCV.Pad.Console;
      BeginInvoke((Action)(() =>
      {
        PCV.Pad.Control.WordWrap = true;
        console.ExecuteFile(initFile);
        this.Activate();
      }));
    }

    private void PythonConsoleForm_Resize(object sender, System.EventArgs e)
    {
      UpdatePCVSize();
    }

    public void UpdatePCVSize()
    {
      PCVHost.Size = ClientSize;
      PCV.Width = ClientSize.Width;
      PCV.Height = ClientSize.Height;
    }

    private string initFile = "C:\\IronPython\\init.py";
    public string InitFile
    {
      get
      {
        return initFile;
      }
      set
      {
        initFile = value;
      }
    }
  }
}

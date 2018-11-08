using System;
using System.Windows;
using System.ComponentModel;
using System.Xaml;
using System.Drawing;

using PythonConsoleControl;

#if TIA
using Siemens.Simatic.Hmi.Utah.Common.Base;
using Siemens.Simatic.Hmi.Utah.GraphX;
using Siemens.Simatic.Hmi.Utah.GraphX.Controls;
#endif
using System.Collections.Generic;
using System.Windows.Threading;

namespace YZX.WINCC.Controls
{

  [ToolboxBitmap(typeof(PythonConsoleView), "Toolbox.PythonConsoleView.bmp")]
  public class PythonConsoleView : WPFInWinForm
  {
#region 属性
    private string initFile = "IronPython\\init.py";
#if TIA
    [Category("YZX")]
    [DisplayName("初始化文件")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
    [DefaultValue(typeof(String), "IronPython\\init.py")]
#endif
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

#endregion 属性

#region 事件
#endregion 事件

    IronPythonConsoleControl pc;
    public PythonConsoleView():base()
    {
      Load += PythonConsoleView_Load;
      HandleDestroyed += PythonConsoleView_HandleDestroyed;
    }

    private void PythonConsoleView_HandleDestroyed(object sender, EventArgs e)
    {
      if (pc != null)
      {
        pc.Dispose();
      }
    }

    private void PythonConsoleView_Load(object sender, EventArgs e)
    {
      PrepareWPF();
    }

    public new void PrepareWPF()
    {
      try {
        pc = new IronPythonConsoleControl();

        pc.Pad.Host.ConsoleCreated += Host_ConsoleCreated;

        WPF.Child = pc;

        pc.Loaded += Pc_Loaded;
      }
      catch (XamlParseException ex)
      {
        showInfo(ex.ToString());
      }
      
    }

    private void Pc_Loaded(object sender, RoutedEventArgs e)
    {
      Console.WriteLine("Pc_Loaded");
      #if RTA
      connect2CC();
      #endif
    }

    private void Host_ConsoleCreated(object sender, EventArgs e)
    {
      Console.WriteLine("Host_ConsoleCreated");
      pc.Pad.Host.Console.ConsoleInitialized += Console_ConsoleInitialized;
      //pc.Pad.Host.Console.Write("Hello", Microsoft.Scripting.Hosting.Shell.Style.Out);
      hideInfo();
    }


    private void RunInitFile()
    {
      try {
        pc.SetVariable("self", this);

        #if RTA
        pc.SetVariable("TAG", TAG);
        #endif

        pc.UpdateVariables();

        var console = pc.Pad.Console;

        BeginInvoke((Action)(() =>
        {
          pc.Pad.Control.WordWrap = true;
          console.ExecuteFile(initFile);
        }));
      }catch(Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }
   
    private void Console_ConsoleInitialized(object sender, EventArgs e)
    {
      Console.WriteLine("Console_ConsoleInitialized");
      RunInitFile();
    }
    public new void Dispose()
    {
      pc.Dispose();
      base.Dispose();
    }
  }
}

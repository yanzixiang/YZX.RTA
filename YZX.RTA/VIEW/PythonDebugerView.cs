using System;
using System.Windows;
using System.Xaml;
using System.Drawing;

using YZXLogicEngine;
using YZXLogicEngine.Task;

#if TIA
using System.ComponentModel;
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

namespace YZX.WINCC.Controls
{

  [ToolboxBitmap(typeof(PythonDebugerView), "Toolbox.PythonDebugerView.bmp")]
  public class PythonDebugerView : WPFInWinForm
#if RTA
    , IronPythonTaskMonitor
#endif
  {
    #region 属性
    private string taskname = "未命名";

#if TIA
    [Category("YZX")]
    [DisplayName("程序名称")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public string TaskName
    {
      get
      {
        return taskname;
      }
      set
      {
        taskname = value;
      }
    }

    private string pypath = "未命名";
#if TIA
    [Category("YZX")]
    [DisplayName("程序文件")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public string PYPath
    {
      get
      {
        return pypath;
      }
      set
      {
        pypath = value;
      }
    }
    #endregion 属性

    IronPythonTask task;
    IronPythonDebuger pc;
    public PythonDebugerView() : base()
    {
      Load += PythonDebugerView_Load;
      HandleDestroyed += PythonDebugerView_HandleDestroyed;
    }

    private void PythonDebugerView_Load(object sender, EventArgs e)
    {
      PrepareWPF();
    }

    private void PythonDebugerView_HandleDestroyed(object sender, EventArgs e)
    {
      #if RTA
      if(pc != null)
      {
        pc.Watching = false;
      }
      if (task != null)
      {
        task.ExceptionEvent -= Task_ExceptionEvent;
        task.Resume();
      }
      DisconnectFromTask();
      #endif
    }

    public override void PrepareWPF()
    {
      try
      {
        pc = new IronPythonDebuger();
        pc.Info1.Content = TaskName;
        pc.Info2.Content = PYPath;

#if RTA
        if (!YZXCPUInRuntime.cpu.HasTask(TaskName))
        {
          YZXCPUInRuntime.AddIronPythonTask(TaskName, PYPath);
        }

        task = YZXCPUInRuntime.cpu.GetTask(TaskName) as IronPythonTask;

        if (task != null){
          task.Scope.SetVariable("self", this);
          task.Scope.SetVariable("CPU", YZXCPUInRuntime.cpu);
          pc.Task = task;
          pc.Loaded += Pc_Loaded;
        }
#endif

        WPF.Child = pc;

        hideInfo();
      }
      catch (XamlParseException ex)
      {
        showInfo(ex.ToString());
      }
    }



    private void Pc_Loaded(object sender, RoutedEventArgs e)
    {
      
#if RTA
      ConnectToTask(task);
      connect2CC();
      YZXCPUInRuntime.TryStart(task);
#endif
    }

    private void Task_ExceptionEvent(object sender, YZXTaskExceptionEventArgs e)
    {
      showInfo(e.exception.ToString());
    }

    public new void Dispose()
    {
#if RTA
      DisconnectFromTask();
#endif
      base.Dispose();
    }

#if RTA
    #region IronPythonTaskMonitor
    public void ConnectToTask(IronPythonTask task)
    {
      task.TracebackEvent += pc.TracebackEvent;
      task.ExceptionEvent += Task_ExceptionEvent;
      pc.Watching = true;
      task.AddMonitor(this);
    }

    public void DisconnectFromTask()
    {
      if (task != null & pc != null)
      {
        task.TracebackEvent -= pc.TracebackEvent;
        task.ExceptionEvent -= Task_ExceptionEvent;
      }
      task.RemoveMonitor(this);
    }
    #endregion
#endif
  }
}
using System;
using System.Windows;
using System.Drawing;

using Common;
using Extensions;

using YZXLogicEngine.Task;
using IronPython.Runtime;

#if TIA
using System.ComponentModel;
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

namespace YZX.WINCC.Controls
{

  [ToolboxBitmap(typeof(PythonRunningLineView), "Toolbox.PythonRunningLineView.bmp")]
  public class PythonRunningLineView : CControl
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

#if TIA
    [Category("YZX")]
    [DisplayName("当前行号")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public int CurrentLineNumber{
      get
      {
        return cln;
      }
      set
      {
        cln = value;
#if RTA
        TryWriteTag("CurrentLineNumber", value);
#endif
      }
    }
    private int cln;

#if TIA
    [Category("YZX")]
    [DisplayName("当前行代码")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public string CurrentLineString {
      get
      {
#if RTA
        return lines[CurrentLineNumber];
#else
        return "CurrentLine";
#endif
      }
      set
      {
#if RTA
        TryWriteTag("CurrentLineString", value);
#endif
      }
    }
#endregion 属性

#if RTA
    IronPythonTask MoniteredTask;
    private string[] lines;

    private void PythonRunningLineView_HandleDestroyed(object sender, EventArgs e)
    {
      Dispose();
    }
#endif
    public PythonRunningLineView() : base()
    {
#if RTA
      Load += PythonRunningLineView_Load;
      HandleDestroyed += PythonRunningLineView_HandleDestroyed;
#endif
    }

    private void PythonRunningLineView_Load(object sender, EventArgs e)
    {
#if RTA
      MoniteredTask = YZXCPUInRuntime.cpu.GetTask(TaskName) as IronPythonTask;

      if (MoniteredTask != null)
      {
        ConnectToTask(MoniteredTask);
        connect2CC();
      }
#endif
    }

    private void Task_ExceptionEvent(object sender, YZXTaskExceptionEventArgs e)
    {
    }

    public new void Dispose()
    {
#if RTA
      DisconnectFromTask();
#endif
      base.Dispose();
    }
#if RTA
    public void TracebackEvent(object sender, IPYTracebackEventArgs e)
    {
      FunctionCode code = e.frame.f_code;

      string filename = code.co_filename;

      try
      {
        switch (e.result)
        {
          case "call":
            TracebackCall(e);
            break;

          case "line":
            TracebackLine(e);
            break;

          case "return":
            TracebackReturn(e);
            break;

          default:
            break;
        }
      }
      catch (Exception ex)
      {
        Invoke((Action)(() =>
        {
          ExceptionViewer ev = new ExceptionViewer("TracebackEvent", ex);
          ev.ShowDialog();
        }));
      }
    }


		private void UpdateCurrentLineString()
    {
      int lineIndex = CurrentLineNumber - 1;
      if (lineIndex < lines.Length)
      {
        CurrentLineString = lines[lineIndex];
      }
    }

    private void TracebackCall(IPYTracebackEventArgs e)
    {
      if (e.frame != null)
      {
        CurrentLineNumber = (int)e.frame.f_lineno;
        UpdateCurrentLineString();
      }
    }

    private void TracebackReturn(IPYTracebackEventArgs e)
    {
      if (e.frame != null)
      {
        CurrentLineNumber = (int)e.frame.f_code.co_firstlineno;
        UpdateCurrentLineString();
      }
    }

    private void TracebackLine(IPYTracebackEventArgs e)
    {
      if (e.frame != null)
      {
        CurrentLineNumber = (int)e.frame.f_lineno;
        UpdateCurrentLineString();
      }
    }
#endif

#if RTA
		#region IronPythonTaskMonitor
    public void ConnectToTask(IronPythonTask task)
    {
      MoniteredTask = task;
      lines = task.Path.ReadLines();
      task.TracebackEvent += TracebackEvent;
      task.ExceptionEvent += Task_ExceptionEvent;
      task.AddMonitor(this);
    }

    public void DisconnectFromTask()
    {
      if (MoniteredTask != null)
      {
        MoniteredTask.TracebackEvent -= TracebackEvent;
        MoniteredTask.ExceptionEvent -= Task_ExceptionEvent;
      }
      MoniteredTask.RemoveMonitor(this);
    }
		#endregion
#endif
	}
}
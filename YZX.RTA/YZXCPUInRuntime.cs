using System;
using System.Collections.Generic;

using YZXLogicEngine;
using YZXLogicEngine.Task;

namespace YZX.WINCC.Controls
{
  public class YZXCPUInRuntime
  {
    public static YZXCPU cpu = new YZXCPU();

    public static bool Started = false;

    public static void TryStart()
    {
      if (!Started)
      {
        try
        {
          cpu.Start();
          Started = true;
        }catch(Exception ex)
        {
          Console.WriteLine("TryStart" + ex.ToString());
        }
      }
    }

    public static void TryStart(IronPythonTask task)
    {
      if (!task.Running)
      {
        cpu.StartTask(task);
      }
    }

    public static IronPythonTask AddIronPythonTask(string TaskName,string PYPath)
    {
      IronPythonTask task = cpu.AddIronPythonTask(TaskName, PYPath);

      List<IronPythonTaskMonitor> monitors = CControlSystem.GetTaskMonitor(TaskName);
      foreach (IronPythonTaskMonitor mm in monitors)
      {
        mm.ConnectToTask(task);
      }
      return task;
    }

    public static CCTimer AddTimer(string tagName,
      string outTagName, int onLast = 1000,
      string offOutTagName = "", int offLast = 1000)
    {
      CControlSystem cc = new CControlSystem();
      CCTimer timer = new CCTimer(cc, tagName, outTagName, onLast, offOutTagName, offLast);
      cpu.AddTask(timer);
      return timer;
    }
  }
}

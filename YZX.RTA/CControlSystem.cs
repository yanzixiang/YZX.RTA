using System;
using System.Collections.Generic;
using Siemens.Runtime.ITag;
using YZXLogicEngine.Task;

namespace YZX.WINCC.Controls
{
  /// <summary>
  /// RTA 系统中所有的 CControl 管理 
  /// </summary>
  public class CControlSystem:ITagOp
  {
    public static Dictionary<string, CControl> CControls =
      new Dictionary<string, CControl>();



    /// <summary>
    /// 任务的监视器不一定在任务创建之前就开始监视
    /// </summary>
    /// <param name="taskName">IronPythonTask 名称</param>
    /// <returns></returns>
    public static List<IronPythonTaskMonitor> GetTaskMonitor(string taskName)
    {
      List<IronPythonTaskMonitor> monitors = new List<IronPythonTaskMonitor>();
      foreach (CControl c in CControls.Values)
      {
        if (c != null)
        {
          IronPythonTaskMonitor mc = c as IronPythonTaskMonitor;
          if (mc != null)
          {
            monitors.Add(mc);
          }
        }
      }
      return monitors;
    }

    /// <summary>
    /// 取得所有存活下来的CControl
    /// </summary>
    public static Dictionary<string,CControl> GetAlives()
    {
      Dictionary<string, CControl> alives =
        new Dictionary<string, CControl>();
      foreach(string key in CControls.Keys)
      {
        CControl control = CControls[key];
        if(control != null & control.IsHandleCreated)
        {
          alives[key] = CControls[key];
        }
      }
      return alives;
    }

    public static CControl RandomSelect()
    {
      Dictionary<string, CControl> alives = GetAlives();
      foreach(string key in alives.Keys)
      {
        return alives[key];
      }
      return null;
    }

    private static CControl current;
    public static CControl Current
    {
      get
      {
        if(current == null)
        {
          current = RandomSelect();
        }
        else
        {
          if (!current.IsHandleCreated)
          {
            current = RandomSelect();
          }
        }
        return current;
      }
    }

    #region ITagOp
    public int RegisterCookie
    {
      get
      {
        if(Current != null)
        {
          return Current.RegisterCookie;
        }
        else
        {
          return 0;
        }
      }
    }

    public ITag TAG
    {
      get
      {
        if (Current != null)
        {
          return Current.TAG;
        }
        else
        {
          return null;
        }
      }
    }

    public bool CConnected
    {
      get
      {
        if (Current != null)
        {
          return Current.CConnected;
        }
        else
        {
          return false;
        }
      }
    }

    public void InvokeAction(Action action)
    {
      if(Current != null)
      {
        Current.InvokeAction(action);
      }
    }

    public object InvokeFunc(Func<object> func)
    {
      if(Current != null)
      {
        return Current.InvokeFunc(func);
      }
      else
      {
        return null;
      }
    }
    #endregion
  }
}

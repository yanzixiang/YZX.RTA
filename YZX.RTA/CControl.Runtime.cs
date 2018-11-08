using System;
using System.Collections.Generic;
using System.Windows;

using Common;
using Extensions;
using Reflection;

using Newtonsoft.Json;
using System.IO.Ports;

#if RTA
using Siemens.Runtime.ITag;
using Siemens.Runtime;
#endif

namespace YZX.WINCC.Controls
{
  partial class CControl
  {
#if RTA
    /// <summary>
    /// 是否在Runtime运行环境
    /// </summary>
    public bool inRuntime
    {
      get
      {
        object irc = null ;
        try {
          irc = Site.GetService(typeof(IRuntimeContext));
        }catch(Exception ex)
        {
          Console.WriteLine("inRuntime" + ex.ToString());
        }
        if (irc != null)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }
#else
	  public bool inRuntime
	  {
		  get{
			  return false;
		  }
	  }
  #endif 

#if RTA

    public void connect2CC()
    {
      if (!inRuntime)
      {
        return;
      }
      try
      {
        SetRuntimeUIRef();
        FillEventMap();
        FillTagMap();
        SubRXS();

        TAG = (ITag)Site.GetService(typeof(ITag));
        CCRuntime = (IRuntimeContext)Site.GetService(typeof(IRuntimeContext));
        if (TAG != null)
        {
          RegisterCookie = TAG.Register(this);
          if (RegisterCookie != 0)
          {
            CConnected = true;
          }
        }
      }
      catch (Exception ex)
      {
        CConnected = false;
        Console.WriteLine(ex.ToString());
      }
    }

    public void SetRuntimeUIRef()
    {
      if(YZXName != null & YZXName != "")
      {
        CControlSystem.CControls[YZXName] = this;
      }
    }

    public event EventHandler TagMapConnected;

    /// <summary>
    /// 在 Runtime 中运行
    /// 从 YZXProperties 取得所有关联的变量链接
    /// </summary>
    public void FillTagMap()
    {
      if(YZXProperties == null)
      {
        return;
      }
      try
      {
        if (YZXProperties != "")
        {
          TagMap = JsonConvert.DeserializeObject<Dictionary<string, CCTag>>(YZXProperties);

          foreach (CCTag tag in TagMap.Values)
          {
            RXSystem.AddTagRx(this,tag);
          }

          TagMapConnected?.Invoke(this, new EventArgs());
        }
      }catch(Exception ex)
      {
        Console.WriteLine("FillTagMap" + ex.ToString());
      }
    }

    

    public void FillEventMap()
    {
      if(YZXEvents != "")
      {
        EventMap = JsonConvert.DeserializeObject<Dictionary<string, List<CCFunction>>>(YZXEvents);
      }
    }

    public void ProcessEvent(string eventName)
    {
      if (EventMap.ContainsKey(eventName))
      {
        List<CCFunction> cceventList = EventMap[eventName];
        foreach (CCFunction function in cceventList)
        {
          ProcessFunction(function);
        }
      }
    }

    public void ProcessFunction(CCFunction func)
    {
      try {
        Reflector.RunInstanceMethodByName(this, func.Name, func.Parameters);
      }
      catch(Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }
    #region 读变量





#endregion 读变量



    #region 串口操作
    public void OpenSerialPort(List<CCParameter> ps)
    {
      string name = ps[0].DisplayValue;
      SerialPort sp = SerialPortsInRuntime.TryGetSerialPort(name);

    }

    public void CloseSerialPort(List<CCParameter> ps)
    {

    }

    public void SerialPortSend(List<CCParameter> ps)
    {

    }
    #endregion


    

#endif
  }
}

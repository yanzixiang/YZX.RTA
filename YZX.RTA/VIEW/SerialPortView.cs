using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#if TIA
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

using Extensions;
using Common;

namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(SerialPortView), "Toolbox.SerialPortView.bmp")]
  public class SerialPortView : CControl
  {
    public SerialPort SP;
    public SerialPortView()
    {
      SuspendLayout();
      BackColor = SystemColors.ControlDark;

#if RTA
      if (SerialPortName != null)
      {
        SP = SerialPortsInRuntime.TryGetSerialPort(SerialPortName);
      }
#endif

      ResumeLayout();
    }

#if RTA

#endif

#region 属性
#if TIA
    [Bindable(false)]
    [DefaultValue(typeof(string), "COM1")]
    [Category("YZX")]
    [Description("串口名称")]
#endif
    public string SerialPortName
    {
      get;
      set;
    }

#if TIA
    [Bindable(false)]
    [DefaultValue(typeof(int), "9600")]
    [Category("YZX")]
    [Description("波特率")]
#endif
    public int BaudRate
    {
      get;
      set;
    }

#if TIA
    [Bindable(false)]
    [DefaultValue(typeof(int), "9600")]
    [Category("YZX")]
    [Description("数据位")]
#endif
    public int DataBits
    {
      get;
      set;
    }

#if TIA
    [Bindable(false)]
    [DefaultValue(typeof(int), "9600")]
    [Category("YZX")]
    [Description("停止位")]
#endif
    public StopBits StopBits
    {
      get;
      set;
    }

#if TIA
    [Bindable(false)]
    [DefaultValue(typeof(int), "9600")]
    [Category("YZX")]
    [Description("校验位")]
#endif
    public Parity Parity
    {
      get;
      set;
    }

#if TIA
    [Bindable(false)]
    [DefaultValue(typeof(int), "1")]
    [Category("YZX")]
    [Description("校验位")]
#endif
    public int ReceivedBytesThreshold
    {
      get;
      set;
    }

    #region 向Runtime更新值
#if TIA
    [Bindable(false)]
    [DefaultValue(typeof(int), "9600")]
    [Category("YZX")]
    [Description("Gets or sets the style of the ToggleSwitch")]
#endif
    public bool IsOpen
    {
      get
      {
        bool i = (bool)this.SafeInvoke(() =>
        {
          if (SP != null)
          {
            return SP.IsOpen;
          }
          else
          {
            return false;
          }
        });
        return i;
      }
    }
#if TIA
    [Bindable(false)]
    [DefaultValue(typeof(int), "9600")]
    [Category("YZX")]
    [Description("Gets or sets the style of the ToggleSwitch")]
#endif
    public int BytesToRead
    {
      get
      {
        int i = (int)this.SafeInvoke(() =>
        {
          if (SP != null)
          {
            if (SP.IsOpen)
            {
              return SP.BytesToRead;
            }
            return 0;
          }
          else
          {
            return 0;
          }
        });
        return i;
      }
    }

#if TIA
    [Bindable(false)]
    [DefaultValue(typeof(int), "9600")]
    [Category("YZX")]
    [Description("Gets or sets the style of the ToggleSwitch")]
#endif
    public int BytesToWrite
    {
      get
      {
        int i = (int)this.SafeInvoke(() =>
        {
          if (SP != null)
          {
            if (SP.IsOpen)
            {
              return SP.BytesToWrite;
            }
            return 0;
          }
          else
          {
            return 0;
          };
        });
        return i;
      }
    }
    #endregion



    #endregion 属性

    public override Control InnerControl
    {
      get
      {
        return null;
      }
    }

#if RTA
    #region RX
    public override List<string> RXUpdateTags
    {
      get
      {
        return new List<string>()
        {
          "BytesToRead",
          "BytesToWrite"
        };
      }
    }
    #endregion
#endif

    #region 事件

#if TIA
    [Description("接收到数据")]
    [Browsable(true)]
    [ScriptEvent]
#endif
    public event SerialDataReceivedEventHandler DataReceived;

#if TIA
    [Description("有错误发生")]
    [Browsable(true)]
    [ScriptEvent]
#endif
    public event SerialErrorReceivedEventHandler ErrorReceived;

#if TIA
    [Description("Raised when the ToggleSwitch has changed state")]
    [Browsable(true)]
    [ScriptEvent]
#endif
    public event SerialPinChangedEventHandler PinChanged;


#endregion
  }
}

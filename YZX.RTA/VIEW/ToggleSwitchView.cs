using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using JCS;

#if TIA
using System.ComponentModel;
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

using Extensions;
using Common;
using static JCS.ToggleSwitch;

namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(ToggleSwitchView), "Toolbox.ToggleSwitch.bmp")]
  public class ToggleSwitchView : CControl
  {
    public ToggleSwitchView()
    {
      this.SuspendLayout();
      ts.UseAnimation = false;

      Controls.Add(ts);

#if RTA
      ts.CheckedChanged += Ts_CheckedChanged;


#endif

      this.ResumeLayout();
    }


#if RTA
    private void Ts_CheckedChanged(object sender, EventArgs e)
    {
      if (!UpdateFromRX)
      {
        string ptag = TryGetTagName("ProcessValue");
        bool pv = ProcessValue;
        this.WriteTag(ptag, pv);
        CheckedChanged?.Invoke(sender, e);
      }
      ProcessEvent("CheckedChanged");
    }

    #region RX

    

    public override List<string> RXTags
    {
      get
      {
        return new List<string>()
        {
          "ProcessValue"
        };
      }
    }
    #endregion
#endif

    #region 事件
#if TIA
    [Description("Raised when the ToggleSwitch has changed state")]
    [Browsable(true)]
    [ScriptEvent]
#endif
    public event EventHandler CheckedChanged;

    #endregion

    #region 属性
#if TIA
    [Category("YZX")]
    [DisplayName("当前值")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public bool ProcessValue
    {
      get
      {
        bool s = (bool)this.SafeInvoke((Func<bool>)(() =>{
          return ts.Checked;
        }));
        return s;
      }
      set
      {
        this.SafeInvoke((Action)(() =>
        {
          if (value != ts.Checked)
          {
            ts.Checked = value;
          }
#if RTA
          ProcessEvent("Click");
#endif
        }));
      }
    }

    #if TIA
    [Bindable(false)]
    [DefaultValue(typeof(ToggleSwitchStyle), "Metro")]
    [Category("YZX")]
    [Description("Gets or sets the style of the ToggleSwitch")]
    #endif
    public ToggleSwitchStyle Style
    {
      get
      {
        return ts.Style;
      }
      set
      {
        ts.Style = value;
      }
    }

#if TIA
    [Bindable(false)]
    [DefaultValue(typeof(string), "ON")]
    [Category("YZX")]
    [Description("Gets or sets the style of the ToggleSwitch")]
#endif
    public string OnText
    {
      get
      {
        return ts.OnText;
      }
      set
      {
        ts.OnText = value;
      }
    }

#if TIA
    [Bindable(false)]
    [DefaultValue(typeof(string), "OFF")]
    [Category("YZX")]
    [Description("Gets or sets the style of the ToggleSwitch")]
#endif
    public string OffText
    {
      get
      {
        return ts.OffText;
      }
      set
      {
        ts.OffText = value;
      }
    }
    #endregion 属性

    public JCS.ToggleSwitch ts = new JCS.ToggleSwitch();
    public override Control InnerControl
    {
      get
      {
        return ts;
      }
    }
  }
}

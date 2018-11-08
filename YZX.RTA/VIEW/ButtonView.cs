using System.ComponentModel;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#if TIA
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

using Extensions;
using Common;
using System.Collections.Generic;

namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(ButtonView), "Toolbox.ButtonView.bmp")]
  public class ButtonView : CControl
  {
    public ButtonView()
    {
      this.SuspendLayout();

      Controls.Add(bv);

      #if RTA

      bv.Click += Bv_Click;

      # endif

      this.ResumeLayout();
    }

#if RTA
    private void Bv_Click(object sender, EventArgs e)
    {
      string ptag = TryGetTagName("ProcessValue");
      if (!UpdateFromRX)
      {
        bool pv = ProcessValue;
        this.WriteTag(ptag, pv);
        Click?.Invoke(sender, e);
      }
      ProcessEvent("Click");
      if(ptag != "")
      {
        this.InvertBit(ptag);
      }
    }
#endif

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
        return pv;
      }
      set
      {
        this.SafeInvoke((Action)(() =>{
          if(value != pv)
          {
            if (value)
            {
              this.bv.Text = OnText;
              this.bv.BackColor = OnColor;
            }
            else
            {
              this.bv.Text = OffText;
              this.bv.BackColor = OffColor;
            }
            pv = value;
#if RTA
            ProcessEvent("Click");
#endif
          }
        }));
      }
    }
    private bool pv;

#if TIA
    [Bindable(false)]
    [DefaultValue(typeof(string), "ON")]
    [Category("YZX")]
    [Description("Gets or sets the style of the ToggleSwitch")]
#endif
    public string OnText
    {
      get;
      set;
    }

#if TIA
    [Bindable(false)]
    [DefaultValue(typeof(string), "OFF")]
    [Category("YZX")]
    [Description("Gets or sets the style of the ToggleSwitch")]
#endif
    public string OffText
    {
      get;
      set;
    }

#if TIA
    [Bindable(false)]
    [DefaultValue(typeof(string), "ON")]
    [Category("YZX")]
    [Description("Gets or sets the style of the ToggleSwitch")]
#endif
    public Color OnColor
    {
      get;
      set;
    }

#if TIA
    [Bindable(false)]
    [DefaultValue(typeof(Color), "OFF")]
    [Category("YZX")]
    [Description("Gets or sets the style of the ToggleSwitch")]
#endif
    public Color OffColor
    {
      get;
      set;
    }
#endregion 属性

    public Button bv = new Button();
    public override Control InnerControl
    {
      get
      {
        return bv;
      }
    }

#if RTA
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
    public event EventHandler Click;

#endregion
  }
}

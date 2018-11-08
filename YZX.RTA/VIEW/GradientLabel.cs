using System;
using System.ComponentModel;
using System.Xaml;
using System.Drawing;
using System.Collections.Generic;
using Common;
using Extensions;

#if TIA
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

using AITSW.PCPANEL.WPF;


namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(GradientLabel), "Toolbox.GradientLabel.bmp")]
  public class GradientLabel : CControl
  {
#region 属性
    Label mc = new Label();
#if TIA
    [Category("YZX")]
    [DisplayName("标签文本")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public string LabelText
    {
      get
      {
        string s = (string)this.SafeInvoke((Func<string>)(() =>
        {
          return (string)mc.Content;
        }));
        return s;
      }
      set
      {
        this.SafeInvoke((Action)(() =>{
          mc.Content = value;
        }));
      }
    }
#endregion 属性
    public GradientLabel()
    {
    }

#if RTA
    #region RX
    public override List<string> RXTags
    {
      get
      {
        return new List<string>()
        {
          "LabelText"
        };
      }
    }
    #endregion
#endif
  }
}

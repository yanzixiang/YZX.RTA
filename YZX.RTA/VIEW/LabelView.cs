using System;
using System.Xaml;
using System.Drawing;
using System.Collections.Generic;

using Common;
using Extensions;

#if TIA
using System.ComponentModel;
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

using AITSW.PCPANEL.WPF;


namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(LabelView), "Toolbox.LabelView.bmp")]
  public class LabelView : WPFInWinForm
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
        string s = this.SafeInvoke(() =>
        {
          return (string)mc.Content;
        });
        return s;
      }
      set
      {
        this.SafeInvoke(() =>
        {
          mc.Content = value;
        });
      }
    }
#endregion 属性
    public LabelView()
    {
      Load += LabelView_Load;
    }

    private void LabelView_Load(object sender, EventArgs e)
    {
      PrepareWPF();
    }

    public new void PrepareWPF()
    {
      try {
        
        WPF.Child = mc;
        hideInfo();
      }
      catch (XamlParseException ex)
      {
        
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
          "LabelText"
        };
      }
    }
    #endregion
#endif
  }
}

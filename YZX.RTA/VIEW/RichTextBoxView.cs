using System.Windows.Forms;
using System.ComponentModel;
using System;
using System.Drawing;
using Common;
using Extensions;
using System.Collections.Generic;

#if TIA
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

namespace YZX.WINCC.Controls
{

  [ToolboxBitmap(typeof(RichTextBoxView), "Toolbox.RichTextBoxView.bmp")]
  public class RichTextBoxView : CControl
  {
    public RichTextBox rtb = new RichTextBox();

    public RichTextBoxView()
    {
      this.SuspendLayout();
      Controls.Add(rtb);

#if RTA
      rtb.TextChanged += Rtb_TextChanged;
#endif
      this.ResumeLayout();
    }

#if RTA
    private void Rtb_TextChanged(object sender, EventArgs e)
    {
      if (!UpdateFromRX)
      {
        string ptag = TryGetTagName("TextValue");
        string pv = rtb.Text;
        if(ptag != "")
        {
          this.WriteTag(ptag, pv);
        }
        RTBTextChanged?.Invoke(sender, e);
      }
      ProcessEvent("RTBTextChanged");
    }
#endif

    public override Control InnerControl
    {
      get
      {
        return rtb;
      }
    }

#region 属性
#if TIA
    [Category("YZX")]
    [DisplayName("文字")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public string TextValue
    {
      get
      {
        string s = (string)this.SafeInvoke(() =>
        {
          return Text;
        });
        return s;
      }
      set
      {
        this.SafeInvoke(() =>
        {
          Text = value;
        });
      }
    }
#endregion 属性

#region 事件

    public delegate void TextChangedDelegate(object sender, EventArgs e);

#if TIA
    [Description("Raised when the ToggleSwitch has changed state")]
    [Browsable(true)]
    [ScriptEvent]
#endif
    public event TextChangedDelegate RTBTextChanged;

#endregion

#if RTA
#region RX
    public override List<string> RXTags
    {
      get
      {
        return new List<string>()
        {
          "TextValue"
        };
      }
    }
#endregion
#endif
  }
}

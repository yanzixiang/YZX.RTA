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
  [ToolboxBitmap(typeof(ByteViewer), "Toolbox.ByteViewer.bmp")]
  public class ByteViewer : CControl
  {
    public ByteViewer()
    {
      this.SuspendLayout();

      Controls.Add(bv);

      this.ResumeLayout();
    }

    #region 属性
#if TIA
    [Category("YZX")]
    [DisplayName("编码")]
    [SupportedDynamicTypes(SupportedDynamicTypes.None)]
#endif
    public EncodingEnum EncodingType { set; get; }

    public Encoding ShowEncoding {
      get
      {
        return EncodingType.ToEncoding();
      }
    }

#if TIA
    [Category("YZX")]
    [DisplayName("字符串")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public string ShowString
    {
      get
      {
        string s = (string)this.SafeInvoke(() =>
        {
          string str = "";
          byte[] bs = bv.GetBytes();
          if (bs != null)
          {
            str = ShowEncoding.GetString(bs);
          }
          return str;
        });
        return s;
      }
      set
      {
        this.SafeInvoke(() =>
        {
          byte[] bs = value.ToBytes(ShowEncoding);
          if (bs != null)
          {
            bv.SetBytes(bs);
          }
        });
      }
    }
#endregion 属性

    public System.ComponentModel.Design.ByteViewer bv = new System.ComponentModel.Design.ByteViewer();
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
          "ShowString"
        };
      }
    }
    #endregion
#endif
  }
}

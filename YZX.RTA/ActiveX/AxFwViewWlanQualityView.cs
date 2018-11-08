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
  [ToolboxBitmap(typeof(AxFwViewWlanQualityView), "Toolbox.AxFwViewWlanQualityView.bmp")]
  public class AxFwViewWlanQualityView : CControl
  {
    public AxFwViewWlanQualityView()
    {
      this.SuspendLayout();

      Controls.Add(bv);

      #if RTA
      # endif

      this.ResumeLayout();
    }

    #if RTA
    #endif

#region 属性
#if TIA
    [Category("YZX")]
    [DisplayName("当前值")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public int QualityValue
    {
      get
      {
        return bv.QualityValue;
      }
      set
      {
        this.SafeInvoke((Action)(() =>{
#if RTA
          bv.QualityValue = value;
#endif
        }));
      }
    }
#endregion 属性

    public AxFwViewWlanQuality bv = new AxFwViewWlanQuality();
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
    #endregion
  }
}

using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System;
using Common;
using System.Collections.Generic;

#if TIA
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(ProgressBarView), "Toolbox.ProgressBarView.bmp")]
  public class ProgressBarView : CControl
#if RTA
    , IObserver<ReadTagResult>
#endif
  {
    #region 属性
#if TIA
    [Category("YZX")]
    [DisplayName("进度")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
    [DefaultValue(typeof(int),"50")]
#endif
    public int ProgressValue
    {
      get
      {
        return pb.Value;
      }
      set
      {
        pb.Value = value;
      }
    }

    public override Control InnerControl
    {
      get
      {
        return pb;
      }
    }
    #endregion 属性

    public ProgressBar pb = new ProgressBar();

    public ProgressBarView()
    {
      this.SuspendLayout();
      pb.Value = 50;
      Controls.Add(pb);
      this.ResumeLayout();
    }

#if RTA
    #region RX
    public override List<string> RXTags
    {
      get
      {
        return new List<string>()
        {
          "ProgressValue"
        };
      }
    }
    #endregion
#endif
  }
}

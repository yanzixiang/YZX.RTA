﻿using System;
using System.ComponentModel;
using System.Xaml;
using System.Drawing;
using System.Collections.Generic;

using Common;
using Extensions;

#if TIA
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(LightEmitterView), "Toolbox.LightEmitterView.bmp")]
  public class LightEmitterView : WPFInWinForm
  {

#region 属性
    LightEmitterWPF mc = new LightEmitterWPF();

    #if TIA
    [Category("YZX")]
    [DisplayName("红灯")]
    [Description("红灯状态")]
    [PropertyChangedEvent]
    [SupportedDynamicTypes(SupportedDynamicTypes.AllWithReport)]
    #endif
    public int Red
    {
      get
      {
        int s = (int)this.SafeInvoke(() =>
        {
          return mc.Red.State;
        });
        return s;
      }
      set
      {
        this.SafeInvoke(() =>
        {
          mc.Red.State = value;
        });
      }
    }

    #if TIA
    [Category("YZX")]
    [DisplayName("黄灯")]
    [Description("黄灯状态")]
    [SupportedDynamicTypes(SupportedDynamicTypes.AllWithReport)]
    [PropertyChangedEvent]
    [Direction(PropertyDirections.InOutput, "Mode")]
    #endif
    public int Yellow
    {
      get
      {
        int s = (int)this.SafeInvoke(() =>
        {
          return mc.Yellow.State;
        });
        return s;
      }
      set
      {
        this.SafeInvoke(() =>
        {
          mc.Yellow.State = value;
        });
      }
    }

    #if TIA
    [Category("YZX")]
    [DisplayName("绿灯")]
    [Description("绿灯状态")]
    [PropertyChangedEvent]
    [SupportedDynamicTypes(SupportedDynamicTypes.AllWithReport)]
    #endif
    public int Green
    {
      get
      {
        int s = (int)this.SafeInvoke(() =>
        {
          return mc.Green.State;
        });
        return s;
      }
      set
      {
        this.SafeInvoke(() =>
        {
          mc.Green.State = value;
        });
      }
    }
#endregion 属性

    public LightEmitterView()
    {
      Load += LightEmitterView_Load;
    }
    private void LightEmitterView_Load(object sender,EventArgs e)
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
          "Red",
          "Green",
          "Yellow"
        };
      }
    }
    #endregion
#endif
  }
}

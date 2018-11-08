using System;
using System.Windows;
using System.Xaml;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Controls;

using Common;
using Extensions;

#if TIA
using System.ComponentModel;
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(DigitalMeterView), "Toolbox.DigitalMeterView.bmp")]
  public class DigitalMeterView : WPFInWinForm
  {

    #region 属性
    DigitalMeterWPF mc;
    public override Control WPFRoot
    {
      get
      {
        return mc;
      }
    }

#if TIA
    [Category("YZX")]
    [DisplayName("当前值")]
    [SupportedDynamicTypes(SupportedDynamicTypes.AllWithReport)]
    #endif
    public int Value
    {
      get
      {
#if RTA
        int s = (int)this.SafeInvoke(() =>
        {
          return mc.dm.Value;
        });
        return s;
#else
        return (int)mc.dm.Value;
#endif
      }
      set
      {
#if RTA
        this.SafeInvoke(() =>
        {
          mc.dm.Value = value;
        });
#else
        mc.dm.Value = value;
#endif
      }
    }

#if TIA
    [Category("YZX")]
    [DisplayName("单位")]
    [SupportedDynamicTypes(SupportedDynamicTypes.None)]
#endif
    public string MeasurementUnit
    {
      get
      {
        return measurementUnit;
      }
      set
      {
#if RTA
        measurementUnit = value;
        this.SafeInvoke(() =>
        {
          mc.dm.MeasurementUnit = value;
        });
#else
        mc.dm.MeasurementUnit = value;
        measurementUnit = value;
#endif
      }
    }
    private string measurementUnit;

#if TIA
    [Category("YZX")]
    [DisplayName("单位宽度")]
    [SupportedDynamicTypes(SupportedDynamicTypes.None)]
#endif
    public double UnitWidth
    {
      get
      {
        return unitWidth;
      }
      set
      {
#if RTA
        unitWidth = value;
        if (unitWidth > 0)
        {
          this.SafeInvoke(() =>
          {
            mc.dm.UnitWidth = value;
          });
        }
#else
        unitWidth = value;
        if(unitWidth >0){
          mc.dm.UnitWidth = value;
        }
#endif
      }
    }
    private double unitWidth;

#if TIA
    [Category("YZX")]
    [DisplayName("数字总长度")]
    [SupportedDynamicTypes(SupportedDynamicTypes.None)]
#endif
    public int Precision
    {
      get
      {
        return precision;
      }
      set
      {
#if RTA
        precision = value;
        if (precision > 0)
        {
          this.SafeInvoke(() =>
          {
            mc.dm.Precision = value;
          });
        }
#else
        precision = value;
        if(precision >0 ){
          mc.dm.Precision = value;
        }
#endif
      }
    }
    private int precision;

#if TIA
    [Category("YZX")]
    [DisplayName("小数点后长度")]
    [SupportedDynamicTypes(SupportedDynamicTypes.None)]
#endif
    public int ScalingFactor
    {
      get
      {
        return scalingFactor;
      }
      set
      {
#if RTA
        scalingFactor = value;
        if (scalingFactor > 0)
        {
          this.SafeInvoke(() =>
          {
            mc.dm.ScalingFactor = value;

          });
        }
#else
        scalingFactor = value;
        if(scalingFactor > 0){
          mc.dm.ScalingFactor = value;
        }
#endif
      }
    }
    private int scalingFactor;
    #endregion 属性

    public DigitalMeterView()
    {
      Load += DigitalMeterView_Load;
      HandleDestroyed += DigitalMeterView_HandleDestroyed;
    }

    private void DigitalMeterView_HandleDestroyed(object sender, EventArgs e)
    {
      if (WPF != null)
      {
        WPF.Dispose();
      }
    }

    private void DigitalMeterView_Load(object sender,EventArgs e)
    {
      PrepareWPF();
    }

    public new void PrepareWPF()
    {
      try {
        mc = new DigitalMeterWPF();
        mc.Loaded += Mc_Loaded;
        WPF.Child = mc;
        hideInfo();
      }
      catch (XamlParseException ex)
      {
      }
    }

    private void Mc_Loaded(object sender, RoutedEventArgs e)
    {
      MeasurementUnit = measurementUnit;
      Precision = precision;
      ScalingFactor = scalingFactor;
      UnitWidth = unitWidth;
    }
#if RTA
    #region RX
    public override List<string> RXTags
    {
      get
      {
        return new List<string>()
        {
          "Value",
        };
      }
    }
#endregion
#endif
  }
}

using System;
using System.Windows;
using System.Xaml;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Controls;

using Common;
using Extensions;
using Codeplex.Dashboarding;

#if TIA
using System.ComponentModel;
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(OdometerView), "Toolbox.OdometerView.bmp")]
  public class OdometerView : WPFInWinForm
  {

    #region 属性
    Odometer dm;
    public override Control WPFRoot
    {
      get
      {
        return dm;
      }
    }

#if TIA
    [Category("YZX")]
    [DisplayName("数字总长度")]
    [SupportedDynamicTypes(SupportedDynamicTypes.None)]
#endif
    public int Digits
    {
      get
      {
        return digits;
      }
      set
      {
#if RTA
        digits = value;
        if (digits > 0)
        {
          this.SafeInvoke(() =>
          {
            dm.Digits = value;
          });
        }
#else
        digits = value;
        if (digits > 0)
        {
          dm.Digits = value;
        }
#endif
      }
    }
    private int digits;
    #endregion 属性

    public OdometerView()
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
        dm = new Odometer();
        dm.MeterMode = Odometer.Mode.Static;
        dm.InitialValue = 0;
        dm.FinalValue = (int)Math.Pow(10, Digits) - 1;
        dm.Loaded += Mc_Loaded;
        WPF.Child = dm;
        hideInfo();
      }
      catch (XamlParseException ex)
      {
      }
    }

    private void Mc_Loaded(object sender, RoutedEventArgs e)
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
          "Value",
        };
      }
    }
#endregion
#endif
  }
}

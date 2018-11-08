using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#if TIA
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(BatteryView), "Toolbox.BatteryView.bmp")]
  public partial class BatteryView : CControl
  {
    Timer UpdateTimer;

    public override Control InnerControl
    {
      get
      {
        return BatteryProgress;
      }
    }

    public BatteryView()
    {
      InitializeComponent();

      Load += BatteryView_Load;

    }

    private void BatteryView_Load(object sender, EventArgs e)
    {
      UpdateTimer = new Timer();
      UpdateTimer.Interval = 1000;
      UpdateTimer.Tick += UpdateTimer_Tick;
      UpdateTimer.Start();
    }

    void UpdateTimer_Tick(object sender, EventArgs e)
    {
      BatteryProgress.BackColor = Color.Blue;

      PowerStatus ps = SystemInformation.PowerStatus;

      BatteryProgress.Value = (int)(ps.BatteryLifePercent * 100);

      BatteryLabel.Text = BatteryProgress.Value.ToString();

      switch (ps.BatteryChargeStatus)
      {
        case BatteryChargeStatus.Charging:
          BatteryProgress.Style = ProgressBarStyle.Marquee;
          break;
        case BatteryChargeStatus.Critical:
          BatteryProgress.Style = ProgressBarStyle.Blocks;
          BatteryProgress.ForeColor = Color.FromKnownColor(KnownColor.Red); 
          break;
        case BatteryChargeStatus.High:
          BatteryProgress.Style = ProgressBarStyle.Blocks;
          BatteryProgress.ForeColor = Color.FromKnownColor(KnownColor.Green); 
          break;
        case BatteryChargeStatus.Low:
          BatteryProgress.Style = ProgressBarStyle.Blocks;
          BatteryProgress.ForeColor = Color.FromKnownColor(KnownColor.Yellow); 
          break;
        case BatteryChargeStatus.NoSystemBattery:
          break;
      }
    }

#region 属性
#if TIA
    [Category("YZX")]
    [DisplayName("电池电量")]
    [Description("电池电量，百分比，绑定到某一个 WINCC 变量用于更新此变量")]
    [SupportedDynamicTypes(SupportedDynamicTypes.AllWithReport)]
#endif
    public int BatteryLifePercent
    {
      get
      {
        return BatteryProgress.Value;
      }
    }
#endregion
  }
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#if TIA
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

namespace YZX.WINCC.Controls
{

  [ToolboxBitmap(typeof(RandomView), "Toolbox.RandomView.bmp")]
  public class RandomView : WPFInWinForm
  {
#region 属性
    private int dbnum = 0;

#if TIA
    [Category("YZX")]
    [DisplayName("更新间隔")]
    [Description("每次向WINCC写入随机值的时间间隔，单位ms")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public int Interval
    {
      get
      {
        return dbnum;
      }
      set
      {
        dbnum = value;
      }
    }


    private int processvalue = 0;

#if TIA
    [Category("YZX")]
    [DisplayName("写入值")]
    [Description("绑定到某一个 WINCC 变量用于向此变量写入随机数")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public int ProcessValue
    {
      get
      {
        return processvalue;
      }
      set
      {
        dbnum = processvalue;
      }
    }

#if TIA
    [Category("YZX")]
    [DisplayName("最大值")]
    [Description("随机数上限")]
    [DefaultValue(typeof(int), "100")]
#endif
    public int Max { get; set; }

    [Category("YZX")]
    [DisplayName("最小值")]
    [Description("随机数下限")]
    [DefaultValue(typeof(int), "0")]
    public int Min { get; set; }
#endregion 属性

    public RandomView()
    {
      #if RTA
      TagMapConnected += RandomView_TagMapConnected;
      #endif
    }

#if RTA
    private void RandomView_TagMapConnected(object sender, EventArgs e)
    {
      UpdateTimer = new Timer();
      UpdateTimer.Interval = Interval;
      UpdateTimer.Tick += UpdateTimer_Tick;
      UpdateTimer.Start();
    }

    Timer UpdateTimer;
    Random seed = new Random();
    public void UpdateTimer_Tick(object sender, EventArgs e)
    {
      int pv = seed.Next(Min, Max);
      TryWriteTag("ProcessValue", pv);
    }
    #endif
  }
}

using System;
using System.ComponentModel;
using System.Xaml;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

#if TIA
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

namespace YZX.WINCC.Controls
{

  [ToolboxBitmap(typeof(PushButtonView), "Toolbox.PushButtonView.bmp")]
  public class PushButtonView : WPFInWinForm
  {
#region 属性
    private string text = "开始";
#if TIA
    [Category("YZX")]
    [DisplayName("文本")]
    [Description("文本")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public override string Text
    {
      get
      {
        return text;
      }
      set
      {
        text = value;
      }
    }
#endregion 属性

    public PushButtonView():base()
    {
      Load += PushButtonView_Load;
    }

    private void PushButtonView_Load(object sender, EventArgs e)
    {
      PrepareWPF();
    }

    public override void PrepareWPF()
    {
      try {
        BackColor = Color.Red;
        PushButtonWPF mc = new PushButtonWPF();
        WPF.Child = mc;
        hideInfo();
        #if RTA
        mc.PushButton.Click += PushButton_Click;
        #endif
        base.PrepareWPF();
      }
      catch (XamlParseException ex)
      {

      }
    }

#if RTA
    private void PushButton_Click(object sender, RoutedEventArgs e)
    {
      ProcessEvent("Pushed");
    }
#endif


    #region 事件

#if TIA
    [Description("Raised when the ToggleSwitch has changed state")]
    [Browsable(true)]
    [ScriptEvent]
#endif
    public event EventHandler Pushed;

#endregion
  }
}

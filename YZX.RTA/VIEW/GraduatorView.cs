using System;
using System.Xaml;
using System.Drawing;
using System.Windows.Controls;

#if TIA
using System.ComponentModel;
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

namespace YZX.WINCC.Controls
{

  [ToolboxBitmap(typeof(GraduatorView), "Toolbox.GraduatorView.bmp")]
  public class GraduatorView : WPFInWinForm
  {
#region 属性
#if TIA
    [Category("YZX")]
    [DisplayName("文本")]
    [Description("文本")]
    [SupportedDynamicTypes(SupportedDynamicTypes.None)]
#endif
    public int GraduatorCount
    {
      get;set;
    }
#endregion 属性

    public GraduatorView():base()
    {
      Load += GraduatorView_Load;
    }

    GraduatorWPF gr;
    public override Control WPFRoot
    {
      get
      {
        return gr;
      }
    }

    private void GraduatorView_Load(object sender, EventArgs e)
    {
      PrepareWPF();
    }

    public override void PrepareWPF()
    {
      try {
        gr = new GraduatorWPF();
        gr.addGraduators(GraduatorCount);
        WPF.Child = gr;
        hideInfo();
        base.PrepareWPF();
      }
      catch (XamlParseException ex)
      {

      }
    }
  }
}

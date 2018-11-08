using System;
using System.ComponentModel;
using System.Xaml;
using System.Drawing;

#if TIA
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(GadgetCommunicationView), "Toolbox.GadgetCommunicationView.bmp")]
  public class GadgetCommunicationView : WPFInWinForm
  {
#region 属性

    private int dbnum = 0;
#if TIA
    [Category("YZX")]
    [DisplayName("图像文件夹")]
    [Description("TagName")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public int DBNum
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
#endregion 属性
    public GadgetCommunicationView()
    {
      Load += GadgetCommunicationView_Load;
    }

    private void GadgetCommunicationView_Load(object sender, EventArgs e)
    {
      PrepareWPF();
    }

    public new void PrepareWPF()
    {
      try {
        GadgetCommunicationWPF mc = new GadgetCommunicationWPF();
        WPF.Child = mc;
        hideInfo();
      }
      catch (XamlParseException ex)
      {
        showInfo(ex.ToString());
      }
      
    }
  }
}

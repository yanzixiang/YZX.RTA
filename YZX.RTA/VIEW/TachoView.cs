using System.Xaml;
using System.Drawing;

#if TIA
using System.ComponentModel;
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

namespace YZX.WINCC.Controls
{

  [ToolboxBitmap(typeof(TachoView), "Toolbox.TachoView.bmp")]
  public class TachoView : WPFInWinForm
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
    public TachoView()
    {
    }

    public new void PrepareWPF()
    {
      

      try {
        TachoWPF mc = new TachoWPF();
        WPF.Child = mc;
       
      }
      catch (XamlParseException ex)
      {

      }
      
    }
  }
}

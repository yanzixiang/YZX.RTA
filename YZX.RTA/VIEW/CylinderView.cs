using System;
using System.ComponentModel;
using System.Xaml;
using System.Drawing;
using System.Windows.Controls;

#if TIA
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

using YZXLogicEngine.UDT;

namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(CylinderView), "Toolbox.CylinderView.bmp")]
  public class CylinderView : WPFInWinForm
  {
#region 属性
    private string cylindername = "未命名";
#if TIA
    [Category("YZX")]
    [DisplayName("气缸名称")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public string CylinderName
    {
      get
      {
        return cylindername;
      }
      set
      {
        cylindername = value;
      }
    }

    private CylinderType cylindertype = CylinderType.ShenSuo;
#if TIA
    [Category("YZX")]
    [DisplayName("气缸类型")]
    [SupportedDynamicTypes(SupportedDynamicTypes.None)]
#endif
    public CylinderType CylinderType
    {
      get
      {
        return cylindertype;
      }
      set
      {
        cylindertype = value;
      }
    }
    
    private int dbnum = 0;

#if TIA
    [Category("YZX")]
    [DisplayName("气缸数据块号")]
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
    public CylinderView()
    {
    }

    CylinderWPF mc;
    public override Control WPFRoot
    {
      get
      {
        return mc;
      }
    }
    public new void PrepareWPF()
    {
      try {
        mc = new CylinderWPF();
        WPF.Child = mc;
        hideInfo();
        base.PrepareWPF();
      }
      catch (XamlParseException ex)
      {
        Console.WriteLine("PrepareWPF" + ex.ToString());
      }
      
    }
  }
}

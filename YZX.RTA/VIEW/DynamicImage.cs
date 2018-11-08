using Common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;
using Extensions;

#if TIA
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

using System.Windows.Forms;

namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(DynamicImage), "Toolbox.DynamicImage.bmp")]
  public partial class DynamicImage : CControl
#if RTA
    , IObserver<ReadTagResult>
#endif
  {
    public DynamicImage()
    {
      InitializeComponent();
    }

#region CControl
    public override Control InnerControl
    {
      get
      {
        return pictureBox1;
      }
    }
#endregion

#region 属性

    private string basepath = "DynmicImage";
#if TIA
    [Category("YZX")]
    [DisplayName("图像文件夹")]
    [Description("TagName")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public string BasePath
    {
      get {
        return basepath;
      }
      set {
        basepath = value;
      }
    }

    private string filename = "404.png";

#if TIA
    [Category("YZX")]
    [DisplayName("图像文件名")]
    [Description("TagName")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public string FileName
    {
      get
      {
#if RTA
        string s = this.SafeInvoke(() =>
        {
          return filename;
        });
        return s;
#else
        return filename;
#endif
      }
      set {
#if RTA
        this.SafeInvoke(() =>
        {
          filename = value;
        });
#else 
        filename = value;
#endif
      }
    }


#endregion 属性

#if RTA
#region RX
    public override List<string> RXTags
    {
      get
      {
        return new List<string>()
        {
          "FileName"
        };
      }
    }
#endregion
#endif
  }
}

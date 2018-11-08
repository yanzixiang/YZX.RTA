using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

#if TIA
using System.ComponentModel;
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

using Extensions;
using Common;
using ZXing;
using ZXing.Common;

namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(ZXingView), "Toolbox.ZXingView.bmp")]
  public class ZXingView : CControl
  {
    public ZXingView()
    {
      this.SuspendLayout();

      Controls.Add(bv);

      this.ResumeLayout();

      Load += ZXingView_Load;
      SizeChanged += ZXingView_SizeChanged;
      HandleDestroyed += ZXingView_HandleDestroyed;
    }

    EncodingOptions options = null;
    BarcodeWriter writer = null;

    private void UpdateWriter()
    {
      options = new EncodingOptions
      {
        Width = Width,
        Height = Height,
        Margin = 0,
      };
      writer = new BarcodeWriter();
      writer.Format = BarcodeFormat;
      writer.Options = options;
    }
    private void ZXingView_Load(object sender, EventArgs e)
    {
      UpdateWriter();
    }

    private void ZXingView_SizeChanged(object sender, EventArgs e)
    {
      UpdateWriter();
    }

    private void ZXingView_HandleDestroyed(object sender, EventArgs e)
    {
      options = null;
      writer = null;
      bv.Image = null;
    }

    #region 属性
#if TIA
    [Category("YZX")]
    [DisplayName("编码")]
    [SupportedDynamicTypes(SupportedDynamicTypes.None)]
    [DefaultValue(typeof(BarcodeFormat), "BarcodeFormat.QR_CODE")]
#endif
    public BarcodeFormat BarcodeFormat { set; get; }

#if TIA
    [Category("YZX")]
    [DisplayName("字符串")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public string ShowString
    {
      get
      {
        return s;
      }
      set
      {
        this.SafeInvoke(() =>
        {
          if (value != null & value != s)
          {
            UpdatePixtureBox(value);
            s = value;
          }
        });
      }
    }
    private string s;
#endregion 属性

    public void UpdatePixtureBox(string s)
    {
      if (s != "")
      {
        if (writer != null)
        {
          Bitmap bitmap = writer.Write(s);
          bv.Image = bitmap;
          bitmap = null;
        }
      }
      else
      {
      }
    }

    public PictureBox bv = new PictureBox();
    public override Control InnerControl
    {
      get
      {
        return bv;
      }
    }

#if RTA
    #region RX
    public override List<string> RXTags
    {
      get
      {
        return new List<string>()
        {
          "ShowString"
        };
      }
    }
    #endregion
#endif
  }
}

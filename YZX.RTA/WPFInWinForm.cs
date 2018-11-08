using System;
using System.Drawing;
using System.Windows.Forms;

using Extensions;

namespace YZX.WINCC.Controls
{
  public partial class WPFInWinForm : CControl
  {
    public WPFInWinForm():base()
    {
      SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      BackColor = Color.Transparent;

      InitializeComponent();

      info.Multiline = true;
      info.BackColor = Color.Red;

      HandleDestroyed += WPFInWinForm_HandleDestroyed;

    }

    private void WPFInWinForm_HandleDestroyed(object sender, EventArgs e)
    {
      if(WPF != null)
      {
        WPF.Dispose();
      }
    }

    public override void UpdateSize(Size size)
    {
      base.UpdateSize(size);
      if (WPFRoot != null)
      {
        WPFRoot.Width = size.Width;
        WPFRoot.Height = size.Height;
      }
      info.Size = size;
    }

    public virtual void PrepareWPF() {
    }

    public override Control InnerControl
    {
      get
      {
        return WPF;
      }
    }

    public virtual System.Windows.Controls.Control WPFRoot
    {
      get
      {
        return null;
      }
    }

    public void showInfo(string text)
    {
      this.SafeInvoke(() =>
      {
        info.Text = text;
        info.Visible = true;
      });
    }
    public void hideInfo()
    {
      this.SafeInvoke(() =>
      {
        info.Visible = false;
      });
    }
  }
}

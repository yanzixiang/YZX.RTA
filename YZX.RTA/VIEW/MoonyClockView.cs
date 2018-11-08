using System.Drawing;

namespace YZX.WINCC.Controls
{

  [ToolboxBitmap(typeof(MoonyClockView), "Toolbox.MoonyClockView.bmp")]
  public class MoonyClockView : WPFInWinForm
  {
    public MoonyClockView()
    {
    }

    public new void PrepareWPF()
    {
      MoonyClockWPF mc = new MoonyClockWPF();
      WPF.Child = mc;
    }
  }
}

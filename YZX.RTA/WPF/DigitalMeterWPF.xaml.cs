using System.Windows;
using System.Windows.Controls;

namespace YZX.WINCC.Controls
{
  public partial class DigitalMeterWPF : UserControl
  {
    public DigitalMeterWPF()
    {
      InitializeComponent();
      SizeChanged += DigitalMeterWPF_SizeChanged;
    }

    private void DigitalMeterWPF_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      dm.Width = Width;
      dm.Height = Height;
    }
  }
}
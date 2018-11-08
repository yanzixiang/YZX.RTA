using System.Windows;
using System.Windows.Controls;

namespace YZX.WINCC.Controls
{
  public partial class GraduatorWPF : UserControl
  {
    public GraduatorWPF()
    {
      InitializeComponent();
    }

    /// <summary>
    /// 
    /// </summary>
    public void addGraduators(int Count)
    {
      for(int i = 0;i < Count; i++)
      {
        addGraduator();
      }
    }

    private void ItemClick(object sender, RoutedEventArgs e)
    {
      Graduator.AnimateIntoView(sender as UIElement, true);
    }

    private void addGraduator()
    {
      Graduator.Children.Add(new Button() { Content = string.Format("分度 {0}", Graduator.Children.Count + 1) });
    }
  }
}
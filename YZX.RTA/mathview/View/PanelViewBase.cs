namespace mathview.View.Expressions
{
  using System.Windows.Controls;
  using System.Windows.Data;
  using System.Windows.Documents;
  using System.Windows.Media;

  /// <summary>
  /// A base class for all panel based views.
  /// <para>Needed for Silverlight compatibility.</para>
  /// </summary>
  public class PanelViewBase : Panel
  {
    /// <summary>
    /// Gets the FontSize.
    /// </summary>
    public double FontSize
    {
      get { return (double)GetValue(Control.FontSizeProperty); }
    }

    /// <summary>
    /// Gets the FontFamily.
    /// </summary>
    public FontFamily FontFamily
    {
      get { return (FontFamily)GetValue(Control.FontFamilyProperty); }
    }

    /// <summary>
    /// Gets the Baseline property of FontFamily.
    /// </summary>
    public double FontFamilyBaseline
    {
      get { return FontFamily.Baseline; }
    }

    /// <summary>
    /// Gets the LineSpacing property of FontFamily.
    /// </summary>
    public double FontFamilyLineSpacing
    {
      get { return FontFamily.LineSpacing; }
    }

    /// <summary>
    /// Setup a new binding on a child content presenter.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <param name="bindingSource">The source of the binding.</param>
    /// <param name="path">The binding path.</param>
    protected void SetupBinding(ContentPresenter view, object bindingSource, string path)
    {
      //view.ClearValue(ContentPresenter.ContentProperty);

      if (bindingSource != null)
      {
        var b = new Binding(path);
        b.Source = bindingSource;
        view.SetBinding(ContentPresenter.ContentProperty, b);
      }
    }
  }
}

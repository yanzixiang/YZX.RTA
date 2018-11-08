namespace mathview.View.Expressions
{
  using System.ComponentModel;
  using System.Windows;
  using System.Windows.Controls;
  using mathview.Expressions;

  /// <summary>
  /// A view class for whitespaces.
  /// </summary>
  public class WhitespaceView : PanelViewBase
  {
    /// <summary>
    /// Creates a new <see cref="WhitespaceView"/> instance.
    /// </summary>
    public WhitespaceView()
    {
      // Nothing to do...
    }

    /// <summary>
    /// The Whitespace.
    /// </summary>
    public static DependencyProperty WhitespaceProperty = DependencyProperty.Register("Whitespace", typeof(Whitespace), typeof(WhitespaceView), new PropertyMetadata(null, WhitespaceChanged));

    /// <summary>
    /// The backing property for <see cref="WhitespaceView.WhitespaceProperty"/>
    /// </summary>
    [Category("Common")]
    public Whitespace Whitespace
    {
      get { return (Whitespace)GetValue(WhitespaceProperty); }
      set { SetValue(WhitespaceProperty, value); }
    }

    private static void WhitespaceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var view = (WhitespaceView)d;

      if (e.OldValue is Whitespace)
        ((Whitespace)e.OldValue).PropertyChanged -= view.WhitespacePropertyChanged;

      if (e.NewValue is Whitespace)
        ((Whitespace)e.NewValue).PropertyChanged += view.WhitespacePropertyChanged;

      view.SetupSize();
    }

    private void WhitespacePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case "EmFactor":
          SetupSize();
          break;
      }
    }

    private void SetupSize()
    {
      var space = (this.Whitespace ?? new Whitespace()).EmFactor * this.FontFamilyLineSpacing * this.FontSize;

      this.Margin = new Thickness(space, 0, 0, 0);
    }
  }
}

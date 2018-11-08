namespace mathview.View.Expressions
{
  using System;
  using System.ComponentModel;
  using System.Windows;
  using System.Windows.Controls;
  using System.Windows.Data;
  using System.Windows.Media;
  using System.Windows.Shapes;
  using mathview.Expressions;

  /// <summary>
  /// A view class for fractions.
  /// </summary>
  public class FractionView : PanelViewBase
  {
    #region Private Variables
    private ContentPresenter NumeratorView;
    private ContentPresenter DenominatorView;
    private Line Line;
    private Size sizeN = Size.Empty;
    private Size sizeD = Size.Empty;
    private Size sizeTotal = Size.Empty;
    private double childFontSize = 12;
    private double offN = 0;
    private double offD = 0;
    private Rect rectN = new Rect();
    private Rect rectD = new Rect();
    #endregion

    /// <summary>
    /// Creates a new <see cref="FractionView"/> instance.
    /// </summary>
    public FractionView()
    {
      NumeratorView = new ContentPresenter();
      DenominatorView = new ContentPresenter();
      Line = new Line();

      this.Children.Add(NumeratorView);
      this.Children.Add(DenominatorView);
      this.Children.Add(Line);
    }

    #region Fraction DependencyProperty
    /// <summary>
    /// The Fraction.
    /// </summary>
    public static DependencyProperty FractionProperty = DependencyProperty.Register("Fraction", typeof(Fraction), typeof(FractionView), new PropertyMetadata(null, FractionChanged));

    /// <summary>
    /// The backing property for <see cref="FractionView.FractionProperty"/>
    /// </summary>
    [Category("Common")]
    public Fraction Fraction
    {
      get { return (Fraction)GetValue(FractionProperty); }
      set { SetValue(FractionProperty, value); }
    }

    private static void FractionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var view = (FractionView)d;

      if (e.OldValue is Fraction)
        ((Fraction)e.OldValue).PropertyChanged -= view.FractionPropertyChanged;

      if (e.NewValue is Fraction)
        ((Fraction)e.NewValue).PropertyChanged += view.FractionPropertyChanged;

      view.SetupBinding(view.NumeratorView, view.Fraction, "Numerator");
      view.SetupBinding(view.DenominatorView, view.Fraction, "Denominator");
      view.SetupSize();

      view.InvalidateVisual();
    }

    private void FractionPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case "Numerator":
          SetupBinding(NumeratorView, this.Fraction, "Numerator");
          break;

        case "Denominator":
          SetupBinding(DenominatorView, this.Fraction, "Denominator");
          break;

        case "Size":
          SetupSize();
          break;

        case "Style":
          // Just invalidate for new arrange and measure steps.
          break;
      }

      InvalidateVisual();
    }
    #endregion

    #region BaseLineDelta DependencyProperty
    /// <summary>
    /// The BaseLineDelta.
    /// </summary>
    public static DependencyProperty BaseLineDeltaProperty = DependencyProperty.Register("BaseLineDelta", typeof(double), typeof(FractionView), new PropertyMetadata(14.0 / 50.0, BaseLineDeltaChanged));

    /// <summary>
    /// The backing property for <see cref="FractionView.BaseLineDeltaProperty"/>
    /// </summary>
    [Category("Common")]
    public double BaseLineDelta
    {
      get { return (double)GetValue(BaseLineDeltaProperty); }
      set { SetValue(BaseLineDeltaProperty, value); }
    }

    private static void BaseLineDeltaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var view = (FractionView)d;

      view.InvalidateVisual();
    }
    #endregion

    #region Size Setup
    private void SetupSize()
    {
#if !SILVERLIGHT
      var frac = this.Fraction;
      childFontSize = this.FontSize;

      if (frac == null)
        return;

      if (frac.Style == FractionStyle.Slanted)
        childFontSize /= 1.5;
      else if (frac.Size == FractionSize.Text || (frac.IsSubFraction && frac.Size == FractionSize.Default))
        childFontSize *= 16.0 / 21.0;

      NumeratorView.SetValue(Control.FontSizeProperty, childFontSize);
      DenominatorView.SetValue(Control.FontSizeProperty, childFontSize);
#endif
    }
    #endregion

    #region Measure & Arrange
    /// <summary>
    /// Measures the size in layout required for child elements and determines a size for the <see cref="FractionView"/>. 
    /// </summary>
    /// <param name="constraint">The available size that this element can give to child elements. Infinity can be specified as a value to indicate that the element will size to whatever content is available.</param>
    /// <returns>The size that this element determines it needs during layout, based on its calculations of child element sizes.</returns>
    protected override Size MeasureOverride(Size constraint)
    {
      NumeratorView.Measure(constraint);
      DenominatorView.Measure(constraint);

      sizeN = NumeratorView.DesiredSize;
      sizeD = DenominatorView.DesiredSize;

      var frac = this.Fraction;

      if (frac != null && frac.Style == FractionStyle.Slanted)
      {
        var d = this.FontFamilyBaseline * (this.FontSize - childFontSize);
        var dx = this.FontSize / 2.5;
        var dy = (sizeN.Height + sizeD.Height + this.FontSize) / 2.0 - d;

        // Normally, (1.0 - Y-ratio) yields offset, but this is too tight.
        offN = dx * (0.9 - sizeN.Height / dy);
        offD = dx * (0.9 - sizeD.Height / dy);

        sizeTotal = new Size();
        sizeTotal.Width = sizeN.Width + sizeD.Width + dx - offN - offD;
        sizeTotal.Height = dy;
      }
      else
      {
        sizeTotal = new Size();
        sizeTotal.Width = Math.Max(sizeN.Width, sizeD.Width);
        sizeTotal.Height = sizeN.Height + sizeD.Height;
      }

      SetValue(BlockView.BaseLineProperty, sizeN.Height + this.BaseLineDelta * this.FontSize);

      if (frac == null)
        return sizeTotal;

      switch (frac.Style)
      {
        case FractionStyle.Default:
          Line.X1 = 0;
          Line.Y1 = sizeN.Height;
          Line.X2 = sizeTotal.Width;
          Line.Y2 = sizeN.Height;
#if SILVERLIGHT
                    Line.Stroke = new SolidColorBrush(Colors.Black);
#else
          Line.Stroke = (Brush)GetValue(Control.ForegroundProperty);
#endif

          Line.Measure(sizeTotal);
          break;

        case FractionStyle.Slanted:
          Line.X1 = sizeN.Width - offN;
          Line.Y1 = sizeTotal.Height;
          Line.X2 = sizeTotal.Width - sizeD.Width + offD;
          Line.Y2 = 0;
#if SILVERLIGHT
                    Line.Stroke = new SolidColorBrush(Colors.Black);
#else
          Line.Stroke = (Brush)GetValue(Control.ForegroundProperty);
#endif

          Line.Measure(sizeTotal);
          break;

        case FractionStyle.Binomial:
          // TODO @ FractionView: Add braces (...).
          break;
      }

      return sizeTotal;
    }

    /// <summary>
    /// Positions child elements and determines a size for the <see cref="FractionView"/>. 
    /// </summary>
    /// <param name="arrangeBounds">The final area within the parent that this element should use to arrange itself and its children.</param>
    /// <returns>The actual size used.</returns>
    protected override Size ArrangeOverride(Size arrangeBounds)
    {
      var frac = this.Fraction;

      if (frac != null && frac.Style == FractionStyle.Slanted)
      {
        rectN = new Rect(0.0, 0.0, sizeN.Width, sizeN.Height);
        rectD = new Rect(sizeTotal.Width - sizeD.Width, sizeTotal.Height - sizeD.Height, sizeD.Width, sizeD.Height);
      }
      else
      {
        rectN = new Rect((sizeTotal.Width - sizeN.Width) / 2.0, 0.0, sizeN.Width, sizeN.Height);
        rectD = new Rect((sizeTotal.Width - sizeD.Width) / 2.0, sizeN.Height, sizeD.Width, sizeD.Height);
      }

      NumeratorView.Arrange(rectN);
      DenominatorView.Arrange(rectD);

      if (frac == null)
        return sizeTotal;

      switch (frac.Style)
      {
        case FractionStyle.Default:
          Line.Arrange(new Rect(new Point(), sizeTotal));
          break;

        case FractionStyle.Slanted:
          Line.Arrange(new Rect(new Point(), sizeTotal));
          break;

        case FractionStyle.Binomial:
          // TODO @ FractionView: Add braces (...).
          break;
      }

      return sizeTotal;
    }
    #endregion
  }
}

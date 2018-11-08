namespace mathview.View.Expressions
{
  using System;
  using System.ComponentModel;
  using System.Windows;
  using System.Windows.Controls;
  using System.Windows.Data;
  using System.Windows.Documents;
  using System.Windows.Media;
  using mathview.Expressions;

  /// <summary>
  /// A view class for sub- and superscripts.
  /// </summary>
  public class SubAndSuperscriptView : PanelViewBase
  {
    #region Private Variables
    private ContentPresenter ExpressionView;
    private ContentPresenter SubscriptView;
    private ContentPresenter SuperscriptView;
    private double childFontSize = 12;
    private Size sizeE = Size.Empty;
    private Size sizeSb = Size.Empty;
    private Size sizeSp = Size.Empty;
    private Size sizeTotal = Size.Empty;
    private Rect rectE = new Rect();
    private Rect rectSb = new Rect();
    private Rect rectSp = new Rect();
    #endregion

    /// <summary>
    /// Creates a new <see cref="SubAndSuperscriptView"/> instance.
    /// </summary>
    public SubAndSuperscriptView()
    {
      ExpressionView = new ContentPresenter();
      SubscriptView = new ContentPresenter();
      SuperscriptView = new ContentPresenter();

      this.Children.Add(ExpressionView);
      this.Children.Add(SubscriptView);
      this.Children.Add(SuperscriptView);
    }

    #region SubAndSuperscript DependencyProperty
    /// <summary>
    /// The SubAndSuperscript.
    /// </summary>
    public static DependencyProperty SubAndSuperscriptProperty = DependencyProperty.Register("SubAndSuperscript", typeof(SubAndSuperscript), typeof(SubAndSuperscriptView), new PropertyMetadata(null, SubAndSuperscriptChanged));

    /// <summary>
    /// The backing property for <see cref="SubAndSuperscriptView.SubAndSuperscriptProperty"/>
    /// </summary>
    [Category("Common")]
    public SubAndSuperscript SubAndSuperscript
    {
      get { return (SubAndSuperscript)GetValue(SubAndSuperscriptProperty); }
      set { SetValue(SubAndSuperscriptProperty, value); }
    }

    private static void SubAndSuperscriptChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var view = (SubAndSuperscriptView)d;

      if (e.OldValue is SubAndSuperscript)
        ((SubAndSuperscript)e.OldValue).PropertyChanged -= view.SubAndSuperscriptPropertyChanged;

      if (e.NewValue is SubAndSuperscript)
        ((SubAndSuperscript)e.NewValue).PropertyChanged += view.SubAndSuperscriptPropertyChanged;

      view.SetupBinding(view.ExpressionView, "Expression");
      view.SetupBinding(view.SubscriptView, "SubScript");
      view.SetupBinding(view.SuperscriptView, "SuperScript");

      view.InvalidateVisual();
    }

    private void SubAndSuperscriptPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case "Expression":
          SetupBinding(ExpressionView, e.PropertyName);
          break;

        case "SubScript":
          SetupBinding(SubscriptView, e.PropertyName);
          break;

        case "SuperScript":
          SetupBinding(SuperscriptView, e.PropertyName);
          break;
      }

      InvalidateVisual();
    }
    #endregion

    #region Binding & Size Setup
    private void SetupBinding(ContentPresenter view, string property)
    {
      var script = this.SubAndSuperscript;

#if !SILVERLIGHT
      BindingOperations.ClearBinding(view, ContentPresenter.ContentProperty);
#endif

      if (script != null)
      {
        var b = new Binding(property);
        b.Source = script;
        view.SetBinding(ContentPresenter.ContentProperty, b);
      }
    }

    private void SetupSize()
    {
#if !SILVERLIGHT
      childFontSize = this.FontSize * 16.0 / 21.0;

      SubscriptView.SetValue(Control.FontSizeProperty, childFontSize);
      SuperscriptView.SetValue(Control.FontSizeProperty, childFontSize);
#endif
    }
    #endregion

    #region Measure & Arrange
    /// <summary>
    /// Measures the size in layout required for child elements and determines a size for the <see cref="SubAndSuperscriptView"/>. 
    /// </summary>
    /// <param name="constraint">The available size that this element can give to child elements. Infinity can be specified as a value to indicate that the element will size to whatever content is available.</param>
    /// <returns>The size that this element determines it needs during layout, based on its calculations of child element sizes.</returns>
    protected override Size MeasureOverride(Size constraint)
    {
      SetupSize();

      ExpressionView.Measure(constraint);
      SubscriptView.Measure(constraint);
      SuperscriptView.Measure(constraint);

      sizeE = ExpressionView.DesiredSize;
      sizeSb = SubscriptView.DesiredSize;
      sizeSp = SuperscriptView.DesiredSize;

      var dW = this.FontSize * 0.1;
      var hS = sizeSb.Height + sizeSp.Height;
      var h = sizeE.Height;

      if (hS > h)
      {
        SetValue(BlockView.BaseLineProperty, (hS - h) / 2.0 + this.FontFamilyBaseline * this.FontSize);
        h = hS;
      }
      else
      {
        ClearValue(BlockView.BaseLineProperty);
      }

      sizeTotal = new Size(sizeE.Width + dW + Math.Max(sizeSb.Width, sizeSp.Width), h);

      return sizeTotal;
    }

    /// <summary>
    /// Positions child elements and determines a size for the <see cref="SubAndSuperscriptView"/>. 
    /// </summary>
    /// <param name="arrangeBounds">The final area within the parent that this element should use to arrange itself and its children.</param>
    /// <returns>The actual size used.</returns>
    protected override Size ArrangeOverride(Size arrangeBounds)
    {
      var dW = this.FontSize * 0.1;
      var hS = sizeSb.Height + sizeSp.Height;

      if (hS > sizeE.Height)
      {
        rectE = new Rect(new Point(0.0, (hS - sizeE.Height) / 2.0), sizeE);
        rectSb = new Rect(new Point(sizeE.Width + dW, hS / 2.0), sizeSb);
        rectSp = new Rect(new Point(sizeE.Width + dW, 0.0), sizeSp);
      }
      else
      {
        rectE = new Rect(new Point(0.0, 0.0), sizeE);
        rectSb = new Rect(new Point(sizeE.Width + dW, sizeE.Height - sizeSb.Height), sizeSb);
        rectSp = new Rect(new Point(sizeE.Width + dW, 0.0), sizeSp);
      }

      ExpressionView.Arrange(rectE);
      SubscriptView.Arrange(rectSb);
      SuperscriptView.Arrange(rectSp);

      return sizeTotal;
    }
    #endregion
  }
}

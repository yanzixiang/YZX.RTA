namespace mathview.View.Expressions
{
  using System;
  using System.ComponentModel;
  using System.Windows;
  using System.Windows.Controls;
  using System.Windows.Documents;
  using System.Windows.Media;

  /// <summary>
  /// A view class for blocks.
  /// </summary>
  public class BlockView : Panel
  {
    private Size finalSize = new Size();
    private double maxBaseLine = 0.0;

    /// <summary>
    /// Creates a new <see cref="BlockView"/> instance.
    /// </summary>
    public BlockView()
    {
      // Nothing to do...
    }

    #region IsVertical DependencyProperty
    /// <summary>
    /// The IsVertical flag.
    /// </summary>
    public static DependencyProperty IsVerticalProperty = DependencyProperty.Register("IsVertical", typeof(bool), typeof(BlockView), new PropertyMetadata(false));

    /// <summary>
    /// The backing property for <see cref="BlockView.IsVerticalProperty"/>
    /// </summary>
    [Category("Common")]
    public bool IsVertical
    {
      get { return (bool)GetValue(IsVerticalProperty); }
      set { SetValue(IsVerticalProperty, value); }
    }
    #endregion

    #region BaseLine Attached DependencyProperty
    /// <summary>
    /// The BaseLine property.
    /// </summary>
    public static readonly DependencyProperty BaseLineProperty = DependencyProperty.RegisterAttached("BaseLine", typeof(double), typeof(BlockView), new PropertyMetadata(double.NaN));

    /// <summary>
    /// Gets the baseline of the given object.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>The baseline, if set.</returns>
    public static double GetBaseLine(DependencyObject obj)
    {
      return (double)obj.GetValue(BaseLineProperty);
    }

    /// <summary>
    /// Sets the baseline to the given object.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <param name="value">The value of baseline.</param>
    public static void SetBaseLine(DependencyObject obj, double value)
    {
      obj.SetValue(BaseLineProperty, value);
    }

    private double GetChildBaseLine(DependencyObject child)
    {
      var baseLine = GetBaseLine(child);

      if (double.IsNaN(baseLine))
      {
        // Calculate the baseline from inherited properties.
#if SILVERLIGHT
                //baseLine = (double)child.GetValue(TextElement.FontSizeProperty);
                baseLine = 12.0;
#else
        baseLine = (double)child.GetValue(TextElement.FontSizeProperty) * ((FontFamily)child.GetValue(TextElement.FontFamilyProperty)).Baseline;
#endif
      }

      return baseLine;
    }
    #endregion

    #region Measure & Arrange
    /// <summary>
    /// Measures the size in layout required for child elements and determines a size for the <see cref="BlockView"/>. 
    /// </summary>
    /// <param name="constraint">The available size that this element can give to child elements. Infinity can be specified as a value to indicate that the element will size to whatever content is available.</param>
    /// <returns>The size that this element determines it needs during layout, based on its calculations of child element sizes.</returns>
    protected override Size MeasureOverride(Size constraint)
    {
      finalSize = new Size();
      maxBaseLine = 0;

      foreach (ContentPresenter child in this.Children)
      {
        child.Measure(constraint);

        var childSize = child.DesiredSize;
        var subChild = VisualTreeHelper.GetChild(child, 0) as FrameworkElement;
        var childMargin = (subChild ?? child).Margin;

        var w = childSize.Width + childMargin.Left + childMargin.Right;
        var h = childSize.Height + childMargin.Top + childMargin.Bottom;

        if (this.IsVertical)
        {
          finalSize.Height += h;
          finalSize.Width = Math.Max(finalSize.Width, w);
        }
        else
        {
          finalSize.Height = Math.Max(finalSize.Height, h);
          finalSize.Width += w;

          // Also, calculate the maximum baseline.
          var baseLine = GetChildBaseLine(subChild ?? child);

          if (maxBaseLine < baseLine)
            maxBaseLine = baseLine;
        }
      }

      return finalSize;
    }

    /// <summary>
    /// Positions child elements and determines a size for the <see cref="BlockView"/>. 
    /// </summary>
    /// <param name="arrangeBounds">The final area within the parent that this element should use to arrange itself and its children.</param>
    /// <returns>The actual size used.</returns>
    protected override Size ArrangeOverride(Size arrangeBounds)
    {
      var pt = new Point();

      foreach (ContentPresenter child in this.Children)
      {
        var childSize = child.DesiredSize;
        var subChild = VisualTreeHelper.GetChild(child, 0) as FrameworkElement;
        var childMargin = (subChild ?? child).Margin;

        var w = childSize.Width + childMargin.Left + childMargin.Right;
        var h = childSize.Height + childMargin.Top + childMargin.Bottom;

        var baseLine = GetChildBaseLine(subChild ?? child);

        if (this.IsVertical)
        {
          // TODO @ BlockView: Alignment to equality signs etc.
          pt.X = (finalSize.Width - w) / 2.0;
        }
        else
        {
          // Calculate the baseline with respect to the other child elements.
          pt.Y = maxBaseLine - baseLine;
        }

        child.Arrange(new Rect(pt, childSize));

        if (this.IsVertical)
        {
          pt.X = 0;
          pt.Y += h;
        }
        else
        {
          pt.X += w;
          pt.Y = 0;
        }
      }

      return finalSize;
    }
    #endregion
  }
}

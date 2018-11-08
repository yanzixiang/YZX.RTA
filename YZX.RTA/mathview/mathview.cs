using System;
using System.Windows.Markup;
using mathview.Expressions;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using mathview.Parsers.LaTeX;

namespace mathview.View
{
  public class MathView : UserControl
  {
    #region Source Property
    /// <summary>
    /// The Source.
    /// </summary>
    public static DependencyProperty SourceProperty = 
      DependencyProperty.Register("Source", 
        typeof(ExpressionBase), 
        typeof(MathView), 
        new PropertyMetadata(null));

    /// <summary>
    /// The backing property for <see cref="MathView.SourceProperty"/>
    /// </summary>
    [Category("Common")]
    public ExpressionBase Source
    {
      get { return (ExpressionBase)GetValue(SourceProperty); }
      set { SetValue(SourceProperty, value); }
    }
    #endregion

    #region ItalicLetters Property
    /// <summary>
    /// Controls the usage of italic letters.
    /// </summary>
    public static DependencyProperty ItalicLettersProperty = 
      DependencyProperty.Register("ItalicLetters", 
      typeof(bool), 
      typeof(MathView), 
      new PropertyMetadata(true));

    /// <summary>
    /// The backing property for <see cref="MathView.ItalicLettersProperty"/>
    /// </summary>
    [Category("Common")]
    public bool ItalicLetters
    {
      get { return (bool)GetValue(ItalicLettersProperty); }
      set { SetValue(ItalicLettersProperty, value); }
    }
    #endregion

    /// <summary>
    /// Creates a new <see cref="MathView"/> instance.
    /// </summary>
    public MathView()
    {

      DataContext = this;
      Width = 60;
      Height = 60; 
      
      string text = String.Format("z_n^2");

      var parser = new LaTeXStringParser();

      var expr = parser.Parse(text);

      Source = expr;
      //Background = Brushes.Red;
      ContentPresenter cp = new ContentPresenter();
      Content = cp;

      //InitializeComponent();
    }
  }
}

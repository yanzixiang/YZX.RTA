namespace mathview.Expressions
{
  using System.Xml.Serialization;

  #region Size and Style enums
  /// <summary>
  /// The size types of fractions.
  /// </summary>
  public enum FractionSize
  {
    /// <summary>
    /// Default fraction size.
    /// </summary>
    Default,
    /// <summary>
    /// Text style.
    /// </summary>
    Text,
    /// <summary>
    /// Display style.
    /// </summary>
    Display,
    /// <summary>
    /// A chain of fractions.
    /// </summary>
    Continued,
  }

  /// <summary>
  /// The style types of fractions.
  /// </summary>
  public enum FractionStyle
  {
    /// <summary>
    /// Default fraction style.
    /// </summary>
    Default,
    /// <summary>
    /// Slanted fraction style.
    /// </summary>
    Slanted,
    /// <summary>
    /// Binomial fractions (no line).
    /// </summary>
    Binomial,
  }
  #endregion

  /// <summary>
  /// A class for fractions.
  /// </summary>
  public class Fraction : CompositeExpression
  {
    #region Variables & Properties
    private FractionStyle style = FractionStyle.Default;
    /// <summary>
    /// The style.
    /// </summary>
    [XmlAttribute]
    public FractionStyle Style
    {
      get { return style; }
      set
      {
        if (style != value)
        {
          style = value;
          RaisePropertyChanged("Style");
        }
      }
    }

    private FractionSize size = FractionSize.Default;
    /// <summary>
    /// The size.
    /// </summary>
    [XmlAttribute]
    public FractionSize Size
    {
      get { return size; }
      set
      {
        if (size != value)
        {
          size = value;
          RaisePropertyChanged("Size");
        }
      }
    }

    /// <summary>
    /// Gets the numerator.
    /// </summary>
    [XmlIgnore]
    public ExpressionBase Numerator
    {
      get
      {
        if (this.Expressions.Count > 0)
          return this.Expressions[0];
        else
          return null;
      }
    }

    /// <summary>
    /// Get the denominator.
    /// </summary>
    [XmlIgnore]
    public ExpressionBase Denominator
    {
      get
      {
        if (this.Expressions.Count > 1)
          return this.Expressions[1];
        else
          return null;
      }
    }

    private bool isSubFraction = false;
    /// <summary>
    /// A flag that determines if this fraction is embedded in another one (used for layout of <see cref="FractionSize.Default"/>).
    /// </summary>
    [XmlAttribute]
    public bool IsSubFraction
    {
      get { return isSubFraction; }
      set
      {
        if (isSubFraction != value)
        {
          isSubFraction = value;
          RaisePropertyChanged("IsSubFraction");
        }
      }
    }

    /// <summary>
    /// Gets a flag, that controls the usage of 1 char or block per expression.
    /// </summary>
    [XmlIgnore]
    public override bool UseOneCharPerExpression
    {
      get { return true; }
    }

    /// <summary>
    /// Gets the required expression count.
    /// </summary>
    [XmlIgnore]
    public override int RequiredExpressionCount
    {
      get
      {
        if (this.Numerator == null)
          return 2;
        if (this.Denominator == null)
          return 1;
        else
          return 0;
      }
    }
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new <see cref="Fraction"/> instance.
    /// </summary>
    public Fraction()
    {
    }

    /// <summary>
    /// Creates a new <see cref="Fraction"/> instance.
    /// </summary>
    /// <param name="style">The style of the fraction.</param>
    /// <param name="size">The size of the fraction.</param>
    public Fraction(FractionStyle style = FractionStyle.Default, FractionSize size = FractionSize.Default)
    {
      this.Style = style;
      this.Size = size;
    }
    #endregion

    /// <summary>
    /// Get a string representing this object.
    /// </summary>
    /// <returns>A string.</returns>
    public override string ToString()
    {
      var str = "/";

      if (this.Numerator != null)
        str = this.Numerator + str;
      if (this.Denominator != null)
        str = str + this.Denominator;

      return str;
    }
  }
}

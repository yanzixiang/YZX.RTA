namespace mathview.Expressions
{
  using System.Xml.Serialization;

  /// <summary>
  /// A class for whitespace characters.
  /// </summary>
  public class Whitespace : ExpressionBase
  {
    #region Variables & Properties
    private double emFactor = 1.0;
    /// <summary>
    /// The em factor.
    /// </summary>
    [XmlAttribute]
    public double EmFactor
    {
      get { return emFactor; }
      set
      {
        if (emFactor != value)
        {
          emFactor = value;
          RaisePropertyChanged("EmFactor");
        }
      }
    }
    #endregion

    /// <summary>
    /// Creates a new instance of <see cref="Whitespace"/>.
    /// </summary>
    public Whitespace()
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="Whitespace"/>.
    /// </summary>
    /// <param name="emFactor">The em factor.</param>
    public Whitespace(double emFactor)
    {
      this.EmFactor = emFactor;
    }
  }
}

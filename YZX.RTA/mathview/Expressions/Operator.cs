namespace mathview.Expressions
{
  using System.Xml.Serialization;

  /// <summary>
  /// This class is used for all operation signs.
  /// </summary>
  public class Operator : Atom
  {
    #region Variables & Properties
    /// <summary>
    /// The math text itself.
    /// </summary>
    [XmlAttribute]
    public override string Text
    {
      get { return base.Text; }
      set
      {
        base.Text = value.Replace('-', (char)0x2212);
      }
    }

    private bool hasLeftWhitespace = true;
    /// <summary>
    /// A flag controlling whitespace display on the left.
    /// </summary>
    [XmlAttribute]
    public bool HasLeftWhitespace
    {
      get { return hasLeftWhitespace; }
      set
      {
        if (hasLeftWhitespace != value)
        {
          hasLeftWhitespace = value;
          RaisePropertyChanged("HasLeftWhitespace");
        }
      }
    }

    private bool hasRightWhitespace = true;
    /// <summary>
    /// A flag controlling whitespace display on the right.
    /// </summary>
    [XmlAttribute]
    public bool HasRightWhitespace
    {
      get { return hasRightWhitespace; }
      set
      {
        if (hasRightWhitespace != value)
        {
          hasRightWhitespace = value;
          RaisePropertyChanged("HasRightWhitespace");
        }
      }
    }
    #endregion

    /// <summary>
    /// Creates a new instance of <see cref="Operator"/>.
    /// </summary>
    public Operator()
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="Operator"/>.
    /// </summary>
    /// <param name="text">The math text.</param>
    /// <param name="isUnknownCommand">A flag indicating unknown commands.</param>
    public Operator(string text, bool isUnknownCommand = false)
    {
      this.Text = text;
      this.IsUnknownCommand = isUnknownCommand;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Operator"/>.
    /// </summary>
    /// <param name="c">A Unicode character code.</param>
    public Operator(uint c)
    {
      if (c > 0xFFFF)
        this.Text = c.ConvertFromUtf32();
      else
        this.Text = "" + (char)c;
      this.IsUnknownCommand = false;
    }
  }
}

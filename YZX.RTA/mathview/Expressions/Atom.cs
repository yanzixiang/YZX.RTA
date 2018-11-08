namespace mathview.Expressions
{
  using System.Xml.Serialization;

  /// <summary>
  /// A class for simple expressions without any special characters and layout guidelines.
  /// </summary>
  public class Atom : ExpressionBase, ITextExpression
  {
    #region Variables & Properties
    private string text = "";
    /// <summary>
    /// The math text itself.
    /// </summary>
    [XmlAttribute]
    public virtual string Text
    {
      get { return text; }
      set
      {
        if (text != value)
        {
          text = value;
          RaisePropertyChanged("Text");
        }
      }
    }

    private bool isUnknownCommand = false;
    /// <summary>
    /// A flag indicating unknown commands.
    /// </summary>
    [XmlAttribute]
    public bool IsUnknownCommand
    {
      get { return isUnknownCommand; }
      set
      {
        if (isUnknownCommand != value)
        {
          isUnknownCommand = value;
          RaisePropertyChanged("IsUnknownCommand");
        }
      }
    }
    #endregion

    /// <summary>
    /// Creates a new instance of <see cref="Atom"/>.
    /// </summary>
    public Atom()
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="Atom"/>.
    /// </summary>
    /// <param name="text">The math text.</param>
    /// <param name="isUnknownCommand">A flag indicating unknown commands.</param>
    public Atom(string text, bool isUnknownCommand = false)
    {
      this.Text = text;
      this.IsUnknownCommand = isUnknownCommand;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Atom"/>.
    /// </summary>
    /// <param name="c">A Unicode character code.</param>
    public Atom(uint c)
    {
      this.Text = "" + (char)c;
      this.IsUnknownCommand = false;
    }

    /// <summary>
    /// Get a string representing this object.
    /// </summary>
    /// <returns>A string.</returns>
    public override string ToString()
    {
      return text;
    }
  }
}

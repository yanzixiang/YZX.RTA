namespace mathview.Expressions
{
  using System.Xml.Serialization;

  /// <summary>
  /// This class is used for variables (text containing letters).
  /// </summary>
  public class Variable : Atom
  {
    #region Variables & Properties
    /// <summary>
    /// The text itself. Setting this text also sets the italic text.
    /// </summary>
    [XmlAttribute]
    public override string Text
    {
      get { return base.Text; }
      set
      {
        base.Text = value;

        var t = "";

        foreach (var c in value)
        {
          if ((uint)c >= 0x03B1 && (uint)c <= 0x03C9)     // Greek small letters
            t += ((uint)c + 0x1D34B).ConvertFromUtf32();
          else if ((uint)c >= 0x0391 && (uint)c <= 0x03A9)
            t += c;
          else if (char.IsLower(c))
            t += ((uint)c + 0x1D3ED).ConvertFromUtf32();
          else
            t += ((uint)c + 0x1D3F3).ConvertFromUtf32();
        }

        this.ItalicText = t;
      }
    }

    private string italicText = "";
    /// <summary>
    /// The italic text.
    /// </summary>
    [XmlIgnore]
    public string ItalicText
    {
      get { return italicText; }
      private set
      {
        if (italicText != value)
        {
          italicText = value;
          RaisePropertyChanged("ItalicText");
        }
      }
    }
    #endregion

    /// <summary>
    /// Creates a new instance of <see cref="Variable"/>.
    /// </summary>
    public Variable()
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="Variable"/>.
    /// </summary>
    /// <param name="text">The math text.</param>
    /// <param name="isUnknownCommand">A flag indicating unknown commands.</param>
    public Variable(string text, bool isUnknownCommand = false)
    {
      this.Text = text;
      this.IsUnknownCommand = isUnknownCommand;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Variable"/>.
    /// </summary>
    /// <param name="c">A Unicode character code.</param>
    /// <param name="cItalic">The italic Unicode character code (optional).</param>
    public Variable(uint c, uint cItalic = 0)
    {
      this.IsUnknownCommand = false;

      if (cItalic > 0)
      {
        base.Text = "" + (char)c;
        italicText = cItalic.ConvertFromUtf32();
      }
      else
        this.Text = "" + (char)c;
    }
  }
}

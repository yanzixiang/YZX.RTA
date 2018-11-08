namespace mathview.Parsers
{
  using mathview.Expressions;
  using mathview.Parsers.LaTeX;
  using System.ComponentModel;
  using System.IO;
  using System.Text;
  using System.Xml.Serialization;

  /// <summary>
  /// An abstract class for parsers.
  /// </summary>
  [XmlInclude(typeof(LaTeXStringParser))]
  public abstract class ParserBase : IParser
  {
    #region INotifyPropertyChanged implementation
    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Raises a new <see cref="E:INotifyPropertyChanged.PropertyChanged"/> event.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected void RaisePropertyChanged(string propertyName)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    #region Variables & Properties
    private Encoding defaultEncoding = Encoding.UTF8;
    /// <summary>
    /// The default encoding.
    /// </summary>
    public Encoding DefaultEncoding
    {
      get { return defaultEncoding; }
      set
      {
        if (defaultEncoding != value)
        {
          defaultEncoding = value;
          RaisePropertyChanged("DefaultEncoding");
        }
      }
    }
    #endregion

    #region Parse functions
    /// <summary>
    /// Parse a text.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>An expression tree representing the text.</returns>
    public virtual ExpressionBase Parse(string text)
    {
      using (var stream = new MemoryStream())
      {
        var writer = new StreamWriter(stream, Encoding.UTF8);
        writer.Write(text);
        writer.Flush();

        stream.Position = 0;
        return Parse(stream);
      }
    }

    /// <summary>
    /// Parse a stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>An expression tree representing the content of the stream.</returns>
    public virtual ExpressionBase Parse(Stream stream)
    {
      return Parse(stream, defaultEncoding);
    }

    /// <summary>
    /// Parse a stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="encoding">The encoding of the stream.</param>
    /// <returns>An expression tree representing the content of the stream.</returns>
    public abstract ExpressionBase Parse(Stream stream, Encoding encoding);
    #endregion
  }
}

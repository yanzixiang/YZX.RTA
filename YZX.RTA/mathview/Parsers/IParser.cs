namespace mathview.Parsers
{
  using mathview.Expressions;
  using System.ComponentModel;
  using System.IO;
  using System.Text;

  /// <summary>
  /// An interface for parser classes.
  /// </summary>
  public interface IParser : INotifyPropertyChanged
  {
    /// <summary>
    /// Gets or sets the default encoding.
    /// </summary>
    Encoding DefaultEncoding { get; set; }

    /// <summary>
    /// Parse a text.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>An expression tree representing the text.</returns>
    ExpressionBase Parse(string text);

    /// <summary>
    /// Parse a stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>An expression tree representing the content of the stream.</returns>
    ExpressionBase Parse(Stream stream);

    /// <summary>
    /// Parse a stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="encoding">The encoding of the stream.</param>
    /// <returns>An expression tree representing the content of the stream.</returns>
    ExpressionBase Parse(Stream stream, Encoding encoding);
  }
}

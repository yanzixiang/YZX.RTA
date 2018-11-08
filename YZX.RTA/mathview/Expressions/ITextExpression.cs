namespace mathview.Expressions
{
  /// <summary>
  /// An interface for expressions containing text.
  /// </summary>
  public interface ITextExpression
  {
    /// <summary>
    /// The math text itself.
    /// </summary>
    string Text { get; set; }
  }
}

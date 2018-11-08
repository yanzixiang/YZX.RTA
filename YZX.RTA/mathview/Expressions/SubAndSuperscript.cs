namespace mathview.Expressions
{
  using System.Xml.Serialization;

  /// <summary>
  /// A class for sub- and superscripts.
  /// </summary>
  public class SubAndSuperscript : ExpressionBase
  {
    #region Variables & Properties
    private ExpressionBase subScript = null;
    /// <summary>
    /// The subscript.
    /// </summary>
    public ExpressionBase SubScript
    {
      get { return subScript; }
      set
      {
        if (subScript != value)
        {
          subScript = value;
          RaisePropertyChanged("SubScript");
        }
      }
    }

    private ExpressionBase superScript = null;
    /// <summary>
    /// The superscript.
    /// </summary>
    public ExpressionBase SuperScript
    {
      get { return superScript; }
      set
      {
        if (superScript != value)
        {
          superScript = value;
          RaisePropertyChanged("SuperScript");
        }
      }
    }

    private ExpressionBase expression = null;
    /// <summary>
    /// The decorated expression.
    /// </summary>
    public ExpressionBase Expression
    {
      get { return expression; }
      set
      {
        if (expression != value)
        {
          expression = value;
          RaisePropertyChanged("Expression");
        }
      }
    }
    #endregion
  }
}

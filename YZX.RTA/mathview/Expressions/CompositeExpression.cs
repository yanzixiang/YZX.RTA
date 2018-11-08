namespace mathview.Expressions
{
  using System.Collections.ObjectModel;
  using System.Xml.Serialization;

  /// <summary>
  /// A base class for composite expressions.
  /// </summary>
  public abstract class CompositeExpression : ExpressionBase
  {
    #region Variables & Properties
    private ObservableCollection<ExpressionBase> expressions = null;
    /// <summary>
    /// A list of expressions.
    /// </summary>
    public ObservableCollection<ExpressionBase> Expressions
    {
      get
      {
        if (expressions == null)
          expressions = new ObservableCollection<ExpressionBase>();
        return expressions;
      }
    }

    /// <summary>
    /// Gets a flag, that controls the usage of 1 char or block per expression.
    /// </summary>
    [XmlIgnore]
    public virtual bool UseOneCharPerExpression
    {
      get { return false; }
    }

    /// <summary>
    /// Gets the required expression count.
    /// </summary>
    [XmlIgnore]
    public abstract int RequiredExpressionCount { get; }
    #endregion

    /// <summary>
    /// Adds another expression.
    /// </summary>
    /// <param name="expr">The new expression.</param>
    public virtual void AddEpression(ExpressionBase expr)
    {
      this.Expressions.Add(expr);
      RaisePropertyChanged("RequiredExpressionCount");
    }
  }
}

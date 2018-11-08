namespace mathview.Expressions
{
  using System.Xml.Serialization;

  /// <summary>
  /// A block of expressions consisting of a list of expressions that are drawn one after the other.
  /// </summary>
  public class Block : CompositeExpression
  {
    #region Variables & Properties
    /// <summary>
    /// Gets the required expression count.
    /// </summary>
    [XmlIgnore]
    public override int RequiredExpressionCount
    {
      get { return 0; }
    }

    private bool isVertical = false;
    /// <summary>
    /// This flag determines vertical flow control.
    /// </summary>
    [XmlAttribute]
    public bool IsVertical
    {
      get { return isVertical; }
      set
      {
        if (isVertical != value)
        {
          isVertical = value;
          RaisePropertyChanged("IsVertical");
        }
      }
    }
    #endregion

    /// <summary>
    /// Get a string representing this object.
    /// </summary>
    /// <returns>A string.</returns>
    public override string ToString()
    {
      var str = "";

      foreach (var expr in this.Expressions)
      {
        if (!string.IsNullOrWhiteSpace(str) && isVertical)
          str += "\r\n";

        str += expr.ToString();
      }

      return "(" + str + ")";
    }
  }
}

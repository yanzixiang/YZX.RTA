namespace mathview.Expressions
{
  using System.ComponentModel;
  using System.Xml.Serialization;

  /// <summary>
  /// The base class for all math expressions.
  /// </summary>
  [XmlInclude(typeof(Atom))]
  [XmlInclude(typeof(Variable))]
  [XmlInclude(typeof(Operator))]
  [XmlInclude(typeof(Block))]
  [XmlInclude(typeof(Whitespace))]
  [XmlInclude(typeof(Fraction))]
  [XmlInclude(typeof(SubAndSuperscript))]
  public abstract class ExpressionBase
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
  }
}

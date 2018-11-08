using System.Xml.Serialization;

namespace FwDotNetContainer
{
  [XmlRoot("Property")]
  public class ComponentProperty
  {
    private string _name;
    private string _value;
    private object _object;
    private int _dispId;

    [XmlAttribute("dispid")]
    public int DispID
    {
      get
      {
        return this._dispId;
      }
      set
      {
        this._dispId = value;
      }
    }

    [XmlAttribute("name")]
    public string Name
    {
      get
      {
        return this._name;
      }
      set
      {
        this._name = value;
      }
    }

    [XmlAttribute("value")]
    public string value
    {
      get
      {
        return object.ReferenceEquals((object) this._value, this._object) ? this._value : "";
      }
      set
      {
        if (string.IsNullOrEmpty(value))
          return;
        this._value = value;
        this._object = (object) value;
      }
    }

    [XmlElement("Value")]
    public string Value
    {
      get
      {
        return object.ReferenceEquals((object) this._value, this._object) ? "" : this._value;
      }
      set
      {
        if (string.IsNullOrEmpty(value))
          return;
        this._value = value;
        this._object = DotNetContainer.GetPropertyValueAsObject(this._value);
      }
    }

    [XmlIgnore]
    public object ObjectValue
    {
      get
      {
        return this._object;
      }
      set
      {
        this._object = value;
        this._value = DotNetContainer.GetPropertyValueAsString(this._object);
      }
    }
  }
}

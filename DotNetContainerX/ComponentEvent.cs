using System.Xml.Serialization;

namespace FwDotNetContainer
{
  [XmlRoot("Event")]
  public class ComponentEvent
  {
    private string _name;
    private string _runtimeAction;
    private string _type;

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

    [XmlElement("Value")]
    public string RuntimeAction
    {
      get
      {
        return this._runtimeAction;
      }
      set
      {
        this._runtimeAction = value;
      }
    }

    [XmlAttribute("type")]
    public string Type
    {
      get
      {
        return this._type;
      }
      set
      {
        this._type = value;
      }
    }
  }
}

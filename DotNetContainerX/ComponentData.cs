using System.Collections.Generic;
using System.Xml.Serialization;

namespace FwDotNetContainer
{
  [XmlRoot("Component")]
  public class ComponentData
  {
    private List<ComponentProperty> _componentProperties = new List<ComponentProperty>();
    private List<ComponentEvent> _componentEvents = new List<ComponentEvent>();
    private string _type;
    private string _codebase;
    private string _assembly;

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

    [XmlAttribute("codebase")]
    public string CodeBase
    {
      get
      {
        return this._codebase;
      }
      set
      {
        this._codebase = value;
      }
    }

    [XmlAttribute("assembly")]
    public string Assembly
    {
      get
      {
        return this._assembly;
      }
      set
      {
        this._assembly = value;
      }
    }

    [XmlArray("Properties")]
    [XmlArrayItem("Property")]
    public List<ComponentProperty> Properties
    {
      get
      {
        return this._componentProperties;
      }
    }

    [XmlArray("Events")]
    [XmlArrayItem("Event")]
    public List<ComponentEvent> Events
    {
      get
      {
        return this._componentEvents;
      }
    }
  }
}

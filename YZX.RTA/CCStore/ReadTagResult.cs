using System;
using Extensions;

namespace YZX.WINCC.Controls
{
  public class ReadTagResult
  {
    public string TagName { get; set; }
    public DateTime UpdateTime { get; set; }

    public object Value { get; set; }

    public ReadTagResult(string tag)
    {
      TagName = tag;
    }

    public int GetValueAsInt()
    {
      int i = Value.ToString().ToInt();
      return i;
    }

    public string GetValueAsString()
    {
      string s = Value.ToString();
      return s;
    }

    public object GetValueAs(Type t)
    {
      if(Value == null)
      {
        return null;
      }
      string ts = t.FullName;
      switch (ts)
      {
        case "System.Int32":
          return GetValueAsInt();
        case "System.String":
          return GetValueAsString();
        default:
          return Value;
      }
    }
  }
}

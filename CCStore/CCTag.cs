using System.Collections.Generic;

#if TIA
using Siemens.Simatic.Hmi.Utah.Tag;
#endif

namespace YZX.WINCC.Controls
{
  public class CCTag
  {
    #if TIA
    private IHmiTag tag;
    #endif

    public string Name { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Type { get; set; } = "";
    public string DeviceName { get; set; } = "";
    public string Address { get; set; } = "";
    public string Comment { get; set; } = "";
    public int AcquisitionCycle { get; set; } = 100;

    public CCTag() {
    }

#if TIA
    public CCTag(IHmiTag tag)
    {
      this.tag = tag;
      Address = tag.Address;
      Comment = tag.CommentAsString;
      Name = tag.Name;
      FullName = tag.FullName;
    }
#endif
  }
}

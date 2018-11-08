using System.Collections.Generic;

namespace YZX.WINCC.Controls
{
  public class CCFunction
  {
    public string Type { get; set; } = "";
    public string Name { get; set; } = "";
    public string VBCode { get; set; } = "";
    public string SourceCodeForRdp { get; set; } = "";

    public List<CCParameter> Parameters { get; set; }
    public CCFunction() {
      VBCode = "";
    }

  }
}

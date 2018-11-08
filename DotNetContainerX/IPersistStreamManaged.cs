using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace FwDotNetContainer
{
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComVisible(true)]
  [Guid("246E5517-663C-4ea8-A14A-2888EA34AA31")]
  public interface IPersistStreamManaged
  {
    [DispId(1)]
    int LoadStream(IStream stream);
  }
}

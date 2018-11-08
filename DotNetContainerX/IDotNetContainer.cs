using System.Runtime.InteropServices;

namespace FwDotNetContainer
{
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [Guid("1C63040F-51FD-40e2-BEA1-C5467E989BBE")]
  public interface IDotNetContainer
  {
    string ErrorText { get; }

    void LoadFrom(string text, bool isFilePath);
  }
}

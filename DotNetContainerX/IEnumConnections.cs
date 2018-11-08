using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FwDotNetContainer
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("B196B287-BAB4-101A-B69C-00AA00341D07")]
  [ComImport]
  public interface IEnumConnections
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next(int celt, [MarshalAs(UnmanagedType.LPArray), Out] System.Runtime.InteropServices.ComTypes.CONNECTDATA[] rgelt, IntPtr pceltFetched);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Skip(int celt);

    void Reset();

    void Clone(out IEnumConnections ppenum);
  }
}

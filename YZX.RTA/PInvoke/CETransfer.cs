using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YZX.WINCC.Controls.PInvoke
{
  public static class CETransfer
  {
    [DllImport("Siemens.Simatic.Hmi.Transfer.CETransfer.dll", 
      CharSet = CharSet.Ansi, 
      SetLastError = true)]
    public static extern uint GetComfortDeviceInfo(
      [MarshalAs(UnmanagedType.LPStr)] string pathToDLL, 
      [MarshalAs(UnmanagedType.LPStr)] string pszAddress, 
      [MarshalAs(UnmanagedType.LPStr)] StringBuilder pszDeviceType, 
      [MarshalAs(UnmanagedType.LPStr)] StringBuilder pszDeviceVersion, 
      [MarshalAs(UnmanagedType.LPStr)] StringBuilder pszRTCV, 
      uint unValueSize);

    [DllImport("Siemens.Simatic.Hmi.Transfer.CETransfer.dll", 
      CharSet = CharSet.Ansi, 
      SetLastError = true)]
    public static extern uint GetComfortFileInfo(
      [MarshalAs(UnmanagedType.LPStr)] string pathToDLL, 
      [MarshalAs(UnmanagedType.LPStr)] string pathToFirmware, 
      [MarshalAs(UnmanagedType.LPStr)] StringBuilder pszCompatibleDeviceTypes, 
      [MarshalAs(UnmanagedType.LPStr)] StringBuilder pszVersion, 
      uint unValueSize);
  }
}

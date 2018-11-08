using System.Collections.Generic;
using System.IO.Ports;

namespace YZX.WINCC.Controls
{
  public class SerialPortsInRuntime
  {
    public static Dictionary<string, SerialPort> SerialPorts =
      new Dictionary<string, SerialPort>();

    public static SerialPort TryGetSerialPort(string name)
    {
      if (SerialPorts.ContainsKey(name))
      {
        return SerialPorts[name];
      }
      return null;
    }
  }
}

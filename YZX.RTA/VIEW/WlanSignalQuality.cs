using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Common;

using static NativeWifi.WlanClient;

namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(WlanSignalQuality), "Toolbox.WlanSignalQuality.bmp")]
  public partial class WlanSignalQuality : CControl
  {
    Timer UpdateTimer;
    public WlanSignalQuality()
    {
      InitializeComponent();

      UpdateTimer = new Timer();
      UpdateTimer.Interval = 1000;
      UpdateTimer.Tick += UpdateTimer_Tick;
      UpdateTimer.Start();
    }


    void UpdateTimer_Tick(object sender, EventArgs e)
    {
      SignalProgress.BackColor = Color.Blue;

      uint signal = getWlanSignalQuqlity();
      SignalProgress.Value = (int)signal;

      SignalLabel.Text = SignalProgress.Value.ToString();
    }

    public uint getWlanSignalQuqlity()
    {
      WlanInterface Iface = WIFI.CurrentInterface;
      if(Iface.InterfaceState == NativeWifi.Wlan.WlanInterfaceState.Connected)
      {
        return Iface.CurrentConnection.wlanAssociationAttributes.wlanSignalQuality;
      }
      else
      {
        return 0;
      }
    }
  }
}

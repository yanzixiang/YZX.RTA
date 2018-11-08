using System;

using AForge.Video.DirectShow;

namespace YZX.WINCC.Controls
{
  public class CameraDevices
  {
    public FilterInfoCollection Devices { get; private set; }
    public VideoCaptureDevice Current { get; private set; }

    public CameraDevices()
    {
      Devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
    }

    public void SelectCamera(int index)
    {
      if (index >= Devices.Count)
      {
        throw new ArgumentOutOfRangeException("index");
      }
      Current = new VideoCaptureDevice(Devices[index].MonikerString);
    }
  }
}
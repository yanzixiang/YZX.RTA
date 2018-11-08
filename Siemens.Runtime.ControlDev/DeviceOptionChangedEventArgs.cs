// Decompiled with JetBrains decompiler
// Type: Siemens.Runtime.DeviceOptionChangedEventArgs
// Assembly: Siemens.Runtime.ControlDev, Version=0.0.0.0, Culture=neutral, PublicKeyToken=64f03793d14957d2
// MVID: F6475F08-60AF-42B6-ADF9-1F6E085475C5
// Assembly location: D:\Program Files (x86)\Siemens\Automation\WinCC RT Advanced\Siemens.Runtime.ControlDev.dll

using System;

namespace Siemens.Runtime
{
  public class DeviceOptionChangedEventArgs : EventArgs
  {
    public readonly string OptionName;

    public DeviceOptionChangedEventArgs(string optionName)
    {
      this.OptionName = optionName;
    }
  }
}

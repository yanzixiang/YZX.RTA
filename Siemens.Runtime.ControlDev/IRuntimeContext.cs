// Decompiled with JetBrains decompiler
// Type: Siemens.Runtime.IRuntimeContext
// Assembly: Siemens.Runtime.ControlDev, Version=0.0.0.0, Culture=neutral, PublicKeyToken=64f03793d14957d2
// MVID: F6475F08-60AF-42B6-ADF9-1F6E085475C5
// Assembly location: D:\Program Files (x86)\Siemens\Automation\WinCC RT Advanced\Siemens.Runtime.ControlDev.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Siemens.Runtime
{
  [Guid("8329B1BB-D9A8-4be2-A0EA-B4F5D40E5416")]
  public interface IRuntimeContext
  {
    string DeviceType { get; }

    IDictionary<string, object> DeviceOptions { get; }

    event EventHandler<DeviceOptionChangedEventArgs> DeviceOptionChanged;
  }
}

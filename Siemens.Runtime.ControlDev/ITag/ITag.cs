// Decompiled with JetBrains decompiler
// Type: Siemens.Runtime.ITag.ITag
// Assembly: Siemens.Runtime.ControlDev, Version=0.0.0.0, Culture=neutral, PublicKeyToken=64f03793d14957d2
// MVID: F6475F08-60AF-42B6-ADF9-1F6E085475C5
// Assembly location: D:\Program Files (x86)\Siemens\Automation\WinCC RT Advanced\Siemens.Runtime.ControlDev.dll

using System.Runtime.InteropServices;

namespace Siemens.Runtime.ITag
{
  [Guid("DAD8385F-F2AC-4B4C-9235-AABAC08C78AA")]
  [TypeLibType((short) 4288)]
  [ComVisible(true)]
  public interface ITag
  {
    [DispId(1)]
    int Register(ITagSink pSink);

    [DispId(2)]
    void Unregister(int RegisterCookie);

    [DispId(3)]
    object ReadTag(int RegisterCookie, object TagNames);

    [DispId(4)]
    void ReadTagAsync(int RegisterCookie, object TagNames, object ClientCookies, ITagDeviceType eCacheDevice);

    [DispId(5)]
    void ReadTagCyclic(int RegisterCookie, object TagNames, object UpdateRates, object ClientCookies, out object ServerCookies);

    [DispId(6)]
    void WriteTag(int RegisterCookie, object TagNames, object Values);

    [DispId(7)]
    void WriteTagAsync(int RegisterCookie, object TagNames, object ClientCookies, object Values);

    [DispId(8)]
    void RemoveCyclicTag(int RegisterCookie, object ServerCookies);

    [DispId(9)]
    void Cancel(int RegisterCookie);
  }
}

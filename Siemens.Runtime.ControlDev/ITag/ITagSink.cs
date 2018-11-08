// Decompiled with JetBrains decompiler
// Type: Siemens.Runtime.ITag.ITagSink
// Assembly: Siemens.Runtime.ControlDev, Version=0.0.0.0, Culture=neutral, PublicKeyToken=64f03793d14957d2
// MVID: F6475F08-60AF-42B6-ADF9-1F6E085475C5
// Assembly location: D:\Program Files (x86)\Siemens\Automation\WinCC RT Advanced\Siemens.Runtime.ControlDev.dll

using System.Runtime.InteropServices;

namespace Siemens.Runtime.ITag
{
  [Guid("C5B029A2-BCAC-4F66-B240-3801ED08ED39")]
  [TypeLibType((short) 4288)]
  [ComVisible(true)]
  public interface ITagSink
  {
    [DispId(1)]
    void OnDataChanged(int RegisterCookie, object TagNames, object Values, object Qualities, object VarStates, object Timestamps, object ClientCookies);

    [DispId(2)]
    void OnWriteComplete(int RegisterCookie, object TagNames, object ClientCookies);

    [DispId(3)]
    void OnRemoved(int RegisterCookie, object ClientCookies);

    [DispId(4)]
    void OnCanceled(int RegisterCookie);

    [DispId(5)]
    void OnError(int RegisterCookie, object TagNames, object ClientCookies, object Errors);
  }
}

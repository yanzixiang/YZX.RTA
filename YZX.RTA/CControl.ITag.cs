using System;
using System.Windows;

using Siemens.Runtime.ITag;

using Extensions;

namespace YZX.WINCC.Controls
{
  partial class CControl:ITagSink,ITagOp
  {
    #region ITagOp
    public ITag TAG { get; private set; }

    public int RegisterCookie { get; private set; }

    public bool CConnected { get; private set; }

    public void InvokeAction(Action action)
    {
      this.SafeInvoke(action);
    }

    public object InvokeFunc(Func<object> func)
    {
      return this.SafeInvoke(func);
    }
    #endregion

    #region ITagSink
    public void OnCanceled(int RegisterCookie)
    {
      MessageBox.Show("OnCanceled");
    }

    public void OnDataChanged(
      int RegisterCookie,
      object TagNames,
      object Values,
      object Qualities,
      object VarStates,
      object Timestamps,
      object ClientCookies)
    {
      MessageBox.Show("OnDataChanged");
    }

    public void OnError(
      int RegisterCookie,
      object TagNames,
      object ClientCookies,
      object Errors)
    {
      MessageBox.Show("OnError");
    }

    public void OnRemoved(
      int RegisterCookie,
      object ClientCookies)
    {
      MessageBox.Show("OnRemoved");
    }

    public void OnWriteComplete(
      int RegisterCookie,
      object TagNames,
      object ClientCookies)
    {
      MessageBox.Show("OnWriteComplete");
    }

    #endregion
  }
}

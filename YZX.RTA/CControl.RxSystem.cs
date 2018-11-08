using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reflection;


using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Windows.Forms;

#if RTA
#endif

using Reflection;

namespace YZX.WINCC.Controls
{

#if RTA
  partial class CControl : IObserver<ReadTagResult>, IDisposable
  {
    /// <summary>
    /// 需要使用 RX 系统从 Runtime 更新的变量 
    /// Runtime -> .Net
    /// </summary>
    public virtual List<string> RXTags
    {
      get
      {
        return new List<string>();
      }
    }

    /// <summary>
    /// 需要使用 RX 系统向 Runtime 写入的变量 
    /// .Net ->  Runtime
    /// </summary>
    public virtual List<string> RXUpdateTags{
      get{
        return new List<string>();
      }
    }

    /// <summary>
    /// 取得需要跟随某个变量变化的所有属性的名称
    /// </summary>
    /// <param name="tagName">Wincc 中定义的变量名称</param>
    /// <returns></returns>
    public List<string> GetTagRxs(string tagName) {
      List<string> rxs = new List<string>();
      foreach (string rxtag in RXTags)
      {
        string p = TryGetTagName(rxtag);
        if (p == tagName)
        {
          rxs.Add(rxtag);
        }
      }
      return rxs;
    }



    public void SubRXS() {
      foreach (string tag in RXTags)
      {
        string tagInCC = TryGetTagName(tag);
        if (!tagSub.ContainsKey(tagInCC))
        {
          tagSub[tag] = RXSystem.Sub(tagInCC, this);
        }
      }
    }

    /// <summary>
    /// 每个实例自己保存变量订阅的引用
    /// <变量名,变量订阅>
    /// </summary>
    public Dictionary<string, IDisposable> tagSub = 
      new Dictionary<string, IDisposable>();

    public ReadTagResult GetReadTagResult(CCTag tag)
    {
      ReadTagResult rtr = new ReadTagResult(tag.Name);
      object value = this.ReadTag(tag.Name);
      rtr.Value = value;
      rtr.UpdateTime = DateTime.Now;
      return rtr;
    }


    #region RX
    public bool UpdateFromRX = false;
    public virtual void OnNext(ReadTagResult value)
    {
      if(Disposing | IsDisposed)
      {
        return;
      }

      List<string> rxs = GetTagRxs(value.TagName);
      foreach (string rx in rxs)
      {
        Type t = this.GetType();
        PropertyInfo pi = t.GetProperty(rx);
        Type pit = pi.PropertyType;
        object o = value.GetValueAs(pit);
        if(o != null)
        {
          UpdateFromRX = true;
          Reflector.SetInstancePropertyByName(this, rx, o);
          UpdateFromRX = false;
        }
      }
    }

    public virtual void OnError(Exception error)
    {
      throw new NotImplementedException();
    }

    public virtual void OnCompleted()
    {
      throw new NotImplementedException();
    }
    #endregion

    #region IDisposable

    public new void Dispose()
    {
      if(tagSub == null)
      {
        return;
      }
      foreach (IDisposable tagsub in tagSub.Values)
      {
        if (tagsub != null)
        {
          tagsub.Dispose();
        }
      }
    }
    ~CControl()
    {
      Dispose();
    }

    #endregion
  }
#endif
}

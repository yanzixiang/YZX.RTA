using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace YZX.WINCC.Controls
{
  public class RXSystem
  {
    public static Dictionary<string, IObservable<long>> HotTimers =
      new Dictionary<string, IObservable<long>>();

    public static Dictionary<string, IObservable<ReadTagResult>> HotTags =
      new Dictionary<string, IObservable<ReadTagResult>>();
    public static Dictionary<string, IDisposable> HotTagsConnect = 
      new Dictionary<string, IDisposable>();
    public static Dictionary<string, TagObserver> TagObservers =
      new Dictionary<string, TagObserver>();

    public static Dictionary<string, IDisposable> TagTimerObservsers =
      new Dictionary<string, IDisposable>();

    /// <summary>
    /// 取得以ms为单位的热监视器
    /// 不管有没有订阅，都会推送通知
    /// </summary>
    /// <param name="ms"></param>
    /// <returns></returns>
    public static IObservable<long> GetOrCreateTimer(int ms)
    {
      string mss = ms.ToString();
      if (!HotTimers.ContainsKey(mss))
      {
        IObservable<long> timer = Observable.Timer(TimeSpan.FromMilliseconds(0),
          TimeSpan.FromMilliseconds(ms)).ObserveOn(TaskPoolScheduler.Default)
          .Publish()
          .RefCount();
        HotTimers[mss] = timer;
        return timer;
      }
      else
      {
        IObservable<long> timer = HotTimers[mss];
        return timer;
      }
    }

    /// <summary>
    /// 添加变量监视
    /// </summary>
    /// <param name="tagObserver"></param>
    /// <param name="tag"></param>
    public static void AddTagRx(CControl tagObserver,CCTag tag)
    {
      if (!HotTags.ContainsKey(tag.Name))
      {
        Subject<ReadTagResult> tagSubject = new Subject<ReadTagResult>();

        IObservable<long> tagTimer = GetOrCreateTimer(tag.AcquisitionCycle);

        TagObserver tagTimerObser = new TagObserver(ref tagObserver, tag,tagSubject);
        IObservable<ReadTagResult> hotTag = tagTimerObser.Publish().RefCount();
        HotTags[tag.Name] = hotTag;
        TagObservers[tag.Name] = tagTimerObser;
        TagTimerObservsers[tag.Name] = tagTimer.Subscribe(tagTimerObser);
      }
      else
      {
        TagObserver tagTimerObser = TagObservers[tag.Name];
        tagTimerObser.changeCC(ref tagObserver);
      }
    }

    public static IDisposable Sub(string p, IObserver<ReadTagResult> tagObserver)
    {
      if (TagObservers.ContainsKey(p))
      {
        return TagObservers[p].SubscribeOn(TaskPoolScheduler.Default).Subscribe(tagObserver);
      }
      else
      {
        return null;
      }
    }
    //public static IObservable<ReadTagResult> Timer(CControl cc, CCTag tag)
    //{
    //  return Observable.Generate(0,
    //    i => true,
    //    i => i + 1,
    //    i => cc.GetReadTagResult(tag),
    //    i => TimeSpan.FromMilliseconds(tag.AcquisitionCycle));
    //}
  }
}

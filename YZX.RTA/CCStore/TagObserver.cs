using System;
using System.Linq;
using System.Reactive.Subjects;

namespace YZX.WINCC.Controls
{
  public class TagObserver : IObserver<long>,IObservable<ReadTagResult>
  {
    CControl CC;
    public void changeCC(ref CControl cc)
    {
      CC = cc;
    }
    CCTag Tag;
    Subject<ReadTagResult> Subject;
    ReadTagResult lastRTR;
    public TagObserver(ref CControl cc, CCTag tag, Subject<ReadTagResult> subject)
    {
      CC = cc;
      Tag = tag;
      Subject = subject;
    }
    public void OnCompleted()
    {
      throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
      throw new NotImplementedException();
    }

    public void OnNext(long value)
    {
      if (CC.IsHandleCreated)
      {
        ReadTagResult rtr = CC.GetReadTagResult(Tag);
        if (lastRTR == null)
        {
          Subject.OnNext(rtr);
          lastRTR = rtr;
        }
        else
        {
          if (rtr.Value != lastRTR.Value)
          {
            Subject.OnNext(rtr);
            lastRTR = rtr;
          }
        }
      }
    }

    public IDisposable Subscribe(IObserver<ReadTagResult> observer)
    {
      return Subject.Subscribe(observer);
    }
  }
}

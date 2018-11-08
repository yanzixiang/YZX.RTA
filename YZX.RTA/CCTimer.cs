using Common.Threading;
using System;

using YZXLogicEngine.Task;

namespace YZX.WINCC.Controls
{
  public class CCTimer:YZXTimerTask
  {
    public ITagOp TagOp { get; private set; }
    public string OutTag { get; private set; }
    public string OffOutTag { get; private set; }
    public CCTimer(ITagOp tagOp,string tagName,
      string outTagName,int onLast = 1000,
      string offOutTagName="", int offLast = 1000)
      :base(tagName)
    {
      TagOp = tagOp;
      OnLastMs = onLast;
      OffLastMs = offLast;
      Func<bool?> check = () => {
        if (tagOp != null)
        {
          return tagOp.ReadBit(tagName);
        }
        else
        {
          return null;
        }
      };

      Name = $"{tagName}-{outTagName}:{onLast}-{offOutTagName}:{offLast}";
      CheckFunc = check;
      OnLastMs = onLast;
      OffLastMs = offLast;

      ThreadStart = new ControlledThreadStart(RunCheck);

      ThreadController = new ThreadController(ThreadStart,null,Name);

      StartThread();

      OutTag = outTagName;
      OffOutTag = offOutTagName;
      OutChanged += Task_OutChanged;
      OffOutChanged += Task_OffOutChanged;
    }

    private void Task_OutChanged(object sender, EventArgs e)
    {
      if(OutTag != "")
      {
        TagOp.WriteTag(OutTag, Out);
      }
    }

    private void Task_OffOutChanged(object sender, EventArgs e)
    {
      if(OffOutTag != "")
      {
        TagOp.WriteTag(OffOutTag, OffOut);
      }
    }
  }
}

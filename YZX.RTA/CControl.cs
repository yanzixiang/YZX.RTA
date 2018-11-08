using System;
using System.Windows.Forms;
using System.Drawing;

#if RTA
using Siemens.Runtime;
#endif

using Common;

namespace YZX.WINCC.Controls
{
  
  public partial class CControl:UserControl
  {
#if RTA
    public IRuntimeContext CCRuntime;
#endif

    public CControl()
    {
      Load += CControl_Load;
      SizeChanged += CControl_SizeChanged;

#if RTA
      HandleCreated += CControl_Load;
      HandleDestroyed += CControl_HandleDestroyed;
#endif
    }
    private void CControl_HandleDestroyed(object sender, EventArgs e)
    {
      Dispose();
    }

    public void CControl_Load(object sender, EventArgs e)
    {
#if RTA
      if (inRuntime)
      {
        connect2CC();

      }
#endif
#if TIA
      if (inTIA)
      {
        Connect_DynamicChangedEvent();
      }
#endif
    }

    #region 
    public virtual void UpdateSize(Size size)
    {
      InnerControl.Size = size;
    }

    private void CControl_SizeChanged(object sender, EventArgs e)
    {
      CControl cc = sender as CControl;
      if (cc != null & cc.InnerControl != null)
      {
        UpdateSize(cc.Size);
      }
    }

#endregion
    public virtual Control InnerControl {
      get;
    }


    public void openConsole()
    {
      try {
        YEDAConsole.OpenInThread();
      }catch(Exception ex)
      {
        ExceptionViewer ev = new ExceptionViewer("", ex);
        ev.Show();
      }
    }

#if RTA
    public string TryGetTagName(string key)
    {
      string tag = "";
      if (TagMap.ContainsKey(key))
      {
        tag = TagMap[key].Name;
      }
      return tag;
    }

    public void TryWriteTag(string key, object value)
    {
      string PVTag = TryGetTagName(key);
      if (PVTag != "")
      {
        this.WriteTag(PVTag, value);
      }
    }
#endif
  }
}

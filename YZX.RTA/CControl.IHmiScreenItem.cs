using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

#if TIA
using System.ComponentModel;

using Siemens.Automation.Basics.Browsing;
using Siemens.Automation.ObjectFrame.Events;

using Siemens.Simatic.Hmi.Utah.Common.Base;
using Siemens.Simatic.Hmi.Utah.Tag;
using Siemens.Simatic.Hmi.Utah.Globalization;
using Siemens.Simatic.Hmi.Utah.GraphX;
using Siemens.Simatic.Hmi.Utah.Dynamics;
using Siemens.Simatic.Hmi.Utah.Common.Base.Eventing;
using Siemens.Simatic.Hmi.Utah.Dynamics.DynamicsOverview;
#endif

using Newtonsoft.Json;
using System.Drawing;

namespace YZX.WINCC.Controls
{
  partial class CControl
  {
#if TIA
    /// <summary>
    /// 是否在TIA运行环境
    /// </summary>
    [NotScan]
    public bool inTIA
    {
      get
      {
        if (UtahSite != null)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    public IUtahSite UtahSite
    {
      get
      {
        IUtahSite us = Site as IUtahSite;
        
        return us;
      }
    }

    public HmiDotNetScreenItem HSI
    {
      get
      {
        return UtahSite.HmiObject as HmiDotNetScreenItem;
      }
    }
    #else
    public bool inTIA
    {
      get
      {
        return false;
      }
    }
    #endif

    public Dictionary<string, CCTag> TagMap = new Dictionary<string, CCTag>();
    public Dictionary<string, List<CCFunction>> EventMap = new Dictionary<string, List<CCFunction>>();

#if TIA
    [Category("YZX")]
    [DisplayName("索引名称")]
    [Browsable(true)]
#endif
    public string YZXName { get; set; }

#if TIA
    [Category("YZX")]
    [DisplayName("动画数据")]
    [Browsable(true)]
#endif
    public string YZXAnimations { get; set; }


#if TIA
    [Category("YZX")]
    [DisplayName("变量链接数据")]
    [Browsable(true)]
#endif
    public string YZXProperties { get; set; }

#if TIA
    [Category("YZX")]
    [DisplayName("事件链接数据")]
    [Description("TagName")]
    [Browsable(true)]
#endif
    public string YZXEvents { get; set; }

#if TIA
    public void UpdateYZXProperties() {
      string s = HSI.GenYZXProperties();
      YZXProperties = s;
      HSI.FireOnObjectChanged(AffectedCategories.Property, 
        "YZXProperties", 
        s, 
        new CoreObjectEventArgs(HSI.CoreObject)
      );

      ScreenItemManager manager = (ScreenItemManager)HSI.Manager;
      var parent = HSI.Parent;
      var parentofhmiscreenItem = HSI.ParentOfHmiScreenItem;
    }

    public void Connect_DynamicChangedEvent()
    {
      HSI.DynamicChangedEvent += HSI_DynamicChangedEvent;
    }

    private void HSI_DynamicChangedEvent(object sender, HmiDynamicChangedEventArgs e)
    {
      UpdateYZXProperties();

    }
#endif
  }
}
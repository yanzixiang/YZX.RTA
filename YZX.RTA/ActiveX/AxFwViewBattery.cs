using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using COM;
using FwViewBatteryLib;

namespace YZX.WINCC.Controls
{
  public class AxFwViewBattery : FlexibleHost
  {
    private FwViewBattery ocx;

    [Bindable(BindableSupport.Yes)]
    [DispId(1)]
    [ComAliasName("System.UInt32")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual Color BorderColor
    {
      get
      {
        if (this.ocx == null)
          throw new InvalidActiveXStateException("BorderColor", ActiveXInvokeKind.PropertyGet);
        return GetColorFromOleColor(this.ocx.BorderColor);
      }
      set
      {
        if (this.ocx == null)
          throw new InvalidActiveXStateException("BorderColor", ActiveXInvokeKind.PropertySet);
        this.ocx.BorderColor = GetOleColorFromColor(value);
      }
    }

    [DispId(2)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(BindableSupport.Yes)]
    [ComAliasName("System.UInt32")]
    public virtual Color CtlForeColor
    {
      get
      {
        if (this.ocx == null)
          throw new InvalidActiveXStateException("CtlForeColor", ActiveXInvokeKind.PropertyGet);
        return GetColorFromOleColor(this.ocx.ForeColor);
      }
      set
      {
        if (this.ocx == null)
          throw new InvalidActiveXStateException("CtlForeColor", ActiveXInvokeKind.PropertySet);
        this.ocx.ForeColor = GetOleColorFromColor(value);
      }
    }

    [Bindable(BindableSupport.Yes)]
    [DispId(4)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual int XPosition
    {
      get
      {
        if (this.ocx == null)
          throw new InvalidActiveXStateException("XPosition", ActiveXInvokeKind.PropertyGet);
        return this.ocx.XPosition;
      }
      set
      {
        if (this.ocx == null)
          throw new InvalidActiveXStateException("XPosition", ActiveXInvokeKind.PropertySet);
        this.ocx.XPosition = value;
      }
    }

    [Bindable(BindableSupport.Yes)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(5)]
    public virtual int YPosition
    {
      get
      {
        if (this.ocx == null)
          throw new InvalidActiveXStateException("YPosition", ActiveXInvokeKind.PropertyGet);
        return this.ocx.YPosition;
      }
      set
      {
        if (this.ocx == null)
          throw new InvalidActiveXStateException("YPosition", ActiveXInvokeKind.PropertySet);
        this.ocx.YPosition = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(6)]
    [Bindable(BindableSupport.Yes)]
    public virtual int CxSize
    {
      get
      {
        if (this.ocx == null)
          throw new InvalidActiveXStateException("CxSize", ActiveXInvokeKind.PropertyGet);
        return this.ocx.CxSize;
      }
      set
      {
        if (this.ocx == null)
          throw new InvalidActiveXStateException("CxSize", ActiveXInvokeKind.PropertySet);
        this.ocx.CxSize = value;
      }
    }

    [Bindable(BindableSupport.Yes)]
    [DispId(7)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual int CySize
    {
      get
      {
        if (this.ocx == null)
          throw new InvalidActiveXStateException("CySize", ActiveXInvokeKind.PropertyGet);
        return this.ocx.CySize;
      }
      set
      {
        if (this.ocx == null)
          throw new InvalidActiveXStateException("CySize", ActiveXInvokeKind.PropertySet);
        this.ocx.CySize = value;
      }
    }

    [Bindable(BindableSupport.Yes)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(8)]
    public virtual int QualityValue
    {
      get
      {
        if (this.ocx == null)
          throw new InvalidActiveXStateException("QualityValue", ActiveXInvokeKind.PropertyGet);
        return this.ocx.QualityValue;
      }
      set
      {
        if (this.ocx == null)
          throw new InvalidActiveXStateException("QualityValue", ActiveXInvokeKind.PropertySet);
        this.ocx.QualityValue = value;
      }
    }

    [Bindable(BindableSupport.Yes)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(503)]
    public virtual object fwCfgPtr
    {
      get
      {
        if (this.ocx == null)
          throw new InvalidActiveXStateException("fwCfgPtr", ActiveXInvokeKind.PropertyGet);
        return this.ocx.fwCfgPtr;
      }
      set
      {
        if (this.ocx == null)
          throw new InvalidActiveXStateException("fwCfgPtr", ActiveXInvokeKind.PropertySet);
        this.ocx.fwCfgPtr = value;
      }
    }

    public AxFwViewBattery()
      : base("c9d109d6-fb70-4c22-98c4-6d57d16afdba")
    {
    }
    protected override void AttachInterfaces()
    {
      base.AttachInterfaces();
      try
      {
        this.ocx = (FwViewBattery)this.GetOcx();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }
  }
}

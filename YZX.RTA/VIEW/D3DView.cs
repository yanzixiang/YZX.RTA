using System;
using System.Xaml;
using System.Drawing;
using System.Reflection;

#if TIA
using System.ComponentModel;
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

using AITSW.PCPANEL.WPF;

namespace YZX.WINCC.Controls
{

  [ToolboxBitmap(typeof(D3DView), "Toolbox.D3DView.bmp")]
  public class D3DView : WPFInWinForm
  {
    #region 属性
    Vehicle mc = new Vehicle();

    #if TIA
    [Category("YZX")]
    [DisplayName("样式")]
    [Description("TagName")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
    #endif
    public Vehicle.VehicleDesign Design
    {
      get
      {
        return mc.Design;
      }
      set
      {
        mc.Design = value;
      }
    }

    #if TIA
    [Category("YZX")]
    [DisplayName("箱体")]
    [Description("TagName")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
    #endif
    public Vehicle.VehicleCargo Cargo
    {
      get
      {
        return mc.Cargo;
      }
      set
      {
        mc.Cargo = value;
      }
    }

    #if TIA
    [Category("YZX")]
    [DisplayName("着色")]
    [Description("TagName")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
    #endif
    public Vehicle.VehicleAuxColorMode AuxColorMode
    {
      get
      {
        return mc.AuxColorMode;
      }
      set
      {
        mc.AuxColorMode = value;
      }
    }
    #endregion 属性
    public D3DView()
    {
      Cargo = Vehicle.VehicleCargo.Tank;
      Load += VehicleView_Load;
    }

    private void VehicleView_Load(object sender, EventArgs e)
    {
      PrepareWPF();
    }

    public new void PrepareWPF()
    {
      try {
        
        WPF.Child = mc;
        hideInfo();
      }
      catch (XamlParseException ex)
      {
        showInfo(ex.ToString());
      }
      
    }
  }
}

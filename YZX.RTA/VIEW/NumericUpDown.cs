using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#if TIA
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(NumericUpDown), "Toolbox.NumericUpDown.bmp")]
  public class NumericUpDown : CControl
  {
    #region 属性
    #if TIA
    [Category("YZX")]
    [DisplayName("数字")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
    #endif
    public decimal InnerValue
    {
      get
      {
        return nu.Value;
      }
      set
      {
        nu.Value = value;
      }
    }

    #if TIA
    [Category("YZX")]
    [DisplayName("不可输入")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
    #endif
    public bool ReadOnlyValue
    {
      get
      {
        return nu.ReadOnly;
      }
      set
      {
        nu.ReadOnly = value;
      }
    }

    #if TIA
    [Category("YZX")]
    [DisplayName("十六进制")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
    #endif
    public bool HexadecimalValue
    {
      get
      {
        return nu.Hexadecimal;
      }
      set
      {
        nu.Hexadecimal = value;
      }
    }

    #if TIA
    [Category("YZX")]
    [DisplayName("显示千分隔符")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
    #endif
    public bool ThousandsSeparatorValue
    {
      get
      {
        return nu.ThousandsSeparator;
      }
      set
      {
        nu.ThousandsSeparator = value;
      }
    }

    #if TIA
    [Category("YZX")]
    [DisplayName("小数位数")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
    #endif
    public int DecimalPlacesValue
    {
      get
      {
        return nu.DecimalPlaces;
      }
      set
      {
        nu.DecimalPlaces = value;
      }
    }

    #if TIA
    [Category("YZX")]
    [DisplayName("最小值")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
    #endif
    public decimal MinimumValue
    {
      get
      {
        return nu.Minimum;
      }
      set
      {
        nu.Minimum = value;
      }
    }

    #if TIA
    [Category("YZX")]
    [DisplayName("最大值")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
    #endif
    public decimal MaximumValue
    {
      get
      {
        return nu.Maximum;
      }
      set
      {
        nu.Maximum = value;
      }
    }

    #if TIA
    [Category("YZX")]
    [DisplayName("步长")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
    #endif
    public decimal IncrementValue
    {
      get
      {
        return nu.Increment;
      }
      set
      {
        nu.Increment = value;
      }
    }
    #endregion 属性

    public NumericUpDown()
    {
      nu = new System.Windows.Forms.NumericUpDown();
    }

    public System.Windows.Forms.NumericUpDown nu;
    public override Control InnerControl
    {
      get
      {
        return nu;
      }
    }
  }
}

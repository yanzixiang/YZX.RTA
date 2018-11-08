using Siemens.Simatic.Hmi.Utah.Common.Base;

namespace YZX.WINCC.Controls
{
  public class SupportedDynamicTypesAttributes
  {
    public static SupportedDynamicTypesAttribute None = 
      new SupportedDynamicTypesAttribute(SupportedDynamicTypes.None);

    public static SupportedDynamicTypesAttribute Tag =
      new SupportedDynamicTypesAttribute(SupportedDynamicTypes.Tag);

    public static SupportedDynamicTypesAttribute Script =
      new SupportedDynamicTypesAttribute(SupportedDynamicTypes.Script);

    public static SupportedDynamicTypesAttribute ReportScript =
      new SupportedDynamicTypesAttribute(SupportedDynamicTypes.ReportScript);

    public static SupportedDynamicTypesAttribute Animation =
      new SupportedDynamicTypesAttribute(SupportedDynamicTypes.Animation);

    public static SupportedDynamicTypesAttribute All =
      new SupportedDynamicTypesAttribute(SupportedDynamicTypes.All);

    public static SupportedDynamicTypesAttribute ReportTag =
      new SupportedDynamicTypesAttribute(SupportedDynamicTypes.ReportTag);

    public static SupportedDynamicTypesAttribute AllWithReport =
      new SupportedDynamicTypesAttribute(SupportedDynamicTypes.AllWithReport);
  }
}

using Common;
using Siemens.Runtime.ITag;
using System;
using System.Collections.Generic;

namespace YZX.WINCC.Controls
{
  public interface ITagOp
  {
    int RegisterCookie
    {
      get;
    }

    ITag TAG
    {
      get;
    }

    bool CConnected
    {
      get;
    }

    void InvokeAction(Action action);
    object InvokeFunc(Func<object> func);
  }

  public static class ITagOpExtensions
  {
    public static object ReadTag(this ITagOp tagOp, string tag)
    {
      if (!tagOp.CConnected)
      {
        return null;
      }
      return tagOp.InvokeFunc(() =>
      {
        return tagOp.ReadTagReal(tag);
      });
    }

    private static object ReadTagReal(this ITagOp tagOp,string tag)
    {
      string[] tagNames = new string[1] { tag };
      ITag itag = tagOp.TAG;
      object result = itag.ReadTag(tagOp.RegisterCookie, tagNames);
      if (result is Array)
      {
        object[] ra = (object[])result;
        return ra[0];
      }
      return result;
    }

    public static bool? ReadBit(this ITagOp tagOp,string tag)
    {
      try
      {
        var bit = tagOp.ReadTag(tag);
        if (bit == null)
        {
          return null;
        }
        else
        {
          return (bool)bit;
        }
      }catch(Exception ex)
      {

      }
      return null;
    }

    #region 写变量
    public static void WriteTag(this ITagOp tagOp,string tag, object value)
    {
      if (!tagOp.CConnected)
      {
        return;
      }
      tagOp.InvokeAction(() =>
      {
        tagOp.WriteTagReal(tag, value);
      });
    }

    private static void WriteTagReal(this ITagOp tagOp,string tag,object value)
    {
      try
      {
        string[] tagNames = new string[1] { tag };
        object[] values = new object[1] { value };
        ITag itag = tagOp.TAG;
        itag.WriteTag(tagOp.RegisterCookie, tagNames, values);
      }
      catch (Exception ex)
      {
        ExceptionViewer ev = new ExceptionViewer("WriteTagReal", ex);
        ev.ShowDialog();
      }
    }

    /// <summary>
    /// 复位位
    /// </summary>
    /// <param name="ps"></param>
    public static void ResetBit(this ITagOp tagOp,List<CCParameter> ps)
    {
      try
      {
        string tag = ps[0].DisplayValue;
        tagOp.WriteTag(tag, false);
      }
      catch (Exception ex)
      {
        Console.WriteLine("ResetBit" + ex.ToString());
      }
    }

    /// <summary>
    /// 置位位
    /// </summary>
    /// <param name="ps"></param>
    public static void SetBit(this ITagOp tagOp, List<CCParameter> ps)
    {
      try
      {
        string tag = ps[0].DisplayValue;
        tagOp.WriteTag(tag, true);
      }
      catch (Exception ex)
      {
        Console.WriteLine("SetBit" + ex.ToString());
      }
    }

    /// <summary>
    /// 反转位
    /// </summary>
    /// <param name="ps"></param>
    public static void InvertBit(this ITagOp tagOp,List<CCParameter> ps)
    {
      try
      {
        string tag = ps[0].DisplayValue;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }

    public static void InvertBit(this ITagOp tagOp, string tag)
    {
      try
      {
        bool current = (bool)tagOp.ReadTag(tag);
        bool toggle = !current;
        tagOp.WriteTag(tag, toggle);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }

    /// <summary>
    /// 设置变量
    /// </summary>
    /// <param name="ps"></param>
    public static void SetTag(this ITagOp tagOp,List<CCParameter> ps)
    {
      try
      {
        string tag = ps[0].DisplayValue;
        string value = ps[1].DisplayValue;
        tagOp.WriteTag(tag, value);
      }
      catch (Exception ex)
      {
        Console.WriteLine("SetTag", ex.ToString());
      }
    }
    #endregion 写变量
  }
}
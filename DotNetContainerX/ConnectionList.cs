using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FwDotNetContainer
{
  public class ConnectionList : IEnumConnections
  {
    private IList<KeyValuePair<int, object>> connections = new List<KeyValuePair<int, object>>();
    private int currentCookie;
    private int currentEnumIndex;

    public IEnumerable<object> Connections
    {
      get
      {
        foreach (KeyValuePair<int, object> keyValuePair in this.connections)
          yield return keyValuePair.Value;
      }
    }

    public IList<KeyValuePair<int, object>> InternalConnectionList
    {
      get
      {
        return this.connections;
      }
    }

    public int Add(object obj)
    {
      ++this.currentCookie;
      this.connections.Add(new KeyValuePair<int, object>(this.currentCookie, obj));
      return this.currentCookie;
    }

    public void Remove(int cookie)
    {
      for (int index = 0; index < this.connections.Count; ++index)
      {
        if (this.connections[index].Key == cookie)
        {
          this.connections.RemoveAt(index);
          break;
        }
      }
    }

    public int Next(int celt, System.Runtime.InteropServices.ComTypes.CONNECTDATA[] rgelt, IntPtr pceltFetched)
    {
      int num = 0;
      for (int i = this.currentEnumIndex; i < this.connections.Count; i++)
      {
        rgelt[num] = new System.Runtime.InteropServices.ComTypes.CONNECTDATA
        {
          dwCookie = this.connections[i].Key,
          pUnk = this.connections[i].Value
        };
        num++;
        if (num == celt)
        {
          break;
        }
      }
      this.currentEnumIndex += num;
      if (pceltFetched != IntPtr.Zero)
      {
        Marshal.WriteInt32(pceltFetched, num);
      }
      return (num == celt) ? 0 : 1;
    }

    public int Skip(int celt)
    {
      this.currentEnumIndex += celt;
      return this.currentEnumIndex < this.connections.Count ? 0 : 1;
    }

    public void Reset()
    {
      this.currentEnumIndex = 0;
    }

    public void Clone(out IEnumConnections ppenum)
    {
      ppenum = new ConnectionList()
      {
        connections = this.connections,
        currentCookie = this.currentCookie,
        currentEnumIndex = this.currentEnumIndex
      };
    }
  }
}

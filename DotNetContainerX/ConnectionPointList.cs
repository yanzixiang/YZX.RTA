using System;
using System.Collections.Generic;

namespace FwDotNetContainer
{
  internal class ConnectionPointList : IEnumConnectionPoints
  {
    private IList<IConnectionPoint> connectionPoints = new List<IConnectionPoint>();
    private int currentEnumIndex;

    public void Add(IConnectionPoint connectionPoint)
    {
      if (connectionPoint == null)
        throw new ArgumentNullException("connectionPoint");
      this.connectionPoints.Add(connectionPoint);
    }

    public int Next(int celt, IConnectionPoint[] rgelt, IntPtr pceltFetched)
    {
      int index1 = 0;
      for (int index2 = this.currentEnumIndex; index2 < this.connectionPoints.Count; ++index2)
      {
        rgelt[index1] = this.connectionPoints[index2];
        ++index1;
        if (index1 == celt)
          break;
      }
      this.currentEnumIndex = this.currentEnumIndex + index1;
      pceltFetched = new IntPtr(index1);
      return index1 == celt ? 0 : 1;
    }

    public int Skip(int celt)
    {
      this.currentEnumIndex += celt;
      return this.currentEnumIndex < this.connectionPoints.Count ? 0 : 1;
    }

    public void Reset()
    {
      this.currentEnumIndex = 0;
    }

    public void Clone(out IEnumConnectionPoints ppenum)
    {
      ppenum = new ConnectionPointList()
      {
        connectionPoints = this.connectionPoints,
        currentEnumIndex = this.currentEnumIndex
      };
    }
  }
}

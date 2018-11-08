using System;
using System.Collections.Generic;

namespace FwDotNetContainer
{
  public class EventContainerHelper : IConnectionPointContainer
  {
    private readonly IList<EventHelper> eventHelpers = new List<EventHelper>();
    private readonly IDictionary<Guid, IConnectionPoint> guidToConnectionPoint = new Dictionary<Guid, IConnectionPoint>();
    private readonly ConnectionPointList connectionPoints = new ConnectionPointList();
    private readonly IConnectionPointContainer connectionPointContainer;

    public EventContainerHelper(IConnectionPointContainer connectionPointContainer)
    {
      if (connectionPointContainer == null)
        throw new ArgumentNullException("connectionPointContainer");
      this.connectionPointContainer = connectionPointContainer;
    }

    public EventHelper<IDotNetContainerEvents> AddEvents<IDotNetContainerEvents>()
    {
      EventHelper<IDotNetContainerEvents> eventHelper = new EventHelper<IDotNetContainerEvents>(this.connectionPointContainer);
      this.eventHelpers.Add(eventHelper);
      this.guidToConnectionPoint.Add(eventHelper.ComEventInterfaceType.GUID, eventHelper);
      this.connectionPoints.Add(eventHelper);
      return eventHelper;
    }

    public void EnumConnectionPoints(out IEnumConnectionPoints ppEnum)
    {
      ppEnum = this.connectionPoints;
    }

    public void FindConnectionPoint(ref Guid riid, out IConnectionPoint ppCP)
    {
      IEnumerator<Guid> enumerator = this.guidToConnectionPoint.Keys.GetEnumerator();
      enumerator.MoveNext();
      Guid current = enumerator.Current;
      ppCP = this.guidToConnectionPoint[current];
    }
  }
}

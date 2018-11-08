using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace FwDotNetContainer
{
  public class EventHelper<IDotNetContainerEvents> : EventHelper, IConnectionPoint
  {
    public IDictionary<Type, MethodInfo> delegatesToMethods = new Dictionary<Type, MethodInfo>();
    public ConnectionList observers = new ConnectionList();
    public IConnectionPointContainer connectionPointContainer;
    public Type comEventInterfaceType;

    public ConnectionList EventObservers
    {
      get
      {
        return this.observers;
      }
    }

    public Type ComEventInterfaceType
    {
      get
      {
        return this.comEventInterfaceType;
      }
    }

    public EventHelper(IConnectionPointContainer connectionPointContainer)
    {
      if (connectionPointContainer == null)
        throw new ArgumentNullException("connectionPointContainer");
      Type type = typeof (IDotNetContainerEvents);
      this.comEventInterfaceType = typeof (IDotNetContainerEvents);
      if (this.comEventInterfaceType == null)
        throw new ArgumentException("The type parameter is not a .NET event interface corresponding to a COM event interface.");
      foreach (MethodInfo methodInfo in this.comEventInterfaceType.GetMethods())
      {
        EventInfo @event = type.GetEvent(methodInfo.Name);
        if (!(@event == null) && !(@event.EventHandlerType == null))
          this.delegatesToMethods.Add(@event.EventHandlerType, methodInfo);
      }
      this.connectionPointContainer = connectionPointContainer;
    }

    public void Raise<EventDelegate>(params object[] args)
    {
      if (!this.delegatesToMethods.ContainsKey(typeof (EventDelegate)))
        return;
      MethodInfo methodInfo = this.delegatesToMethods[typeof (EventDelegate)];
      foreach (object obj in this.observers.Connections)
        methodInfo.Invoke(obj, args);
    }

    void IConnectionPoint.GetConnectionInterface(out Guid pIID)
    {
      pIID = this.comEventInterfaceType.GUID;
    }

    void IConnectionPoint.GetConnectionPointContainer(out IConnectionPointContainer ppCPC)
    {
      ppCPC = this.connectionPointContainer;
    }

    void IConnectionPoint.Advise(object pUnkSink, out int pdwCookie)
    {
      pdwCookie = this.observers.Add(pUnkSink);
    }

    void IConnectionPoint.Unadvise(int dwCookie)
    {
      for (int index = 0; index < this.observers.InternalConnectionList.Count; ++index)
      {
        if (this.observers.InternalConnectionList[index].Key == dwCookie)
        {
          Marshal.ReleaseComObject(this.observers.InternalConnectionList[index].Value);
          break;
        }
      }
      this.observers.Remove(dwCookie);
    }

    void IConnectionPoint.EnumConnections(out IEnumConnections ppEnum)
    {
      ppEnum = this.observers;
    }
  }
}

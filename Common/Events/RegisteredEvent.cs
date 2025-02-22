using System;
using System.Reflection;

namespace Leclair.Stardew.Common.Events {
	public struct RegisteredEvent : IDisposable {

		public object EventHost;
		public EventInfo Event;
		public Delegate Delegate;

		public RegisteredEvent(object eventHost, EventInfo @event, Delegate @delegate) {
			EventHost = eventHost;
			Event = @event;
			Delegate = @delegate;
		}

		public void Dispose() {
			Event.RemoveEventHandler(EventHost, Delegate);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Reflection;

using StardewModdingAPI;

namespace Leclair.Stardew.Common.Events {
	public abstract class EventSubscriber<T> : IDisposable where T : Mod {

		public readonly T Mod;

		private Dictionary<MethodInfo, RegisteredEvent> Events;

		public EventSubscriber(T mod, bool registerImmediate = true) {
			Mod = mod;

			if (registerImmediate)
				RegisterEvents();
		}

		public virtual void Dispose() {
			UnregisterEvents();
		}

		public void RegisterEvents(Action<string, LogLevel> logger = null) {
			Events = EventHelper.RegisterEvents(this, Mod.Helper.Events, Events, logger);
		}

		public void UnregisterEvents() {
			if (Events == null)
				return;

			EventHelper.UnregisterEvents(Events);
			Events = null;
		}

	}
}

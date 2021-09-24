// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using ServerlessActors.Core.Events;

namespace ServerlessActors.Core
{
	public class Entity
	{
		private List<DomainEvent> _events;

		public Entity()
		{
			this._events = new List<DomainEvent>();
		}

		public IReadOnlyCollection<DomainEvent> Events => this._events;

		public void AddEvent(DomainEvent evt)
		{
			if (this._events.Any(p => p.Id == evt.Id))
			{
				return;
			}
			
			this._events.Add(evt);
		}
	}
}
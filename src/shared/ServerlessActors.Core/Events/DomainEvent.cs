// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace ServerlessActors.Core.Events
{
	public abstract class DomainEvent
	{
		public DomainEvent()
		{
			this.Id = Guid.NewGuid().ToString();
			this.Time = DateTime.Now;
		}
		
		[JsonProperty]
		public string Id { get; }
		
		[JsonProperty]
		public DateTime Time { get; }
		
		[JsonProperty]
		public abstract string Name { get; }
		
		[JsonProperty]
		public abstract string Source { get; }
	}
}
// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

namespace ServerlessActors.Core
{
	public abstract class StatefulEntity : Entity
	{
		public abstract string EntityType { get; }
	}
}
// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

namespace ServerlessActors.Core.Commands
{
	public abstract class ActorCommand
	{
		public abstract string Actor { get; }
		
		public abstract string Type { get; }
	}
}
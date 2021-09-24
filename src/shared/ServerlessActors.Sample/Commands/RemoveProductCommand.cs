// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using ServerlessActors.Core.Commands;

namespace JEasthamDev.Aws.ServerlessActors.Commands
{
	public class RemoveProductCommand : ActorCommand
	{
		public string CustomerId { get; set; }
		
		public string ProductName { get; set; }

		/// <inheritdoc />
		public override string Actor => "cart";

		/// <inheritdoc />
		public override string Type => "remove.product";
	}
}
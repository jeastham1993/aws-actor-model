// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using ServerlessActors.Core.Commands;

namespace JEasthamDev.Aws.ServerlessActors.Commands
{
	public class CreateOrderCommand : ActorCommand
	{
		public string CustomerId { get; set; }

		/// <inheritdoc />
		public override string Actor => "order";

		/// <inheritdoc />
		public override string Type => "create.order";
	}
}
// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using ServerlessActors.Core.Commands;

namespace JEasthamDev.Aws.ServerlessActors.Commands
{
	public class ClearCartCommand : ActorCommand
	{
		public string CustomerId { get; set; }

		/// <inheritdoc />
		public override string Actor => "cart";

		/// <inheritdoc />
		public override string Type => "clear.cart";
	}
}
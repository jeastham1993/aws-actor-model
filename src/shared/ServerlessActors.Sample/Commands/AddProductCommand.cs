// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using ServerlessActors.Core.Commands;

namespace JEasthamDev.Aws.ServerlessActors.Commands
{
	public class AddProductCommand : ActorCommand
	{
		public string CustomerId { get; set; }
		
		public string ProductName { get; set; }
		
		public int Quantity { get; set; }

		/// <inheritdoc />
		public override string Actor => "cart";

		/// <inheritdoc />
		public override string Type => "add.product";
	}
}
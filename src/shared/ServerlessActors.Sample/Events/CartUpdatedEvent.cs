// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using ServerlessActors.Core.Events;

namespace JEasthamDev.Aws.ServerlessActors.Events
{
	public class CartUpdatedEvent : DomainEvent
	{
		public CartUpdatedEvent() : base()
		{
		}

		/// <inheritdoc />
		public override string Name => "product-added-to-cart";

		/// <inheritdoc />
		public override string Source => "com.cart";

		public string CustomerId { get; set; }

		public string ProductName { get; set; }

		public int Quantity { get; set; }
	}
}
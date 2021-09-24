// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using ServerlessActors.Core.Events;

namespace JEasthamDev.Aws.ServerlessActors.Events
{
	public class CartCreatedEvent : DomainEvent
	{
		public CartCreatedEvent() : base()
		{
		}
		
		/// <inheritdoc />
		public override string Name => "new-customer-cart-created";

		/// <inheritdoc />
		public override string Source => "com.cart";
		
		public string CustomerId { get; set; }
	}
}
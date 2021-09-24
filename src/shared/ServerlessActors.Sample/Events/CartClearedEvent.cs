// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using ServerlessActors.Core.Events;

namespace JEasthamDev.Aws.ServerlessActors.Events
{
	public class CartClearedEvent : DomainEvent
	{
		public CartClearedEvent() : base()
		{
		}
		
		/// <inheritdoc />
		public override string Name => "cart-cleared";

		/// <inheritdoc />
		public override string Source => "com.cart";
		
		public string CustomerId { get; set; }
	}
}
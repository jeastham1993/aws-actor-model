// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using ServerlessActors.Core;

namespace JEasthamDev.Aws.ServerlessActors
{
	public class Order : StatefulEntity
	{
		public static string Type => "Order";

		/// <inheritdoc />
		public override string EntityType => Order.Type;
		
		public string CustomerId { get; set; }
		
		public string OrderNumber { get; set; }
	}
}
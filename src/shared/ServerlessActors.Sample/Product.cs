// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using Newtonsoft.Json;

namespace JEasthamDev.Aws.ServerlessActors
{
	public class Product
	{
		[JsonConstructor]
		private Product()
		{
		}

		public Product(string name, int quantity)
		{
			this.ProductName = name;
			this.Quantity = quantity;
		}
		
		[JsonProperty]
		public string ProductName { get; private set; }
		
		[JsonProperty]
		public int Quantity { get; private set; }
	}
}
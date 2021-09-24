// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using JEasthamDev.Aws.ServerlessActors.Events;
using Newtonsoft.Json;
using ServerlessActors.Core;

namespace JEasthamDev.Aws.ServerlessActors
{
	public class Cart : StatefulEntity
	{
		public static string Type => "Cart";

		[JsonProperty("products")] private List<Product> _products;

		[JsonConstructor]
		private Cart() : base()
		{
		}

		public Cart(string customerId) : base()
		{
			this.CustomerId = customerId;
			
			this.AddEvent(new CartCreatedEvent()
			{
				CustomerId = customerId,
			});
		}

		[JsonProperty]
		public string CustomerId { get; }

		[JsonIgnore]
		public List<Product> Products => this._products;

		/// <inheritdoc />
		public override string EntityType => Cart.Type;

		public Product AddProduct(string productName, int quantity)
		{
			if (this._products == null)
			{
				this._products = new List<Product>();
			}
			
			var existingProduct =
				this._products.FirstOrDefault(
					p => p.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase));

			Product newProduct = null;

			if (existingProduct != null)
			{
				this._products.Remove(existingProduct);

				newProduct = new Product(productName, existingProduct.Quantity + quantity);

				this._products.Add(newProduct);
			}
			else
			{
				newProduct = new Product(productName, quantity);
				this._products.Add(newProduct);
			}
			
			this.AddEvent(new CartUpdatedEvent()
			{
				CustomerId = this.CustomerId,
				ProductName = newProduct.ProductName,
				Quantity = newProduct.Quantity,
			});
			
			return newProduct;
		}

		public void RemoveProduct(string productName)
		{
			var existingProduct =
				this._products.FirstOrDefault(
					p => p.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase));

			if (existingProduct != null)
			{
				this._products.Remove(existingProduct);
			
				this.AddEvent(new CartUpdatedEvent()
				{
					CustomerId = this.CustomerId,
					ProductName = productName,
					Quantity = 0,
				});
			}
		}

		public void Clear()
		{
			this._products = new List<Product>();
			
			this.AddEvent(new CartClearedEvent()
			{
				CustomerId = this.CustomerId
			});
		}
	}
}
// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace JEasthamDev.Aws.ServerlessActors
{
	public interface ICartActor
	{
		Task<Cart> Retrieve(string customerId);
		
		Task<List<Product>> GetProducts(string customerId);

		Task AddProduct(string customerId, Product product);
	}
}
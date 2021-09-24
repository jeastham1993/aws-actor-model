using System;
using System.Linq;
using Xunit;

namespace JEasthamDev.Aws.ServerlessActors.UnitTest
{
    public class CartTest
    {
        private const string DefaultCustomerId = "dev@jameseastham.co.uk";
        
        [Fact]
        public void CanAddMultipleProducts_ShouldIncrement()
        {
            var cart = new Cart(DefaultCustomerId);

            cart.AddProduct("Pizza", 1);
            cart.AddProduct("Pizza", 1);
            
            Assert.Single(cart.Products);
            Assert.Equal(2, cart.Products.FirstOrDefault().Quantity);
        }
    }
}
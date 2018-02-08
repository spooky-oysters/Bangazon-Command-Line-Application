using System;
using Xunit;
using bangazon_cli.Models;


/*
    Author: Kimberly Bird
    References: Ticket #4 - User should be able to add a product for a customer 
*/

namespace bangazon_cli.Models.Tests
{
    public class Product_Should
    {
        // private instance of a new customer
        private Customer _customer;

        [Fact]
        public void ProductHasProperties()
        {
            Product product = new Product();
            product.Id = 1;
            // active customer Id - this is hard coded for the time being
            product.CustomerId = 1;
            product.Name = "Kite";
            product.Price = 45.00;
            product.Description = "blue kite";
            product.Quantity = 3;

            Assert.Equal(product.Id, 1);
            Assert.Equal(product.CustomerId, 1);
            Assert.Equal(product.Name, "Kite");
            Assert.Equal(product.Price, 45.00);
            Assert.Equal(product.Description, "blue kite");
            Assert.Equal(product.Quantity, 3);
        }

    }
}
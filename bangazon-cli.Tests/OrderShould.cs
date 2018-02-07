using System;
using Xunit;
/*
    Author: Greg Lawrence
    Purpose: test Order class 
    
 */
namespace bangazon_cli.Models.Tests
{
    public class Order_Should
    {
        [Fact]
        public void HaveProperties()
        {
            // create a dummy customer
            Customer _customer = new Customer(){
                Id=1
            };

            // create a new instance of an order
            Order order = new Order(_customer.Id);

            order.Id = 1;
            order.PaymentTypeId = null;
            order.CompletedDate = null;
           

            Assert.Equal(order.Id,1);
            Assert.Equal(order.CustomerId,1);
            Assert.Equal(order.PaymentTypeId, null);
            Assert.Equal(order.CompletedDate, null);
        }
    }
}
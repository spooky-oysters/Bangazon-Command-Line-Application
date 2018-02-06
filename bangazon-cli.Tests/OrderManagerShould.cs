using System;
using Xunit;
using bangazon_cli.Models;

/*
    Author: Greg Lawrence
    Purpose: To test methods of the Order Manager such as creating order, adding product to order
*/

namespace bangazon_cli.Managers.Tests
{
    public class OrderManagerShould
    {
        private readonly OrderManager _manager;

        public OrderManagerShould()
        {
            _manager = new OrderManager();
        }

        [Fact]
        public void CreateNewOrder()
        {
           Order newOrder = new Order(){
               Id = 1
           };
            
            _manager.AddOrder(newOrder);
        }

        [Fact]
        public void ListOrders()
        {
            // create a new order
            Order newOrder = new Order(){
               Id = 1, 
               CustomerId = 1,
               PaymentTypeId = null,
               CompletedDate = null
            };

            // add the newly created order to the order list
            _manager.AddOrder(newOrder);

            Assert.Contains(newOrder, _manager.GetUnpaidOrder(1));
        }

    }
}
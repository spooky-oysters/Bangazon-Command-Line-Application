/*
    Author: Greg Lawrence
    Purpose: To test methods of the Order Manager such as creating order, adding product to order
*/
using System;
using Xunit;

namespace bangazon_cli.Tests
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
            Product kite = new Product();
            _manager.CreateOrder(kite);
        }

        [Fact]
        public void CreateNewOrder()
        {
            Product kite = new Product();
            _manager.CreateOrder(kite);
        }

    }
}
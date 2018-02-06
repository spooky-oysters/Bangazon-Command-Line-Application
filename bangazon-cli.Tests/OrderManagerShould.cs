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
            
            _manager.CreateOrder(newOrder);
        }

        [Fact]
        public void ListOrders()
        {
            _manager.GetUnpaidOrder();
        }

    }
}
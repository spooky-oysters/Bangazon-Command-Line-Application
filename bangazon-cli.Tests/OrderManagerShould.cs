using System;
using Xunit;
using bangazon_cli.Models;
using System.Collections.Generic;

/*
    Author: Greg Lawrence
    Purpose: To test methods of the Order Manager such as creating order, adding product to order
*/

namespace bangazon_cli.Managers.Tests
{


    public class OrderManagerShould
    {
        // private instances of things needed in unit tests
        private Customer _customer;
        private readonly OrderManager _orderManager;
        private Order _testOrder;
        private Product _testProduct;

        // constructor for unit test
        public OrderManagerShould()
        {
            // create a new customer instance
            _customer = new Customer();
            // properties added to new customer
            _customer.Id = 1;
            _customer.Name = "G Lawrence";
            _customer.StreetAddress = "123 Somewhere";
            _customer.City = "Nashville";
            _customer.State = "TN";
            _customer.PostalCode = "37206";
            _customer.PhoneNumber = "8018959001";
            
            // create a new orderManager instance
            _orderManager = new OrderManager();
            // create a new order instance
            _testOrder = new Order(1);
            // create a new product instance
            _testProduct = new Product()
            {
                Id = 1,
                CustomerId = 1,
                Name = "Bicycle"
            };
        }

        [Fact]
        public void AddOrder()
        {
            _orderManager.AddOrder(_testOrder);

            Assert.Contains(_testOrder, _orderManager.GetUnpaidOrder(1));
        }

        [Fact]
        public void ListOrders()
        {
            // add the newly created order to the order list
            _orderManager.AddOrder(_testOrder);

            Assert.Contains(_testOrder, _orderManager.GetUnpaidOrder(1));
        }

        public void AddProductToOrder()
        {
            // add product to order
            _orderManager.AddProductToOrder(1, 1);

            // assert that the joiner table holds the relationship of that order and product
            Assert.Equal(_orderManager.GetProduct(1, 1), true);
        }

    }
}
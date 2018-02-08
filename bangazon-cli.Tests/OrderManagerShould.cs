using System;
using Xunit;
using bangazon_cli.Models;
using System.Collections.Generic;
using System.Linq;

/*
    Author: Greg Lawrence
    Purpose: To test methods of the Order Manager such as creating order, adding product to order
*/

namespace bangazon_cli.Managers.Tests
{
    

    public class OrderManagerShould
    {
        // private instances of things needed in unit tests
        private DatabaseInterface _db;
        private Customer _customer;
        private readonly OrderManager _orderManager;
        private Order _testOrder;
        private Product _testProduct;

        // constructor for unit test
        public OrderManagerShould()
        {
            // create database path
            string testPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB_TEST");
            _db = new DatabaseInterface(testPath);

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
            _orderManager = new OrderManager(_db);
            // create a new order instance
            _testOrder = new Order(1);
            _testOrder.CustomerId = 1;
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
            // assign the id that the db assigned to the order back to the test order for testing
            _testOrder.Id = _orderManager.GetUnpaidOrder(1).Id;
            // check if the fields all match between the order sent to the db and the order retrieved from the db
            Assert.Equal(_orderManager.GetUnpaidOrder(1).Id, _testOrder.Id);
            Assert.Equal(_orderManager.GetUnpaidOrder(1).CustomerId, _testOrder.CustomerId);
            Assert.Equal(_orderManager.GetUnpaidOrder(1).CompletedDate, _testOrder.CompletedDate);
            Assert.Equal(_orderManager.GetUnpaidOrder(1).PaymentTypeId, _testOrder.PaymentTypeId);
        }

        [Fact]
        public void AddProductToOrder()
        {
            // add product to order
            _orderManager.AddProductToOrder(1, 1);

            Order currentOrder = _orderManager.GetProductFromOrder(1);
            
            // foreach (var product in currentOrder.Products)
            // {
            //     Console.WriteLine(product.Name + product.Id + product.Description);
            // }

            Product storedProduct = currentOrder.Products.Where(p => p.Id == 1).Single();

            // assert that the joiner table holds the relationship of that order and product
            Assert.Equal(storedProduct.Id, 1);
        }

    }
}
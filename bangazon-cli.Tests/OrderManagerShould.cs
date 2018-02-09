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
        private readonly ProductManager _productManager;
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
            // create a new productManager instance
            _productManager = new ProductManager(_db);
            // create a new order instance
            _testOrder = new Order(1);
            _testOrder.CustomerId = 1;
            // create a new product instance
            _testProduct = new Product()
            {
                CustomerId = 1,
                Name = "Bicycle",
                Price = 55.25,
                Description = "Awesome bike",
                Quantity = 1
            };
        }

        [Fact]
        public void AddOrder()
        {
            // add _testOrder to db
            int orderId = _orderManager.AddOrder(_testOrder);
            // check if the fields all match between the order sent to the db and the order retrieved from the db
            Assert.Equal(_orderManager.GetUnpaidOrder(1).Id, orderId);
            Assert.Equal(_orderManager.GetUnpaidOrder(1).CustomerId, _testOrder.CustomerId);
            Assert.Equal(_orderManager.GetUnpaidOrder(1).CompletedDate, _testOrder.CompletedDate);
            Assert.Equal(_orderManager.GetUnpaidOrder(1).PaymentTypeId, _testOrder.PaymentTypeId);
        }

        [Fact]
        public void AddProductToOrder()
        {
            // add a test order
            int orderId = _orderManager.AddOrder(_testOrder);
            // add a test product
            int productId = _productManager.AddProduct(_testProduct);

            // add product to order by creating a record in the OrderProduct join table
            _orderManager.AddProductToOrder(orderId, productId);

            // get the Active Users order by passing in active userId
            // Order currentOrder = _orderManager.GetProductFromOrder(orderId);
            
            // Retrieve the order that was just added to db
            Product returnedProduct = _orderManager.GetSingleProductFromOrder(orderId, productId);
            // assert that the product stored on the order is the same product that we sent in. 
            Assert.Equal(returnedProduct.Id, productId);
            Assert.Equal(returnedProduct.Price, 55.25);
            Assert.Equal(returnedProduct.Quantity, 1);
            Assert.Equal(returnedProduct.Name, "Bicycle");
            Assert.Equal(returnedProduct.Description, "Awesome bike");
        }

        public void Dispose()
        {
            _db.Update("DELETE FROM OrderProduct");
            _db.Update("DELETE FROM Product");
            _db.Update("DELETE FROM `Order`");
        }
    }
}
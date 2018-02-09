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
        private readonly CustomerManager _customerManager;
        private Order _testOrder;
        private Product _testProduct;

        // constructor for unit test
        public OrderManagerShould()
        {
            // create database path
            string testPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB_TEST");
            _db = new DatabaseInterface(testPath);

            // initialize managers to create db tables and use later in tests
            _orderManager = new OrderManager(_db);
            _productManager = new ProductManager(_db);
            _customerManager = new CustomerManager(_db);
            
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
            
            // create a new order instance
            _testOrder = new Order();
            // create a new product instance
            _testProduct = new Product()
            {
                CustomerId = _customer.Id,
                Name = "Bicycle",
                Price = 55.25,
                Description = "Awesome bike",
                Quantity = 1
            };
        }

        [Fact]
        public void AddOrder()
        {
            // add customer to DB and get id
            int CustomerId = _customerManager.AddCustomer(_customer);
            // add the customerId to _testOrder
            _testOrder.CustomerId = CustomerId;
            
            // add _testOrder to db
            int orderId = _orderManager.AddOrder(_testOrder);
            // check if the fields all match between the order sent to the db and the order retrieved from the db
            Assert.Equal(_orderManager.GetUnpaidOrder(CustomerId).Id, orderId);
            Assert.Equal(_orderManager.GetUnpaidOrder(CustomerId).CustomerId, CustomerId);
            Assert.Equal(_orderManager.GetUnpaidOrder(CustomerId).CompletedDate, _testOrder.CompletedDate);
            Assert.Equal(_orderManager.GetUnpaidOrder(CustomerId).PaymentTypeId, _testOrder.PaymentTypeId);
        }

        [Fact]
        public void AddProductToOrder()
        {
            // add customer to DB and get id
            int CustomerId = _customerManager.AddCustomer(_customer);
            // add the customerId to _testOrder
            _testOrder.CustomerId = CustomerId;
            // add the customerId to _testProduct
            _testProduct.CustomerId = CustomerId;

            // add a test order
            int orderId = _orderManager.AddOrder(_testOrder);
            // add a test product
            int productId = _productManager.AddProduct(_testProduct);

            // add product to order by creating a record in the OrderProduct join table
            _orderManager.AddProductToOrder(orderId, productId);

            // Retrieve the order that was just added to db
            Product returnedProduct = _orderManager.GetSingleProductFromOrder(orderId, productId);
            // assert that the product stored on the order is the same product that we sent in. 
            Assert.Equal(returnedProduct.Id, productId);
            Assert.Equal(returnedProduct.Price, 55.25);
            Assert.Equal(returnedProduct.Quantity, 1);
            Assert.Equal(returnedProduct.Name, "Bicycle");
            Assert.Equal(returnedProduct.Description, "Awesome bike");
        }

        [Fact]
        public void Dispose()
        {
            _db.DeleteTables();
        }
    }
}
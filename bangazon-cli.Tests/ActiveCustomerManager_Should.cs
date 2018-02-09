using System;
using Xunit;
using bangazon_cli.Managers;
using bangazon_cli.Models;

/*
    Author: Dre Randaci
    References: Ticket #2 - Select active customer
    
 */
namespace bangazon_cli.Actions.Tests
{
    public class ActiveCustomerManager_Should
    {
        /* 
            Variables that will be initialized in the constructor
        */
        private DatabaseInterface _db;
        private readonly CustomerManager _customerManager;
        private readonly ActiveCustomerManager _activeCustomerManager;
        private readonly ProductManager _productManager;
        private readonly OrderManager _orderManager;
        private readonly PaymentTypeManager _paymentTypeManager;
        private Customer _customer;

        public ActiveCustomerManager_Should()
        {
            // path to the environment variable on the users computer
            string testPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB_TEST");

            // Instantiating a new DatabaseInterface and passing in the string path
            _db = new DatabaseInterface(testPath);

            // Initializing class instances to access class methods. Also these all create their respective tables in database, which wil be erased when tests are complete.
            _customerManager = new Managers.CustomerManager(_db);
            _activeCustomerManager = new Managers.ActiveCustomerManager(_customerManager);
            _productManager = new ProductManager(_db);
            _orderManager = new OrderManager(_db);
            _paymentTypeManager = new PaymentTypeManager(_db);

            // Initializing a mock customer for testing purposes
            _customer = new Models.Customer();
            _customer.Id = 1;
            _customer.Name = "Dre Man";
            _customer.StreetAddress = "123 Somewhere";
            _customer.City = "Nashville";
            _customer.State = "TN";
            _customer.PostalCode = "323232";
            _customer.PhoneNumber = "9876543";
        }

        [Fact]
        public void SetActiveCustomer_Should()
        {
            // Adds a customer to the database and returns the ID of that new customer
            int id = _customerManager.AddCustomer(_customer);
            
            // Method to extract the customer based off the ID of the newly added customer 
            var ac = _activeCustomerManager.SetActiveCustomer(id);
            
            // Asserts that the id of the new customer and the id of the returned customer from SetActiveCustomer are the same
            Assert.Equal(id, ac.Id);
        }

        [Fact]
        public void Dispose()
        {
            _db.DeleteTables();
        }


    }
}
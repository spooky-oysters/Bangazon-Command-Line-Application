using System;
using Xunit;

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
        private Managers.CustomerManager _customerManager;
        private Managers.ActiveCustomerManager _activeCustomerManager;
        private Models.Customer _customer;
        private DatabaseInterface _db;

        public ActiveCustomerManager_Should()
        {
            // path to the environment variable on the users computer
            string testPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB_TEST");

            // Instantiating a new DatabaseInterface and passing in the string path
            _db = new DatabaseInterface(testPath);

            // Initializing class instances to access class methods
            _customerManager = new Managers.CustomerManager(_db);
            _activeCustomerManager = new Managers.ActiveCustomerManager(_customerManager);

            // Initializing a mock customer for testing purposes
            _customer = new Models.Customer();
            _customer.Id = 1;
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

    }
}
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

            string testPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB_TEST");
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
            // Fires off a method to return a customer at a given index from the list of all customers    

            // Adding the customer to the manager class for testing purposes
            int id = _customerManager.AddCustomer(_customer);
            var ac = _activeCustomerManager.SetActiveCustomer(id);

            Assert.Equal(id, ac.Id);
        }

    }
}
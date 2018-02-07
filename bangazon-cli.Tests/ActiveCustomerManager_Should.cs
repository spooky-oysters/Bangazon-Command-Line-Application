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
        
        public ActiveCustomerManager_Should()
        {
            // Initializing class instances to access class methods
             _customerManager = new Managers.CustomerManager();
             _activeCustomerManager = new Managers.ActiveCustomerManager();
            
            // Initializing a mock customer for testing purposes
            _customer = new Models.Customer();                               
            _customer.Id = 1;

            // Adding the customer to the manager class for testing purposes
            _customerManager.AddCustomer(_customer);
        }        

        [Fact]
        public void CustomerExists()
        {            
            // Testing that the customer exists in the CustomerManager customer list 
            Assert.Contains(_customer, _customerManager.GetCustomers());            
        }

        [Fact]
        public void SetActiveCustomer_Should()
        {
            // Fires off a method to return a customer at a given index from the list of all customers            
            var ac = _activeCustomerManager.SetActiveCustomer(0);

            Assert.Equal(_customer.Id, ac.Id);
            Assert.Equal(_customer.City, ac.City);
            Assert.Equal(_customer.Name, ac.Name);
        }

    }
}
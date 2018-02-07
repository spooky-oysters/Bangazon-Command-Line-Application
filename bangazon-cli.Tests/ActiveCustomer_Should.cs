using System;
using Xunit;

/*
    Author: Dre Randaci
    References: Ticket #2 - Select active customer
    
 */
namespace bangazon_cli.Actions.Tests
{
    public class ActiveCustomer_Should
    {

        private Managers.CustomerManager _customerManager;
        private Managers.ActiveCustomerManager _activeCustomerManager;
        private Models.Customer _customer; 
        

        public ActiveCustomer_Should()
        {
             _customerManager = new Managers.CustomerManager();
             _activeCustomerManager = new Managers.ActiveCustomerManager();
            
            _customer = new Models.Customer();                               
            _customer.Id = 1;

            _customerManager.AddCustomer(_customer);
        }        

        [Fact]
        public void CustomerExists()
        {            
            _customerManager.AddCustomer(_customer);
            Assert.Contains(_customer, _customerManager.GetCustomers());            
        }

        [Fact]
        public void SetActiveCustomer()
        {
            var customers = _customerManager.GetCustomers();
            
            Assert.Equal(_activeCustomerManager.SetActiveCustomer(1), _customer);
        }

    }
}
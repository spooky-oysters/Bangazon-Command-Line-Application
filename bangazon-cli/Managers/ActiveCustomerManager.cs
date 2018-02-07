using System;
using bangazon_cli.Models;

namespace bangazon_cli.Managers
{
    public class ActiveCustomerManager
    {
        private CustomerManager _customerManager;
        private Customer _customer;
        public ActiveCustomerManager()
        {
            _customerManager = new CustomerManager();
            _customer = new Customer();                               
            _customer.Id = 1;
        }
        public Customer SetActiveCustomer(int id)
        {
            
            _customerManager.AddCustomer(_customer);
            var customers = _customerManager.GetCustomers();
            return customers.Find(c => c.Id == id);
        }
    }
}
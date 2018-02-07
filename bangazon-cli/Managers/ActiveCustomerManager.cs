using System;
using System.Linq;
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
            _customerManager.AddCustomer(_customer);

        }
        public Customer SetActiveCustomer(int idx)
        {
            var customers = _customerManager.GetCustomers();
            return customers.ElementAt(idx);
        }
    }
}
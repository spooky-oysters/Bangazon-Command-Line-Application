using System;
using System.Linq;
using bangazon_cli.Models;

namespace bangazon_cli.Managers
/*
    Author: Dre Randaci
    Responsibility: Make a customer active from a list of all customers
 */
{
    public class ActiveCustomerManager
    {
        /* 
            Variables that will be initialized in the constructor
        */
        private CustomerManager _customerManager;
        private Customer _customer;
        public ActiveCustomerManager()
        {
        // Initializing class instances to access class methods
            _customerManager = new CustomerManager();
            _customer = new Customer();
            _customer.Id = 1;
            _customerManager.AddCustomer(_customer);

        }
        public Customer SetActiveCustomer(int idx)
        {
            /* 
                Method takes an index integer from Program.cs user selection and returns the customer at that index with the GetCustomers list of all customers
            */
            return _customerManager.GetCustomers().ElementAt(idx);
        }
    }
}
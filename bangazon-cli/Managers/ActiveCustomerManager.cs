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
        public ActiveCustomerManager(CustomerManager customerManager)
        {
        // Dependancy injection to ensure the _customerManager instance is the same as the test
            _customerManager = customerManager;
        }
        public Customer SetActiveCustomer(int id)
        {
            /* 
                Method takes an index integer from Program.cs user selection and returns the customer at that index with the GetCustomers list of all customers
            */
            return _customerManager.GetSingleCustomer(id); 
        }
    }
}
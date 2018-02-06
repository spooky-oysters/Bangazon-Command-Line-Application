using System.Collections.Generic;
using bangazon_cli.Models;

namespace bangazon_cli.Managers
{
    /*
        Author: Krys Mathis
        Responsibility: Manage the database related tasks for Customers
     */
    public class CustomerManager
    {
        private List<Customer> _customers = new List<Customer>();

        /*
            Adds a customer record to the database
            Parameters: 
                - Customer object
        */        
        public void AddCustomer(Customer customer) {
            _customers.Add(customer);
        }

        // returns all customers from the database
        public List<Customer> GetCustomers() {
            return _customers;
        }
        
    }
}
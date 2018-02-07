using System.Collections.Generic;
using bangazon_cli.Models;
using bangazon_cli.Managers;

namespace bangazon_cli.Actions
{
    public class ActiveCustomer
    {
        private CustomerManager _manager;
        private Customer _customer; 

        public ActiveCustomer()
        {
            _customer = new Customer();
            _manager = new CustomerManager();     
            
            _customer.Id = 1;
            _manager.AddCustomer(_customer);            
        }

        // public static Customer GetActiveCustomer(int id)
        // {
        //     var customers = CustomerManager.GetCustomers();
        //     return Program.ActiveCustomer = customers.Where(c => c.Id == id).Single();
        // }
    }
        

}
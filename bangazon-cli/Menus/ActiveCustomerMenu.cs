using System;
using System.Collections.Generic;
using System.Linq;
using bangazon_cli.Managers;
using bangazon_cli.Models;

namespace bangazon_cli.Menus
{
    public class ActiveCustomerMenu
    {   
        private CustomerManager _customerManager;
        public ActiveCustomerMenu(CustomerManager customerManager, ActiveCustomerManager activeCustomerManager)
        {   
            _customerManager = customerManager;
        }

        public Customer Show() {

            Console.WriteLine("Which customer will be active?");

            // Get the list of customers ordered by customer name
            List<Customer> customers = _customerManager.GetCustomers().OrderBy(c => c.Name).ToList();
            
            int index = 1;
            customers.ForEach(c => {
                Console.WriteLine($"{index}. {c.Name}");
                index ++;
            });
            
            Console.Write ("> ");
            ConsoleKeyInfo enteredKey = Console.ReadKey();
            Console.WriteLine("");
            int output = 0;
            int.TryParse(enteredKey.KeyChar.ToString(), out output);
            
            Customer activeCustomer = customers.ElementAt(output-1);
            return activeCustomer;

            }
        }
}

using System;
using System.Collections.Generic;
using System.Linq;
using bangazon_cli.Managers;
using bangazon_cli.Models;

namespace bangazon_cli.Menus
{
    /*
        Author: Krys Mathis
        Set the active customer
        Requires a customer manager to access the customers from the database
     */
    
    public class ActiveCustomerMenu
    {   
        private CustomerManager _customerManager;
        public ActiveCustomerMenu(CustomerManager customerManager)
        {   
            _customerManager = customerManager;
        }

        /*
            Summary: Returns the id of the selected customer
             - pulls the customers from the database
             - calls the ListCustomers method to display the customers
             - checks to see that the user has selected one of the customers
             - returns the selected customer's Id
         */
        public int Show() {

            // Get the list of customers ordered by customer name
            List<Customer> customers = _customerManager.GetCustomers().OrderBy(c => c.Name).ToList();
            
            // Accept user input, convert it to a number
            int output = 0;
            do {
                // Write the menu to the screen
                ListCustomers(customers);
                Console.Write ("> ");
                ConsoleKeyInfo enteredKey = Console.ReadKey();
                Console.WriteLine("");
                int.TryParse(enteredKey.KeyChar.ToString(), out output);

            } while (output < 1 || output > customers.Count());
            
            // find the selected customer from the list
            Customer customer = customers.ElementAt(output-1); 

            // return the id value
            return customer.Id;

            }

            // Method for listing the customers
            public void ListCustomers(List<Customer> customers) {
                Console.Clear();
                Console.WriteLine("Which customer will be active?");
                int index = 1;
                customers.ForEach(c => {
                    Console.WriteLine($"{index}. {c.Name}");
                    index ++;
                });
            }
        }
}

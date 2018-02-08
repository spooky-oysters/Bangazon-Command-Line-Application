using System;
using bangazon_cli.Models;
using bangazon_cli.Managers;

namespace bangazon_cli.Menus
{
    /*
        Author: Krys Mathis
        Summary - controls the add customer menu
        Parameters 
            - customer object
            - customer manager
    */
    public class AddCustomerMenu
    {   
        private Customer _customer;
        private CustomerManager _customerManager;

        public AddCustomerMenu(Customer customer, CustomerManager customerManager)
        {   
            _customer = customer;
            _customerManager = customerManager;
        }

        /*
            Summary: Displays the add customer menu to the user
         */
        public void Show() {
            
            Console.Clear();
            // Customer name must contain some value
            do {
                Console.WriteLine ("Enter customer name");
                Console.Write ("> ");
                _customer.Name = Console.ReadLine();
            } while (_customer.Name.Length == 0);

            // Street address cannot be null
            do {
                Console.WriteLine ("Enter street address");
                Console.Write ("> ");
                _customer.StreetAddress = Console.ReadLine();
            } while (_customer.StreetAddress.Length == 0);

            // City cannot be null
            do {
                Console.WriteLine ("Enter customer city");
                Console.Write ("> ");
                _customer.City = Console.ReadLine();
            } while (_customer.City.Length == 0);

            // State cannot be null
            do {
                Console.WriteLine ("Enter customer state");
                Console.Write ("> ");
                _customer.State = Console.ReadLine();
            } while (_customer.State.Length == 0);

            // PostalCode cannot be null
            do {
                Console.WriteLine ("Enter customer postal code");
                Console.Write ("> ");
                _customer.PostalCode = Console.ReadLine();
            } while (_customer.PostalCode.Length == 0);

            // Phone number cannot be null
            do {
                Console.WriteLine ("Enter customer phone number");
                Console.Write ("> ");
                _customer.PhoneNumber = Console.ReadLine();
            } while (_customer.PhoneNumber.Length == 0);

            // Add the customer to the database
            int id = _customerManager.AddCustomer(_customer);

            if (id > 0) {
                Console.WriteLine("*** SUCCESS                ***");
                Console.WriteLine("*** PRESS ENTER TO CONTINE ***");
                Console.ReadLine();
            }

        }

    }
}
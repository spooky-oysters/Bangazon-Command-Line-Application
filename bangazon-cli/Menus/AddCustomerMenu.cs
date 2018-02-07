using System;
using bangazon_cli.Models;
using bangazon_cli.Managers;

namespace bangazon_cli.Menus
{
    public class AddCustomerMenu
    {
        public static void Show(Customer customer, CustomerManager customerManager) {

            Console.WriteLine ("Enter customer name");
            Console.Write ("> ");
            string name = Console.ReadLine();
            Console.WriteLine ("Enter customer street address");
            Console.Write ("> ");
            string streetAddress = Console.ReadLine();
            Console.WriteLine ("Enter customer city");
            Console.Write ("> ");
            string city = Console.ReadLine();
            Console.WriteLine ("Enter customer state");
            Console.Write ("> ");
            string state = Console.ReadLine();
            Console.WriteLine ("Enter customer postal code");
            Console.Write ("> ");
            string postalCode = Console.ReadLine();
            Console.WriteLine ("Enter customer phone number");
            Console.Write ("> ");
            string phoneNumber = Console.ReadLine();

            // Assign those values to the customer
            customer.Name = name;
            customer.StreetAddress = streetAddress;
            customer.City = city;
            customer.State = state;
            customer.PostalCode = postalCode;
            customer.PhoneNumber = phoneNumber;

            // Add the customer to the database
            int id = customerManager.AddCustomer(customer);

            if (id > 0) {
                Console.WriteLine("*** SUCCESS ***");
                Console.WriteLine("*** PRESS ENTER TO CONTINE ***");
                Console.ReadLine();
            }


        }
    }
}
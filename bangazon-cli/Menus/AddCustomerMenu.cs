using System;
using bangazon_cli.Models;
using bangazon_cli.Managers;

namespace bangazon_cli.Menus
{
    public class AddCustomerMenu
    {
        public static void Show(Customer customer, CustomerManager customerManager) {
            
            // Customer name must contain some value
            string name;
            do {
                Console.WriteLine ("Enter customer name");
                Console.Write ("> ");
                name = Console.ReadLine();
            } while (name.Length == 0);

            // Street address cannot be null
            string streetAddress;
            do {
                Console.WriteLine ("Enter street address");
                Console.Write ("> ");
                streetAddress = Console.ReadLine();
            } while (streetAddress.Length == 0);

            // City cannot be null
            string city;
            do {
                Console.WriteLine ("Enter customer city");
                Console.Write ("> ");
                city = Console.ReadLine();
            } while (city.Length == 0);

            // State cannot be null
            string state;
            do {
                Console.WriteLine ("Enter customer state");
                Console.Write ("> ");
                state = Console.ReadLine();
            } while (state.Length == 0);

            // PostalCode cannot be null
            string postalCode;
            do {
                Console.WriteLine ("Enter customer postal code");
                Console.Write ("> ");
                postalCode = Console.ReadLine();
            } while (postalCode.Length == 0);

            // Phone number cannot be null
            string phoneNumber;
            do {
                Console.WriteLine ("Enter customer phone number");
                Console.Write ("> ");
                phoneNumber = Console.ReadLine();
            } while (phoneNumber.Length == 0);

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
using System;
using bangazon_cli.Menus;
using bangazon_cli.Models;
using bangazon_cli.Managers;

namespace bangazon_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initalize the interface
            string prodPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB");
            DatabaseInterface db = new DatabaseInterface(prodPath);
            
            // Initialize the Manager objects
            Managers.CustomerManager customerManager = new Managers.CustomerManager(db);
            ActiveCustomerManager activeCustomerManager = new ActiveCustomerManager(customerManager);
            Customer activeCustomer = new Customer();

            int choice;
            // When the user enters the system show the main menu
            do {
                choice = MainMenu.Show(activeCustomer);

                switch (choice) {
                    
                    // Add customer
                    case 1: {
                        AddCustomerMenu customerMenu = new AddCustomerMenu(new Customer(), customerManager);
                        customerMenu.Show();
                        break;
                    }

                    /*
                        List the customers and allow the user to select a customer based on the
                        position in the list
                    */
                    case 2: {
                        ActiveCustomerMenu activeCustomerMenu = new ActiveCustomerMenu(customerManager);
                        int customerId = activeCustomerMenu.Show();
                        activeCustomer = activeCustomerManager.SetActiveCustomer(customerId);
                        break;
                    }

                    default: {
                        break;
                    }
                }

            } while (choice != 9);

      }
    }
}

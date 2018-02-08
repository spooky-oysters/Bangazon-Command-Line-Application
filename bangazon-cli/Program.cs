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
            string prodPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB");
            DatabaseInterface db = new DatabaseInterface(prodPath);
            Managers.CustomerManager customerManager = new Managers.CustomerManager(db);
            ActiveCustomerManager activeCustomerManager = new ActiveCustomerManager(customerManager);
            Customer activeCustomer = new Customer();

            int choice;
            // When the user enters the system show the main menu
            do {
                choice = MainMenu.Show();

                switch (choice) {
                    
                    case 1: {
                        AddCustomerMenu customerMenu = new AddCustomerMenu(new Customer(), customerManager);
                        customerMenu.Show();
                        break;
                    }

                    case 2: {
                        ActiveCustomerMenu activeCustomerMenu = new ActiveCustomerMenu(customerManager, activeCustomerManager);
                        activeCustomer = activeCustomerMenu.Show();
                        break;
                    }

                    default: {
                        break;
                    }
                }

            } while (choice != 9);
            MainMenu.Show()
;        }
    }
}

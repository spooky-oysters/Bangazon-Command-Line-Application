using System;
using bangazon_cli.Menus;
using bangazon_cli.Models;

namespace bangazon_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            string prodPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB");
            DatabaseInterface db = new DatabaseInterface(prodPath);
            Managers.CustomerManager customerManager = new Managers.CustomerManager(db);
            
            int choice;
            // When the user enters the system show the main menu
            do {
                choice = MainMenu.Show();

                switch (choice) {
                    
                    case 1: {
                        AddCustomerMenu.Show(new Customer(), customerManager);
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

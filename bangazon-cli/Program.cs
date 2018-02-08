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
            CustomerManager customerManager = new Managers.CustomerManager(db);
            ActiveCustomerManager activeCustomerManager = new ActiveCustomerManager(customerManager);
<<<<<<< HEAD
            ProductManager productManager = new ProductManager(db);
||||||| merged common ancestors
            Customer activeCustomer = new Customer();
=======
            Customer activeCustomer = new Customer();
            ProductManager productManager = new Managers.ProductManager(db);
            OrderManager orderManager = new OrderManager(db);
>>>>>>> 8ee1e84b842a45f33396bd07a70f47e0dc3b64c0

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
                    
                    /*
                        List the active customer's product(s) 
                        The user cannot delete products that are on active orders
                    */
                    case 6: {
                        DeleteActiveCustomerProducts menu = new DeleteActiveCustomerProducts(activeCustomer, productManager);
                        menu.Show();
                        break;
                    }
                    default: {
                        break;
                    }
                }

            } while (choice != 10);

      }
    }
}

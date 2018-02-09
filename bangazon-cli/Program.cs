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
            CustomerManager customerManager = new CustomerManager(db);
            ActiveCustomerManager activeCustomerManager = new ActiveCustomerManager(customerManager);
            Customer activeCustomer = new Customer();
            ProductManager productManager = new ProductManager(db);
            OrderManager orderManager = new OrderManager(db);
            PaymentTypeManager paymentTypeManager = new PaymentTypeManager(db);

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
                        Add a new payment type for a customer
                     */

                    case 3: {
                        PaymentTypeMenu addPaymentTypeMenu = new PaymentTypeMenu(new PaymentType(), paymentTypeManager, activeCustomer);
                        addPaymentTypeMenu.Show();
                        break;
                    }

                    /*
                        Lists all products to allow user to choose one to add to their order. When product is chosen, the product is added to the active customer's order
                    */
                    case 6: {
                        AddProductToCartMenu addProductToCartMenu = new AddProductToCartMenu(activeCustomer, orderManager, productManager);
                        addProductToCartMenu.Show();
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

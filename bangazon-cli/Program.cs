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
            ProductManager productManager = new Managers.ProductManager(db);
            OrderManager orderManager = new OrderManager(db);
            PaymentTypeManager paymentTypeManager = new PaymentTypeManager(db);

            Customer activeCustomer = new Customer();
            
            int choice;
            // When the user enters the system show the main menu
            do
            {
                choice = MainMenu.Show(activeCustomer);

                switch (choice)
                {

                    // Add customer
                    case 1:
                        {
                            AddCustomerMenu customerMenu = new AddCustomerMenu(new Customer(), customerManager);
                            customerMenu.Show();
                            break;
                        }

                    /*
                        List the customers and allow the user to select a customer based on the
                        position in the list
                    */
                    case 2:
                        {
                            ActiveCustomerMenu activeCustomerMenu = new ActiveCustomerMenu(customerManager);
                            int customerId = activeCustomerMenu.Show();
                            activeCustomer = activeCustomerManager.SetActiveCustomer(customerId);
                            break;
                        }

                    /*
                        Add product to active customer 
                    */
                    case 4:
                        {
                            AddProductMenu addProductMenu = new AddProductMenu(activeCustomer, new Product(), productManager);
                            addProductMenu.Show();
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
                        List the active customer's product(s) 
                        The user cannot delete products that are on active orders
                    */
                    case 6: {
                        DeleteActiveCustomerProductsMenu menu = new DeleteActiveCustomerProductsMenu(activeCustomer, productManager);
                        menu.Show();
                        break;
                    }

                    /*
                        Lists all products to allow user to choose one to add to their order. When product is chosen, the product is added to the active customer's order
                    */

                    case 7: {
                        AddProductToCartMenu addProductToCartMenu = new AddProductToCartMenu(activeCustomer, orderManager, productManager);
                        addProductToCartMenu.Show();
                        break;
                    }

                    /*
                        Allow user to complete order.
                        Checks to make sure active customer's order contains products
                        If products exist on order, displays the total order amount in dollars.
                        Allows user to Add Payment type to their order.
                    */

                    case 8: {
                        CloseOrderMenu closeOrderMenu = new CloseOrderMenu(activeCustomer, orderManager, productManager, paymentTypeManager);
                        closeOrderMenu.Show();
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

using System;
using bangazon_cli.Models;
using bangazon_cli.Managers;
using System.Collections.Generic;
using System.Linq;

namespace bangazon_cli.Menus
{
    /*
        Author: Greg Lawrence
        Summary - controls the Add Product to Cart menu
        Parameters 
            - customer object
            - order manager
            - product manager
    */
    public class AddProductToCartMenu
    {   
        private Customer _customer;
        private OrderManager _orderManager;
        private ProductManager _productManager;

        public AddProductToCartMenu(Customer customer, OrderManager orderManager, ProductManager productManager)
        {   
            _customer = customer;
            _orderManager = orderManager;
            _productManager = productManager;
        }

        /*
            Summary: Displays the add customer menu to the user
         */
        public void Show() {
            // get a list of all products in the database
            List<Product> AllProducts = _productManager.GetProducts();
            
            // create variable to hold the user choice
            int output = 0;

            // create a variable to hold a number to use as the menu number.
            int menuNum = 1;

            // create a list of products to hold all the available products with quantities greated than 0
            List<Product> AllAvailableProducts = new List<Product>();

            do {
                // start the menuNum at 1 for each loop
                menuNum = 1;

                // Clear the console
                Console.Clear();

                // check if there are products in the database
                if (AllProducts.Count > 0) {
                    
                    Console.WriteLine("Choose a Product to add to the order:");
                    Console.WriteLine();

                    // Loop through list of products and only print available products to the console, which are products with a quantity greater than 0. 
                    foreach (Product product in AllProducts)
                    {
                        if (_orderManager.hasAvailableQuantity(product)) {
                            // add product to list of available products
                            AllAvailableProducts.Add(product);
                            Console.WriteLine($"{menuNum}. {product.Name}");
                            menuNum++;
                        }
                    }
                    // show alert to user if there are no available products
                    if (AllAvailableProducts.Count() < 1) {
                        Console.WriteLine("*** NO PRODUCTS CURRENTLY AVAILABLE TO PURCHASE. ***");
                    }
                } else {
                    // if there are no products, alert the user
                    Console.WriteLine("*** NO PRODUCTS CURRENTLY IN SYSTEM ***");
                    Console.WriteLine("*** PRESS ENTER TO CONTINUE.        ***");
                }

                // menu option to exit back to main menu. This will always be the last number, no matter how many products are in the system.
                Console.WriteLine ($"{menuNum + 1}. Exit back to Main Menu. ");

                Console.Write ("> ");
                String enteredKey = Console.ReadLine();
                Console.WriteLine("");
                int.TryParse(enteredKey, out output);

                if(output != menuNum + 1) {
                    // find the selected product from the list
                    Product selectedProduct = AllAvailableProducts.ElementAt(output - 1);

                    // retrieve customer's Unpaid order
                    Order customerOrder = _orderManager.GetUnpaidOrder(_customer.Id);

                    // check if the customer does not have a current order started. If they do not, add an order and retrieve it to get the OrderId. 
                    if (customerOrder.Id < 1) {
                        Order newOrder = new Order(_customer.Id);
                        customerOrder.Id = _orderManager.AddOrder(newOrder);
                    }

                    // create a record in the OrderProduct table for the relationship of the selected product and the customer's orderId
                    _orderManager.AddProductToOrder(customerOrder.Id, selectedProduct.Id);

                    // Alert the user press enter to continue adding products or to select the option to got back to main menu
                    Console.WriteLine("*** Press ENTER to continue to add products                   ***");
                    Console.WriteLine($"*** Or Choose {menuNum + 1} and press enter to exit back to main menu. ***");
                    int.TryParse(Console.ReadLine(), out output);
                }
                
            } while (output != menuNum + 1);
        }

    }
}
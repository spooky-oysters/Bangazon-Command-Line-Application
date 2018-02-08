using System;
using bangazon_cli.Models;
using bangazon_cli.Managers;
using System.Collections.Generic;

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
            
            if (AllProducts.Count > 0) {
                // create a variable to hold a number to use as the menu number.
                int menuNum = 1;
                // Loop through list of products and print each to console.
                Console.WriteLine("Choose a Product to add to the order:");
                    Console.WriteLine();
                foreach (Product product in AllProducts)
                {
                    Console.WriteLine($"{menuNum}. {product.Name}");
                    menuNum++;
                }
            } else {
                Console.WriteLine("No products currently in system.");
                Console.WriteLine("Press Enter to continue.");
            }
           
            Console.Write ("> ");
            ConsoleKeyInfo enteredKey = Console.ReadKey();
            Console.WriteLine("");
            // int output = 0;
            // int.TryParse(enteredKey.KeyChar.ToString(), out output);
            // return output;
        }

    }
}
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
            if (_customer.Id > 0) {
                // get a list of all products in the database
                List<Product> AllProducts = _productManager.GetProducts();
                
                // create variable to hold the user choice
                int output = 0;

                    do {
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
                            Console.WriteLine("*** NO PRODUCTS CURRENTLY IN SYSTEM ***");
                            Console.WriteLine("*** PRESS ENTER TO CONTINUE.        ***");
                        }
                        Console.Write ("> ");
                        ConsoleKeyInfo enteredKey = Console.ReadKey();
                        Console.WriteLine("");
                        int.TryParse(enteredKey.KeyChar.ToString(), out output);

                    } while (output < 1 || output > AllProducts.Count);

                
                // find the selected product from the list
                Product selectedProduct = AllProducts.ElementAt(output - 1);


                //retrieve customers Unpaid order
                Order customerOrder = _orderManager.GetUnpaidOrder(_customer.Id);

                // check if the customer does not have a current order started. If they do not, add an order and retrieve it to get the OrderId. 
                if (customerOrder.Id < 1) {
                    Order newOrder = new Order(_customer.Id);
                    customerOrder.Id = _orderManager.AddOrder(newOrder);
                    Console.WriteLine("customerOrderId", customerOrder.Id);
                }

                try {
                    // create a record in the OrderProduct table for the relationship of the selected product and the customer's orderId
                    _orderManager.AddProductToOrder(customerOrder.Id, selectedProduct.Id);

                    Console.WriteLine("*** SUCCESS                ***");
                    Console.WriteLine("*** PRESS ENTER TO CONTINE ***");
                    Console.ReadLine();
                } catch (Exception err) {
                    Console.WriteLine("Error adding Product to Order", err.Message);
                }
            } else {
                Console.WriteLine("*** SELECT AN ACTIVE CUSTOMER FIRST! ***");
                Console.WriteLine("*** PRESS ENTER TO CONTINUE ***");
                Console.ReadLine();
            }

        }

    }
}
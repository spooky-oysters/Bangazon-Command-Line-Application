using System;
using bangazon_cli.Models;
using bangazon_cli.Managers;
using System.Collections.Generic;
using System.Linq;

namespace bangazon_cli.Menus
{
    /*
        Author: Greg Lawrence
        Summary - controls the Close Order menu
        Parameters 
            - customer object
            - order manager
            - product manager
            - payment type manager
    */
    public class CloseOrderMenu
    {
        private Customer _customer;
        private OrderManager _orderManager;
        private ProductManager _productManager;
        private PaymentTypeManager _paymentTypeManager;

        public CloseOrderMenu(Customer customer, OrderManager orderManager, ProductManager productManager, PaymentTypeManager paymentTypeManager)
        {
            _customer = customer;
            _orderManager = orderManager;
            _productManager = productManager;
            _paymentTypeManager = paymentTypeManager;
        }

        /*
            Summary: Displays the add customer menu to the user
         */
        public void Show()
        {
            // get the users order
            Order UserOrder = _orderManager.GetProductFromOrder(_customer.Id);
            // print the amount of orders in the list to console
            Console.WriteLine(UserOrder.Products.Count());
            
            // Check if order contains products
            if (UserOrder.Products.Count() < 1) {
                // if no products on order, display message for user to add products first
                Console.WriteLine("*** NO PRODUCTS EXIST IN YOUR SHOPPING CART. ***");
                Console.WriteLine("*** ADD PRODUCTS TO YOUR SHOPPING CART FIRST. ***");
                Console.WriteLine("*** PRESS ENTER TO RETURN TO MAIN MENU ***");
                Console.ReadLine();

            } else {
                // if orders present, display order total and prompt user to choose a payment type
                /* 
                    Your order total is $149.54. Ready to purchase
                    (Y/N) >

                    # If user entered Y
                    Choose a payment option
                    1. Amex
                    2. Visa
                    >
                */
                Double OrderTotal = 0;
                UserOrder.Products.ForEach(p=> OrderTotal += p.Price);
                Console.WriteLine($"Your order total is ${OrderTotal}. Ready to purchase?");
                Console.WriteLine("(Y/N) >");  
                ConsoleKeyInfo enteredKey = Console.ReadKey();                 
                Console.WriteLine(""); 
                string response = enteredKey.KeyChar.ToString();

                // check user response (Y or N)
                if (response.ToLower() == "y") 
                {
                    // prompt user to pick a payment type
                    Console.WriteLine("Choose a payment option:");

                    // if user selected Y, display their available Payment Types
                    List<PaymentType> custPaymentTypes = _paymentTypeManager.GetPaymentTypesByCustomerId(_customer.Id);

                    // create a starting menu item number
                    int menuNum = 1;
                    // create a variable to hold the payment type choice
                    int output = -1;
                    // loop through payment types and write each to the console and increment the menuNum by 1.
                    custPaymentTypes.ForEach(pt => {
                        Console.WriteLine($"{menuNum}. {pt.Type}");
                        menuNum++;
                    });
                    Console.Write("> ");
                    Console.ReadKey();
                    ConsoleKeyInfo paymentChoice = Console.ReadKey();
                    Console.WriteLine("");
                    int.TryParse(paymentChoice.KeyChar.ToString(), out output);
                    
                        

                } else {
                    Console.WriteLine("*** PURCHASE CANCELLED.             ***");
                    Console.WriteLine("*** PRESS ENTER TO CONTINUE.        ***");
                    Console.ReadLine();
                }




            }
        }
    }
}
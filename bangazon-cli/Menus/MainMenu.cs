using System;
using bangazon_cli.Models;
using System.Collections.Generic;

namespace bangazon_cli.Menus
{
   /*
        Author: Krys Mathis
        Summary: the main menu will display when the user enters the system
        Returns: the selected menu option
   */

    public class MainMenu
    {
        public static int Show(Customer activeCustomer)
        {
            /*
                If no active user, display fewer choices
                Validate the user selection against the available options.
                Continue looping until the users chooses a valid option.
            */
            bool hasActiveCustomer = activeCustomer.Id > 0;
            int output = 0;
            bool isValidChoice = false;
            List<int> validChoicesWithoutActiveCustomer = new List<int>(){1,2,10};
            
            do {
                 Console.Clear();
            
                Console.WriteLine ("*********************************************************");
                Console.WriteLine ("**  Welcome to Bangazon! Command Line Ordering System  **");
                Console.WriteLine ("*********************************************************");
                
                if (hasActiveCustomer) {
                    Console.WriteLine("*********************************************************");
                    Console.WriteLine ($"  Active Customer: {activeCustomer.Name}");
                    Console.WriteLine ("*********************************************************");
                }

                Console.WriteLine ("1. Create a customer account");
                Console.WriteLine ("2. Choose active customer");
                
                // options available only if isActiveUser = true
                if (hasActiveCustomer) {
                    Console.WriteLine ("3. Create a payment option");
                    Console.WriteLine ("4. Add a product to active customer");
                    Console.WriteLine ("5. Update active customer's product");
                    Console.WriteLine ("6. Delete active customer's product");
                    Console.WriteLine ("7. Add product to shopping cart");
                    Console.WriteLine ("8. Complete an order");
                    Console.WriteLine ("9. View Reports");
                }
                
                Console.WriteLine ("10. Leave Bangazon!");

                // get the user input
                Console.Write ("> ");
                string enteredValue = Console.ReadLine();
                Console.WriteLine("");
                output = 0;
                if (int.TryParse(enteredValue, out output) == false) {
                    output = 0;
                }

                // Check validity of users's selection - which depends on if 
                // an active customer was selected
                if (hasActiveCustomer == false) {
                    
                    // Without a valid customer the only valid choices are 1,2 and 10
                    isValidChoice = validChoicesWithoutActiveCustomer.Contains(output);
                    
                    // if it is not a valid choice do not return an option
                    if (!isValidChoice) {
                        output = 0;
                    }

                } else {
                    isValidChoice = true;
                }

            // don't exit unless the output is >0 and is a valid choice
            } while (output == 0 && !isValidChoice);


            return output;
        }

    }
} 
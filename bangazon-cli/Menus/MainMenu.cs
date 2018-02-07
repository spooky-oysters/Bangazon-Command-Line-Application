using System;

namespace bangazon_cli.Menus
{
   /*
        Author: Krys Mathis
        Summary: the main menu will display when the user enters the system
        Returns: the selected menu option
   */

    public class MainMenu
    {
        public static int Show()
        {
            Console.Clear();

            Console.WriteLine ("*********************************************************");
            Console.WriteLine ("**  Welcome to Bangazon! Command Line Ordering System  **");
            Console.WriteLine ("*********************************************************");
            
            Console.WriteLine ("1. Create a customer account");
            Console.WriteLine ("2. Choose active customer");
            Console.WriteLine ("3. Create a payment option");
            Console.WriteLine ("4. Add a product to active customer");
            Console.WriteLine ("5. Update active customer's product");
            Console.WriteLine ("6. Add product to shopping cart");
            Console.WriteLine ("7. Complete an order");
            Console.WriteLine ("8. View Reports");
            Console.WriteLine ("9. Leave Bangazon!");


            Console.Write ("> ");
            ConsoleKeyInfo enteredKey = Console.ReadKey();
            Console.WriteLine("");
            int output = 0;
            int.TryParse(enteredKey.KeyChar.ToString(), out output);
            return output;
        }

    }
}
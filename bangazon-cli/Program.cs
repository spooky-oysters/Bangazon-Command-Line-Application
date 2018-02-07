using System;
using bangazon_cli.Menus;
using bangazon_cli.Managers;


namespace bangazon_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            string prodPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB");
            DatabaseInterface db = new DatabaseInterface(prodPath);
            Managers.CustomerManager customerManager = new Managers.CustomerManager(db);
            Managers.ProductManager productManager = new Managers.ProductManager(db);
            OrderManager orderManager = new OrderManager(db);
            
            int choice;
            // When the user enters the system show the main menu
            do {
                choice = MainMenu.Show();

            } while (choice != 9);
            MainMenu.Show()
;        }
    }
}

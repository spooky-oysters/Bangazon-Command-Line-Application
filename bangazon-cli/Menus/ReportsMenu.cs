using System;
using bangazon_cli.Models;
using bangazon_cli.Managers;

namespace bangazon_cli.Menus
{
    public class ReportsMenu
    {
        private PopularProductsReportMenu popularProductsMenu;
        public void Show()
        {
            int choice = 0;
            do
            {
                Console.Clear();

                Console.WriteLine("1. View Most Popular Products");
                
                choice = Convert.ToInt32(Console.ReadLine());
                
            switch (choice)
            {
                // View popular products report
                case 1:
                {
                    popularProductsMenu = new PopularProductsReportMenu();
                    popularProductsMenu.Show();
                    break;
                }
            }
            } while (choice == 0);
        }

    }
}
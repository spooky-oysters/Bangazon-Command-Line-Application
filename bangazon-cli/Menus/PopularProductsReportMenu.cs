using System;

namespace bangazon_cli.Menus
{
    public class PopularProductsReportMenu
    {
        public void Show()
        {
            string keyPress;
            do
            {
                Console.Clear();
                Console.WriteLine("IN THE POPULAR PRODUCTS MENU");
                keyPress = Console.ReadLine();
            } while (keyPress != "");
            
        }
    }
}
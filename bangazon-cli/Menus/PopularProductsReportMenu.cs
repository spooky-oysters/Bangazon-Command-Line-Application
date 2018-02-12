using System;

namespace bangazon_cli.Menus
{
    public class PopularProductsReportMenu
    {
        static int tableWidth = 61;
        static int productCol = 20;
        static int ordersCol = 11;
        static int purchasersCol = 15;
        static int revenueCol = 15;
        public void Show()
        {
            Console.WriteLine($"{r.Product,-20}{r.Orders,-11}{r.Purchasers,-15}${r.Revenue,-15}, Product, Orders, Purchasers, Revenue");
            string keyPress;
            do
            {
                Console.Clear();
                Console.WriteLine(new string('Product', productCol));
                Console.WriteLine(new string('*', tableWidth));
                Console.WriteLine("");
                keyPress = Console.ReadLine();
            } while (keyPress != "");
            
        }
    }
}
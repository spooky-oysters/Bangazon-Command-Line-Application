using System;
using System.Collections.Generic;
using bangazon_cli.Models;
using Microsoft.Data.Sqlite;

namespace bangazon_cli.Menus
{
    public class PopularProductsReportMenu
    {
        private DatabaseInterface _db;
        private List<PopularProductsReport> _popularProducts;

        public PopularProductsReportMenu()
        {
            string dbPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB");
            _db = new DatabaseInterface(dbPath);

            _popularProducts = new List<PopularProductsReport>();

            var item1 = new PopularProductsReport("Kite1", "5000", "2000", "1920.20");
            var item2 = new PopularProductsReport("Kite2", "500", "200", "920.20");
            var item3 = new PopularProductsReport("Kite3", "50", "20", "90.20");

            _popularProducts.Add(item1);
            _popularProducts.Add(item2);
            _popularProducts.Add(item3);

            // _db.Query($@"SELECT * FROM Product;", (SqliteDataReader reader) =>
            // {
            //     while (reader.Read())
            //     {

            //     }
            // });   
        }
        
        
        public void Show()
        {
            string keyPress;
            
            do
            {
                Console.Clear();
                Console.WriteLine($"{"Product",-20}{"Orders",-11}{"Purchasers",-15}{"Revenue",-15}");
                Console.WriteLine(new string('*', 61));
                Console.WriteLine("");
                keyPress = Console.ReadLine();
            } while (keyPress != "");
            
        }
    }
}
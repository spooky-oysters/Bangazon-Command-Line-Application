using System;
using System.Collections.Generic;
using bangazon_cli.Models;
using Microsoft.Data.Sqlite;

namespace bangazon_cli.Menus
{
    public class PopularProductsReportMenu
    {
        // Initializing a db to use for the SQL query
        private DatabaseInterface _db;

        // List to hold all the return query items
        private List<PopularProductsReport> _popularProducts;

        public PopularProductsReportMenu()
        {
            // Path to the local database file
            string dbPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB");
            _db = new DatabaseInterface(dbPath);

            // New list instance to hold query results and loop over to print to the console
            _popularProducts = new List<PopularProductsReport>();

            // SQL query. Creates a temporary table that is then sub-queried  
            _db.Query($@"
            with PopularItems as
                (select p.Name Product, count(op.Id) as Orders, CustomerCount.Purchasers as Purchasers, sum(p.Price) as Revenue
                from Product p
                join OrderProduct op on p.Id = op.ProductId
                join 
                (select p.Id ProductId, count(distinct o.CustomerId) Purchasers
                from Product p
                join OrderProduct on p.Id = OrderProduct.ProductId
                join `Order` o on OrderProduct.OrderId = o.Id
                group by p.Name) 
                CustomerCount on p.Id = CustomerCount.ProductId
                group by p.Name 
                order by Revenue desc
                limit 3
                )
                select * from PopularItems
                union all
                select ""Totals"" Totals, sum(""Orders"") Orders, sum(""Purchasers"") Purchasers, sum(""Revenue"") Revenue
                from PopularItems;", (SqliteDataReader reader) =>
            {
                while (reader.Read())
                {
                    // Creates an instance of a single PopularProductsReport class to hold each report that comes back from the database
                    PopularProductsReport report = new PopularProductsReport();
                    report.Product = Convert.ToString(reader["Product"]);
                    report.Orders = Convert.ToString(reader["Orders"]);
                    report.Purchasers = Convert.ToString(reader["Purchasers"]);
                    report.Revenue = Convert.ToString(reader["Revenue"]);

                    // Adds each report to the list
                    _popularProducts.Add(report);
                }
            });
        }
        
        public void Show()
        {
            // Listens for a keypress to return the user to the main menu
            ConsoleKeyInfo enteredKey;

            // Builds the console table to display the information
            do
            {
                Console.Clear();

                // The string args represent column headers
                Console.WriteLine($"{"Product",-20}{"Orders",-11}{"Purchasers",-15}{"Revenue",-15}");

                // Prints 61 asterisks
                Console.WriteLine(new string('*', 61));

                // Loops over the query list and once the counter is above 3 items, prints another row of asterisks to separate the final "Totals" list element
                for (int i = 0; i <= _popularProducts.Count; i++)
                {
                    if (i != 3)
                    {
                        Console.WriteLine($"{_popularProducts[i].Product,-20}{_popularProducts[i].Orders,-11}{_popularProducts[i].Purchasers,-15}${_popularProducts[i].Revenue,-15}");
                    } else {
                        Console.WriteLine(new string('*', 61));
                        Console.WriteLine($"{_popularProducts[i].Product + ':',-20}{_popularProducts[i].Orders,-11}{_popularProducts[i].Purchasers,-15}${_popularProducts[i].Revenue,-15}");
                        break;
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("-> Press any key to return to main menu");
                enteredKey = Console.ReadKey();
            } while (enteredKey.KeyChar.ToString() == "");
        }
    }
}
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

            _db.Query($@"select *
                from
                (
                select p.Name, count(op.Id) as Orders, CustomerCount.Purchasers as Purchasers, sum(p.Price) as Revenue
                from Product p
                join OrderProduct op on p.Id = op.ProductId
                join (select p.Id ProductId, count(distinct o.CustomerId) Purchasers
                from Product p
                join OrderProduct on p.Id = OrderProduct.ProductId
                join `Order` o on OrderProduct.OrderId = o.Id
                group by p.Name) CustomerCount on p.Id = CustomerCount.ProductId
                group by p.Name 
                order by Revenue desc
                limit 3
                )

                union all
                select ""Totals"" Totals, sum(""Orders"") Orders, sum(""Purchasers"") Purchasers, sum(""Revenue"") Revenue
                from (
                select p.Name Product, count(op.Id) as Orders, CustomerCount.Purchasers as Purchasers, sum(p.Price) as Revenue
                from Product p
                join OrderProduct op on p.Id = op.ProductId
                join (select p.Id ProductId, count(distinct o.CustomerId) Purchasers
                from Product p
                join OrderProduct on p.Id = OrderProduct.ProductId
                join `Order` o on OrderProduct.OrderId = o.Id
                group by p.Name) CustomerCount on p.Id = CustomerCount.ProductId
                group by p.Name 
                order by Revenue desc
                limit 3
                );", (SqliteDataReader reader) =>
            {
                while (reader.Read() && _popularProducts.Count <= 3)
                {
                    PopularProductsReport report = new PopularProductsReport();
                    report.Product = Convert.ToString(reader["Product"]);
                    report.Orders = Convert.ToString(reader["Orders"]);
                    report.Purchasers = Convert.ToString(reader["Purchasers"]);
                    report.Revenue = Convert.ToString(reader["Revenue"]);
                    _popularProducts.Add(report);
                }
            });   
        }
        
        
        public void Show()
        {
            string keyPress;
            do
            {
                Console.Clear();
                Console.WriteLine($"{"Product",-20}{"Orders",-11}{"Purchasers",-15}{"Revenue",-15}");
                Console.WriteLine(new string('*', 61));
                foreach (var row in _popularProducts)
                {
                    Console.WriteLine($"{row.Product,-18}{row.Orders,-11}{row.Purchasers,-15}${row.Revenue,-15}");
                }
                Console.WriteLine(new string('*', 61));
                Console.WriteLine($"{"Totals",-20}{"",-11}{"",-15}{"",-15}");
                keyPress = Console.ReadLine();
            } while (keyPress != "");
            
        }
    }
}
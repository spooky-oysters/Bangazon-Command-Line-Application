using System;
using System.Collections.Generic;
using System.Linq;
using bangazon_cli.Models;
using Microsoft.Data.Sqlite;


namespace bangazon_cli.Managers
{
    /*
        Author: Greg Lawrnece
        Responsibility: Manage the database related tasks for Orders
     */
    public class OrderManager
    {
        
        // collection to store orders until our database is setup
        private List<Order> _orders = new List<Order>();
        private DatabaseInterface _db;
        
        // dictionary to act as joiner table for Product and Order
        private Dictionary<int, int> _orderProduct = new Dictionary<int, int>();
        
        // injecting databaseinterface dependency
        public OrderManager(DatabaseInterface db)
        {
            _db = db;
            this.CreateOrderTable();
            this.CreateOrderProductTable();
        }


        private void CreateOrderTable() {
            try {
                _db.Update(@"CREATE TABLE IF NOT EXISTS `Order` (
                    `Id` INTEGER PRIMARY KEY AUTOINCREMENT,
                    `CustomerId` INTEGER NOT NULL,
                    `PaymentTypeId` INTEGER,
                    `CompletedDate` TEXT,
                    FOREIGN KEY(`CustomerId`) REFERENCES `Customer`(`Id`)
                    );
                ");
            } catch (Exception ex) {
                Console.WriteLine("CreateOrderTable", ex.Message);
            }
        }

        // creates the OrderProduct joiner table 
        private void CreateOrderProductTable() {
            try {
                _db.Update(@"CREATE TABLE IF NOT EXISTS `OrderProduct` (
                    `Id` INTEGER PRIMARY KEY AUTOINCREMENT,
                    `OrderId` INTEGER NOT NULL,
                    `ProductId` INTEGER NOT NULL,
                    FOREIGN KEY(`OrderId`) REFERENCES `Order`(`Id`)
                    FOREIGN KEY(`ProductId`) REFERENCES `Product`(`Id`)
                    );
                ");
            } catch (Exception ex) {
                Console.WriteLine("CreateOrderTable", ex.Message);
            }
        }
        /*
            Adds a Order record to the database
            Parameters: 
                - Order object
        */        
        public void AddOrder(Order order) {
            string SQLInsert = $@"INSERT INTO `Order`
            VALUES (
                null,
                {order.CustomerId},
                null,
                null
            );";

            try {
                _db.Insert(SQLInsert);
            } catch (Exception err) {
                Console.WriteLine("Add Order Error", err.Message);
            }
        }

        // returns customer's unpaid order from the database
        public Order GetUnpaidOrder(int id) {
            // initialize a new order to hold the return from db
            Order order = new Order();
            // query the database for a matching order
            _db.Query($@"SELECT * 
                        FROM `Order` o
                        WHERE o.CustomerId = {id}
                        AND o.PaymentTypeId IS NULL;
                        ",
                        (SqliteDataReader reader) =>
                        {
                            while (reader.Read())
                            {
                                order.Id = Convert.ToInt32(reader["Id"]);
                                order.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                                order.PaymentTypeId = null;
                                order.CompletedDate = null;
                            }
                        });
            
            return order;

        }

        // store a product on an order, by using a joiner table
        public void AddProductToOrder(int orderId, int productId)
        {
            // add the product and order relationship to the OrderProduct join table
            string SQLInsert = $@"INSERT INTO OrderProduct
            VALUES (
                null,
                {orderId},
                {productId}
            );";

            try {
                _db.Insert(SQLInsert);
            } catch (Exception err) {
                Console.WriteLine("Add OrderProduct Error", err.Message);
            }
        }

        // function to check if customer's order contains a product.
        public Order GetProductFromOrder(int orderId)
        {
            // if (_orderProduct.Count > 0) {

            //     foreach (KeyValuePair<int, int>product in _orderProduct)
            //     {
            //         if (product.Key == orderId && product.Value == productId) 
            //         {
            //             return true;
            //         } else {
            //             return false;
            //         }
            //     }
            // } 

            // return false;

            // initialize a new order to hold the return from db
            Order CurrentOrder = new Order();
            CurrentOrder.Products = new List<Product>();    

            // query the database for a matching order
            _db.Query($@"SELECT o.Id as OrderId, o.CustomerId as CustomerId, o.PaymentTypeId, o.CompletedDate, op.Id as OrderProductId, p.Id as ProductId, p.Name as ProductName, p.Description ProductDescription, p.Price as ProductPrice, p.Quantity as ProductQuantity
                        FROM `Order` o, OrderProduct op, Product p
                        WHERE o.Id = 1
                        AND o.Id = op.OrderId
						AND op.ProductId = p.Id;
                        ",
                        (SqliteDataReader reader) =>
                        {
                            while (reader.Read())
                            {
                                CurrentOrder.Id = Convert.ToInt32(reader["OrderId"]);
                                CurrentOrder.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                                CurrentOrder.PaymentTypeId = null;
                                CurrentOrder.CompletedDate = null;

                                // int productId = Convert.ToInt32(reader["p.Id"]);

                                Product CurProduct = new Product();

                                CurProduct.Id = Convert.ToInt32(reader["ProductId"]);
                                CurProduct.Name = Convert.ToString(reader["ProductName"]);
                                CurProduct.Price = Convert.ToDouble(reader["ProductPrice"]);
                                CurProduct.Description = Convert.ToString(reader["ProductDescription"]);
                                CurProduct.Quantity = Convert.ToInt32(reader["ProductQuantity"]);
                                
                                CurrentOrder.Products.Add(CurProduct);
                            }
                        
                        });
            
            return CurrentOrder;
        }

    }
}
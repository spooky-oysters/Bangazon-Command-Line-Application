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
        public int AddOrder(Order order) {
            string SQLInsert = $@"INSERT INTO `Order`
            VALUES (
                null,
                {order.CustomerId},
                null,
                null
            );";

            int orderId = 0;
            try {
                orderId = _db.Insert(SQLInsert);
                order.Id = orderId;
            } catch (Exception err) {
                Console.WriteLine("Add Order Error", err.Message);
            }
            return orderId;
        }

        // returns customer's unpaid order from the database
        public Order GetUnpaidOrder(int customerId) {
            // initialize a new order to hold the return from db
            Order order = new Order(customerId);
            // query the database for a matching order
            _db.Query($@"SELECT * 
                        FROM `Order` o
                        WHERE o.CustomerId = {customerId}
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
            // initialize a new order to hold the return from db
            Order CurrentOrder = new Order();

            // query the database for a matching order
            _db.Query($@"SELECT o.Id as OrderId, o.CustomerId as CustomerId, o.PaymentTypeId, o.CompletedDate, op.Id as OrderProductId, p.Id as ProductId, p.Name as ProductName, p.Description ProductDescription, p.Price as ProductPrice, p.Quantity as ProductQuantity
                        FROM `Order` o, OrderProduct op, Product p
                        WHERE o.Id = {orderId}
                        AND o.Id = op.OrderId
						AND op.ProductId = p.Id;
                        ",
                        (SqliteDataReader reader) =>
                        {
                            while (reader.Read())
                            {
                                // assign order details to the order created above
                                CurrentOrder.Id = Convert.ToInt32(reader["OrderId"]);
                                CurrentOrder.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                                CurrentOrder.PaymentTypeId = null;
                                CurrentOrder.CompletedDate = null;

                                // create a new product to hold retrieved product from db
                                Product CurProduct = new Product();
                                // assign product details to the product created above
                                CurProduct.Id = Convert.ToInt32(reader["ProductId"]);
                                CurProduct.Name = Convert.ToString(reader["ProductName"]);
                                CurProduct.Price = Convert.ToDouble(reader["ProductPrice"]);
                                CurProduct.Description = Convert.ToString(reader["ProductDescription"]);
                                CurProduct.Quantity = Convert.ToInt32(reader["ProductQuantity"]);
                                
                                // store product in a list on the order
                                CurrentOrder.Products.Add(CurProduct);
                            }
                        });
            
            return CurrentOrder;
        }

        /*
            Author: Krys Mathis
            Summary: Queries the database and returns the available quantity for a product
                    this is the result of the product quantity minus the total number of 
                    order rows for the product on closed orders (PaymentTypeId is not null)
            Parameter: Product object
            Returns: amount of available product (initial minus sold)
         */
        public int getAvailableQuantity(Product product)
        {
            int intialQuantity = product.Quantity;
            int soldQuantity = 0;

            _db.Query($@"SELECT Count(*) as Sold FROM Product p 
                        LEFT JOIN OrderProduct op ON p.Id = op.ProductId
                        LEFT JOIN (SELECT * from `Order` WHERE CompletedDate is not null) o ON op.OrderId = o.Id
                        WHERE p.Id = {product.Id};",

                     (SqliteDataReader reader) =>
                            {
                                while (reader.Read())
                                {
                                    // if the value is null, do not reassign it
                                    if(!reader.IsDBNull(0))
                                    {
                                        soldQuantity = Convert.ToInt32(reader["Sold"]);
                                    }
                                }
                    });
                    
            return intialQuantity - soldQuantity;
        }

        /*
            Author: Krys Mathis
            Summary: Checks if available quantity is > 0
            Parameter: Product object
            Returns: true of false
         */
        public bool hasAvailableQuantity(Product product){
            return getAvailableQuantity(product) > 0;
        }

        public Product GetSingleProductFromOrder(int orderId, int productId)
        {

            try {
                // initialize a new order to hold the return from db
                Order CurrentOrder = new Order();
                // create a new product to hold retrieved product from db
                Product CurProduct = new Product();    

                // query the database for a matching order
                _db.Query($@"SELECT o.Id as OrderId, o.CustomerId as CustomerId, o.PaymentTypeId, o.CompletedDate, op.Id as OrderProductId, p.Id as ProductId, p.Name as ProductName, p.Description ProductDescription, p.Price as ProductPrice, p.Quantity as ProductQuantity
                            FROM `Order` o, OrderProduct op, Product p
                            WHERE o.Id = {orderId}
                            AND o.Id = op.OrderId
                            AND op.ProductId = p.Id
                            AND p.Id = {productId};
                            ",
                            (SqliteDataReader reader) =>
                            {
                                while (reader.Read())
                                {
                                    // assign order details to the order created above
                                    CurrentOrder.Id = Convert.ToInt32(reader["OrderId"]);
                                    CurrentOrder.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                                    CurrentOrder.PaymentTypeId = null;
                                    CurrentOrder.CompletedDate = null;

                                    // assign product details to the product created above
                                    CurProduct.Id = Convert.ToInt32(reader["ProductId"]);
                                    CurProduct.Name = Convert.ToString(reader["ProductName"]);
                                    CurProduct.Price = Convert.ToDouble(reader["ProductPrice"]);
                                    CurProduct.Description = Convert.ToString(reader["ProductDescription"]);
                                    CurProduct.Quantity = Convert.ToInt32(reader["ProductQuantity"]);
                                }
                            });
                return CurProduct;

            } catch  (Exception err) {
                Console.WriteLine("Get Single Product From Order Error", err.Message);
                return null;
            }
        }

    }
}
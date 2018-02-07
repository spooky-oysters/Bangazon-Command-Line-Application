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
        }


        private void CreateOrderTable() {
            try {
                _db.Update(@"CREATE TABLE IF NOT EXISTS `Order` (
                    `Id` INTEGER PRIMARY KEY AUTOINCREMENT,
                    `CustomerId` INTEGER NOT NULL,
                    `PaymentTypeId` INTEGER,
                    `CompletedDate` TEXT,
                    FOREIGN KEY(`CustomerId`) REFERENCES `Customer`(`Id`)
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
                '{order.CustomerId}',
                null,
                null
            );";
        }

        // returns customer's unpaid order from the database
        public Order GetUnpaidOrder(int id) {
            // initialize a new order to hold the return from db
            Order order = new Order();
            // query the database for a matching order
            _db.Query($@"SELECT * 
                        FROM Order o
                        WHERE o.CustomerId = {id}
                        AND o.PaymentType = null;
                        ",
                        (SqliteDataReader reader) =>
                        {
                            while (reader.Read())
                            {
                                order.Id = Convert.ToInt32(reader["Id"]);
                                order.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                                order.PaymentTypeId = Convert.ToInt32(reader["PaymentTypeId"]);
                                order.CompletedDate = Convert.ToDateTime(reader["CompletedDate"]);
                            }
                        });
            
            return order;

            // return _orders.Where(o => o.CustomerId == id && o.PaymentTypeId == null).ToList();
        }

        // store a product on an order, by using a joiner table
        public void AddProductToOrder(int orderId, int productId)
        {
            // create a dictionary for a joiner table to hold the relationship of product and order
            _orderProduct.Add(orderId, productId);
        }

        // function to check if customer's order contains a product.
        public bool GetProduct(int orderId, int productId)
        {
            if (_orderProduct.Count > 0) {

                foreach (KeyValuePair<int, int>product in _orderProduct)
                {
                    if (product.Key == orderId && product.Value == productId) 
                    {
                        return true;
                    } else {
                        return false;
                    }
                }
            } 

            return false;
        }

    }
}
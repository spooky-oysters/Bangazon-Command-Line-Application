using System.Collections.Generic;
using bangazon_cli.Models;
using System.Linq;
using System;
using Microsoft.Data.Sqlite;

namespace bangazon_cli.Managers
{
    /*
        Author: Kimberly Bird
        Responsibility: Manage the database related tasks for Product
     */
    public class ProductManager
    {

        private DatabaseInterface _db;

        public ProductManager(DatabaseInterface db)
        {
            _db = db;
            this.CreateProductTable();
        }

        private void CreateProductTable()
        {
            try
            {
                _db.Update(@"CREATE TABLE IF NOT EXISTS
                `Product` (
                    `Id` INTEGER PRIMARY KEY AUTOINCREMENT,
                    `CustomerId` INTEGER NOT NULL,
                    `Name` TEXT NOT NULL,
                    `Price` DOUBLE NOT NULL,
                    `Description` TEXT NOT NULL,
                    `Quantity` INTEGER NOT NULL,
                    FOREIGN KEY(`CustomerId`) REFERENCES `Customer`(`Id`)
                );
                ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("CreateProductTable", ex.Message);
            }
        }

        /* 
            Adds a product record to the database. 
            Assigns the id to the product object based on the id it is assigned in the database.
            Parameters: 
            - Product object
        */
        public int AddProduct(Product product)
        {
            string SQLInsert = $@"INSERT INTO `Product`
            VALUES (
                null,
                '{product.CustomerId}',
                '{product.Name}',
                '{product.Price}',
                '{product.Description}',
                '{product.Quantity}'
                );";

            int productId = 0;
            try
            {
                productId = _db.Insert(SQLInsert);
                product.Id = productId;
            }
            catch (Exception err)
            {
                Console.WriteLine("Add Product Error", err.Message);
            }
            return productId;
        }

        // returns all products from database
        public List<Product> GetProducts()
        {
            List<Product> _products = new List<Product>();
            // clear existing customers
            _products.Clear();
            // find the record for the product in the db and retrieve data
            _db.Query($@"SELECT * FROM Product;",
            (SqliteDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        // new product object
                        Product product = new Product();
                        product.Id = Convert.ToInt32(reader["Id"]);
                        product.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        product.Name = Convert.ToString(reader["Name"]);
                        product.Price = Convert.ToDouble(reader["Price"]);
                        product.Description = Convert.ToString(reader["Description"]);
                        product.Quantity = Convert.ToInt32(reader["Quantity"]);

                        // add it to collection
                        _products.Add(product);
                    }
                });
            return _products;
        }

        /*
            Updates product name in database. 
            Parameters: 
            - Product Object
            - String name
        */
        public void UpdateName(Product product, string name)
        {

            //update name in SQL
            string SQLUpdate = $@"UPDATE `Product`
            SET `Name` = '{product.Name = name}'
            WHERE `Id` = 1";

            try
            {
                _db.Update(SQLUpdate);
            }
            catch (Exception err)
            {
                Console.WriteLine("Update Product Error", err.Message);
            }
        }

        /*
            Updates product description in database. 
            Parameters: 
            - Product Object
            - String description
        */
        public void UpdateDescription(Product product, string desc)
        {
            // update description in SQL
            string SQLUpdate = $@"UPDATE `Product`
            SET `Description` = '{product.Description = desc}'
            WHERE `Id` = 1";

            try
            {
                _db.Update(SQLUpdate);
            }
            catch (Exception err)
            {
                Console.WriteLine("Update Product Error", err.Message);
            }
        }

        /*
            Updates product price in database. 
            Parameters: 
            - Product Object
            - Double price
        */
        public void UpdatePrice(Product product, double price)
        {
            // update price in SQL
            string SQLUpdate = $@"UPDATE `Product`
            SET `Price` = '{product.Price = price}'
            WHERE `Id` = 1";

            try
            {
                _db.Update(SQLUpdate);
            }
            catch (Exception err)
            {
                Console.WriteLine("Update Product Error", err.Message);
            }
        }

        /*
            Updates product price in database. 
            Parameters: 
            - Product Object
            - Double price
        */
        public void UpdateQuantity(Product product, int quanity)
        {
            // update quantity in SQL
            string SQLUpdate = $@"UPDATE `Product`
            SET `Quantity` = '{product.Quantity = quanity}'
            WHERE `Id` = 1";

            try
            {
                _db.Update(SQLUpdate);
            }
            catch (Exception err)
            {
                Console.WriteLine("Update Product Error", err.Message);
            }
        }

        // gets one product. Parameters: id
        public Product GetSingleProduct(int id)
        {
            return GetProducts().Where(p => p.Id == id).Single();
        }

    }
}
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
                    `DateAdded` TEXT NOT NULL,
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
                '{product.DateAdded}',
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
            // clear existing products
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
                        product.DateAdded = Convert.ToDateTime(reader["DateAdded"]);
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
        // returns all products for one customer
        public List<Product> GetProducts(int id)
        {
            List<Product> _products = new List<Product>();
            // clear existing customers
            _products.Clear();
            // find the record for the product in the db and retrieve data
            _db.Query($@"SELECT * FROM Product WHERE CustomerId = {id};",
            (SqliteDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        // new product object
                        Product product = new Product();
                        product.Id = Convert.ToInt32(reader["Id"]);
                        product.DateAdded = Convert.ToDateTime(reader["DateAdded"]);
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

        // update products
        public void Update(int productId, int customerId, Product updatedProduct)
        {
            string SQLUpdate = $"UPDATE Product SET Name = '{updatedProduct.Name}', Description = '{updatedProduct.Description}', Price = '{updatedProduct.Price}', Quantity = '{updatedProduct.Quantity}' WHERE Id= {productId} AND CustomerId = {customerId}";

            _db.Update(SQLUpdate);
        }

        // gets one product. Parameters: id
        public Product GetSingleProduct(int id)
        {
            try
            {
                return GetProducts().Where(p => p.Id == id).Single();
            }
            catch
            {
                return null;
            }
        }

        /*
            Author: Krys Mathis
            Summary: Checks if the product is on an existing order
            Parameter: Product Id
            Return: true if it is on an order, in which case the 
                    system cannot delete the product
        */
        public bool IsProductOnOrder(int productId)
        {

            int rowCount = 0;
            _db.Query($@"SELECT Count(o.Id) as ordercount 
                        FROM `Order` o, OrderProduct op
                        WHERE o.Id = op.OrderId
                        AND op.ProductId = {productId};",

            (SqliteDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        rowCount = Convert.ToInt32(reader["ordercount"]);
                    }
                });

            return rowCount > 0;
        }

        /*
            Author: Krys Mathis
            Summary: Deletes a product from the products table based on Id
            Parameter: Product Id
         */
        public void DeleteProduct(int productId)
        {
            // update description in SQL
            string SQLUpdate = $@"DELETE FROM `Product`
            WHERE id = {productId};";
            try
            {
                _db.Update(SQLUpdate);
            }
            catch (Exception err)
            {
                Console.WriteLine("There was an error with the delete statement", err.Message);
            }
        }

    }
}
using System.Collections.Generic;
using bangazon_cli.Models;

namespace bangazon_cli.Managers
{
    /*
        Author: Kimberly Bird
        Responsibility: Manage the database related tasks for Product
     */
    public class ProductManager
    {
        private List<Product> _products = new List<Product>();

        /* 
            Adds a product record to the database.
            Parameters: 
            - Product object
        */
        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        // returns all products from database
        public List<Product> GetProducts()
        {
            return _products;
        }

    }
}
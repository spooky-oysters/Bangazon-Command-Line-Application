using System.Collections.Generic;
using bangazon_cli.Models;
using System.Linq;

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

        /*
            Updates product property in database. 
            Parameters: 
            - Product Object
            - String property 
            - Key
        */
        public void Update (Product product, string property, int key)
        {
            System.Collections.Hashtable h = new System.Collections.Hashtable();
            h[product.key] = property;
            // product.Name = name;
        }

        // gets one product. Parameters: id
        public Product GetSingleProduct (int id)
        {
            return _products.Where(p => p.Id == id).Single();
        }

    }
}
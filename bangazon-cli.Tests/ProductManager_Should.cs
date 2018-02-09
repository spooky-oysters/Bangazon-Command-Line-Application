using System;
using bangazon_cli.Models;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace bangazon_cli.Managers.Tests
{
    /*
        Author: Kimberly Bird
        References #4 - Create Product, #8 - Update Product Properties
    */
    public class ProductManager_Should
    {
        private Product _product;
        private DatabaseInterface _db;
        private string _dbPath;
        private ProductManager productManager;

        // instatiate the test
        public ProductManager_Should()
        {
            string testPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB_TEST");
            _db = new DatabaseInterface(testPath);
            productManager = new ProductManager(_db);
        }

        [Fact]
        public void AddProductToCollection()
        {
            // generate product
            Product product = new Product();
            product.CustomerId = 1;
            product.Name = "Kite";
            product.Price = 37.00;
            product.Description = "Kite description";
            product.Quantity = 5;

            // manager new instance
            // ProductManager productManager = new ProductManager(_db);

            // capture existing record count. Test will use this to determine if the add method increased the number of records
            int initialRecordCount = productManager.GetProducts().Count();

            // assign the id to the product object using AddProduct
            product.Id = productManager.AddProduct(product);

            // get the product from the manager
            Product storedProduct = productManager.GetSingleProduct(product.Id);

            // assert there are products in the list
            Assert.True(productManager.GetProducts().Count() > initialRecordCount);

            // assert the product created by test is in the list
            Assert.Equal(product.Id, storedProduct.Id);
        }

        [Fact]
        public void GetProductsForSingleCustomer()
        {
            // generate products
            Product product1 = new Product();
            product1.CustomerId = 2;
            product1.Name = "some name";
            product1.Price = 24.00;
            product1.Description = "product1 description";
            product1.Quantity = 4;
            // generate products
            Product product2 = new Product();
            product2.CustomerId = 2;
            product2.Name = "another name";
            product2.Price = 754.00;
            product2.Description = "product2 description";
            product2.Quantity = 2;
            // generate products
            Product product3 = new Product();
            product3.CustomerId = 2;
            product3.Name = "third name";
            product3.Price = 64.00;
            product3.Description = "product3 description";
            product3.Quantity = 9;

            // manager new instance
            // ProductManager productManager = new ProductManager(_db);

            // capture existing record count. Test will use this to determine if the add method increased the number of records
            int initialRecordCount = productManager.GetProducts().Count();

            // assign the id to the product object using AddProduct
            product1.Id = productManager.AddProduct(product1);
            product2.Id = productManager.AddProduct(product2);
            product3.Id = productManager.AddProduct(product3);

            List<Product> listOfProdForSingleCustomer = productManager.GetProducts(2);

            // assert there are products in the list
            Assert.True(productManager.GetProducts().Count() > initialRecordCount);

            // assert the customer id matches each product.
            Assert.Equal(listOfProdForSingleCustomer.Count(), 3);
        }



        [Fact]
        public void AddsProductIdToAddedRecords()
        {
            Product product = new Product();
            product.Id = 0;
            product.CustomerId = 1;
            product.Name = "Shirt";
            product.Price = 14.00;
            product.Description = "shirt description";
            product.Quantity = 48;

            productManager.AddProduct(product);

            // the product Id should be greater than one
            Assert.True(product.Id > 0);
        }

        [Fact]
        public void GetSingleProd()
        {
            // add new product with product name
            _product = new Models.Product();
            _product.Name = "Kite";
            _product.CustomerId = 1;
            _product.Price = 45.00;
            _product.Description = "THIS IS UPDATED";
            _product.Quantity = 3;

            // manager instance
            // ProductManager productManager = new ProductManager(_db);
            int id = productManager.AddProduct(_product);

            // get product from manager
            Product storedProduct = productManager.GetSingleProduct(id);

            // products should match
            Assert.Equal(id, storedProduct.Id);
        }


        [Fact]
        public void UpdateProductName()
        {
            // add new product with product name
            _product = new Models.Product();
            _product.Name = "THIS IS UPDATED";
            _product.CustomerId = 1;
            _product.Price = 45.00;
            _product.Quantity = 3;
            _product.Description = "Kite description";

            // new product manager instance 
            // ProductManager productManager = new ProductManager(_db);
            int id = productManager.AddProduct(_product);

            // select one product to update based on id
            Product prodToUpdate = productManager.GetSingleProduct(id);

            // updates product name 
            productManager.UpdateName(prodToUpdate, "New Kite");

            // tests that new product name is updated
            Assert.Equal(prodToUpdate.Name, "New Kite");
        }

        [Fact]
        public void UpdateProductDesc()
        {
            // add new product with product description
            _product = new Models.Product();
            _product.Description = "THIS IS UPDATED";
            _product.Name = "Kite";
            _product.CustomerId = 1;
            _product.Price = 45.00;
            _product.Quantity = 3;

            // new product manager instance 
            // ProductManager productManager = new ProductManager(_db);
            int id = productManager.AddProduct(_product);

            // select one product to update based on id
            Product prodToUpdate = productManager.GetSingleProduct(id);

            // updates product description 
            productManager.UpdateDescription(prodToUpdate, "New Kite description");

            // tests that new product description is updated
            Assert.Equal(prodToUpdate.Description, "New Kite description");
        }

        [Fact]
        public void UpdateProductPrice()
        {
            // add new product with product price
            _product = new Models.Product();
            _product.Price = 75.00;
            _product.Description = "Kite Description";
            _product.Name = "Kite";
            _product.CustomerId = 1;
            _product.Quantity = 3;

            // new product manager instance 
            // ProductManager productManager = new ProductManager(_db);
            int id = productManager.AddProduct(_product);

            // select one product to update based on id
            Product prodToUpdate = productManager.GetSingleProduct(id);

            // updates product price 
            productManager.UpdatePrice(prodToUpdate, 50.97);

            // tests that new product price is updated
            Assert.Equal(prodToUpdate.Price, 50.97);
        }

        [Fact]
        public void UpdateProductQuantity()
        {
            // add new product with product quantity
            _product = new Models.Product();
            _product.Quantity = 70;
            _product.Description = "Kite Description";
            _product.Name = "Kite";
            _product.CustomerId = 1;
            _product.Price = 45.00;

            // new product manager instance 
            // ProductManager productManager = new ProductManager(_db);
            int id = productManager.AddProduct(_product);

            // select one product to update based on id
            Product prodToUpdate = productManager.GetSingleProduct(id);

            // updates product quantity 
            productManager.UpdateQuantity(prodToUpdate, 10);

            // tests that new product quantity is updated
            Assert.Equal(prodToUpdate.Quantity, 10);
        }

        [Fact]
        public void Dispose()
        {
            _db.DeleteTables();
        }

    }
}
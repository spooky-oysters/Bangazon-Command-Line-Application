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

        // instatiate the test
        public ProductManager_Should()
        {
            string testPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB_TEST");
            _db = new DatabaseInterface(testPath);
        }

        [Fact]
        public void AddProductToCollection()
        {
            // generate product
            Product product = new Product();
            product.CustomerId = 1;
            product.Name = "AddProductToCollection";
            product.Price = 34.00;
            product.Description = "Product description";
            product.Quantity = 45;

            // manager new instance
            ProductManager productManager = new ProductManager(_db);

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
        public void AddsProductIdToAddedRecords()
        {
            Product product = new Product();
            product.Id = 0;
            product.CustomerId = 1;
            product.Name = "AddProductIdToAddedRecords";
            product.Price = 34.00;
            product.Description = "Product description";
            product.Quantity = 45;

            ProductManager productManager = new ProductManager(_db);

            productManager.AddProduct(product);

            // the product Id should be greater than one
            Assert.True(product.Id > 0);
        }

        [Fact]
        public void GetSingleProd()
        {

            Product product = new Product();
            product.Id = 0;
            product.CustomerId = 1;
            product.Name = "Name of prod";
            product.Price = 2.00;
            product.Description = "Product description";
            product.Quantity = 34;

            // manager instance
            ProductManager productManager = new ProductManager(_db);

            // get product from manager
            Product storedProduct = productManager.GetSingleProduct(product.Id);

            // products should match
            Assert.Equal(product.Id, storedProduct.Id);
        }


        [Fact]
        public void UpdateProductName()
        {
            // add new product with product name
            _product = new Models.Product();
            _product.Id = 1;
            _product.Name = "Kite";

            // new product manager instance 
            ProductManager productManager = new ProductManager(_db);
            productManager.AddProduct(_product);

            // select one product to update based on id
            Product prodToUpdate = productManager.GetSingleProduct(1);

            // updates product name 
            productManager.UpdateName(prodToUpdate, "New Kite");

            // tests that new product name is updated
            Assert.Equal(prodToUpdate.Name, "New Kite");
        }

        /* 
                [Fact]
                public void UpdateProductDesc()
                {
                    // add new product with product description
                    _product = new Models.Product();
                    _product.Id = 1;
                    _product.Description = "Kite description";

                    // new product manager instance 
                    ProductManager productManager = new ProductManager();
                    productManager.AddProduct(_product);

                    // select one product to update based on id
                    Product prodToUpdate = productManager.GetSingleProduct(1);

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
                    _product.Id = 1;
                    _product.Price = 45.00;

                    // new product manager instance 
                    ProductManager productManager = new ProductManager();
                    productManager.AddProduct(_product);

                    // select one product to update based on id
                    Product prodToUpdate = productManager.GetSingleProduct(1);

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
                    _product.Id = 1;
                    _product.Quantity = 4;

                    // new product manager instance 
                    ProductManager productManager = new ProductManager();
                    productManager.AddProduct(_product);

                    // select one product to update based on id
                    Product prodToUpdate = productManager.GetSingleProduct(1);

                    // updates product quantity 
                    productManager.UpdateQuantity(prodToUpdate, 5);

                    // tests that new product quantity is updated
                    Assert.Equal(prodToUpdate.Quantity, 5);
                }
                */
    }
}
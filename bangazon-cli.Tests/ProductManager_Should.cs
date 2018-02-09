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
        private readonly CustomerManager _customerManager;
        private readonly ProductManager _productManager;
        private readonly OrderManager _orderManager;
        private readonly PaymentTypeManager _paymentTypeManager;
        private Customer _customer;

        // instatiate the test
        public ProductManager_Should()
        {
            string testPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB_TEST");
            _db = new DatabaseInterface(testPath);
            // initialize managers to create db tables and use later in tests
            _customerManager = new CustomerManager(_db);
            _productManager = new ProductManager(_db);
            _orderManager = new OrderManager(_db);
            _paymentTypeManager = new PaymentTypeManager(_db);

            // create customer
            _customer = new Customer();
            _customer.Id = 1;
            _customer.Name = "K Bird";
            _customer.StreetAddress = "123 Somewhere";
            _customer.City = "Nashville";
            _customer.State = "TN";
            _customer.PostalCode = "323232";
            _customer.PhoneNumber = "9876543";
        }

        [Fact]
        public void AddProductToCollection()
        {
            // add customer to DB and get id
            int CustomerId = _customerManager.AddCustomer(_customer);
            // generate product
            Product product = new Product();
            product.CustomerId = CustomerId;
            product.Name = "Kite";
            product.Price = 37.00;
            product.Description = "Kite description";
            product.Quantity = 5;

            // capture existing record count. Test will use this to determine if the add method increased the number of records
            int initialRecordCount = _productManager.GetProducts().Count();

            // assign the id to the product object using AddProduct
            product.Id = _productManager.AddProduct(product);

            // get the product from the manager
            Product storedProduct = _productManager.GetSingleProduct(product.Id);

            // assert there are products in the list
            Assert.True(_productManager.GetProducts().Count() > initialRecordCount);

            // assert the product created by test is in the list
            Assert.Equal(product.Id, storedProduct.Id);
        }

        [Fact]
        public void GetProductsForSingleCustomer()
        {
            // add customer to DB and get id
            int CustomerId = _customerManager.AddCustomer(_customer);
            // generate products
            Product product1 = new Product();
            product1.CustomerId = CustomerId;
            product1.Name = "some name";
            product1.Price = 24.00;
            product1.Description = "product1 description";
            product1.Quantity = 4;
            // generate products
            Product product2 = new Product();
            product2.CustomerId = CustomerId;
            product2.Name = "another name";
            product2.Price = 754.00;
            product2.Description = "product2 description";
            product2.Quantity = 2;
            // generate products
            Product product3 = new Product();
            product3.CustomerId = CustomerId;
            product3.Name = "third name";
            product3.Price = 64.00;
            product3.Description = "product3 description";
            product3.Quantity = 9;

            // capture existing record count. Test will use this to determine if the add method increased the number of records
            int initialRecordCount = _productManager.GetProducts().Count();

            // assign the id to the product object using AddProduct
            product1.Id = _productManager.AddProduct(product1);
            product2.Id = _productManager.AddProduct(product2);
            product3.Id = _productManager.AddProduct(product3);

            List<Product> listOfProdForSingleCustomer = _productManager.GetProducts(CustomerId);

            // assert there are products in the list
            Assert.True(_productManager.GetProducts().Count() > initialRecordCount);

            // assert the customer id matches each product.
            Assert.Equal(listOfProdForSingleCustomer.Count(), 3);
        }



        [Fact]
        public void AddsProductIdToAddedRecords()
        {
            // add customer to DB and get id
            int CustomerId = _customerManager.AddCustomer(_customer);

            Product product = new Product();
            product.Id = 0;
            product.CustomerId = CustomerId;
            product.Name = "Shirt";
            product.Price = 14.00;
            product.Description = "shirt description";
            product.Quantity = 48;

            _productManager.AddProduct(product);

            // the product Id should be greater than one
            Assert.True(product.Id > 0);
        }

        [Fact]
        public void GetSingleProd()
        {
            // add customer to DB and get id
            int CustomerId = _customerManager.AddCustomer(_customer);

            // add new product with product name
            _product = new Models.Product();
            _product.Name = "Kite";
            _product.CustomerId = CustomerId;
            _product.Price = 45.00;
            _product.Description = "THIS IS UPDATED";
            _product.Quantity = 3;

            // add product to database and get id returned
            int id = _productManager.AddProduct(_product);

            // get product from manager
            Product storedProduct = _productManager.GetSingleProduct(id);

            // products should match
            Assert.Equal(id, storedProduct.Id);
        }


        [Fact]
        public void UpdateProductName()
        {
            // add customer to DB and get id
            int CustomerId = _customerManager.AddCustomer(_customer);

            // add new product with product name
            _product = new Models.Product();
            _product.Name = "THIS IS UPDATED";
            _product.CustomerId = CustomerId;
            _product.Price = 45.00;
            _product.Quantity = 3;
            _product.Description = "Kite description";

            // add product to database and get id returned
            int id = _productManager.AddProduct(_product);

            // select one product to update based on id
            Product prodToUpdate = _productManager.GetSingleProduct(id);

            // updates product name 
            _productManager.UpdateName(prodToUpdate, "New Kite");

            Product updatedProduct = _productManager.GetSingleProduct(id);
            // tests that new product name is updated
            Assert.Equal(updatedProduct.Name, "New Kite");
        }

        [Fact]
        public void UpdateProductDesc()
        {
            // add customer to DB and get id
            int CustomerId = _customerManager.AddCustomer(_customer);

            // add new product with product description
            _product = new Models.Product();
            _product.Description = "THIS IS UPDATED";
            _product.Name = "Kite";
            _product.CustomerId = CustomerId;
            _product.Price = 45.00;
            _product.Quantity = 3;

        
            // add product to database and get id returned
            int id = _productManager.AddProduct(_product);

            // select one product to update based on id
            Product prodToUpdate = _productManager.GetSingleProduct(id);

            // updates product description 
            _productManager.UpdateDescription(prodToUpdate, "New Kite description");

            // tests that new product description is updated
            Assert.Equal(prodToUpdate.Description, "New Kite description");
        }

        [Fact]
        public void UpdateProductPrice()
        {
            // add customer to DB and get id
            int CustomerId = _customerManager.AddCustomer(_customer);

            // add new product with product price
            _product = new Models.Product();
            _product.Price = 75.00;
            _product.Description = "Kite Description";
            _product.Name = "Kite";
            _product.CustomerId = CustomerId;
            _product.Quantity = 3;

            // add product to database and get id returned
            int id = _productManager.AddProduct(_product);

            // select one product to update based on id
            Product prodToUpdate = _productManager.GetSingleProduct(id);

            // updates product price 
            _productManager.UpdatePrice(prodToUpdate, 50.97);

            // tests that new product price is updated
            Assert.Equal(prodToUpdate.Price, 50.97);
        }

        [Fact]
        public void UpdateProductQuantity()
        {
            // add customer to DB and get id
            int CustomerId = _customerManager.AddCustomer(_customer);

            // add new product with product quantity
            _product = new Models.Product();
            _product.Quantity = 70;
            _product.Description = "Kite Description";
            _product.Name = "Kite";
            _product.CustomerId = CustomerId;
            _product.Price = 45.00;

            // add product to database and get id returned
            int id = _productManager.AddProduct(_product);

            // select one product to update based on id
            Product prodToUpdate = _productManager.GetSingleProduct(id);

            // updates product quantity 
            _productManager.UpdateQuantity(prodToUpdate, 10);

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
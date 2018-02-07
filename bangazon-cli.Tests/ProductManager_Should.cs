using System;
using bangazon_cli.Models;
using Xunit;

namespace bangazon_cli.Managers.Tests
{
    /*
        Author: Kimberly Bird
        References #4 - Create Product
    */
    public class ProductManager_Should
    {
        private Product _product;

        [Fact]
        public void AddProductToCollection()
        {
            _product = new Models.Product();
            _product.Id = 1;

            ProductManager productManager = new ProductManager();
            productManager.AddProduct(_product);

            Assert.Contains(_product, productManager.GetProducts());

        }

        [Fact]
        public void UpdateProductName()
        {
            // add new product with product name
            _product = new Models.Product();
            _product.Id = 1;
            _product.Name = "Kite";

            // new product manager instance 
            ProductManager productManager = new ProductManager();
            productManager.AddProduct(_product);

            // list all products
            var _listProducts = productManager.GetProducts();

            // select one product to update based on id
            Product prodToUpdate = productManager.GetSingleProduct(1);

            // updates product name 
            productManager.UpdateName(prodToUpdate, "New Kite");

            // tests that new product name is updated
            Assert.Equal(prodToUpdate.Name, "New Kite");
        }

        [Fact]
        public void UpdateProductDesc()
        {
            // add new product with product name
            _product = new Models.Product();
            _product.Id = 1;
            _product.Description = "Kite description";

            // new product manager instance 
            ProductManager productManager = new ProductManager();
            productManager.AddProduct(_product);

            // list all products
            var _listProducts = productManager.GetProducts();

            // select one product to update based on id
            Product prodToUpdate = productManager.GetSingleProduct(1);

            // updates product name 
            productManager.UpdateDescription(prodToUpdate, "New Kite description");

            // tests that new product name is updated
            Assert.Equal(prodToUpdate.Description, "New Kite description");
        }

    }
}
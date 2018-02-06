using System;
using Xunit;

namespace bangazon_cli.Managers.Tests
{
    /*
        Author: Kimberly Bird
        References #4 - Create Product
    */
    public class ProductManager_Should
    {
        [Fact]
        public void AddProductToCollection()
        {
        Models.Product product = new Models.Product();
        product.Id = 1;

        ProductManager productManager = new ProductManager();
        productManager.AddProduct(product);

        Assert.Contains(product, productManager.GetProducts());

        }

    }
}
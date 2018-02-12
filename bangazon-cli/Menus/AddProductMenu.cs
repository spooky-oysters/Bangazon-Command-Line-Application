using System;
using bangazon_cli.Models;
using bangazon_cli.Managers;

namespace bangazon_cli.Menus
{
    /*
        Author: Kimberly Bird
        Summary - controls the add product menu
        Parameters:
            - Customer object (Active Customer Id)
            - Product object (Product Name, Price, Description, Quantity)
            - Product manager
    */

    public class AddProductMenu
    {
        private Customer _customer;
        private Product _product;
        private ProductManager _productManager;

        public AddProductMenu(Customer customer, Product product, ProductManager productManager)
        {
            _customer = customer;
            _product = product;
            _productManager = productManager;
        }

        // Displays the add product menu to the user
        public void Show()
        {
            Console.Clear();
            // user enter product name
            do
            {
                Console.WriteLine("Enter Product Name");
                Console.Write("> ");
                _product.Name = Console.ReadLine();
                // product name must contain a value
            } while (_product.Name.Length == 0);

            // user enter product price
            do
            {
                Console.WriteLine("Enter Product Price. Ex: 45.97");
                Console.Write("> ");
                _product.Price = Convert.ToDouble(Console.ReadLine());
                // price must contain a value
            } while (_product.Price == 0);

            // user enter product description
            do
            {
                Console.WriteLine("Enter Product Description");
                Console.Write("> ");
                _product.Description = Console.ReadLine();
                // product description must contain a value
            } while (_product.Description.Length == 0);

            // user enter product quantity
            do
            {
                Console.WriteLine("Enter Product Quantity. Ex: 15");
                Console.Write("> ");
                _product.Quantity = Convert.ToInt32(Console.ReadLine());
                // product quantity must contain a value
            } while (_product.Quantity == 0);

            // add active customer id to new product
            _product.CustomerId = _customer.Id;

            // add the product to the database
            int id = _productManager.AddProduct(_product);

            if (id > 0) {
                Console.WriteLine("*** SUCCESS ***");
                Console.WriteLine("*** PRESS ENTER TO CONTINUE ***");
                Console.ReadLine();
            }
        }
    }
}
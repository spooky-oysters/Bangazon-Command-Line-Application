using System;
using bangazon_cli.Models;
using bangazon_cli.Managers;

namespace bangazon_cli.Menus
{
    /*
        Author: Kimberly Bird
        Summary - controls updating product fields
        Parameters:
            - Customer Object 
            - Product Manager
    */

    public class UpdateProductMenu
    {
        private Customer _customer;
        private ProductManager _productManager;

        public UpdateProductMenu(Customer customer, ProductManager productManager)
        {
            _customer = customer;
            _productManager = productManager;
        }
    }
}
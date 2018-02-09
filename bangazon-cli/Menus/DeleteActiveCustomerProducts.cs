using System;
using System.Linq;
using bangazon_cli.Models;
using bangazon_cli.Managers;
using System.Collections.Generic;

namespace bangazon_cli.Menus
{
    public class DeleteActiveCustomerProducts
    {
        private Customer _activeCustomer;
        private ProductManager _productManager;

        public DeleteActiveCustomerProducts(Customer activeCustomer, ProductManager productManager)
        {
            _activeCustomer = activeCustomer;
            _productManager = productManager;
        }

        public void Show() {
            
            Console.Clear();
            Console.WriteLine("Choose product to delete:");

            // get the active customer's products
            // TODO: Use productManager's getCustomersProducts method 
            List<Product> products = _productManager.GetProducts()
                            .Where(p => p.CustomerId == _activeCustomer.Id)
                            .OrderBy(x => x.Name).ToList();

            // display the options to select the product and get the selected product
            Product product = GetProductFromList(products);

            // error check if this customer has no products
            if (product.Id == 0) {
                return;
            }
            
            if (_productManager.IsProductOnUnpaidOrder(product.Id)==true) {
                Console.WriteLine("This product is on an unpaid order - cannot delete");
                Console.ReadLine();
                return;
            }
            // only delete if it is not on an active order
            // check if it is on an active order

            // branch for delete
            Console.WriteLine(product.Id + " press enter to continue...");
            Console.ReadLine();
        }

        public Product GetProductFromList(List<Product> products) {
            
            int choice = 0;
            Product selectedProduct = new Product();

            // make sure there are products to display
            if (products.Count() == 0 ) {
                Console.WriteLine("No Products - press any key to continue");
                Console.ReadLine();
                return selectedProduct;
            }

            do {
                
                int index = 1;
                products.ForEach(c => {
                    Console.WriteLine($"{index}. {c.Name}");
                    index ++;
                });

                Console.Write("> ");
                string userSelection = Console.ReadLine();
                
                if (int.TryParse(userSelection, out choice)) {
                    selectedProduct = products.ElementAt(choice -1);
                }

            } while (choice == 0 || choice > products.Count());

            return selectedProduct;
            
        }
    }
}
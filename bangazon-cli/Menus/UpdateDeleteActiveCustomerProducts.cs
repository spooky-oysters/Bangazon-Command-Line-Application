using System;
using System.Linq;
using bangazon_cli.Models;
using bangazon_cli.Managers;
using System.Collections.Generic;

namespace bangazon_cli.Menus
{
    public class UpdateDeleteActiveCustomerProducts
    {
        private Customer _activeCustomer;
        private ProductManager _productManager;

        public UpdateDeleteActiveCustomerProducts(Customer activeCustomer, ProductManager productManager)
        {
            _activeCustomer = activeCustomer;
            _productManager = productManager;
        }

        public void Show() {
            
            Console.Clear();

            
            // list the active customers products
            List<Product> products = _productManager.GetProducts()
                            .Where(p => p.CustomerId == _activeCustomer.Id)
                            .OrderBy(x => x.Name).ToList();

            // options to select the product
            Product product = GetProductFromList(products);

            // error check if this customer has no products
            if (product.Id == 0) {
                return;
            }
            
            // option to update or delete


            // branch for update


            // branch for delete
            Console.ReadLine();
        }

        public Product GetProductFromList(List<Product> products) {
            
            int id = 0;
            int choice = 1;
            Product selectedProduct = new Product();

            // make sure there are products to display
            if (products.Count() == 0 ) {
                Console.WriteLine("No Products - press any key to continue");
                Console.ReadLine();
                return selectedProduct;
            }

            do {
                Console.Clear();
                Console.WriteLine("Which Product?");
                int index = 1;
                products.ForEach(c => {
                    Console.WriteLine($"{index}. {c.Name}");
                    index ++;
                });
                Console.WriteLine("> ");
                string userSelection = Console.ReadLine();
                
                if (int.TryParse(userSelection, out choice)) {
                    selectedProduct = products.ElementAt(choice -1);
                }

            } while (id == 0 || choice > products.Count());

            return selectedProduct;
            
        }
    }
}
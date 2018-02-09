using System;
using bangazon_cli.Models;
using bangazon_cli.Managers;
using System.Collections.Generic;
using System.Linq;

namespace bangazon_cli.Menus
{
    /*
        Author: Kimberly Bird
        Summary - controls updating product fields
        Parameters:
            - Customer Object 
            - Product Manager
        TODO: need to add choice to finish updating products - currently getting an error.
    */

    public class UpdateProductMenu
    {
        private Customer _activeCustomer;
        private ProductManager _productManager;
        private Product _product;

        public UpdateProductMenu(Customer customer, ProductManager productManager)
        {
            _activeCustomer = customer;
            _productManager = productManager;
        }

        public void Show()
        {
            // get all customer's products
            List<Product> customerProducts = _productManager.GetProducts(_activeCustomer.Id);

            // if there are products in the list, give option to update
            if (customerProducts.Count > 0)
            {
                Dictionary<int, int> productId = new Dictionary<int, int>();
                int i = 1;
                Console.WriteLine("Which product should be updated?");
                // loop through list of products and write them on the console
                foreach (Product p in customerProducts)
                {
                    Console.WriteLine($"{i}. {p.Name}");
                    productId.Add(i, p.Id);

                    // make sure the numbers stay in order
                    i += 1;
                }

                Console.Write("> ");
                int userChoice = Int32.Parse(Console.ReadLine());
                if (userChoice <= i && userChoice != 0)
                {
                    // store user's choice in a variable
                    int prodId = productId[userChoice];
                    // get correct product from the database
                    Product ProductToUpdate = _productManager.GetSingleProduct(prodId);
                    displayUpdate(ProductToUpdate, prodId);
                }
                else
                {
                    Show();
                }

                // method to allow user to update one or more product properties
                void displayUpdate(Product product, int id)
                {
                    Console.Clear();

                    Console.WriteLine($"What field of {product.Name} should be updated?");
                    Console.WriteLine("1. Name");
                    Console.WriteLine("2. Price");
                    Console.WriteLine("3. Description");
                    Console.WriteLine("4. Quantity");
                    Console.WriteLine($"5. Finished updating {product.Name}");
                    Console.Write("> ");

                    int choice = Int32.Parse(Console.ReadLine());

                    // if the user has selected a valid choice
                    if (choice <= 5 && choice != 0)
                    {
                        switch (choice)
                        {

                            case 1:

                                Console.WriteLine("Enter new product name");
                                Console.Write("> ");
                                product.Name = Console.ReadLine();
                                displayUpdate(product, id);
                                _productManager.UpdateName(_product, product.Name);
                                break;

                            case 2:

                                Console.WriteLine("Enter new product price");
                                Console.Write("> ");
                                product.Price = Convert.ToDouble(Console.ReadLine());
                                displayUpdate(product, id);
                                _productManager.UpdatePrice(_product, product.Price);
                                break;

                            case 3:

                                Console.WriteLine("Enter new product description");
                                Console.Write("> ");
                                product.Description = Console.ReadLine();
                                displayUpdate(product, id);
                                _productManager.UpdateDescription(_product, product.Description);
                                break;

                            case 4:

                                Console.WriteLine("Enter new product quantity");
                                Console.Write("> ");
                                product.Quantity = Convert.ToInt32(Console.ReadLine());
                                displayUpdate(product, id);
                                _productManager.UpdateQuantity(_product, product.Quantity);
                                break;
                        }
                        // if user selects invalid choice, return to product menu
                    }
                    else
                    {
                        displayUpdate(product, id);
                    }
                }

            }
            // if active customer has no products, alert and return to product menu
            else
            {
                Console.WriteLine("No products found for current customer.");
                // UpdateProductMenu.Show();
            }
        }


    }
}
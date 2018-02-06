using System;
using Xunit;
using bangazon_cli.Models;

/*
    Author: Greg Lawrence
    Purpose: To test methods of the Order Manager such as creating order, adding product to order
*/

namespace bangazon_cli.Managers.Tests
{


    public class OrderManagerShould
    {
        // private instance of a new customer
        private Customer _customer;
        private readonly OrderManager _manager;
        private Order _testOrder;
        private Product _testProduct;

        public OrderManagerShould()
        {
            // create a new customer instance
            _customer = new Customer();
            // properties added to new customer
            _customer.Id = 1;
            _customer.Name = "G Lawrence";
            _customer.StreetAddress = "123 Somewhere";
            _customer.City = "Nashville";
            _customer.State = "TN";
            _customer.PostalCode = "37206";
            _customer.PhoneNumber = "8018959001";
            
            // create a new orderManager instance
            _manager = new OrderManager();
            // create a new order instance
            _testOrder = new Order(1);
            // create a new product instance
            _testProduct = new Product()
            {
                Id = 1,
                CustomerId = 1,
                Name = "Bicycle"
            };
        }

        [Fact]
        public void AddOrder()
        {
            _manager.AddOrder(_testOrder);

            Assert.Contains(_testOrder, _manager.GetUnpaidOrder(1));
        }

        [Fact]
        public void ListOrders()
        {
            // add the newly created order to the order list
            _manager.AddOrder(_testOrder);

            Assert.Contains(_testOrder, _manager.GetUnpaidOrder(1));
        }

        public void AddProduct()
        {
            // add product to order
            _testOrder.OrderProducts.Add(_testProduct);


            // Assert.Contains(_testOrder.OrderProducts, _manager.GetUnpaidOrder(1));
        }

    }
}
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
        // create a private instance of a new customer
        private readonly OrderManager _manager;
        // private instance of a new customer
        private Customer _customer;

        public OrderManagerShould()
        {
            // create a new orderManager instance
            _manager = new OrderManager();

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
        }

        [Fact]
        public void CreateNewOrder()
        {
           Order newOrder = new Order(_customer.Id){
                Id = 1, 
                PaymentTypeId = null,
                CompletedDate = null
           };
            
            _manager.AddOrder(newOrder);
        }

        [Fact]
        public void ListOrders()
        {
            // create a new order
            Order newOrder = new Order(_customer.Id){
               Id = 1, 
               PaymentTypeId = null,
               CompletedDate = null
            };

            // add the newly created order to the order list
            _manager.AddOrder(newOrder);

            Assert.Contains(newOrder, _manager.GetUnpaidOrder(1));
        }

    }
}
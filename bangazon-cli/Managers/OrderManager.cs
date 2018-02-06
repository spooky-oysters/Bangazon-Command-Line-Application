using System.Collections.Generic;
using System.Linq;
using bangazon_cli.Models;

namespace bangazon_cli.Managers
{
    /*
        Author: Greg Lawrnece
        Responsibility: Manage the database related tasks for Orders
     */
    public class OrderManager
    {
        // collection to store orders until our database is setup
        private List<Order> _orders = new List<Order>();
        
        // dictionary to act as joiner table for Product and Order
        private Dictionary<int, int> _OrderProduct = new Dictionary<int, int>();
        
        
        /*
            Adds a Order record to the database
            Parameters: 
                - Order object
        */        
        public void AddOrder(Order order) {
            _orders.Add(order);
        }

        // returns customer's unpaid order from the database
        public List<Order> GetUnpaidOrder(int id) {
            return _orders.Where(o => o.CustomerId == id && o.PaymentTypeId == null).ToList();
        }

        // store a product on an order, by using a joiner table
        public void AddProductToOrder(int productId, int orderId)
        {
            // create a dictionary for a joiner table to hold the relationship of product and order
            _OrderProduct.Add(productId, orderId);
        }
    }
}
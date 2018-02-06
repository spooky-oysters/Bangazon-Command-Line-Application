using System.Collections.Generic;
using bangazon_cli.Models;

namespace bangazon_cli.Managers
{
    /*
        Author: Greg Lawrnece
        Responsibility: Manage the database related tasks for Orders
     */
    public class OrderManager
    {
        private List<Order> _orders = new List<Order>();

        /*
            Adds a customer record to the database
            Parameters: 
                - Customer object
        */        
        public void AddOrder(Order order) {
            _orders.Add(order);
        }

        // returns customer's unpaid order from the database
        public List<Order> GetUnpaidOrder() {
            return _orders;
            // create foreach loop to look for the customer's unpaid order and return it. 
        }
        
    }
}
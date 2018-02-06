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

        /*
            Adds a customer record to the database
            Parameters: 
                - Customer object
        */        
        public void AddOrder(Order order) {
            _orders.Add(order);
        }

        // returns customer's unpaid order from the database
        public List<Order> GetUnpaidOrder(int id) {
            // return _orders;
            // later: create foreach loop to look for the customer's unpaid order and return it.
            return _orders.Where(o => o.CustomerId == id && o.PaymentTypeId == null).ToList();
        }
        
    }
}
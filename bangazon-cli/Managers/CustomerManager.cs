using System;
using System.Collections.Generic;
using System.Linq;
using bangazon_cli.Models;

namespace bangazon_cli.Managers
{
    /*
        Author: Krys Mathis
        Responsibility: Manage the database related tasks for Customers
     */
    public class CustomerManager
    {
        private DatabaseInterface _db;

        public CustomerManager(DatabaseInterface db)
        {
            _db = db;
        }

        private List<Customer> _customers = new List<Customer>();

        /*
            Adds a customer record to the database
            Parameters: 
                - Customer object
        */        
        public void AddCustomer(Customer customer) {
            
            _customers.Add(customer);
            // create customer table if it does not exist
            try {
                _db.Update(@"CREATE TABLE IF NOT EXISTS `Customers` (
                    `Id` INTEGER PRIMARY KEY AUTOINCREMENT,
                    `Name` TEXT NOT NULL,
                    `City` TEXT NOT NULL,
                    `State` TEXT NOT NULL,
                    `PostalCode` TEXT NOT NULL,
                    `PhoneNumber` TEXT NOT NULL);
                ");
            } catch (Exception err) {
                Console.WriteLine("Table exists");
            }

            // append the record and get the integer
            int customerId = 0;
            string SQLInsert = $@"INSERT INTO `Customers`
            VALUES (
                null, 
                '{customer.Name}', 
                '{customer.City}',
                '{customer.State}',
                '{customer.PostalCode}',
                '{customer.PhoneNumber}'
            );";

            
            customerId = _db.Insert(SQLInsert);

            customer.Id = customerId;

        }

        // returns all customers from the database
        public List<Customer> GetCustomers() {
            return _customers;
        }
        
    }
}
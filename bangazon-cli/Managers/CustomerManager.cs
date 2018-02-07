using System;
using System.Collections.Generic;
using System.Linq;
using bangazon_cli.Models;
using Microsoft.Data.Sqlite;

namespace bangazon_cli.Managers
{
    /*
        Author: Krys Mathis
        Responsibility: Manage the database related tasks for Customers
        On init, create the customer table if it does not exist
     */
    public class CustomerManager
    {
        private List<Customer> _customers = new List<Customer>();
        private DatabaseInterface _db;
        private bool _tableExists;

        public CustomerManager(DatabaseInterface db)
        {
            _db = db;
            this.CreateCustomerTable();
        }

        private void CreateCustomerTable() {
            try {
                _db.Update(@"CREATE TABLE IF NOT EXISTS `Customer` (
                    `Id` INTEGER PRIMARY KEY AUTOINCREMENT,
                    `Name` TEXT NOT NULL,
                    `City` TEXT NOT NULL,
                    `State` TEXT NOT NULL,
                    `PostalCode` TEXT NOT NULL,
                    `PhoneNumber` TEXT NOT NULL);
                ");
            } catch (Exception ex) {
                Console.WriteLine("CreateCustomerTable", ex.Message);
            }
        }

        // public void Add(Customer customer)
        // {
        //     throw new NotImplementedException();
        // }

        /*
            Adds a customer record to the database
            This assigns the id to the customer object based on 
            the id it is assigned in the database
            Parameters: 
                - Customer object
            
        */        
        public int AddCustomer(Customer customer) {

            string SQLInsert = $@"INSERT INTO `Customer`
            VALUES (
                null, 
                '{customer.Name}', 
                '{customer.City}',
                '{customer.State}',
                '{customer.PostalCode}',
                '{customer.PhoneNumber}'
            );";

            int customerId = 0;
            try {
                customerId = _db.Insert(SQLInsert);
                customer.Id = customerId;
            } catch (Exception err) {
                Console.WriteLine("Add Customer Error", err.Message);
            }
            return customerId;
        }

        // returns all customers from the database
        public List<Customer> GetCustomers() {
            
            // clear the existing customers
            _customers.Clear();
             // find the record for the cohort in the db and retrieve data
                _db.Query($@"SELECT * FROM Customer;", 
                    (SqliteDataReader reader) =>
                        {
                            while (reader.Read ())
                            {
                                // new customer object
                                Customer customer = new Customer();
                                customer.Id = Convert.ToInt32(reader["Id"]);
                                customer.Name = Convert.ToString(reader["Name"]);
                                customer.City = Convert.ToString(reader["City"]);
                                customer.State = Convert.ToString(reader["State"]);
                                customer.PostalCode = Convert.ToString(reader["PostalCode"]);
                                customer.PhoneNumber = Convert.ToString(reader["PhoneNumber"]);
                                
                                //add it to the collection
                                _customers.Add(customer);
                            }
                        });

            return _customers;
        }

        /*
            Summary: Retrieves a single customer object from collection
            Parameters: Customer Id property
            Returns: A single customer object
         */
        public Customer GetSingleCustomer(int id) {
            return this.GetCustomers().Where(c => c.Id == id).Single();
        }
        
    }
}
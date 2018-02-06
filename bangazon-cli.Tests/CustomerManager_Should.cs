using System;
using Xunit;
using bangazon_cli.Models;
/*
    Author: Krys Mathis
    References: Ticket #1 - Create customer account
    
 */
namespace bangazon_cli.Managers.Tests
{
    public class CustomerManager_Should
    {
        private DatabaseInterface _db;
        private string _dbPath;
        
        // Instantiate the Test
        public CustomerManager_Should()
        {
            _db = new DatabaseInterface();
            _dbPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB");
        }
        
        [Fact]
        public void AddCustomerToCollection()
        {
            Models.Customer customer = new Models.Customer();;
            customer.Id = 1;
            
            DatabaseInterface db = new DatabaseInterface();

            CustomerManager manager = new CustomerManager(db);
            manager.AddCustomer(customer);
            
            Assert.Contains(customer, manager.GetCustomers());
        }

        [Fact]
        public void AddsCustomerIdToAddedRecords() {
            Models.Customer customer = new Models.Customer();
            customer.Name = "Steve Brownlee";
            customer.City = "CITY";
            customer.State = "STATE";
            customer.PostalCode = "POSTALCODE";
            customer.PhoneNumber = "PHONENUMBER";


            // CustomerManager manager = new CustomerManager(_db);
            // manager.AddCustomer(customer);
            // // The customer Id should be greater than one
            Assert.True(customer.Id > 0);
            

        }
        
    }
}

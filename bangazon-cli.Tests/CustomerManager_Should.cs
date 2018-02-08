using System;
using Xunit;
using bangazon_cli.Models;
using System.Collections.Generic;
using System.Linq;

/*
Author: Krys Mathis
References: Ticket #1 - Create customer account

*/
namespace bangazon_cli.Managers.Tests
{
    public class CustomerManager_Should
    {
        private DatabaseInterface _db;
        
        // Instantiate the Test
        public CustomerManager_Should()
        {
            string testPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB_TEST");
            _db = new DatabaseInterface(testPath);
        }
        
        [Fact]
        public void AddCustomerToCollection()
        {
            // generate a customer
            Customer customer = new Customer();;
            customer.Name = "AddCustomerToCollection";
            customer.StreetAddress = "STREETADDRESS";
            customer.City = "CITY";
            customer.State = "STATE";
            customer.PostalCode = "POSTALCODE";
            customer.PhoneNumber = "PHONENUMBER";
            
            // instantiate the manager
            CustomerManager manager = new CustomerManager(_db);
            
            // capture the existing record count. The test will use this
            // to determine if the add method increased the number of records
            int initialRecordCount = manager.GetCustomers().Count();
            
            // assign the id to the customer object using AddCustomer
            customer.Id = manager.AddCustomer(customer);

            // get the customer from the manager
            Customer storedCustomer = manager.GetSingleCustomer(customer.Id);
            
            // There are customers in the list
            Assert.True(manager.GetCustomers().Count() > initialRecordCount);

            // The customer created by the test is in the list
            Assert.Equal(customer.Id, storedCustomer.Id);
            
        }

        [Fact]
        public void AddsCustomerIdToAddedRecords() {

            Customer customer = new Customer();
            customer.Id = 0;
            customer.Name = "AddCustomerIdToAddedRecords";
            customer.StreetAddress = "STREETADDRESS";
            customer.City = "CITY";
            customer.State = "STATE";
            customer.PostalCode = "POSTALCODE";
            customer.PhoneNumber = "PHONENUMBER";

            CustomerManager manager = new CustomerManager(_db);

            manager.AddCustomer(customer);
            
            // The customer Id should be greater than one
            Assert.True(customer.Id > 0);
    
        }
        
    }
}

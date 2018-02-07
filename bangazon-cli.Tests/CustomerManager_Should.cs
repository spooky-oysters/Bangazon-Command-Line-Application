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
        private string _dbPath;
        
        // Instantiate the Test
        public CustomerManager_Should()
        {
            _db = new DatabaseInterface();
        }
        
        [Fact]
        public void AddCustomerToCollection()
        {
            // generate a customer
            Models.Customer customer = new Models.Customer();;
            customer.Name = "AddCustomerToCollection";
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
            Customer storedCustomer = manager.GetCustomers().Where(c => c.Id == customer.Id).Single();
            
            // There are customers in the list
            Assert.True(manager.GetCustomers().Count() > initialRecordCount);

            // The customer created by the test is in the list
            Assert.Equal(customer.Id, storedCustomer.Id);
            
            // Remove the customer added during the test
            _db.Update($"DELETE FROM Customer WHERE Id = {customer.Id}");

        }

        [Fact]
        public void AddsCustomerIdToAddedRecords() {

            Models.Customer customer = new Models.Customer();
            customer.Id = 0;
            customer.Name = "AddCustomerIdToAddedRecords";
            customer.City = "CITY";
            customer.State = "STATE";
            customer.PostalCode = "POSTALCODE";
            customer.PhoneNumber = "PHONENUMBER";

            DatabaseInterface db = new DatabaseInterface();
            CustomerManager manager = new CustomerManager(db);

            manager.AddCustomer(customer);
            
            // The customer Id should be greater than one
            Assert.True(customer.Id > 0);

            // // Remove the customer added during the test
            _db.Update($"DELETE FROM Customer WHERE Id = {customer.Id}");
            
        }
        
    }
}

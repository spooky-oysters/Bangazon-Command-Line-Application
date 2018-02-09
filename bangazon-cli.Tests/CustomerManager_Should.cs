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
        private CustomerManager _customerManager;
        private ProductManager _productManager;
        private OrderManager _orderManager;
        // Instantiate the Test
        public CustomerManager_Should()
        {
            string testPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB_TEST");
            _db = new DatabaseInterface(testPath);
            // initialize managers to create db tables and use later in tests
            _customerManager = new CustomerManager(_db);
            _productManager = new ProductManager(_db);
            _orderManager = new OrderManager(_db);
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
            
            
            // capture the existing record count. The test will use this
            // to determine if the add method increased the number of records
            int initialRecordCount = _customerManager.GetCustomers().Count();
            
            // assign the id to the customer object using AddCustomer
            customer.Id = _customerManager.AddCustomer(customer);

            // get the customer from the manager
            Customer storedCustomer = _customerManager.GetSingleCustomer(customer.Id);
            
            // There are customers in the list
            Assert.True(_customerManager.GetCustomers().Count() > initialRecordCount);

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

            _customerManager.AddCustomer(customer);
            
            // The customer Id should be greater than one
            Assert.True(customer.Id > 0);
    
        }
        
       [Fact]
        public void Dispose()
        {
            _db.DeleteTables();
        }
    }
}

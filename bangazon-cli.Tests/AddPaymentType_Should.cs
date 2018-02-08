using System;
using Xunit;
using bangazon_cli.Managers;
using bangazon_cli.Models;
using System.Collections.Generic;

namespace bangazon_cli.Tests
{
    /*
        Author: Dre Randaci
        References: Ticket #3 - Add a payment type to an active customer
    */
    public class PaymentTypeManager_Should
    {
        /* 
            Variables that will be initialized in the constructor
            
            !TODO: Refactor to grab active customer from Program.ActiveCustomer variable once created
        */
        private List<PaymentType> _payments;
        private ActiveCustomerManager _activeCustomerManager;
        private Customer _customer;
        private PaymentType _paymentType;
        private DatabaseInterface _db;
        private CustomerManager _customerManager;
        private PaymentTypeManager _paymentTypeManager;

        public PaymentTypeManager_Should()
        {
            _payments = new List<PaymentType>();

            // Path to the environment variable on the users computer
            string testPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB_TEST");

            // Initializing a new DatabaseInterface and passing in the string path
            _db = new DatabaseInterface(testPath);
            
            // Initializing an instance of _customerManager to pass to the _activeCustomerManager ctor
            _customerManager = new CustomerManager(_db);

            _paymentTypeManager = new PaymentTypeManager(_db);

            // Initializing the _activeCustomerManager and passing in an instance of _customerManager to the ctor
            _activeCustomerManager = new ActiveCustomerManager(_customerManager);

            // Initializing a payment type 
            _paymentType = new PaymentType();

            // Initializing a mock customer for testing purposes with only an ID
            _customer = new Customer();
            _customer.Id = 1;
        }

        [Fact]
        public void AddNewPaymentType_Should()
        {
            // Adds properties to the _paymentType instance
            _paymentType.Id = 1;
            _paymentType.CustomerId = 10;
            _paymentType.Type = "Mastercard";
            _paymentType.AccountNumber = 12345678910;

            // Adds the _paymentType to the _payments list
            _payments.Add(_paymentType);

            // Asserts the properties exist
            Assert.Equal(_paymentType.Id, 1);
            Assert.Equal(_paymentType.CustomerId, 10);
            Assert.Equal(_paymentType.Type, "Mastercard");
            Assert.Equal(_paymentType.AccountNumber, 12345678910);

            // Asserts the _paymentType exists in the list
            Assert.Contains(_paymentType, _payments);
        }

        [Fact]
        public void AddCustomerIdToPaymentType_Should()
        {
            // Assigns the _customer.Id instance to _paymentType
            _paymentType.CustomerId = _customer.Id;
            
            // Asserts that the _customer.Id is equal to the instance of _paymentType
            Assert.Equal(_paymentType.CustomerId, 1);
            Assert.Equal(_paymentType.CustomerId, _customer.Id);
        }

        [Fact]
        public void AddPaymentTypeToCustomer_Should()
        {
            // Requests active customer with the ID of 1, returns the requested customer instance 
            var customer = _activeCustomerManager.SetActiveCustomer(1);

            // Assigns the returned customer ID to the _paymentType.CustomerId
            _paymentType.CustomerId = customer.Id;

            // Asserts that the _customer.Id is equal to the instance of _paymentType
            Assert.Equal(_paymentType.CustomerId, 1);
            Assert.Equal(_paymentType.CustomerId, customer.Id);
        }

        [Fact]
        public void GetSinglePaymentType_Should()
        {

        }

        [Fact]
        public void AddPaymentTypeToActiveCustomer_Should()
        {
            
        }
    }
}
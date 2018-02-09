using Xunit;
using System;
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
        */
        private DatabaseInterface _db;
        private PaymentType _paymentType1;
        private PaymentTypeManager _paymentTypeManager;
        private CustomerManager _custManager;
        private Customer _testCustomer;

        public PaymentTypeManager_Should()
        {
            string testPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB_TEST");

            _db = new DatabaseInterface(testPath);

            _custManager = new CustomerManager(_db);
            _testCustomer = new Customer();
        }

        [Fact]
        public void AddNewPaymentType_Should()
        {
            // Variable initializations
            _paymentType1 = new PaymentType();
            _paymentTypeManager = new PaymentTypeManager(_db);

            // Add a test customer to get the returned ID
            int custId = _custManager.AddCustomer(_testCustomer);

            // Adds properties to the _paymentType instance
            _paymentType1.Type = "Mastercard";
            _paymentType1.AccountNumber = 12345678910;

            // Adds the _paymentType to the _paymentList in the paymentTypeManager, passes in a customerId
            int paymentId = _paymentTypeManager.AddNewPaymentType(_paymentType1, custId);

            // Asserts the properties exist
            var payment = _paymentTypeManager.GetSinglePaymentType(paymentId);
            Assert.Equal(payment.Id, paymentId);
            Assert.Equal(payment.CustomerId, custId);
            Assert.Equal(payment.Type, "Mastercard");
            Assert.Equal(payment.AccountNumber, 12345678910);
        }

        [Fact]
        public void GetPaymentTypesByCustomerId_Should()
        {
            // Variable initializations
            var _paymentType1 = new PaymentType();
            _paymentTypeManager = new PaymentTypeManager(_db);

            // Add a test customer to get the returned ID
            int custId = _custManager.AddCustomer(_testCustomer);

            // Adds properties to the _paymentType instance
            _paymentType1.Type = "Mastercard";
            _paymentType1.AccountNumber = 12345678910;

            // Adds the _paymentType to the _paymentList in the paymentTypeManager, passes in a customerId
            _paymentTypeManager.AddNewPaymentType(_paymentType1, custId);

            // Adding a second payment type to the list
            var _paymentType2 = new PaymentType();
            _paymentType2.Type = "Visa";
            _paymentType2.AccountNumber = 0987654321;

            // Adds the _paymentType to the _paymentList in the paymentTypeManager, passes in a customerId
            _paymentTypeManager.AddNewPaymentType(_paymentType2, custId);

            // Requests all the payments associated with a customer
            var customerPayments = _paymentTypeManager.GetPaymentTypesByCustomerId(custId);

            // Checks that both payment types exist in the return list
            Assert.Equal(2, customerPayments.Count);
        }

        [Fact]
        public void GetSinglePaymentType_Should()
        {
            // Variable initializations
            _paymentType1 = new PaymentType();
            _paymentTypeManager = new PaymentTypeManager(_db);

            // Add a test customer to get the returned ID
            int custId = _custManager.AddCustomer(_testCustomer);

            _paymentType1.Type = "Mastercard";
            _paymentType1.AccountNumber = 12345678910;
            int paymentId = _paymentTypeManager.AddNewPaymentType(_paymentType1, custId);

            // Pulls a single payment based off a paymentType.Id
            var payment = _paymentTypeManager.GetSinglePaymentType(paymentId);

            // Asserts the _paymentType.Id instance and the single payment.Id are equal
            Assert.Equal(paymentId, payment.Id);
        }

        // Clears the database after the test suite runs
        [Fact]
        public void Dispose()
        {
            _db.Update("DELETE FROM OrderProduct");
            _db.Update("DELETE FROM `Order`");
            _db.Update("DELETE FROM PaymentType");
        }
    }
}
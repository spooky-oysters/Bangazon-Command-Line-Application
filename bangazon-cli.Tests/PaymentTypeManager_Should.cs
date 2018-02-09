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
        */
        private DatabaseInterface _db;
        private readonly PaymentTypeManager _paymentTypeManager;
        private readonly CustomerManager _custManager;
        private readonly ProductManager _prodManager;
        private readonly OrderManager _orderManager;
        private Customer _testCustomer;
        private PaymentType _paymentType1;

        public PaymentTypeManager_Should()
        {
            // Establishing the path to the database
            string testPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB_TEST");

            // Initializing an instance of the DatabaseInterface
            _db = new DatabaseInterface(testPath);

            // By initializing these managers, all of the tables are created
            _paymentTypeManager = new PaymentTypeManager(_db);
            _custManager = new CustomerManager(_db);
            _prodManager = new ProductManager(_db);
            _orderManager = new OrderManager(_db);
        }

        [Fact]
        public void AddNewPaymentType_Should()
        {
            // Variable initializations
            _paymentType1 = new PaymentType();
            _testCustomer = new Customer();

            // Add a test customer to get the returned ID
            int custId = _custManager.AddCustomer(_testCustomer);

            // Adds properties to the _paymentType instance
            _paymentType1.Type = "Mastercard";
            _paymentType1.AccountNumber = 12345678910;

            // Adds the _paymentType to the _paymentList in the paymentTypeManager, passes in a customerId
            int paymentId = _paymentTypeManager.AddNewPaymentType(_paymentType1, custId);

            // Asserts the properties exist
            PaymentType payment = _paymentTypeManager.GetSinglePaymentType(paymentId);
            Assert.Equal(payment.Id, paymentId);
            Assert.Equal(payment.CustomerId, custId);
            Assert.Equal(payment.Type, "Mastercard");
            Assert.Equal(payment.AccountNumber, 12345678910);
        }

        [Fact]
        public void GetPaymentTypesByCustomerId_Should()
        {
            // Variable initializations
            _paymentType1 = new PaymentType();
            _testCustomer = new Customer();

            // Add a test customer to get the returned ID
            int custId = _custManager.AddCustomer(_testCustomer);

            // Adds properties to the _paymentType instance
            _paymentType1.Type = "Mastercard";
            _paymentType1.AccountNumber = 12345678910;

            // Adds the _paymentType to the _paymentList in the paymentTypeManager, passes in a customerId
            _paymentTypeManager.AddNewPaymentType(_paymentType1, custId);

            // Adding a second payment type to the list
            PaymentType _paymentType2 = new PaymentType();
            _paymentType2.Type = "Visa";
            _paymentType2.AccountNumber = 0987654321;

            // Adds the _paymentType to the _paymentList in the paymentTypeManager, passes in a customerId
            _paymentTypeManager.AddNewPaymentType(_paymentType2, custId);

            // Requests all the payments associated with a customer
            List<PaymentType> customerPayments = _paymentTypeManager.GetPaymentTypesByCustomerId(custId);

            // Checks that both payment types exist in the return list. Since a new customer ID is retrieved within this test, the number of times the payment types are added won't effect the test
            Assert.Equal(2, customerPayments.Count);
        }

        [Fact]
        public void GetSinglePaymentType_Should()
        {
            // Variable initializations
            _paymentType1 = new PaymentType();
            _testCustomer = new Customer();

            // Add a test customer to get the returned ID
            int custId = _custManager.AddCustomer(_testCustomer);

            _paymentType1.Type = "Mastercard";
            _paymentType1.AccountNumber = 12345678910;
            int paymentId = _paymentTypeManager.AddNewPaymentType(_paymentType1, custId);

            // Pulls a single payment based off a paymentType.Id
            PaymentType payment = _paymentTypeManager.GetSinglePaymentType(paymentId);

            // Asserts the _paymentType.Id instance and the single payment.Id are equal
            Assert.Equal(paymentId, payment.Id);
        }

        // Clears the database after the test suite runs
        [Fact]
        public void Dispose()
        {
            _db.DeleteTables();
        }
    }
}
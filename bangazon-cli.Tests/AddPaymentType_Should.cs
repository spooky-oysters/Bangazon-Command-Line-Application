using System;
using Xunit;
using bangazon_cli.Managers;
using bangazon_cli.Models;

namespace bangazon_cli.Tests
{
    /*
        Author: Dre Randaci
        References: Ticket #3 - Add a payment type to an active customer
    */
    public class AddPaymentType_Should
    {
        /* 
            Variables that will be initialized in the constructor
        */
        private ActiveCustomerManager _activeCustomerManager;
        private Customer _customer;
        private PaymentType _paymentType;


        // TODO: Refactor to grab active customer from Program.ActiveCustomer variable

        public AddPaymentType_Should()
        {
            // Initializing the _activeCustomerManager and passing in a db
            _activeCustomerManager = new ActiveCustomerManager();

            // Initializing a payment type 
            _paymentType = new PaymentType();

            // Initializing a mock customer for testing purposes
            _customer = new Customer();
            _customer.Id = 1;
        }

        [Fact]
        public void PaymentTypeProperties_Should()
        {
            // Checks that properties exist on the _paymentType instance
            _paymentType.Id = 1;
            _paymentType.CustomerId = 10;
            _paymentType.Type = "Mastercard";
            _paymentType.AccountNumber = 12345678910;

            Assert.Equal(_paymentType.Id, 1);
            Assert.Equal(_paymentType.CustomerId, 10);
            Assert.Equal(_paymentType.Type, "Mastercard");
            Assert.Equal(_paymentType.AccountNumber, 12345678910);
        }

        [Fact]
        public void AddCustomerIdToPaymentType_Should()
        {
            // Assigns the _customer.Id instance to _paymentType
            _paymentType.CustomerId = _customer.Id;
            Assert.Equal(_paymentType.CustomerId, 1);
        }

        [Fact]
        public void AddPaymentTypeToActiveCustomer_Should()
        {
            var customer = _activeCustomerManager.SetActiveCustomer(1);

            _paymentType.CustomerId = customer.Id;
            Assert.Equal(_paymentType.CustomerId, 1);
        }
    }
}
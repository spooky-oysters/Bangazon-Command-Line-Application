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
        private CustomerManager _customerManager;
        private ActiveCustomerManager _activeCustomerManager;
        private Customer _customer;
        private PaymentType _paymentType;


        // TODO: Refactor to grab active customer from Program.ActiveCustomer variable

        public AddPaymentType_Should()
        {
            // Initializing a payment type 
            _paymentType = new PaymentType();

            // Initializing a mock customer for testing purposes
            _customer = new Customer();
            _customer.Id = 1;
        }

        [Fact]
        public void AddCustomerIdToPaymentType_Should()
        {
            _paymentType.CustomerId = _customer.Id;
            Assert.Equal(_paymentType.CustomerId, 1);
        }

        [Fact]
        public void AddPaymentTypeToActiveCustomer_Should()
        {
            _paymentType.CustomerId = _customer.Id;
            Assert.Equal(_paymentType.CustomerId, 1);
        }
    }
}
using Xunit;
using bangazon_cli.Managers;
using bangazon_cli.Models;

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
        private PaymentType _paymentType;
        private PaymentTypeManager _paymentTypeManager;

        public PaymentTypeManager_Should()
        {
            // Initializing a payment manager
            _paymentTypeManager = new PaymentTypeManager();

            // Initializing a payment type 
            _paymentType = new PaymentType();
        }

        [Fact]
        public void AddNewPaymentType_Should()
        {
            // Adds properties to the _paymentType instance
            _paymentType.Id = 0;
            _paymentType.Type = "Mastercard";
            _paymentType.AccountNumber = 12345678910;

            // Adds the _paymentType to the _paymentList in the paymentTypeManager, passes in a customerId
            _paymentTypeManager.AddNewPaymentType(_paymentType, 10);

            // Asserts the properties exist
            var payment = _paymentTypeManager.GetSinglePaymentType(0);
            Assert.Equal(payment.Id, 0);
            Assert.Equal(payment.CustomerId, 10);
            Assert.Equal(payment.Type, "Mastercard");
            Assert.Equal(payment.AccountNumber, 12345678910);

            // Asserts the _paymentType exists in the _payments list
            Assert.Contains(_paymentType, _paymentTypeManager.GetPaymentTypesByCustomerId(10));
        }

        [Fact]
        public void GetPaymentTypesByCustomerId_Should()
        {
            // Adds properties to the _paymentType instance
            _paymentType.Id = 0;
            _paymentType.Type = "Mastercard";
            _paymentType.AccountNumber = 12345678910;

            // Adds the _paymentType to the _paymentList in the paymentTypeManager, passes in a customerId
            _paymentTypeManager.AddNewPaymentType(_paymentType, 10);

            // Adding a second payment type to the list
            var _paymentType2 = new PaymentType();
            _paymentType2.Id = 1;
            _paymentType2.Type = "Visa";
            _paymentType2.AccountNumber = 0987654321;
            _paymentTypeManager.AddNewPaymentType(_paymentType2, 10);

            // Requests all the payments associated with a customer
            var customerPayments = _paymentTypeManager.GetPaymentTypesByCustomerId(10);

            // Checks that both payment types exist in the return list
            Assert.Contains(_paymentType, customerPayments);
            Assert.Contains(_paymentType2, customerPayments);
        }

        [Fact]
        public void GetSinglePaymentType_Should()
        {
            _paymentType.Id = 0;
            _paymentTypeManager.AddNewPaymentType(_paymentType, 10);

            // Pulls a single payment based off a paymentType.Id
            var payment = _paymentTypeManager.GetSinglePaymentType(0);

            // Asserts the _paymentType.Id instance and the single payment.Id are equal
            Assert.Equal(_paymentType.Id, payment.Id);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using bangazon_cli.Models;

namespace bangazon_cli.Managers
    /*
        Author: Dre Randaci
        Responsibility: Add a payment type to an active customer
    */
{
    public class PaymentTypeManager
    {
        // Variable that will hold all the payment types for a customer
        private List<PaymentType> _paymentsList;

        public PaymentTypeManager()
        {
            // Initializes the payments list
            _paymentsList = new List<PaymentType>();
        }

        // Method adds a new payment type for a customer. Takes an instance of a payment type and an active customer id
        public void AddNewPaymentType(PaymentType paymentType, int custId)
        {
            paymentType.CustomerId = custId;
            _paymentsList.Add(paymentType);
        }

        // Method returns a list of payment types for a given customer. Takes a customer id
        public List<PaymentType> GetPaymentTypesByCustomerId(int custId)
        {
            return _paymentsList.Where(p => p.CustomerId == custId).ToList();
        }

        // Method extracts a single payment type. Takes a payment id
        public PaymentType GetSinglePaymentType(int paymentId)
        {
            return _paymentsList.Where(p => p.Id == paymentId).Single();
        }
    }
}
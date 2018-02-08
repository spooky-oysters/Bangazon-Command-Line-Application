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
        private List<PaymentType> _paymentsList;
        private PaymentType _payment;

        public PaymentTypeManager()
        {
            _paymentsList = new List<PaymentType>();
            _payment = new PaymentType();
        }

        public void AddNewPaymentType(PaymentType paymentType, int custId)
        {
            paymentType.CustomerId = custId;
            _paymentsList.Add(paymentType);
        }

        public List<PaymentType> GetPaymentTypesByCustomerId(int custId)
        {
            return _paymentsList.Where(p => p.CustomerId == custId).ToList();
        }

        public PaymentType GetSinglePaymentType(int paymentId)
        {
            return _paymentsList.Where(p => p.Id == paymentId).Single();
        }
    }
}
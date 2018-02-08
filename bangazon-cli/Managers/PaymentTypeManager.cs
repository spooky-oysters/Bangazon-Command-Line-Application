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
        private List<PaymentType> _payments;

        public PaymentTypeManager()
        {
            _payments = new List<PaymentType>();
        }

        public void AddNewPaymentType(PaymentType paymentType, int custId)
        {
            paymentType.Id = 1;
            paymentType.CustomerId = custId;
            _payments.Add(paymentType);
        }

        public PaymentType GetSinglePaymentType(int paymentId)
        {
            return _payments.Where(p => p.Id == paymentId).Single();
        }
    }
}
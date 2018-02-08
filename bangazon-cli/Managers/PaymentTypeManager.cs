using System;
using System.Collections.Generic;
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
        private DatabaseInterface _db;

        public PaymentTypeManager(DatabaseInterface db)
        {
            _db = db;   
            _payments = new List<PaymentType>();
        }

        public void AddNewPaymentType(PaymentType paymentType, int custId)
        {
            paymentType.CustomerId = custId;
            _payments.Add(paymentType);
        }
    }
}
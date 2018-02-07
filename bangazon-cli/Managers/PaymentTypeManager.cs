using System;
using bangazon_cli.Models;

namespace bangazon_cli.Managers
    /*
        Author: Dre Randaci
        Responsibility: Add a payment type to an active customer
    */
{

    public class PaymentTypeManager
    {
        private DatabaseInterface _db;

        public PaymentTypeManager(DatabaseInterface db)
        {
            _db = db;   
            this.CreatePaymentTypeTable();
        }

        private void CreatePaymentTypeTable() 
        {
            try {
                _db.Update(@"CREATE TABLE IF NOT EXISTS `PaymentType` (
                    `Id` INTEGER PRIMARY KEY AUTOINCREMENT,
                    `Type` TEXT NOT NULL,
                    `AccountNumber` TEXT NOT NULL);
                ");
            } catch (Exception ex) {
                Console.WriteLine("CreatePaymentTypeTable", ex.Message);
            }
        }

        public void AddPaymentTypeToActiveCustomer(PaymentType pT)
        {
            var NewPaymentType= new PaymentType();
        }
    }
}
using System;
using System.Linq;
using bangazon_cli.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

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
            try
            {
                _db.Update(@"CREATE TABLE IF NOT EXISTS
                `PaymentType` (
                    `Id` INTEGER PRIMARY KEY AUTOINCREMENT,
                    `CustomerId` INTEGER NOT NULL,
                    `Type` TEXT NOT NULL,
                    `AccountNumber` INTEGER NOT NULL,
                    FOREIGN KEY(`CustomerId`) REFERENCES `Customer`(`Id`)
                );
                ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("CreatePaymentTypeTable", ex.Message);
            }
        }


        // Method adds a new payment type for a customer. Takes an instance of a payment type and an active customer id
        public int AddNewPaymentType(PaymentType paymentType, int custId)
        {
            string SQLInsert = $@"INSERT INTO `PaymentType`
            VALUES (
                null,
                '{custId}',
                '{paymentType.Type}',
                '{paymentType.AccountNumber}'
                );";

            int paymentTypeId = 0;
            try
            {
                paymentTypeId = _db.Insert(SQLInsert);
                paymentType.Id = paymentTypeId;
            }
            catch (Exception err)
            {
                Console.WriteLine("Add PaymentType Error", err.Message);
            }
            return paymentTypeId;
        }


        // Method returns a list of payment types for a given customer. Takes a customer id
        public List<PaymentType> GetPaymentTypesByCustomerId(int custId)
        {
            // Initialize a list to hold payment types
            var _paymentTypesList = new List<PaymentType>();

            // Clear the payment types list
            _paymentTypesList.Clear();

            // Find the record for the payment type in the db and retrieve data
            _db.Query($@"SELECT * FROM PaymentType WHERE CustomerId == {custId};",
            (SqliteDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        // Initializing payment type instance
                        var paymentType = new PaymentType();

                        paymentType.Id = Convert.ToInt32(reader["Id"]);
                        paymentType.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        paymentType.Type = Convert.ToString(reader["Type"]);
                        paymentType.AccountNumber = Convert.ToInt64(reader["AccountNumber"]);

                        // add it to collection
                        _paymentTypesList.Add(paymentType);
                    }
                });
            return _paymentTypesList;
        }

        // Method extracts a single payment type. Takes a payment id
        public PaymentType GetSinglePaymentType(int paymentId)
        {
            // Initializing payment type instance
            var paymentType = new PaymentType();

            _db.Query($@"SELECT * FROM PaymentType WHERE Id == {paymentId};",
            (SqliteDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        paymentType.Id = Convert.ToInt32(reader["Id"]);
                        paymentType.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        paymentType.Type = Convert.ToString(reader["Type"]);
                        paymentType.AccountNumber = Convert.ToInt64(reader["AccountNumber"]);
                    }
                });
            return paymentType;
        }
    }
}
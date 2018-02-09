using System;
using bangazon_cli.Models;
using bangazon_cli.Managers;

namespace bangazon_cli.Menus
{
    /*
        Summary: Controls the add payment type for an active customer menu
        Parameters: 
            - payment type
            - payment type manager
            - active customer ID and name
    */
    public class PaymentTypeMenu
    {
        private PaymentType _paymentType;
        private PaymentTypeManager _paymentTypeManager;
        private int _activeCustormerId;
        private string _activeCustomerName;

        // Class constructor. Takes 
        public PaymentTypeMenu(PaymentType paymentType, PaymentTypeManager paymentTypeManager, Customer activeCustomer)
        {
            _activeCustormerId = activeCustomer.Id;
            _activeCustomerName = activeCustomer.Name;
            _paymentType = paymentType;
            _paymentTypeManager = paymentTypeManager;
        }

        /*
            Summary: Displays the add payment type menu to the user
        */
        public void Show()
        {
            // Both fields must contain value
            do
            {
                Console.Clear();
                Console.WriteLine("Enter payment type (e.g. AmEx, Visa, Checking)");
                Console.Write("> ");
                _paymentType.Type = Console.ReadLine();
            } while (_paymentType.Type.Length == 0);

            do
            {
                Console.WriteLine("Enter account number");
                Console.Write("> ");
                _paymentType.AccountNumber = Convert.ToInt64(Console.ReadLine());
            } while (_paymentType.AccountNumber < 0);

            // Adds the payment type to the database, takes the active customer ID, returns the added payment type ID
            int id = _paymentTypeManager.AddNewPaymentType(_paymentType, _activeCustormerId);

            // Checks if the payment was added successfully. 0 == failure 
            if (id > 0)
            {
                Console.WriteLine($"*** {_paymentType.Type} PAYMENT TYPE ADDED FOR {_activeCustomerName} ***");
                Console.WriteLine("*** PRESS ENTER TO CONTINUE ***");
                Console.ReadLine();
            }
        }
    }
}
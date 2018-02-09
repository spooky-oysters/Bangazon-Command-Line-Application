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
            - customer id
    */
    public class PaymentTypeMenu
    {
        private PaymentType _paymentType;
        private PaymentTypeManager _paymentTypeManager;
        private int _activeCustormerId;
        private string _activeCustomerName;

        public PaymentTypeMenu(PaymentType paymentType, PaymentTypeManager paymentTypeManager, Customer activeCustomer)
        {
            _activeCustormerId = activeCustomer.Id;
            _activeCustomerName = activeCustomer.Name;
            _paymentType = paymentType;
            _paymentTypeManager = paymentTypeManager;
        }

        public void Show()
        {
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
                _paymentType.AccountNumber = Convert.ToInt32(Console.ReadLine());
            } while (_paymentType.AccountNumber < 0);

            int id = _paymentTypeManager.AddNewPaymentType(_paymentType, _activeCustormerId);

            if (id > 0)
            {
                Console.WriteLine($"*** {_paymentType.Type} PAYMENT TYPE ADDED FOR {_activeCustomerName}               ***");
                Console.WriteLine("*** PRESS ENTER TO CONTINUE ***");
                Console.ReadLine();
            }

        }






    }
}
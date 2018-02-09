using System;
using bangazon_cli.Models;
using bangazon_cli.Managers;

namespace bangazon_cli.Menus
{
    /*
        Summary: Controls the add payment type for an active customer menu
        Parameters: 
            - customer object
            - customer manager
    */
    public class PaymentTypeMenu
    {   
        private PaymentType _paymentType;
        private PaymentTypeManager _paymentTypeManager;

        public PaymentTypeMenu(PaymentType paymentType, PaymentTypeManager paymentTypeManager)
        {   
            _paymentType = paymentType;
            _paymentTypeManager = paymentTypeManager;
        }

        public void Show()
        {
            do {
                Console.WriteLine ("Enter payment type (e.g. AmEx, Visa, Checking)");
                Console.Write ("> ");
                _paymentType.Type = Console.ReadLine();
                Console.WriteLine ("Enter account number");
                Console.Write ("> ");
                _paymentType.AccountNumber = Convert.ToInt32(Console.ReadLine());
                
            } while (_paymentType.AccountNumber < 0);






        }






    }
}
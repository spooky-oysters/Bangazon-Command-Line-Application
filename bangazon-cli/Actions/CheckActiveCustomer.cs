using System;
using bangazon_cli.Models;

namespace bangazon_cli.Actions
{
    public class CheckActiveCustomer
    {
        public static bool Exists(Customer activeCustomer) {
            bool exists = activeCustomer.Id > 0 ? true : false;
            if (!exists) {
                Console.WriteLine("Set the active customer first");
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
            }
            return exists;
        }   
    }
}
using System;

namespace bangazon_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseInterface db = new DatabaseInterface();
            Managers.CustomerManager customerManager = new Managers.CustomerManager(db);
            Console.WriteLine("Hello World!");
        }
    }
}

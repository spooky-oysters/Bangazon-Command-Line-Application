using System;

namespace bangazon_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            string prodPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB");
            DatabaseInterface db = new DatabaseInterface(prodPath);
            Managers.CustomerManager customerManager = new Managers.CustomerManager(db);
            Console.WriteLine("Hello World!");
        }
    }
}

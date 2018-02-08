using System;
using Xunit;

namespace bangazon_cli.Tests
{
    public class zDisposeDb
    {
        /* 
            Variables that will be initialized in the constructor
        */
        private DatabaseInterface _db;
        public zDisposeDb()
        {
            string testPath = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB_TEST");

            _db = new DatabaseInterface(testPath);
        }

        [Fact]
        public void Dispose()
        {
            _db.Update("DELETE FROM OrderProduct");
            _db.Update("DELETE FROM `Order`");
            _db.Update("DELETE FROM Product");
            _db.Update("DELETE FROM PaymentType");
            _db.Update("DELETE FROM Customer");
        }
    }
}
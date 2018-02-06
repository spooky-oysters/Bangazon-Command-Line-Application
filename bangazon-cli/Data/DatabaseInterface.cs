using System;
using Microsoft.Data.Sqlite;

namespace bangazon_cli
{
    /* 
    Author: Dre Randaci
    Description: Database interface to access data in a local database file 
    Example access method: 

    public class ExampleClass
    {
        private DatabaseInterface db;
        db.Query(
                $@"select Name
                From Customer
                Where CustomerId = '{Id}';",
                (SqliteDataReader reader) =>
                {
                    while (reader.Read ()) {
                        CustomerId = reader.GetInt32(0);
                    }
                }
            ); 
    }
    */

    public class DatabaseInterface
    {        
        private string _connectionString;
        private SqliteConnection _connection;

        public DatabaseInterface()
        {
            // Method to extract the environment variable holding the database path 
            try {
                _connectionString = $"Data Source=BANGAZON_CLI_APP_DB.db";
                _connection = new SqliteConnection(_connectionString);
                Console.Write("Connected...");

            } catch (Exception err) {
                Console.WriteLine("ERROR: Not connected to db " + err.Data);
                Console.ReadLine();
            }
        }

        // Method to query any table in the database
        public void Query(string command, Action<SqliteDataReader> handler)
        {
            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();
                dbcmd.CommandText = command;

                using (SqliteDataReader dataReader = dbcmd.ExecuteReader())
                {
                    handler (dataReader);
                }

                dbcmd.Dispose ();
            }
        }

        // Method to update any method in the database
        public void Update(string command)
        {
            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();
                dbcmd.CommandText = command;
                dbcmd.ExecuteNonQuery ();
                dbcmd.Dispose ();
            }
        }

        // Method to insert new rows into the database
        public int Insert(string command)
        {
            int insertedItemId = 0;

            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();
                dbcmd.CommandText = command;

                dbcmd.ExecuteNonQuery ();

                this.Query("select last_insert_rowid()",
                    (SqliteDataReader reader) => {
                        while (reader.Read ())
                        {
                            insertedItemId = reader.GetInt32(0);
                        }
                    }
                );

                dbcmd.Dispose ();
            }
            
            return insertedItemId;
        }


        // Method to check if a table exists. If the table does not exist, the method will create that table 
        public void CheckTableName ()
        {
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Query the account table to see if table is created
                dbcmd.CommandText = $"SELECT `Id` FROM `ExampleTable`";

                try
                {
                    // Try to run the query. If it throws an exception, create the table
                    using (SqliteDataReader reader = dbcmd.ExecuteReader()) { }
                    dbcmd.Dispose ();
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.Contains("no such table"))
                    {
                        dbcmd.CommandText = $@"CREATE TABLE `ExampleTable` (
                            
                        )";

                        try
                        {
                            dbcmd.ExecuteNonQuery ();
                        }
                        catch (Microsoft.Data.Sqlite.SqliteException crex)
                        {
                            Console.WriteLine("Table already exists. Ignoring");
                        }
                    }
                }
                _connection.Close();
            }
        }
    }
}
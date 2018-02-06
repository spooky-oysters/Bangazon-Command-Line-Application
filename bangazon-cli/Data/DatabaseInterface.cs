using System;
using Microsoft.Data.Sqlite;

namespace bangazon_cli
{
    /* 
    Author: Dre Randaci
    Purpose: Methods to access SQL data in a locally saved database file 
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
        // Environment variable to store the path to the local DB file
        private string _connectionString;
        
        // Variable to store the connection to the database. Passes _connectionString as an argument
        private SqliteConnection _connection;

        // Method to extract the developers environment variable holding the BANGAZON_CLI_APP_DB.db filepath 
        public DatabaseInterface()
        {            
            try {
                _connectionString = $"Data Source=BANGAZON_CLI_APP_DB.db";
                _connection = new SqliteConnection(_connectionString);
                Console.Write("Connected...");
            // If the filepath cannot be found, throw an exception message
            } catch (Exception err) {
                Console.WriteLine("ERROR: Not connected to db " + err.Data);
                Console.ReadLine();
            }
        }

        // Method to query any table in the database. Takes a string SQL command when called
        public void Query(string command, Action<SqliteDataReader> handler)
        {
            using (_connection)
            {
                // Creates a connection to the database and passes the SQL command in as the CommandText
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
                // Creates a connection to the database and passes the SQL command in as the CommandText
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
            // Initializes an ID variable used to hold the returned inserted item ID
            int insertedItemId = 0;

            using (_connection)
            {
                // Creates a connection to the database and passes the SQL command in as the CommandText
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();
                dbcmd.CommandText = command;

                dbcmd.ExecuteNonQuery ();

                // Accesses the Query method within this class and passes a SQL command 
                this.Query("select last_insert_rowid()",
                    (SqliteDataReader reader) => {
                        while (reader.Read ())
                        {
                            // Loop runs once and assigns the initialized insertedItemId variable to the inserted returned items ID
                            insertedItemId = reader.GetInt32(0);
                        }
                    }
                );

                dbcmd.Dispose ();
            }
            // Returns the inserted item ID
            return insertedItemId;
        }


        // Method to check if a table exists. If the table does not exist, the method will create that table 
        public void CheckTableName ()
        {
            using (_connection)
            {
                // Creates a connection to the database and passes the SQL command in as the CommandText
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
                        // Create table template command that can create a table if it does not exist
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
                // Closes the connection to the database
                _connection.Close();
            }
        }
    }
}
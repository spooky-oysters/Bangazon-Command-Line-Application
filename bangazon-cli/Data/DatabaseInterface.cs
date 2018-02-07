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
                $@"select ProductId
                From Product
                Where CustomerId = '{Id}';",
                (SqliteDataReader reader) =>
                {
                    while (reader.Read ()) {
                        ProductId = reader.GetInt32(0);
                    }
                }
            ); 
    }
    */

    public class DatabaseInterface
    {        
        // Variable to store the path to the local DB file
        private string _connectionString;
        
        // Variable to store the connection to the database. Passes _connectionString as an argument
        private SqliteConnection _connection;

        // Method to extract the developers environment variable holding the BANGAZON_CLI_APP_DB.db filepath 
        public DatabaseInterface()
        {            
            try {
                string path = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB");
                _connectionString = $"Data Source={path}";
                _connection = new SqliteConnection(_connectionString); 
                Console.Write("Connected...");
            // If the filepath cannot be found, throw an exception message
            } catch (Exception err) {
                Console.WriteLine("ERROR: Not connected to db " + err.Data);
                Console.ReadLine();
                
            }
        }

        public string GetConnectionString() {
            return _connectionString;
        }

        // Method to generate the database file if it does not exist
        // public void GenerateDatabaseFile() {
        //     // environment variable
        //     string path = System.Environment.GetEnvironmentVariable("BANGAZON_CLI_APP_DB.db");
        //     SqliteConnection
        // }
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

        // Method to update tables in the database
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
                            // Loop runs once and assigns the inserted items ID to the initialized insertedItemId variable
                            insertedItemId = reader.GetInt32(0);
                        }
                    }
                );

                dbcmd.Dispose ();
            }
            // Returns the inserted item ID
            return insertedItemId;
        }
    }
}
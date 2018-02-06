# Bangazon Command Line Application
The Bangazon CLA provides Bangazon employees access to information about Bangazon customers. The application provides information to manage aspects of the customer base outlined below:
* **Customer** personal profiles
* **Orders** and **order details**
* **Products** and **product details**
* **Customer** product sales reports
* Viewing lists of all **customers**, **products**, and **orders**

**Access to this API is restricted to employees of Bangazon**

## Installation:

1. Clone or download the repo
1. Configure an environment variable for the database named: ```BANGAZON_CLI_APP_DB```
1. In the terminal, navigate to the directory with the repo. Begin by installing the required packages:

 ```sh
 // Install .NET SDK
 https://www.microsoft.com/net/learn/get-started/macos
 
// Include these package references in the bangazon-cli.csproj file

    <PackageReference Include="Microsoft.Data.SQLite" Version="2.0.0" />
    <PackageReference Include="System.Data.SQLite" Version="1.0.107.0" />
    <PackageReference Include="System.ValueTuple" Version="4.4.0" />
 ```
- Restore the project
```sh
dotnet restore
```

- Build and run
```sh
dotnet run
```

### Database Interface
The `Data/DatabaseInterface.cs` file holds all the methods to access the database using `SQLite`. Each time a specific customer profile is requested, added, updated, or deleted all the methods for those actions are relayed to this file. 

- Example query method:
```sh
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
``` 

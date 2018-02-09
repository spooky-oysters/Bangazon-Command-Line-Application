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
1. Configure an environment variable for the production/development database named: ```BANGAZON_CLI_APP_DB```
1. Configure an environment variable for the test database named: ```BANGAZON_CLI_APP_DB_TEST```
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
            $@"select CustomerId
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
### Add Customers
Users can enter individual customer into the system using the "Create a customer account" option. The system will guide them through the steps, one value at a time:

```sh
Enter customer name
> Beatric Quimby
Enter street address
> 1720 Klickitat St.
Enter customer city
> Portland 
Enter customer state
> OR
Enter customer postal code
> 97212
Enter customer phone number
> 855-555-5000
```

### Active Customer 
Every user of the Bangazon-Command-Line-Application should be able to view, select, and make a customer active from a list of all customers in the database. When a user enters a key selection for a given customer on the selection menu, then access to that customer's profile and associated data will be available to create, update, and delete.

After, navigating to the active customer, the user will be prompted to select an active user:

```sh
Which customer will be active?
1. Henry Huggins
2. Ramona Quimby
```
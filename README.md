# Bangazon Command Line Application
The Bangazon CLA provides Bangazon employees access to information about Bangazon customers. The application provides information to manage aspects of the customer base outlined below:
* **Customer** personal profiles
* **Orders** and **order details**
* **Products** and **product details**
* **Customer** product sales reports
* Viewing lists of all **customers**, **products**, and **orders**

**Access to this API is restricted to employees of Bangazon**

## Contents:
1. Installation instructions
1. User Features
1. Technical Details

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

# Features of Bangazon!

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

### Select an Active Customer 
Every user of the Bangazon-Command-Line-Application should be able to view, select, and make a customer active from a list of all customers in the database. When a user enters a key selection for a given customer on the selection menu, then access to that customer's profile and associated data will be available to create, update, and delete.

After, navigating to the active customer, the user will be prompted to select an active user:

```sh
Which customer will be active?
1. Henry Huggins
2. Ramona Quimby
```

Once the user selects an active user, the system reveals the full menu:
```sh
1. Create a customer account
2. Choose active customer
3. Create a payment option
4. Add a product to active customer
5. Update active customers product
6. Delete active customers product
7. Add product to shopping cart
8. Complete an order
9. View Reports
10. Leave Bangazon!
> 
```

### Creating a payment option
Provides the ability to assign a payment option to the active customer. Customers can have up to 8 payment options in the system.

### Adding a product to active customer
Allows the user to add a product to the active customer. The system guides the user step-by-step through the product set-up process.

### Update active customer's product
Changes happen. If a customer needs to update the information about one of their products, this menu will prompt the user for the type of change then allow the user to make the change to the product.

### Delete active customer's product
When a customer no longer wishes to list one of their items, this menu option allows the user to de-list an item for the active user.

**Only items that are not on an order can be deleted.** For data integrity reasons, Bangazon must keep records for ordered items for up to 10 years.

### Add products to shopping cart
When a customer wants to purchase a product, this menu option guides them through the steps to add a product to their shopping cart. The user will only be presented with products that have an "In Stock" status with available inventory. They can continue shopping if they like and complete their order at a later time. 

Sample of menu options:
```sh
Choose a Product to add to the order:

1. Baseball
2. phone
4. Exit back to Main Menu. 
> 
```

### Complete an order
Completing an order means the customer is ready to pay. This menu allows the user to associate an order with a customer's payment type. This action closes an order in the Bangazon system. A closed order is an order which includes a paymentTypeId and a CompleteDate on its database record. 

A check on available inventory is made when an order is closed. Any items that are out of stock are automatically removed from the order. 

Sample of menu options:
```sh
Your order total is $1511. Ready to purchase?
(Y/N) > 
```

```sh
Your order total is $1511. Ready to purchase?
(Y/N) > y
Choose a payment option:
1. Visa
2. AmEx
3. Checking
> 
```



### View reports
There are currently three reports available to Bangazon! users:
1. Product popularity - view the popularity of products in the Bangazon! universe
1. Stale Products - allows the user to see products that are not selling based on 
    - Has never been added to an order, and has been in the system for more than 180 days
    - Has been added to an order, but the order hasn't been completed, and the order was created more than 90 days ago 
    - Has been added to one, or more orders, and the order were completed, but there is remaining quantity for the product, and the product has been in the system for more than 180 days
1. Revenue report for the active customer. Sample output:

    ```sh
    Revenue report for Svetlana:

    Order #34
    ----------------------------------------------------
    Marble                          15         $21.43

    Order #109
    ----------------------------------------------------
    Kite                            1          $5.12
    Marble                          5          $5.52

    Total Revenue: $32.07
    ```


# Technical Details

### Test Driven Design

The development team included the test project, bangazon-cli.Tests with the application to allow future developers to refactor with confidence.

To run the test project:
1. Navigate to the banazon-cli.Tests project
1. run the command ```dotnet test``

At the time of this writing, there are 26 unit tests. **Run these each time you refactor or add features**

### Architecture:

* Program.cs - the main method for the application
* Data - contains the DatabaseInterface class
* Managers - the managers interact with the database using the DatabaseInterface
* Menus - The menus interact with the customers and utilize the managers to complete tasks based on user input
* Models - the data models for the application

## Data
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
### Managers
All managers take, as part of their constructors, a DatabaseInterface object. They communicate with the database and essentially manage the database actions related to a specific feature or data model.

For example, the CustomerManager manages the database queries related to customer data.

### Menu
The menu classes control the visual interaction of the program and accept user input. These classes allow the program to coordinate between user input and Managers.

### Models
Each class represents the data structure for the application.
Current models include:
- Customer
- Order
- Payment Type
- Product

# Development Team:
- [Dre Randaci](https://github.com/DreRandaci)
- [Kimberly Bird](https://github.com/kimberly-bird)
- [Greg Lawrence](https://github.com/Chewieez)
- [Krys Mathis](https://github.com/krysmathis)

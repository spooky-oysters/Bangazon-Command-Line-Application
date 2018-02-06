using System;
using Xunit;

/*
    Author: Krys Mathis
    References: Ticket #1 - Create customer account
    
 */
namespace bangazon_cli.Managers.Tests
{
    public class CustomerManager_Should
    {
        [Fact]
        public void AddCustomerToCollection()
        {
            Models.Customer customer = new Models.Customer();;
            customer.Id = 1;
            
            CustomerManager manager = new CustomerManager();
            manager.AddCustomer(customer);
            
            Assert.Contains(customer, manager.GetCustomers());
        }
        
    }
}

using System;
using Xunit;

/*
    Author: Krys Mathis
    References: Ticket #1 - Create customer account
 */
namespace bangazon_cli.Models.Tests
{
    public class Customer_Should
    {
        [Fact]
        public void HaveProperties()
        {
            Customer customer = new Customer();
            customer.Id = 1;
            customer.Name = "Steve";
            customer.StreetAddress = "1710 Shelby Ave";
            customer.City = "Nashville";
            customer.State = "TN";
            customer.PostalCode = "37206";
            customer.PhoneNumber = "615-855-5769";
            
            Assert.Equal(customer.Id,1);
            Assert.Equal(customer.Name,"Steve");
            Assert.Equal(customer.StreetAddress, "1710 Shelby Ave");
            Assert.Equal(customer.City, "Nashville");
            Assert.Equal(customer.State, "TN");
            Assert.Equal(customer.PostalCode, "37206");
            Assert.Equal(customer.PhoneNumber,"615-855-5769");

        }

        
    }
}

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
            
            Assert.Equal(1, customer.Id);
            Assert.Equal("Steve",customer.Name);
            Assert.Equal("1710 Shelby Ave",customer.StreetAddress);
            Assert.Equal("Nashville",customer.City);
            Assert.Equal("TN",customer.State);
            Assert.Equal("37206",customer.PostalCode);
            Assert.Equal("615-855-5769",customer.PhoneNumber);

        }

        
    }
}

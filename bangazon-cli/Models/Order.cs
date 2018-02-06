using System;
using System.Collections.Generic;

namespace bangazon_cli.Models
{
    /*
        Author: Greg Lawrence
        Model for the Order class
     */
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? PaymentTypeId { get; set; }
        public DateTime? CompletedDate { get; set; }

        public List<Product> OrderProducts { get; set; }
    
    
        // constructor for the Order class. Takes one parameter, customerId, and assigned it to CustomerId field.
        public Order(int customerId) 
        {
            this.Id = 0;
            this.CustomerId = customerId;
            this.PaymentTypeId = null;
            this.CompletedDate = null;
            OrderProducts = new List<Product>();
        }
    }

}
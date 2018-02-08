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
    
        public List<Product> Products { get; set; }
    
        // constructor for the Order class. Takes one parameter, customerId, and assigned it to CustomerId field.
        public Order(int customerId) 
        {
            this.CustomerId = customerId;
            this.PaymentTypeId = null;
            this.CompletedDate = null;
            Products = new List<Product>();
        }

        // Constuctor function to have ability to initialize an order without any properties set
        public Order(){
            this.PaymentTypeId = null;
            this.CompletedDate = null;
            Products = new List<Product>();
        }
    }
}
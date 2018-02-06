using System;

namespace bangazon_cli.Models
{
    /*
        Author: Greg Lawrence
        Model for the Order class
     */
    public class Order
    {
        public int Id { get; set; }
        public int? PaymentTypeId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? CompletedDate { get; set; }
        
    }
}
using System;

namespace bangazon_cli.Models
{
    /*
        Author: Kimberly Bird
        Model for the product class
    */
    public class Product
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
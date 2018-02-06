namespace bangazon_cli.Models
{
    /*
        Author: Greg Lawrence
        Model for the Order class
     */
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress {get;set;}
        public string City { get; set; }
        public string State {get; set;}
        public string PostalCode {get;set;}
        public string PhoneNumber { get; set; }
        
    }
}
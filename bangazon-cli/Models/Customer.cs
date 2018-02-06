namespace bangazon_cli.Models
{
    /*
        Author: Krys Mathis
        Model for the customer class
     */
    public class Customer
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
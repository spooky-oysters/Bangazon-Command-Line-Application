namespace bangazon_cli.Models
{
    public class PaymentType
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Type { get; set; }
        public long AccountNumber { get; set; }
    }
}
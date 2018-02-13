namespace bangazon_cli.Models
{
    /*
        Author: Kimberly Bird
        Model with fields on report
     */
    public class ActiveCustomerRevenue
    {
        public string CustomerName { get; }
        public int OrderNumber { get; }
        public string ProductName { get; }
        public int Quantity { get; }
        public double Price { get; }
        public double TotalRev { get; }
        public string PaymentType { get; }
    }
}
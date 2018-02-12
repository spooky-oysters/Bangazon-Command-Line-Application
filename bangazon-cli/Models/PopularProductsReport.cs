namespace bangazon_cli.Models
{
    public class PopularProductsReport
    {
        private string Product { get; set; }
        private string Orders { get; set; }
        private string Purchasers { get; set; }
        private string Revenue { get; set; }

        public PopularProductsReport(string product, string order, string purchasers, string revenue)
        {
            this.Product = product;
            this.Orders = order;
            this.Purchasers = purchasers;
            this.Revenue = revenue;
        }

    }
}
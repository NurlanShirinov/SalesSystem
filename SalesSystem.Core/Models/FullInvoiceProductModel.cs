namespace SalesSystem.Core.Models
{
    public class FullInvoiceProductModel
    {
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public string DiscountDescription { get; set; }
        public DateTime DiscountStartDate { get; set; }
        public DateTime DiscountEndDate { get; set; }
        public int DiscountPercentage { get; set; }
    }
}

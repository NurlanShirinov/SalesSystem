namespace SalesSystem.Core.Models
{
    public class Product : BaseModel
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid DiscountId { get; set; }
        public decimal DiscountPrice { get; set; }
    }
}

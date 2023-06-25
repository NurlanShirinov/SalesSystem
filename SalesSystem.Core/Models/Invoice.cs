namespace SalesSystem.Core.Models
{
    public class Invoice:BaseModel
    {
        public decimal TotalAmount { get; set; }
        public Guid CashierId { get; set; }
        public Guid CustomerId { get; set; }
    }
}

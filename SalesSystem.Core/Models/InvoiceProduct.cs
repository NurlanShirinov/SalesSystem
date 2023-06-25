namespace SalesSystem.Core.Models
{
    public class InvoiceProduct : BaseModel
    {
        public Guid InvoiceId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

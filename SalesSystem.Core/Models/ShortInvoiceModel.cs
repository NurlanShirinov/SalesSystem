namespace SalesSystem.Core.Models
{
    public class ShortInvoiceModel
    {
        public Guid InvoiceId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CashierFullName { get; set; }
        public string CustomerFullName { get; set; }
    }
}

namespace SalesSystem.Core.Models
{
    public class FullInvoiceModel
    {
        public ShortInvoiceModel Invoice { get; set; }
        public IEnumerable<FullInvoiceProductModel> Products { get; set; }
    }
}

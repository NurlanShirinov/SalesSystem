using SalesSystem.Core.Models;

namespace SalesSystem.Core.RequestModels
{
    public class PostInvoiceRequestModel
    {
        public Invoice Invoice { get; set; }
        public IEnumerable<InvoiceProduct> Products { get; set; }
    }
}

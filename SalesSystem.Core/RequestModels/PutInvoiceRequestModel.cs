using SalesSystem.Core.Models;

namespace SalesSystem.Core.RequestModels
{
    public class PutInvoiceRequestModel
    {
        public Invoice Invoice { get; set; }
        public IEnumerable<UpdatedInvoiceProuduct> Products { get; set; }
    }
}

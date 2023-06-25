using SalesSystem.Core.Enums;

namespace SalesSystem.Core.Models
{
    public class UpdatedInvoiceProuduct : InvoiceProduct
    {
        public Guid Id { get; set; } = Guid.Empty;
        public InvoiceProductType Type { get; set; }
    }
}

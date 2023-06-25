using SalesSystem.Core.Models;

namespace SalesSystem.Service.Services.Abstract
{
    public interface IInvoiceProductService 
    {
        Task<Guid> Post(InvoiceProduct product);
        Task<bool> Delete(Guid id);
        Task<bool> DeleteSelected(Guid id);
        Task<InvoiceProduct> Put(InvoiceProduct invoice);
    }
}

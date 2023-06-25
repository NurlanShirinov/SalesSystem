using SalesSystem.Core.Models;

namespace SalesSystem.Repository.CQRS.Commands.Abstract
{
    public interface IInvoiceProductCommand
    {
        Task<Guid> Post(InvoiceProduct product);
        Task<InvoiceProduct> Put(InvoiceProduct invoice);
        Task<bool> Delete(Guid id);
        Task<bool> DeleteSelected(Guid id);
    }
}

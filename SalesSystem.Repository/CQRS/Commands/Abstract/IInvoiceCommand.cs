using SalesSystem.Core.Models;

namespace SalesSystem.Repository.CQRS.Commands.Abstract
{
    public interface IInvoiceCommand
    {
        Task<Guid> Post(Invoice invoice);
        Task<Invoice> Put(Invoice invoice);
        Task<bool> Delete(Guid id);
    }
}
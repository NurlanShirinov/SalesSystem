using SalesSystem.Core.Models;

namespace SalesSystem.Repository.CQRS.Queries.Abstract
{
    public interface IInvoiceQuery
    {
        Task<FullInvoiceModel> GetById(Guid id);
    }
}

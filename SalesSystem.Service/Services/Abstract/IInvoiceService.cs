using SalesSystem.Core.Models;
using SalesSystem.Core.RequestModels;

namespace SalesSystem.Service.Services.Abstract
{
    public interface IInvoiceService
    {
        Task<Guid> Post(PostInvoiceRequestModel model);
        Task<Invoice> Put(PutInvoiceRequestModel model);
        Task<FullInvoiceModel> GetById(Guid id);
        Task<bool> Delete(Guid id);
    }
}

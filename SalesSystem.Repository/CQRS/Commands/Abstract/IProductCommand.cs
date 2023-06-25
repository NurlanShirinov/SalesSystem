using SalesSystem.Core.Models;
using SalesSystem.Core.RequestModels;

namespace SalesSystem.Repository.CQRS.Commands.Abstract
{
    public interface IProductCommand
    {
        Task<Product> Post(ProductRequestModel product);
        Task Put(Product product);
        Task PutQuantity(Product product);
        Task<bool> Delete(Guid id);
        Task ApplyDiscount(ApplyDiscountRequestModel model);
    }
}

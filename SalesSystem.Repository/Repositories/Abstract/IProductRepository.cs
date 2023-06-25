using SalesSystem.Core.Models;
using SalesSystem.Core.RequestModels;

namespace SalesSystem.Repository.Repositories.Abstract
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetByNameAsync(string name);
        Task<Product> Post(ProductRequestModel product);
        Task Put(Product product);
        Task PutQuantity(Product product);
        Task<bool> Delete(Guid id);
        Task ApplyDiscount(ApplyDiscountRequestModel model);
        Task<IEnumerable<Product>> GetProductsByDiscount(Guid discountId);
        Task<int> GetRemainingProductQuary(Guid productId,int quantity);
    }
}

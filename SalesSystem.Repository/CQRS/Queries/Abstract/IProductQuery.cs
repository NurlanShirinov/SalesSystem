using SalesSystem.Core.Models;

namespace SalesSystem.Repository.CQRS.Queries.Abstract
{
    public interface IProductQuery
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetByNameAsync(string name);
        Task<IEnumerable<Product>> GetProductsByDiscount(Guid discountId);
        Task<int> GetRemainingProductQuantity(Guid productId,int quantity);
    }
}

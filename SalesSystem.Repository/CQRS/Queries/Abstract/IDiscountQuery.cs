using SalesSystem.Core.Models;

namespace SalesSystem.Repository.CQRS.Queries.Abstract
{
    public interface IDiscountQuery
    {
        Task<IEnumerable<Discount>> GetAllAsync();
        Task<Discount> GetByIdAsync(Guid id);
    }
}

using SalesSystem.Core.Models;

namespace SalesSystem.Service.Services.Abstract
{
    public interface IDiscountService
    {
        Task<IEnumerable<Discount>> GetAllAsync();
        Task<Discount> GetByIdAsync(Guid id);
        Task<Discount> Post(Discount discount);
        Task<Discount> Put(Discount discount);
        Task<bool> Delete(Guid id);
    }
}

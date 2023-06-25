using SalesSystem.Core.Models;

namespace SalesSystem.Repository.CQRS.Commands.Abstract
{
    public interface IDiscountCommand
    {
        Task<Discount> Post(Discount discount);
        Task<Discount> Put(Discount discount);
        Task<bool> Delete(Guid id);
    }
}

using SalesSystem.Repository.CQRS.Commands.Abstract;
using SalesSystem.Repository.CQRS.Queries.Abstract;

namespace SalesSystem.Repository.Repositories.Abstract
{
    public interface IDiscountRepository : IDiscountQuery, IDiscountCommand
    {
    }
}

using SalesSystem.Core.Models;
using SalesSystem.Repository.CQRS.Commands.Abstract;
using SalesSystem.Repository.CQRS.Queries.Abstract;
using SalesSystem.Repository.Repositories.Abstract;

namespace SalesSystem.Repository.Repositories.Concrete
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IDiscountCommand _command;
        private readonly IDiscountQuery _query;

        public DiscountRepository(IDiscountCommand command, IDiscountQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _command.Delete(id);
            return result;
        }

        public async Task<IEnumerable<Discount>> GetAllAsync()
        {
            var result = await _query.GetAllAsync();
            return result;
        }

        public async Task<Discount> GetByIdAsync(Guid id)
        {
            var result = await _query.GetByIdAsync(id);
            return result;
        }

        public async Task<Discount> Post(Discount discount)
        {
            var result = await _command.Post(discount);
            return result;
        }

        public async Task<Discount> Put(Discount discount)
        {
            var result = await _command.Put(discount);
            return result;
        }
    }
}

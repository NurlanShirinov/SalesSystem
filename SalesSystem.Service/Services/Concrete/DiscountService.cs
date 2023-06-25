using SalesSystem.Core.Models;
using SalesSystem.Repository.Repositories.Abstract;
using SalesSystem.Service.Services.Abstract;

namespace SalesSystem.Service.Services.Concrete
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _repository;

        public DiscountService(IDiscountRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _repository.Delete(id);
            return result;
        }

        public async Task<IEnumerable<Discount>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            return result;
        }

        public async Task<Discount> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result;
        }

        public async Task<Discount> Post(Discount discount)
        {
            var result = await _repository.Post(discount);
            return result;
        }

        public async Task<Discount> Put(Discount discount)
        {
            var result = await _repository.Put(discount);
            return result;
        }
    }
}

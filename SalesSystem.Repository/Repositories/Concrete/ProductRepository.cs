using SalesSystem.Core.Models;
using SalesSystem.Core.RequestModels;
using SalesSystem.Repository.CQRS.Commands.Abstract;
using SalesSystem.Repository.CQRS.Queries.Abstract;
using SalesSystem.Repository.Repositories.Abstract;

namespace SalesSystem.Repository.Repositories.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductQuery _productQuery;
        private readonly IProductCommand _productCommand;

        public ProductRepository(IProductQuery productQuery, IProductCommand productCommand)
        {
            _productQuery = productQuery;
            _productCommand = productCommand;
        }

        public async Task ApplyDiscount(ApplyDiscountRequestModel model)
        {
            await _productCommand.ApplyDiscount(model);
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _productCommand.Delete(id);
            return result;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var result = await _productQuery.GetAllAsync();
            return result;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var result = await _productQuery.GetByIdAsync(id);
            return result;
        }

        public async Task<IEnumerable<Product>> GetByNameAsync(string name)
        {
            var result = await _productQuery.GetByNameAsync(name);
            return result;
        }

        public async Task<IEnumerable<Product>> GetProductsByDiscount(Guid discountId)
        {
            var result = await _productQuery.GetProductsByDiscount(discountId);
            return result;
        }

        public async Task<int> GetRemainingProductQuary(Guid productId, int quantity)
        {
            var res = await _productQuery.GetRemainingProductQuantity(productId, quantity);
            return res;
        }

        public async Task<Product> Post(ProductRequestModel product)
        {
            var result = await _productCommand.Post(product);
            return result;
        }

        public async Task Put(Product product)
        {
            await _productCommand.Put(product);
        }

        public async Task PutQuantity(Product product)
        {
            await _productCommand.PutQuantity(product);
        }
    }
}

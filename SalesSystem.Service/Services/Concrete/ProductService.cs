using SalesSystem.Core.Models;
using SalesSystem.Core.RequestModels;
using SalesSystem.Repository.Repositories.Abstract;
using SalesSystem.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Service.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task ApplyDiscount(ApplyDiscountRequestModel model)
        {
            await _productRepository.ApplyDiscount(model);
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _productRepository.Delete(id);
            return result;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var result = await _productRepository.GetAllAsync();
            return result;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var result = await _productRepository.GetByIdAsync(id);
            return result;
        }

        public async Task<IEnumerable<Product>> GetByNameAsync(string name)
        {
            var result = await _productRepository.GetByNameAsync(name);
            return result;
        }

        public async Task<IEnumerable<Product>> GetProductsByDiscount(Guid discountId)
        {
            var result = await _productRepository.GetProductsByDiscount(discountId);
            return result;
        }

        public async Task<Product> Post(ProductRequestModel product)
        {
            var result = await _productRepository.Post(product);
            return result;
        }

        public async Task Put(Product product)
        {
            await _productRepository.Put(product);
        }
    }
}

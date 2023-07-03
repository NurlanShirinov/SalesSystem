using SalesSystem.Core.Models;
using SalesSystem.Repository.Repositories.Abstract;
using SalesSystem.Service.Services.Abstract;

namespace SalesSystem.Service.Services.Concrete
{
    public class InvoiceProductService : IInvoiceProductService
    {
        private readonly IInvoiceProductRepository _invoiceProductRepository;
        private readonly IProductRepository _productRepository;

        public InvoiceProductService(IInvoiceProductRepository invoiceProductRepository, IProductRepository productRepository)
        {
            _invoiceProductRepository = invoiceProductRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _invoiceProductRepository.Delete(id);
            return result;
        }

        public async Task<bool> DeleteSelected(Guid id)
        {
            var result = await _invoiceProductRepository.DeleteSelected(id);
            return result;
        }

        public async Task<Guid> Post(InvoiceProduct product)
        {
            var remainingQuantity = await _productRepository.GetRemainingProductQuary(product.ProductId, product.Quantity);

            if (remainingQuantity >= 0)
            {
                var result = await _invoiceProductRepository.Post(product);

                await _productRepository.PutQuantity(new Product
                {
                    Id = product.ProductId,
                    Quantity = remainingQuantity
                });

                return result;
            }
            else
            {
                throw new Exception($"You dont have {product.Quantity} product in your stock");
            }
        }

        public async Task<InvoiceProduct> Put(InvoiceProduct product)
        {
            var remainingQuantity = await _productRepository.GetRemainingProductQuary(product.ProductId, product.Quantity);

            if (remainingQuantity >= 0)
            {
                await _invoiceProductRepository.Put(product);
                return product;
            }
            else
            {
                throw new Exception($"You dont have {product.Quantity} product in your stock");
            }
        }
    }
}

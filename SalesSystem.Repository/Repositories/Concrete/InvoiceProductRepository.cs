using SalesSystem.Core.Models;
using SalesSystem.Repository.CQRS.Commands.Abstract;
using SalesSystem.Repository.Repositories.Abstract;

namespace SalesSystem.Repository.Repositories.Concrete
{
    public class InvoiceProductRepository : IInvoiceProductRepository
    {
        private readonly IInvoiceProductCommand _productCommand;

        public InvoiceProductRepository(IInvoiceProductCommand productCommand)
        {
            _productCommand = productCommand;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _productCommand.Delete(id);
            return result;
        }

        public async Task<bool> DeleteSelected(Guid id)
        {
            var result = await _productCommand.DeleteSelected(id);
            return result;
        }

        public async Task<Guid> Post(InvoiceProduct product)
        {
            var result = await _productCommand.Post(product);
            return result;
        }

        public async Task<InvoiceProduct> Put(InvoiceProduct invoice)
        {
            await _productCommand.Put(invoice);
            return invoice;
        }
    }
}

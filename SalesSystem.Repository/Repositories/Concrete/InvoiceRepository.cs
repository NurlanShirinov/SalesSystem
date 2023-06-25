using SalesSystem.Core.Models;
using SalesSystem.Repository.CQRS.Commands.Abstract;
using SalesSystem.Repository.CQRS.Queries.Abstract;
using SalesSystem.Repository.Repositories.Abstract;

namespace SalesSystem.Repository.Repositories.Concrete
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IInvoiceCommand _invoiceCommand;
        private readonly IInvoiceQuery _invoiceQuery;

        public InvoiceRepository(IInvoiceCommand invoiceCommand, IInvoiceQuery invoiceQuery)
        {
            _invoiceCommand = invoiceCommand;
            _invoiceQuery = invoiceQuery;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _invoiceCommand.Delete(id);
            return result;
        }

        public async Task<FullInvoiceModel> GetById(Guid id)
        {
            var res = await _invoiceQuery.GetById(id);
            return res;
        }

        public async Task<Guid> Post(Invoice invoice)
        {
            var result = await _invoiceCommand.Post(invoice);
            return result;
        }

        public async Task<Invoice> Put(Invoice invoice)
        {
            await _invoiceCommand.Put(invoice);
            return invoice;
        }
    }
}
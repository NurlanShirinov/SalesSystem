using SalesSystem.Core.Enums;
using SalesSystem.Core.Models;
using SalesSystem.Core.RequestModels;
using SalesSystem.Repository.Repositories.Abstract;
using SalesSystem.Service.Services.Abstract;

namespace SalesSystem.Service.Services.Concrete
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceProductService _invoiceProductService;

        public InvoiceService(IInvoiceRepository invoiceRepository, IInvoiceProductService invoiceProductService)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceProductService = invoiceProductService;
        }

        public async Task<bool> Delete(Guid id)
        {
            var res = await _invoiceRepository.Delete(id);
            var resPr = await _invoiceProductService.Delete(id);

            return res && resPr;
        }

        public async Task<FullInvoiceModel> GetById(Guid id)
        {
            var res = await _invoiceRepository.GetById(id);

            foreach (var product in res.Products)
            {
                if(DateTime.Now >= product.DiscountStartDate && DateTime.Now <= product.DiscountEndDate)
                {
                    product.DiscountPrice = product.ProductPrice - (product.ProductPrice * product.DiscountPercentage) / 100;
                }

                res.Invoice.TotalAmount += product.DiscountPrice == 0 ? product.ProductPrice : product.DiscountPrice * product.ProductQuantity;
            }

            return res;
        }

        public async Task<Guid> Post(PostInvoiceRequestModel model)
        {
            var result = await _invoiceRepository.Post(model.Invoice);

            foreach (var product in model.Products)
            {
                product.InvoiceId = result;
                await _invoiceProductService.Post(product);
            }

            return result;
        }

        public async Task<Invoice> Put(PutInvoiceRequestModel model)
        {
            var result = await _invoiceRepository.Put(model.Invoice);

            foreach (var product in model.Products)
            {
                switch (product.Type)
                {
                    case InvoiceProductType.Added:
                        product.InvoiceId = result.Id;
                        await _invoiceProductService.Post(product);
                        break;
                    case InvoiceProductType.Updated:
                        await _invoiceProductService.Put(product);
                        break;
                    case InvoiceProductType.Deleted:
                        await _invoiceProductService.DeleteSelected(product.Id);
                        break;
                    default:
                        break;
                }
            }

            return result;
        }


    }
}

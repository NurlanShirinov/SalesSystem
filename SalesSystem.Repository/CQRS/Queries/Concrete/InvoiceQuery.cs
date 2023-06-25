using Dapper;
using SalesSystem.Core.Models;
using SalesSystem.Repository.CQRS.Queries.Abstract;
using SalesSystem.Repository.Infrastructure;

namespace SalesSystem.Repository.CQRS.Queries.Concrete
{
    public class InvoiceQuery : IInvoiceQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly string _getByIdSql = $@"SELECT I.Id InvoiceId, I.TotalAmount,I.CreatedDate,
                                                 CA.Name + ' ' + CA.Surname CashierFullName,
                                                 CU.Name + ' ' + CU.Surname CustomerFullName
                                                 FROM INVOICES I
                                                 LEFT JOIN Users CA ON CA.Id = I.CashierId
                                                 LEFT JOIN Users CU ON CU.Id = I.CustomerId
                                                 WHERE I.ID = @id
                                                 SELECT P.Name ProductName, INP.Quantity ProductQuantity,P.Price ProductPrice,
                                                 D.Description DiscountDescription, D.StartDate DiscountStartDate, D.EndDate DiscountEndDate,
                                                 D.Percentage DiscountPercentage
                                                 FROM INVOICES I
                                                 LEFT JOIN INVOICEPRODUCTS INP ON I.ID = INP.INVOICEID
                                                 LEFT JOIN PRODUCTS P ON INP.PRODUCTID = P.ID
                                                 LEFT JOIN Users CA ON CA.Id = I.CashierId
                                                 LEFT JOIN Users CU ON CU.Id = I.CustomerId
                                                 LEFT JOIN Discounts D ON D.Id = P.DiscountId
                                                 WHERE I.ID = @id";

        public async Task<FullInvoiceModel> GetById(Guid id)
        {
            var resGrid = await _unitOfWork.GetConnection().QueryMultipleAsync(_getByIdSql, new { id }, _unitOfWork.GetTransaction());

            var res = new FullInvoiceModel
            {
                Invoice = resGrid.ReadFirst<ShortInvoiceModel>(),
                Products = resGrid.Read<FullInvoiceProductModel>()
            };

            return res;
        }
    }
}

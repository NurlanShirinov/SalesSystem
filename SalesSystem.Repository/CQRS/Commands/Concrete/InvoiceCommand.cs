using Dapper;
using SalesSystem.Core.Models;
using SalesSystem.Repository.CQRS.Commands.Abstract;
using SalesSystem.Repository.Infrastructure;

namespace SalesSystem.Repository.CQRS.Commands.Concrete
{
    public class InvoiceCommand : IInvoiceCommand
    {

        private readonly IUnitOfWork _unitOfWork;

        public InvoiceCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string _addSql = $@"INSERT INTO Invoices([CashierId],[CustomerId],[TotalAmount])
                                    OUTPUT inserted.Id
                                    VALUES(@{nameof(Invoice.CashierId)},
                                           @{nameof(Invoice.CustomerId)},
                                           @{nameof(Invoice.TotalAmount)})";

        private string _deleteSql = $@"UPDATE Invoices
                                        SET DeleteStatus = 1
                                        Where Id=@id";

        private string _updateSql = $@"UPDATE Invoices
                                       SET CustomerId = @{nameof(Invoice.CustomerId)}
                                        WHERE Id = @id";


        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<bool>(_deleteSql, new { id }, _unitOfWork.GetTransaction());
                return result != null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Guid> Post(Invoice invoice)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Guid>(_addSql, invoice, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Invoice> Put(Invoice invoice)
        {
            try
            {
                await _unitOfWork.GetConnection().QueryAsync(_updateSql, invoice, _unitOfWork.GetTransaction());
                return invoice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

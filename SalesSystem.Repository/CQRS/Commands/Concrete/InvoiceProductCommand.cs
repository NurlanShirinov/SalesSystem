using Dapper;
using SalesSystem.Core.Models;
using SalesSystem.Repository.CQRS.Commands.Abstract;
using SalesSystem.Repository.Infrastructure;

namespace SalesSystem.Repository.CQRS.Commands.Concrete
{
    public class InvoiceProductCommand : IInvoiceProductCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly string _addSql = $@"INSERT INTO InvoiceProducts(InvoiceId,ProductId,Quantity)
                                             VALUES(@{nameof(InvoiceProduct.InvoiceId)},
                                                    @{nameof(InvoiceProduct.ProductId)},
                                                    @{nameof(InvoiceProduct.Quantity)})";


        private readonly string _deleteSql = $@"UPDATE InvoiceProducts
                                                SET DeleteStatus = 1 
                                                WHERE InvoiceId = @id";

        private readonly string _deleteSelectedSql = $@"UPDATE InvoiceProducts
                                                        SET DeleteStatus = 1
                                                        WHERE Id = @id";

        private readonly string _updateSql = $@"UPDATE InvoiceProducts
                                                SET Quantity = @{nameof(InvoiceProduct.Quantity)}
                                                WHERE Id = @id";

        public InvoiceProductCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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

        public async Task<bool> DeleteSelected(Guid id)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<bool>(_deleteSelectedSql, new { id }, _unitOfWork.GetTransaction());
                return result != null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Guid> Post(InvoiceProduct product)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Guid>(_addSql, product, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<InvoiceProduct> Put(InvoiceProduct invoice)
        {
            try
            {
                await _unitOfWork.GetConnection().QueryAsync(_updateSql, invoice, _unitOfWork.GetTransaction());
                return invoice;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

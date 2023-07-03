using Dapper;
using SalesSystem.Core.Models;
using SalesSystem.Repository.CQRS.Queries.Abstract;
using SalesSystem.Repository.Infrastructure;

namespace SalesSystem.Repository.CQRS.Queries.Concrete
{
    public class DiscountQuery : IDiscountQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly string _getAllSql = $@"SELECT * FROM Discounts WHERE DeleteStatus = 0";

        private readonly string _getByIdSql = $@"SELECT * FROM Discounts WHERE Id = @id";

        public DiscountQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Discount>> GetAllAsync()
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<Discount>(_getAllSql, null, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Discount> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Discount>(_getByIdSql, new { id }, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using Dapper;
using SalesSystem.Core.Models;
using SalesSystem.Repository.CQRS.Queries.Abstract;
using SalesSystem.Repository.Infrastructure;

namespace SalesSystem.Repository.CQRS.Queries.Concrete
{
    public class ProductQuery : IProductQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string _getAllSql = $@"SELECT P.*  FROM Products P
                                       WHERE P.DeleteStatus = 0";

        private string _getByIdSql = $@"SELECT P.* FROM Products P
                                        WHERE P.Id=@id AND P.DeleteStatus = 0";

        private string _getByNameSql = $@"DECLARE  @searchText NVARCHAR(max)
                                        SET @searchText = '%'+  @name +'%'
                                        SELECT P.*  FROM Products P
                                        WHERE P.Name LIKE @searchText and P.DeleteStatus=0";


        private string _getProductsByDiscount = $@"SELECT P.*  FROM Products P
                                                   WHERE P.DiscountId = @discountId AND P.DeleteStatus = 0";

        private string _getRemainingProduct = $@"SELECT Quantity - @quantity RemainingQuantity FROM Products
                                                 WHERE Id = @productId";

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<Product>(_getAllSql, null, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Product>(_getByIdSql, new { id }, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetByNameAsync(string name)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<Product>(_getByNameSql, new { name }, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByDiscount(Guid discountId)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<Product>(_getProductsByDiscount, new { discountId }, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetRemainingProductQuantity(Guid productId, int quantity)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<int>(_getRemainingProduct, new { productId, quantity }, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

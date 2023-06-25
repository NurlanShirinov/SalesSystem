using Dapper;
using SalesSystem.Core.Models;
using SalesSystem.Core.RequestModels;
using SalesSystem.Repository.CQRS.Commands.Abstract;
using SalesSystem.Repository.Infrastructure;

namespace SalesSystem.Repository.CQRS.Commands.Concrete
{
    public class ProductCommand : IProductCommand
    {

        private readonly IUnitOfWork _unitOfWork;

        public ProductCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string _addSql = $@"DECLARE @MyTableVar table(CurrentId uniqueidentifier);

                                        INSERT INTO PRODUCTS ([Name],[Price],[Quantity])
                                        OUTPUT inserted.Id 
                                        	INTO @MyTableVar
                                        VALUES ( @{nameof(Product.Name)},
                                                 @{nameof(Product.Price)},
                                                 @{nameof(Product.Quantity)})  

                                    SELECT TOP(1) * FROM PRODUCTS WHERE ID IN (SELECT * FROM @MyTableVar)";


        private string _updateSql = $@"UPDATE PRODUCTS
                                           SET Name = @{nameof(Product.Name)},
                                            Price = @{nameof(Product.Price)},
                                            Quantity = @{nameof(Product.Quantity)}
                                           WHERE Id=@id";

        private string _updateQuantitySql = $@"UPDATE PRODUCTS
                                            SET Quantity = @Quantity
                                            WHERE Id=@Id";

        private string _deleteSql = $@"UPDATE PRODUCTS SET DeleteStatus = 1 WHERE Id=@id";

        private string _applyDiscountSql = $@"UPDATE PRODUCTS
                                              SET DiscountId = @DiscountId
                                              WHERE Id IN @ProductId";


        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<bool>(_deleteSql, new { id }, _unitOfWork.GetTransaction());
                return result !=null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Product> Post(ProductRequestModel product)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Product>(_addSql, product, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Put(Product product)
        {
            try
            {
                await _unitOfWork.GetConnection().QueryAsync(_updateSql, product, _unitOfWork.GetTransaction());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PutQuantity(Product product)
        {
            try
            {
                await _unitOfWork.GetConnection().QueryAsync(_updateQuantitySql, product, _unitOfWork.GetTransaction());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ApplyDiscount(ApplyDiscountRequestModel model)
        {
            try
            {
                await _unitOfWork.GetConnection().QueryAsync(_applyDiscountSql, model, _unitOfWork.GetTransaction());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

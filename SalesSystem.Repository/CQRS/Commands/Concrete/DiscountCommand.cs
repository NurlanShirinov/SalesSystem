using Dapper;
using SalesSystem.Core.Models;
using SalesSystem.Repository.CQRS.Commands.Abstract;
using SalesSystem.Repository.Infrastructure;

namespace SalesSystem.Repository.CQRS.Commands.Concrete
{
    public class DiscountCommand : IDiscountCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly string _addSql = $@"
                            DECLARE @MyTableVar table(CurrentId uniqueidentifier);

                            INSERT INTO Discounts                                                              (StartDate,EndDate,Description,Percentage)
                                        OUTPUT inserted.Id
                                            INTO @MyTableVar
                                            VALUES(@{nameof(Discount.StartDate)},
                                                   @{nameof(Discount.EndDate)},
                                                   @{nameof(Discount.Description)},
                                                   @{nameof(Discount.Percentage)})

                                        SELECT TOP(1) * FROM Discounts WHERE ID IN (SELECT * FROM @MyTableVar)";

        private readonly string _putSql = @$"UPDATE Discounts
                                             SET StartDate = @{nameof(Discount.StartDate)},
                                                 EndDate = @{nameof(Discount.EndDate)},
                                                 Description = @{nameof(Discount.Description)},
                                                 Percentage = @{nameof(Discount.Percentage)}

                                                WHERE Id = @id";

        private readonly string _deleteSql = $@"UPDATE Discounts SET DeleteStatus = 1
                                                WHERE Id = @id";

        public DiscountCommand(IUnitOfWork unitOfWork)
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

        public async Task<Discount> Post(Discount discount)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Discount>(_addSql, discount, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Discount> Put(Discount discount)
        {
            try
            {
                await _unitOfWork.GetConnection().QueryAsync(_putSql, discount, _unitOfWork.GetTransaction());

                return discount;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

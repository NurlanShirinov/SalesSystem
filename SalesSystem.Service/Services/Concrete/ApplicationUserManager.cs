using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SalesSystem.Core.Models;
using SalesSystem.Repository.Infrastructure;
using SalesSystem.Service.Services.Abstract;

namespace SalesSystem.Service.Services.Concrete
{
    public class ApplicationUserManager : UserManager<User>
    {
        private readonly IApplicationUserStore<User> _userStore;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserManager(IApplicationUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger,
            IUnitOfWork unitOfWork) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators,
            keyNormalizer, errors, services, logger)
        {
            _userStore = store;
            _unitOfWork = unitOfWork;
        }

        private readonly string _updateUserSql = $@"UPDATE Users SET 
                      
                       WHERE Id=@{nameof(User.Id)}

                       SELECT * FROM Users WHERE Id=@{nameof(User.Id)} AND DeleteStatus=0";

        private readonly string _getByIdSql = $@"SELECT U.*
                                                 FROM Users U
                                                 WHERE U.Id=@id";

        private readonly string _setPasswordHash = $@"UPDATE Users SET PasswordHash=@newPasswordHash WHERE Id=@Id";

        public async Task<User> GetById(string id)
        {
            try
            {
                var result = await _unitOfWork.GetConnection()
                    .QuerySingleAsync<User>(_getByIdSql, new { id }, _unitOfWork.GetTransaction());

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<User> FindByEmail(string email)
        {
            var res = await _userStore.FindByEmail(email);
            return res;
        }



        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var res = await _userStore.GetAllAsync();
            return res;
        }

        public async Task<IdentityResult> UpdateTokenAsync(User user)
        {
            var res = await _userStore.UpdateTokenAsync(user);
            return res;
        }

        public async Task<User> UpdateUser(User user)
        {
            try
            {
                var result = await _unitOfWork.GetConnection()
                    .QuerySingleAsync<User>(_updateUserSql, user, _unitOfWork.GetTransaction());


                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdatePasswordHash(string id, string newPasswordHash)
        {
            try
            {
                await _unitOfWork.GetConnection()
                    .QueryFirstOrDefaultAsync(_setPasswordHash, new { id, newPasswordHash },
                        _unitOfWork.GetTransaction());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

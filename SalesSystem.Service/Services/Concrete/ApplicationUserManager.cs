using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SalesSystem.Core.Models;
using SalesSystem.Repository.Infrastructure;
using SalesSystem.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private readonly string _updateUserSql = $@"UPDATE AspNetUsers SET 
                      
                       WHERE Id=@{nameof(User.Id)}

                       SELECT * FROM AspNetUsers WHERE Id=@{nameof(User.Id)} AND DeleteStatus=0";

        private readonly string _getByIdSql = $@"SELECT ANU.*, RG.Name RoleGroupName, C.Name CompanyName, C.Id CompanyId
                                                 FROM AspNetUsers ANU
                                                 LEFT JOIN RoleGroups RG ON RG.Id = ANU.RoleGroupId
                                                 LEFT JOIN Companies C on ANU.CompanyId = C.Id WHERE ANU.Id=@id";

        private readonly string _setPasswordHash = $@"UPDATE AspNetUsers SET PasswordHash=@newPasswordHash WHERE Id=@Id";

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

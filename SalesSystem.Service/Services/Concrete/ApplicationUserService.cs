using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SalesSystem.Core.Models;
using SalesSystem.Repository.Infrastructure;
using SalesSystem.Service.Services.Abstract;

namespace SalesSystem.Service.Services.Concrete
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string secretKey;
        private readonly IConfiguration _configuration;

        public ApplicationUserService(ApplicationUserManager userManager, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            this.secretKey = _configuration.GetSection("SecretKey").Value;
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            try
            {
                var currUser = await _userManager.FindByIdAsync(user.Id.ToString());
                var result = await _userManager.CheckPasswordAsync(currUser, password);
                if (!result)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IdentityResult> CreateAsync(User model, string password)
        {
            if (model is null) return IdentityResult.Failed(new IdentityError() { Code = "400", Description = "Value cannot be null" });

            User user = await _userManager.FindByNameAsync(model.Username);

            if (user is not null) return IdentityResult.Failed(new IdentityError() { Code = "400", Description = "usernameExist" });

            user = await _userManager.FindByEmailAsync(model.Email);

            if (user is not null) return IdentityResult.Failed(new IdentityError() { Code = "400", Description = "emailExist" });

            var now = DateTime.Now;

            var passwordErros = await ValidatePassword(password);

            if (passwordErros != null)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "400", Description = passwordErros });
            }

            try
            {
                var result = await _userManager.CreateAsync(model, password);

                var errors = result.Errors;

                if (errors.ToList().FirstOrDefault(i => i.Description.Contains("Email")) != null)
                {
                    return IdentityResult.Failed(new IdentityError() { Code = "400", Description = "emailExist" });
                }

                var currentUser = await _userManager.FindByEmail(model.Email);

                //await _roleManager.AddUserToRoleAsync(currentUser.Id, roleId);

            }
            catch (Exception e)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "400", Description = e.Message });
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user)
        {
            var res = await _userManager.DeleteAsync(user);
            return res;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            var res = await _userManager.FindByEmailAsync(email);
            return res;
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            var res = await _userManager.FindByIdAsync(userId);
            return res;
        }

        public async Task<User> FindByNameAsync(string normalizedUserName)
        {
            var res = await _userManager.FindByNameAsync(normalizedUserName);
            return res;
        }

        public async Task<IdentityResult> UpdateAsync(User model)
        {
            if (model is null) return IdentityResult.Failed(new IdentityError() { Code = "400", Description = "Value cannot be null" });

            User user = await _userManager.FindByNameAsync(model.Username);

            if (user is not null && !String.Equals(user?.Id.ToString(), model.Id.ToString(), StringComparison.CurrentCultureIgnoreCase)) return IdentityResult.Failed(new IdentityError() { Code = "400", Description = "username_exist" });

            user = await _userManager.FindByEmailAsync(model.Email);

            if (user is not null && !String.Equals(user?.Id.ToString(), model.Id.ToString(), StringComparison.CurrentCultureIgnoreCase)) return IdentityResult.Failed(new IdentityError() { Code = "400", Description = "email_exist" });

            var now = DateTime.Now;
            try
            {
                await _userManager.UpdateUser(model);
                
                var passwordErros = await ValidatePassword(model.Password);

                if (passwordErros != null)
                {
                    return IdentityResult.Failed(new IdentityError() { Code = "400", Description = passwordErros });
                }


                var newPasswordHash = _userManager.PasswordHasher.HashPassword(model, model.Password);
                await _userManager.UpdatePasswordHash(model.Id.ToString(), newPasswordHash);

                return IdentityResult.Success;
            }
            catch (Exception e)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Description = e.Message
                });
            }
        }

        public async Task<IdentityResult> UpdateTokenAsync(User user)
        {
            var res = await _userManager.UpdateTokenAsync(user);
            return res;
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            var res = await _userManager.GetById(userId);
            return res;
        }

        public async Task<string> ValidatePassword(string password)
        {
            List<string> passwordErrors = new List<string>();

            var validators = _userManager.PasswordValidators;

            foreach (var validator in validators)
            {
                var validation = await validator.ValidateAsync(_userManager, null, password);

                if (!validation.Succeeded)
                {
                    foreach (var error in validation.Errors)
                    {
                        passwordErrors.Add(error.Description);
                    }
                }
            }

            var result = passwordErrors.Count > 0 ? passwordErrors.Aggregate((i, j) => i + "\n" + j) : null;

            return result;
        }
    }
}

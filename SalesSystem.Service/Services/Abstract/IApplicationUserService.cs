using Microsoft.AspNetCore.Identity;
using SalesSystem.Core.Models;

namespace SalesSystem.Service.Services.Abstract
{
    public interface IApplicationUserService
    {
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> DeleteAsync(User user);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByIdAsync(string userId);
        Task<User> FindByNameAsync(string normalizedUserName);
        Task<IdentityResult> UpdateAsync(User user);
        Task<string> ValidatePassword(string password);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<IdentityResult> UpdateTokenAsync(User user);
        Task<User> GetByIdAsync(string userId);
    }
}

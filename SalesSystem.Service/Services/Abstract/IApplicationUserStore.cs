using Microsoft.AspNetCore.Identity;
using SalesSystem.Core.Models;

namespace SalesSystem.Service.Services.Abstract
{
    public interface IApplicationUserStore<TUser> : IUserStore<TUser> where TUser : class
    {
        Task<User> FindByEmail(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IdentityResult> UpdateTokenAsync(User user);
    }
}

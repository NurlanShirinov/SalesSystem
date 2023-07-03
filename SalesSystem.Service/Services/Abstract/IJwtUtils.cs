using SalesSystem.Core.Models;

namespace SalesSystem.Service.Services.Abstract
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public Guid? ValidateJwtToken(string? token);
    }
}

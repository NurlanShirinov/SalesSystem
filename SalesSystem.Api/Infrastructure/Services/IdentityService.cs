using Microsoft.AspNetCore.Identity;
using SalesSystem.Core.Models;
using SalesSystem.Service.Services.Abstract;
using SalesSystem.Service.Services.Concrete;

namespace SalesSystem.API.Infrastructure.Services
{
    public static class IdentityService
    {
        public static IServiceCollection AddIdentityDependency(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IApplicationUserStore<User>, ApplicationUserStore>();
            services.AddTransient<IUserStore<User>, ApplicationUserStore>();


            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
            }).AddUserManager<ApplicationUserManager>()
            .AddDefaultTokenProviders();

            services.AddTransient<IApplicationUserService, ApplicationUserService>();

            return services;
        }
    }
}

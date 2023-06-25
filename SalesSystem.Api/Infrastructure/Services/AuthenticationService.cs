using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SalesSystem.Api.Infrastructure.Services
{
    public static class AuthenticationService
    {
        public static IServiceCollection AddAuthenticationDependency(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = configuration["JwtSettings:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!)),
                        ValidAudience = configuration["JwtSettings:ValidAudience"],
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true
                    };
                    cfg.IncludeErrorDetails = true;
                    cfg.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            var te = context.Exception;
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {

                            return Task.CompletedTask;
                        }
                    };
                }).Services.ConfigureApplicationCookie(
                    options =>
                    {
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(double.Parse(configuration["JwtSettings:TokenValidityInMinutes"]!));
                    });

            return services;
        }
    }
}

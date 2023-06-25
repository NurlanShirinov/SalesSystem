using Microsoft.Extensions.DependencyInjection.Extensions;
using NetCore.AutoRegisterDi;
using SalesSystem.Api.Infrastructure.Services;
using SalesSystem.API.Infrastructure.Services;
using SalesSystem.Repository.Infrastructure;
using SalesSystem.Repository.Repositories.Concrete;
using SalesSystem.Service.Services.Concrete;
using System.Reflection;

namespace SalesSystem.API.Infrastructure
{
    public static class ProjectDependencies
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services,
    IConfiguration configuration)
        {
            var repositoryAssembly = Assembly.GetAssembly(typeof(ProductRepository));

            services.RegisterAssemblyPublicNonGenericClasses(repositoryAssembly)
                .Where(c => c.Name.EndsWith("Repository"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.RegisterAssemblyPublicNonGenericClasses(repositoryAssembly)
                .Where(c => c.Name.EndsWith("Command"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.RegisterAssemblyPublicNonGenericClasses(repositoryAssembly)
                .Where(c => c.Name.EndsWith("Query"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            var serviceAssembly = Assembly.GetAssembly(typeof(ProductService));

            services.RegisterAssemblyPublicNonGenericClasses(serviceAssembly)
                .Where(c => c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddCorsDependency(configuration);
            services.AddIdentityDependency(configuration);
            services.AddAuthenticationDependency(configuration);
            services.AddSwaggerDependency(configuration);
            services.AddControllers();


            return services;
        }
    }
}

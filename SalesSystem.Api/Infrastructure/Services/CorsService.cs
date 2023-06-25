namespace SalesSystem.API.Infrastructure.Services
{
    public static class CorsService
    {

        public static IServiceCollection AddCorsDependency(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}

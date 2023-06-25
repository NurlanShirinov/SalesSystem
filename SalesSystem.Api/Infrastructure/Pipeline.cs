
using SalesSystem.Api.Infrastructure.Middleware;

namespace SalesSystem.Api.Infrastructure
{
    public static class Pipeline
    {
        public static IApplicationBuilder AddPipeline(this IApplicationBuilder builder, WebApplication app, IConfiguration configuration)
        {
          
                app.UseSwagger();
                app.UseSwaggerUI();
          
            app.UseStaticFiles();
            app.UseCors("EnableCORS");
            app.UseHttpsRedirection();
           

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseRouting();
            // Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            return builder;
        }

    }
}

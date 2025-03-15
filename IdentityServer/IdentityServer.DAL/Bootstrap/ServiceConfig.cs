using IdentityServer.DAL.Context;
using IdentityServer.DAL.Context.Interfaces;
using IdentityServer.DAL.Repositories;
using IdentityServer.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace IdentityServer.DAL.Bootstrap
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            AddContext(services, configuration);
            RegisterContexts(services);
            RegisterRepositories(services);


            return services;
        }

        private static void AddContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );
        }

        private static void RegisterContexts(IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>()
                .AddScoped<IUserContext,ApplicationDbContext>();

        }
        
        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}

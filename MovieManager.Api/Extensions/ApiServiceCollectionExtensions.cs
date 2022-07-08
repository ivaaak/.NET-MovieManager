using Microsoft.EntityFrameworkCore;
using MovieManager.Api.ApiServices;
using MovieManager.Api.ApiServices.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiServiceCollectionExtension
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            //services.AddScoped<IApplicationDbRepository, ApplicationDbRepository>();
            services.AddScoped<IGetFromDbService, GetFromDbService>();

            return services;
        }

        public static IServiceCollection AddApiDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<MovieContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenServices, TokenServices>();

            services.AddDbContext<DataContext>(options =>
                 options.UseSqlite(
                     config.GetConnectionString("DatingAppConnection")));
            

            return services;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using WebApp.Core.Domain.Repositores;
using WebApp.Core.Domain.Repositories;
using WebApp.Core.Domain.Repositories.Managment;
using WebApp.Core.Infrastructure.Repositores;
using WebApp.Core.Infrastructure.Repositores.Managment;
using WebApp.Core.Infrastructure.Services;


namespace WebApp.Core.Infrastructure
{
    public static class DependancyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        }
    }

}

using Microsoft.AspNetCore.Identity;
using WebApp.Core.Domain.AggregatesModel.Managment.UsersAggregator;

namespace WebApp.Core.Infrastructure.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(ApplicationUser User);
    }
}

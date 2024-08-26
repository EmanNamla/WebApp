using Microsoft.AspNetCore.Identity;
using WebApp.Core.Domain.AggregatesModel.Managment.UsersAggregator;

namespace WebApp.Core.Domain.Repositories.Managment
{
    public interface IApplicationUserRepository
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUser? user, string password);
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<ApplicationUser> GetUserByPhoneNumberAsync(string phoneNumber);
        Task<bool> CheckPassword(ApplicationUser? user, string password);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
    }
}

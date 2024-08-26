using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Core.Domain.AggregatesModel.Managment.UsersAggregator;
using WebApp.Core.Domain.Repositories.Managment;

namespace WebApp.Core.Infrastructure.Repositores.Managment
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser?> _userManager;

        public ApplicationUserRepository(UserManager<ApplicationUser?> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> CreateUserAsync(ApplicationUser? user, string password)
        => await _userManager.CreateAsync(user, password);

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        => await _userManager.Users.SingleOrDefaultAsync(u =>
           u.Email.ToLower() == email.ToLower().Trim());

        public async Task<ApplicationUser> GetUserByPhoneNumberAsync(string phoneNumber)
       => await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

        public async Task<bool> CheckPassword(ApplicationUser? user, string password) 
       => await _userManager.CheckPasswordAsync(user, password);

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        => await _userManager.FindByIdAsync(userId);
    }
}

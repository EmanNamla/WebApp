using Microsoft.AspNetCore.Identity;
using REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator;
using WebApp.Core.Domain.AggregatesModel.Managment.UsersAggregator.DomainModel;
using WebApp.Framwork.Domain.Interfaces;

namespace WebApp.Core.Domain.AggregatesModel.Managment.UsersAggregator
{
    public class ApplicationUser : IdentityUser, IAggregateRoot
    {

        #region Constractors
        protected ApplicationUser()
        {
                
        }

        protected ApplicationUser(RegisterUserDto dto)
        {
            UserName = dto.UserName;
            Email = dto.Email;
            PhoneNumber = dto.PhoneNumber;
            DateOfBirth = dto.DateOfBirth;
            PasswordHash = dto.Password;

        }
        #endregion

        #region Public Propertie
        public virtual DateTime DateOfBirth { get; set; }
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        #endregion


        #region  Business Logic

        #endregion

        #region Factory Method
        public static ApplicationUser Create(RegisterUserDto dto) => new ApplicationUser(dto);
        #endregion
    }
}

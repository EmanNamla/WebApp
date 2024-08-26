namespace WebApp.Core.Domain.AggregatesModel.Managment.UsersAggregator.DomainModel
{
    public class RegisterUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
    }
}

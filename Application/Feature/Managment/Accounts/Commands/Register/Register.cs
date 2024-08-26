using FluentValidation;
using Mapster;
using MediatR;
using WebApp.Core.Domain.AggregatesModel.Managment.UsersAggregator;
using WebApp.Core.Domain.AggregatesModel.Managment.UsersAggregator.DomainModel;
using WebApp.Core.Domain.AggregatesModel.Managment.UsersAggregator.DomainResult;
using WebApp.Core.Domain.Repositories.Managment;

namespace WebApp.Core.Application.Feature.Managment.Accounts.Commands.Register
{
    public static class Register
    {
        #region Command
        public record Command(string UserName, string Email, string Password, string PhoneNumber, DateTime DateOfBirth) : IRequest<RegisterUserResultDto>;
        #endregion

        #region Handler
        public class Handler : IRequestHandler<Command, RegisterUserResultDto>
        {
            private readonly IApplicationUserRepository _applicationUserRepository;

            public Handler(IApplicationUserRepository applicationUserRepository)
            {
                _applicationUserRepository = applicationUserRepository;
            }
            public async Task<RegisterUserResultDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = ApplicationUser.Create(request.Adapt<RegisterUserDto>());
                var result = await _applicationUserRepository.CreateUserAsync(user, user.PasswordHash);

                if (!result.Succeeded)
                    throw new ArgumentException(string.Join(", @\n", result.Errors.Select(e => e.Description)));

                return new RegisterUserResultDto(true, "User registered successfully", user.Id);
            }
        }
        #endregion

        #region Validator
        public class Validator : AbstractValidator<Command>
        {
            private readonly IApplicationUserRepository _applicationUserRepository;

            public Validator(IApplicationUserRepository applicationUserRepository)
            {
                _applicationUserRepository = applicationUserRepository;

                CascadeMode = CascadeMode.Stop;

                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email is required")
                    .EmailAddress().WithMessage("Invalid email format")
                    .MustAsync(BeEmailUnique).WithMessage("Email already exists")
                    .DependentRules(() =>
                    {
                        RuleFor(x => x.PhoneNumber)
                           .NotEmpty().WithMessage("PhoneNumber is required")
                           .Matches(@"^\d+$").WithMessage("PhoneNumber must contain only digits")
                           .MustAsync(BePhoneNumberUnique).WithMessage("PhoneNumber already exists");
                    });
            }

            private async Task<bool> BeEmailUnique(string email, CancellationToken cancellationToken)
            =>await _applicationUserRepository.GetByEmailAsync(email) == null;
               
            private async Task<bool> BePhoneNumberUnique(string phoneNumber, CancellationToken cancellationToken)
            => await _applicationUserRepository.GetUserByPhoneNumberAsync(phoneNumber) == null;

        }
        #endregion
    }
}

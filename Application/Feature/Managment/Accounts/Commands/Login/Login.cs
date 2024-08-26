using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WebApp.Core.Domain.AggregatesModel.Managment.UsersAggregator;
using WebApp.Core.Domain.AggregatesModel.Managment.UsersAggregator.DomainResult;
using WebApp.Core.Domain.Repositories.Managment;
using WebApp.Core.Infrastructure.Services;

namespace WebApp.Core.Application.Feature.Managment.Accounts.Commands.Login
{
    public static class Login
    {
        #region Commands
        public record Command(string Email, string Password) : IRequest<LoginUserResultDto>;

        #endregion

        #region Handler

        public class Handler : IRequestHandler<Command, LoginUserResultDto>
        {
            private readonly IApplicationUserRepository _applicationUserRepository;
            private readonly SignInManager<ApplicationUser?> _signInManager;
            private readonly ITokenService _tokenService;

            public Handler(IApplicationUserRepository applicationUserRepository, SignInManager<ApplicationUser?> signInManager, ITokenService tokenService)
            {
                _applicationUserRepository = applicationUserRepository;
                _signInManager = signInManager;
                _tokenService = tokenService;
            }
            public async Task<LoginUserResultDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var _user = await _applicationUserRepository.GetByEmailAsync(request.Email);
                await _signInManager.SignInAsync(_user, true);
                var token = await _tokenService.CreateTokenAsync(_user);
                return new LoginUserResultDto(true, "Login successful", token);
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
                        .NotEmpty().WithMessage("Email Wrong")
                        .NotNull().WithMessage("Email Wrong")
                        .MustAsync(BeExist).WithMessage("User Not Exist")
                        .MustAsync(IsRightPassword).WithMessage("Password Wrong").DependentRules(() =>
                        {
                            RuleFor(x => x.Password)
                                .NotEmpty().WithMessage("Password Wrong")
                                .NotNull().WithMessage("Password Wrong");
                        });
                }

                private async Task<bool> IsRightPassword(Command command, string userName, CancellationToken cancellation)
                {
                    var user = await _applicationUserRepository.GetByEmailAsync(command.Email);
                    return user != null && await _applicationUserRepository.CheckPassword(user, command.Password);
                }

                private async Task<bool> BeExist(string email, CancellationToken cancellation)
                {
                    var user = await _applicationUserRepository.GetByEmailAsync(email);
                    return user != null;
                }
            }

            #endregion
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Application.Feature.Managment.Accounts.Commands.Login;
using WebApp.Core.Application.Feature.Managment.Accounts.Commands.Register;
using WebApp.Core.Domain.AggregatesModel.Managment.UsersAggregator.DomainResult;

namespace WebApp.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region Methods
        [HttpPost("Register")]
        public async Task<RegisterUserResultDto> Register([FromBody] Register.Command command)
          => await _mediator.Send(command);

        [HttpPost("Login")]
        public async Task<LoginUserResultDto> Login([FromBody] Login.Command command)
         => await _mediator.Send(command);
        #endregion
    }
}

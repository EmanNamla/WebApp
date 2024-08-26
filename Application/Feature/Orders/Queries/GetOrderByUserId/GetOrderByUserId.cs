using FluentValidation;
using Mapster;
using MediatR;
using REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator.DomainResult;
using WebApp.Core.Domain.Repositores;
using WebApp.Core.Domain.Repositories.Managment;

namespace WebApp.Core.Application.Feature.Orders.Queries.GetOrderByUserId
{
    public static class GetOrderByUserId
    {
        #region Query
        public record Query(string UserId) : IRequest<List<OrderBaseResultDto>>;
        #endregion
        #region Handler
        public class Handler : IRequestHandler<Query, List<OrderBaseResultDto>>
        {
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }
            public async Task<List<OrderBaseResultDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetOrdersByUserIdAsync(request.UserId);
                ArgumentNullException.ThrowIfNull(order, "This user has not placed any order");
                return order.Adapt<List<OrderBaseResultDto>>();
            }

        }
        #endregion
        #region Validator
        public class Validator : AbstractValidator<Query>
        {
            private readonly IApplicationUserRepository _applicationUserRepository;

            public Validator(IApplicationUserRepository applicationUserRepository)
            {
                _applicationUserRepository = applicationUserRepository;
                RuleFor(x => x.UserId)
                    .NotEmpty().WithMessage("UserId cannot be empty")
                    .NotNull().WithMessage("UserId cannot be Null")
                    .MustAsync(BeUserExistAsync).WithMessage("User not found");
            }

            private async Task<bool> BeUserExistAsync(string userId, CancellationToken cancellation)
            {
                var user=await _applicationUserRepository.GetUserByIdAsync(userId);
                return user != null;
            }
        }
        #endregion
    }
}

using MediatR;
using REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator.DomainResult;
using WebApp.Core.Domain.Repositores;
using Mapster;
using FluentValidation;

namespace WebApp.Core.Application.Feature.Orders.Queries.GetOrderById
{
    public static class GetOrder
    {
        #region Query
        public record Query(long OrderId) : IRequest<OrderBaseResultDto>;
        #endregion
        #region Handler
        public class Handler : IRequestHandler<Query, OrderBaseResultDto>
        {
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }
            public async Task<OrderBaseResultDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetByIdAsync(request.OrderId);
                ArgumentNullException.ThrowIfNull(order, "Order Not Exist");
                return order.Adapt<OrderBaseResultDto>();
            }
        }
        #endregion

        #region Validator
        public class Validator : AbstractValidator<Query>
        {
            private readonly IOrderRepository _orderRepository;

            public Validator(IOrderRepository orderRepository)
            {
                RuleFor(x => x.OrderId)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Order Id Required")
                    .NotEmpty().WithMessage("Order Id Required")
                    .MustAsync(BeExistAsync).WithMessage("Order Id Required");
                _orderRepository = orderRepository;
            }
            private async Task<bool> BeExistAsync(long orderId, CancellationToken cancellation)
            {
                var order = await _orderRepository.GetByIdAsync(orderId);
                return order != null;
            }
        }
        #endregion
    }
}

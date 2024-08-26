using FluentValidation;
using MediatR;
using REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator.DomainResult;
using WebApp.Core.Domain.Repositores;

namespace WebApp.Core.Application.Feature.Orders.Queries.GetOrderDetailsByOrderId
{
    public static class GetOrderDetailsByOrderId
    {
        #region Query
        public record Query(long OrderId) : IRequest<List<OrderResultDto>>;
        #endregion

        #region Handler
        public class Handler : IRequestHandler<Query, List<OrderResultDto>>
        {
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }
            public async Task<List<OrderResultDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetOrderDetailsByOrderIdAsync(request.OrderId);
                ArgumentNullException.ThrowIfNull(order, "Order Not Exist");

                return order.Select(detail => new OrderResultDto
                {
                    OrderId = detail.OrderId,
                    Id=detail.OrderId,
                    ProductName = detail.ProductName,
                    Quantity = detail.Quantity,
                    Price = detail.Price,
                    TotalAmount = detail.Amount,
                    UserId = detail.Order.User.UserName,
                }).ToList();
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

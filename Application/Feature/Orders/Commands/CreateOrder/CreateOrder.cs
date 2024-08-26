using FluentValidation;
using MediatR;
using REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator.DomainModel;
using REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator;
using WebApp.Core.Domain.Repositores;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WebApp.Core.Domain.Repositories;

namespace WebApp.Core.Application.Feature.Orders.Commands.CreateOrder
{
    public static class CreateOrder
    {
        #region Command
        public record Command(List<OrderDetailsDto> OrderDetails) : IRequest<long>;
        #endregion

        #region Handler
        public class Handler : IRequestHandler<Command, long>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IUnitOfWork _unitofWork;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IOrderRepository orderRepository, IUnitOfWork unitofWork, IHttpContextAccessor httpContextAccessor)
            {
                _orderRepository = orderRepository;
                _unitofWork = unitofWork;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<long> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("User is not authenticated");
                }
                var orderDto = new OrderDto
                {
                    OrderDetails = request.OrderDetails,
                    UserId = userId
                };

                var order = Order.Create(orderDto);
                await _orderRepository.AddAsync(order, cancellationToken);
                await _unitofWork.CompleteAsync(cancellationToken);
                return order.Id;
            }
        }
        #endregion

        #region Validator
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.OrderDetails)
                   .NotNull().WithMessage("Order details are required")
                   .NotEmpty().WithMessage("Order details cannot be empty")
                .ForEach(orderDetail =>
                {
                   orderDetail.NotNull().WithMessage("Order detail cannot be null");

                   orderDetail.ChildRules(detail =>
                   {
                     detail.RuleFor(x => x.ProductName)
                     .NotEmpty().WithMessage("Product name is required");

                      detail.RuleFor(x => x.Quantity)
                     .GreaterThan(0).WithMessage("Quantity must be greater than zero");

                      detail.RuleFor(x => x.Price)
                     .GreaterThan(0).WithMessage("Price must be greater than zero");
                   });
                });
            }
        }
        #endregion
    }
}


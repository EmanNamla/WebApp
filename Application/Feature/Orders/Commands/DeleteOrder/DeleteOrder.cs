using FluentValidation;
using MediatR;
using WebApp.Core.Domain.Repositores;
using WebApp.Core.Domain.Repositories;


namespace WebApp.Core.Application.Feature.Orders.Commands.DeleteOrder
{
    public static class DeleteOrder
    {
        #region Command
        public record Command(long Id) : IRequest<long>;
        #endregion

        #region Handler
        public class Handler : IRequestHandler<Command, long>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IOrderRepository orderRepository,IUnitOfWork unitOfWork)
            {
                _orderRepository = orderRepository;
                _unitOfWork = unitOfWork;
            }
            public async Task<long> Handle(Command request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetByIdAsync(request.Id);

                ArgumentNullException.ThrowIfNull(order, "Order Id Not Exist");
                await _orderRepository.DeleteAsync(request.Id);
                await _unitOfWork.CompleteAsync(cancellationToken);
                return order.Id;
            }
        }
        #endregion

        #region Validator
        public class Validator : AbstractValidator<Command>
        {
            private readonly IOrderRepository _orderRepository;

            public Validator(IOrderRepository orderRepository)
            {
                RuleFor(x => x.Id)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Order Id Required")
                    .NotEmpty().WithMessage("Order Id Required")
                    .MustAsync(BeExistAsync).WithMessage("Order Id Required");
                _orderRepository = orderRepository;
            }
            private async Task<bool> BeExistAsync(long orderId, CancellationToken cancellation)
            {
                var order = await _orderRepository.GetByIdAsync(orderId);
                return order is not null;
            }
        }
        #endregion
    }
}

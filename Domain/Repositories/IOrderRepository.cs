using REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator;

namespace WebApp.Core.Domain.Repositores
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(long id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task AddAsync(Order order,CancellationToken cancellationToken);
        Task DeleteAsync(long id);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(long orderId);
    }
}

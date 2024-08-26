using Microsoft.EntityFrameworkCore;
using REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator;
using WebApp.Core.Domain.Repositores;
using WebApp.Core.Infrastructure.Database;
namespace WebApp.Core.Infrastructure.Repositores
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Context _context;

        public OrderRepository(Context context)
        {
            _context = context;
        }
        public async Task<Order> GetByIdAsync(long id)
            => await _context.Orders
                                 .Include(o => o.OrderDetails)
                                 .FirstOrDefaultAsync(o => o.Id == id);


        public async Task<IEnumerable<Order>> GetAllAsync()
            => await _context.Orders
                                 .Include(o => o.OrderDetails)
                                 .ToListAsync();

        public async Task AddAsync(Order order, CancellationToken cancellationToken )
        => await _context.Orders.AddAsync(order, cancellationToken);

        public async Task DeleteAsync(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await Task.CompletedTask;
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
            => await _context.Orders
                .Where(order => order.UserId == userId)
                .Include(order => order.OrderDetails)
                .ToListAsync();

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(long orderId)
            => await _context.OrderDetails
                .Where(detail => detail.OrderId == orderId)
                .Include(ds => ds.Order).ThenInclude(o => o.User)
                .ToListAsync();

    }

}

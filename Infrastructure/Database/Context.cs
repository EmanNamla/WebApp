using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator;
using WebApp.Core.Domain.AggregatesModel.Managment.UsersAggregator;

namespace WebApp.Core.Infrastructure.Database
{
    public class Context:IdentityDbContext<ApplicationUser>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

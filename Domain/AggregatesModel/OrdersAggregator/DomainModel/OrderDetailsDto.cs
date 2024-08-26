using WebApp.Framwork.Domain.Interfaces;
namespace REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator.DomainModel
{
    public class OrderDetailsDto : IDto
    {
        public long? Id { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

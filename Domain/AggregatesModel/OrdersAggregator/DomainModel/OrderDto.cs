using WebApp.Framwork.Domain.Interfaces;

namespace REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator.DomainModel
{
    public record OrderDto : IDto
    {
        public long? Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string UserId { get;  set; }
        public List<OrderDetailsDto> OrderDetails { get; set; }
    }
}
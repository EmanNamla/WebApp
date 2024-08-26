using WebApp.Framwork.Domain.Interfaces;

namespace REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator.DomainResult
{
    public record OrderBaseResultDto : IDto
    {
        public long? Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string UserId { get; set; }
    }

    public record OrderResultDto : OrderBaseResultDto
    {
        public long OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
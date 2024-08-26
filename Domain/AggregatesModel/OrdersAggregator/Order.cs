using System.ComponentModel.DataAnnotations.Schema;
using REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator.DomainModel;
using WebApp.Core.Domain.AggregatesModel.Managment.UsersAggregator;
using WebApp.Framwork.Domain.Common;
using WebApp.Framwork.Domain.Interfaces;

namespace REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator
{

    [Table("Orders")]
    public class Order : EntityBase, IAggregateRoot
    {
        #region Constructors
        protected Order()
        {
        }

        private Order(OrderDto dto)
        {
            OrderDate = DateTime.UtcNow;
            UserId = dto.UserId;
            AddOrderDetails(dto.OrderDetails);
            TotalAmount = CalculateTotalAmount();
        }

        #endregion

        #region PrivateProperties
        private readonly List<OrderDetail> _orderDetails = new List<OrderDetail>();
        #endregion

        #region Public Properties
        public virtual DateTime OrderDate { get; private set; }
        public virtual decimal TotalAmount { get; private set; }
        public virtual string UserId { get; private set; }
        public virtual ApplicationUser User { get; private set; }
        public virtual IReadOnlyCollection<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();
        #endregion

        #region Business Logic
        private decimal CalculateTotalAmount()
        {
            return _orderDetails.Sum(detail => detail.Quantity * detail.Price);
        }

        #endregion

        #region Helpers
        private void AddOrderDetails(List<OrderDetailsDto> dto) =>
            dto.ForEach(detail => _orderDetails.Add(new OrderDetail(detail)));
        #endregion

        #region Factory Method
        public static Order Create(OrderDto dto) => new Order(dto);
        #endregion
    }
}
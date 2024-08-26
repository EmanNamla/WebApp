using REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator.DomainModel;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Framwork.Domain.Common;

namespace REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator
{
    [Table("OrderDetails")]
    public class OrderDetail : EntityBase
    {
        #region Constructors
        protected OrderDetail()
        {

        }
        public OrderDetail(OrderDetailsDto dto)
        {
            ProductName = dto.ProductName;
            Quantity = dto.Quantity;
            Price = dto.Price;
        }

        #endregion

        #region Private Properties

        #endregion

        #region Public Properties
        public virtual Order Order { get; private set; }
        public virtual long OrderId { get; private set; }
        public virtual string ProductName { get; private set; }
        public virtual int Quantity { get; private set; }
        public virtual decimal Price { get; private set; }
        public decimal Amount => Quantity * Price;
        #endregion

        #region Business Logic

        #endregion

        #region Helpers

        #endregion

        #region Factory Method

        #endregion
    }
}

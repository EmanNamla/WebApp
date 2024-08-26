using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using REPOTECH.Core.Domain.AggregatesModel.Managements.OrdersAggregator.DomainResult;
using WebApp.Core.Application.Feature.Orders.Commands.CreateOrder;
using WebApp.Core.Application.Feature.Orders.Commands.DeleteOrder;
using WebApp.Core.Application.Feature.Orders.Queries.GetOrderById;
using WebApp.Core.Application.Feature.Orders.Queries.GetOrderByUserId;
using WebApp.Core.Application.Feature.Orders.Queries.GetOrderDetailsByOrderId;

namespace WebApp.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region Methods
        [HttpPost("Create")]
        public async Task<long> Create([FromBody] CreateOrder.Command command)
        => await _mediator.Send(command);


        [HttpDelete("{id}")]
        public async Task<long> Delete([FromRoute] long id)
        => await _mediator.Send(new DeleteOrder.Command(id));

        [HttpGet("{id}")]
        public async Task<OrderBaseResultDto> Get([FromRoute] long id)
            =>await _mediator.Send(new GetOrder.Query(id));

        [HttpGet("GetOrderByUserId/{id}")]
        public async Task<IEnumerable<OrderBaseResultDto>> Get([FromRoute] string id)
        => await _mediator.Send(new GetOrderByUserId.Query(id));


        [HttpGet("GetOrderDetailsByOrderId/{id}")]
        public async Task<List<OrderResultDto>> GetDetails([FromRoute] long id)
        => await _mediator.Send(new GetOrderDetailsByOrderId.Query(id));



        #endregion
    }   }

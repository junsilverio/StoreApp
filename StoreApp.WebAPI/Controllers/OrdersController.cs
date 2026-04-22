using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.DTOs;
using StoreApp.Application.Features.Orders.Commands;
using StoreApp.Application.Features.Orders.Queries;

namespace StoreApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAll(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetAllOrdersQuery(), cancellationToken));

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id), cancellationToken);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPatch("{id:int}/status")]
        public async Task<ActionResult<OrderDto>> UpdateStatus(int id, [FromBody] int status, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateOrderStatusCommand(id, status), cancellationToken);
            return result == null ? NotFound() : Ok(result);
        }
    }
}

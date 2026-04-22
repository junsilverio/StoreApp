using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.DTOs;
using StoreApp.Application.Features.Stock.Commands;
using StoreApp.Application.Features.Stock.Queries;

namespace StoreApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StocksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StocksController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<StockDto>>> GetAll(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetAllStocksQuery(), cancellationToken));

        [HttpGet("product/{productId:int}")]
        public async Task<ActionResult<List<StockDto>>> GetByProduct(int productId, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetStockByProductQuery(productId), cancellationToken));

        [HttpPut]
        public async Task<ActionResult<StockDto>> Update([FromBody] UpdateStockCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result == null ? NotFound() : Ok(result);
        }
    }
}

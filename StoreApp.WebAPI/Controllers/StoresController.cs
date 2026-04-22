using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.DTOs;
using StoreApp.Application.Features.Stores.Queries;

namespace StoreApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StoresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StoresController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<StoreDto>>> GetAll(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetAllStoresQuery(), cancellationToken));
    }
}

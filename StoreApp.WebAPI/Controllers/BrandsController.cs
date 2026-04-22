using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.DTOs;
using StoreApp.Application.Features.Brands.Queries;

namespace StoreApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<BrandDto>>> GetAll(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetAllBrandsQuery(), cancellationToken));
    }
}

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Common.Interfaces;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.Features.Orders.Queries
{
    public record GetAllOrdersQuery : IRequest<List<OrderDto>>;

    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.AsNoTracking().ToListAsync(cancellationToken);
            return _mapper.Map<List<OrderDto>>(orders);
        }
    }
}

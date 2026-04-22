using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Common.Interfaces;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.Features.Stores.Queries
{
    public record GetAllStoresQuery : IRequest<List<StoreDto>>;

    public class GetAllStoresQueryHandler : IRequestHandler<GetAllStoresQuery, List<StoreDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllStoresQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<StoreDto>> Handle(GetAllStoresQuery request, CancellationToken cancellationToken)
        {
            var stores = await _context.Stores.AsNoTracking().ToListAsync(cancellationToken);
            return _mapper.Map<List<StoreDto>>(stores);
        }
    }
}

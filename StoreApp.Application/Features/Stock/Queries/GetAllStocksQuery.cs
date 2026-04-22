using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Common.Interfaces;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.Features.Stock.Queries
{
    public record GetAllStocksQuery : IRequest<List<StockDto>>;

    public class GetAllStocksQueryHandler : IRequestHandler<GetAllStocksQuery, List<StockDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllStocksQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<StockDto>> Handle(GetAllStocksQuery request, CancellationToken cancellationToken)
        {
            var stocks = await _context.Stocks.AsNoTracking().ToListAsync(cancellationToken);
            return _mapper.Map<List<StockDto>>(stocks);
        }
    }
}

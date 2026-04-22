using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Common.Interfaces;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.Features.Stock.Queries
{
    public record GetStockByProductQuery(int ProductId) : IRequest<List<StockDto>>;

    public class GetStockByProductQueryHandler : IRequestHandler<GetStockByProductQuery, List<StockDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStockByProductQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<StockDto>> Handle(GetStockByProductQuery request, CancellationToken cancellationToken)
        {
            var stocks = await _context.Stocks.AsNoTracking()
                .Where(s => s.ProductId == request.ProductId)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<StockDto>>(stocks);
        }
    }
}

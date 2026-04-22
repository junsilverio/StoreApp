using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Common.Interfaces;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.Features.Stock.Commands
{
    public record UpdateStockCommand(int StoreId, int ProductId, int Quantity) : IRequest<StockDto?>;

    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, StockDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateStockCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StockDto?> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            // Stock.BrandId maps to Store_Id column
            var stock = await _context.Stocks
                .FirstOrDefaultAsync(s => s.BrandId == request.StoreId && s.ProductId == request.ProductId, cancellationToken);
            if (stock == null) return null;

            stock.Quantity = request.Quantity;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<StockDto>(stock);
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Common.Interfaces;
using StoreApp.Application.DTOs;
using AutoMapper;

namespace StoreApp.Application.Features.Products.Commands
{
    public record UpdateProductCommand(
        int Id,
        string ProductName,
        int BrandId,
        int CategoryId,
        int ModelYear,
        decimal ListPrice
    ) : IRequest<ProductDto?>;

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (product == null) return null;

            product.ProductName = request.ProductName;
            product.BrandId = request.BrandId;
            product.CategoryId = request.CategoryId;
            product.ModelYear = request.ModelYear;
            product.ListPrice = request.ListPrice;
            product.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ProductDto>(product);
        }
    }
}

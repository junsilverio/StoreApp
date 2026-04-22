using MediatR;
using StoreApp.Application.Common.Interfaces;
using StoreApp.Application.DTOs;
using StoreApp.Domain.Entities;
using AutoMapper;

namespace StoreApp.Application.Features.Products.Commands
{
    public record CreateProductCommand(
        string ProductName,
        int BrandId,
        int CategoryId,
        int ModelYear,
        decimal ListPrice
    ) : IRequest<ProductDto>;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                ProductName = request.ProductName,
                BrandId = request.BrandId,
                CategoryId = request.CategoryId,
                ModelYear = request.ModelYear,
                ListPrice = request.ListPrice,
                CreatedDate = DateTime.UtcNow
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ProductDto>(product);
        }
    }
}

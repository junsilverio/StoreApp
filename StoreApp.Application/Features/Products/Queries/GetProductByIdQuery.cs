using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Common.Interfaces;
using StoreApp.Application.DTOs;
using AutoMapper;

namespace StoreApp.Application.Features.Products.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }
    }
}

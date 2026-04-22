using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Common.Interfaces;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.Features.Customers.Queries
{
    public record GetCustomerByIdQuery(int Id) : IRequest<CustomerDto?>;

    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            return customer == null ? null : _mapper.Map<CustomerDto>(customer);
        }
    }
}

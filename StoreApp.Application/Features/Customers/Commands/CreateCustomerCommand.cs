using AutoMapper;
using MediatR;
using StoreApp.Application.Common.Interfaces;
using StoreApp.Application.DTOs;
using StoreApp.Domain.Entities;

namespace StoreApp.Application.Features.Customers.Commands
{
    public record CreateCustomerCommand(
        string FirstName,
        string LastName,
        string? Email,
        string? Phone,
        string? City,
        string? State,
        string? ZipCode
    ) : IRequest<CustomerDto>;

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email ?? string.Empty,
                Phone = request.Phone ?? string.Empty,
                City = request.City ?? string.Empty,
                State = request.State ?? string.Empty,
                ZipCode = request.ZipCode ?? string.Empty,
                CreatedDate = DateTime.UtcNow
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CustomerDto>(customer);
        }
    }
}

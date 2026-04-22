using AutoMapper;
using MediatR;
using StoreApp.Application.Common.Interfaces;
using StoreApp.Application.DTOs;
using StoreApp.Domain.Entities;

namespace StoreApp.Application.Features.Orders.Commands
{
    public record CreateOrderCommand(
        int CustomerId,
        int OrderStatus,
        DateTime OrderDate,
        DateTime RequiredDate,
        DateTime ShippedDate,
        int StoreId,
        int StaffId
    ) : IRequest<OrderDto>;

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                CustomerId = request.CustomerId,
                OrderStatus = request.OrderStatus,
                OrderDate = request.OrderDate,
                RequiredDate = request.RequiredDate,
                ShippedDate = request.ShippedDate,
                StoreId = request.StoreId,
                StaffId = request.StaffId,
                CreatedDate = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<OrderDto>(order);
        }
    }
}

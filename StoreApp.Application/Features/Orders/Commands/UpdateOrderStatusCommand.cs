using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Common.Interfaces;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.Features.Orders.Commands
{
    public record UpdateOrderStatusCommand(int Id, int OrderStatus) : IRequest<OrderDto?>;

    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, OrderDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateOrderStatusCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto?> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);
            if (order == null) return null;

            order.OrderStatus = request.OrderStatus;
            order.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<OrderDto>(order);
        }
    }
}

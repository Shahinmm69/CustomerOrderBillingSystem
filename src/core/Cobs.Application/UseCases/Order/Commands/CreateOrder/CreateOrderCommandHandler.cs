using Cobs.Application.Notification.CreateInvoice;

namespace Cobs.Application.UseCases.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly ICobsDbContext _context;
        private readonly IMediator _mediator;

        public CreateOrderCommandHandler(ICobsDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Domain.Entities.Order
            {
                CustomerId = request.CustomerId,
                Product = request.Product,
                Quantity = request.Quantity,
                TotalAmount = request.TotalAmount,
                OrderDate = request.OrderDate
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);


            await _mediator.Publish(new CreateInvoiceNotification
            {
                OrderId = order.Id,
                TotalAmount = order.TotalAmount
            }, cancellationToken);

            return order.Id;
        }
    }
}

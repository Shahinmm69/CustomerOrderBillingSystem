namespace Cobs.Application.UseCases.Order.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdHandler
        : IRequestHandler<GetOrdersByCustomerIdQuery, List<OrderDto>>
    {
        private readonly ICobsDbContext _context;
        public GetOrdersByCustomerIdHandler(ICobsDbContext context) => _context = context;

        public async Task<List<OrderDto>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var customerExists = await _context.Customers
                .AsNoTracking()
                .AnyAsync(c => c.Id == request.CustomerId, cancellationToken);

            if (!customerExists)
                throw new KeyNotFoundException($"مشتری با شناسه {request.CustomerId} یافت نشد.");

            var orders = await _context.Orders
                .AsNoTracking()
                .Where(o => o.CustomerId == request.CustomerId)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    Product = o.Product,
                    Quantity = o.Quantity,
                    TotalAmount = o.TotalAmount,
                    OrderDate = o.OrderDate
                })
                .ToListAsync(cancellationToken);

            if (!orders.Any())
                throw new KeyNotFoundException($"هیچ سفارشی برای مشتری با شناسه {request.CustomerId} یافت نشد.");

            return orders;
        }
    }

}

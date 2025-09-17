namespace Cobs.Application.UseCases.Order.Queries.GetCurrentUserOrders
{
    public class GetCurrentUserOrdersQueryHandler
        : IRequestHandler<GetCurrentUserOrdersQuery, List<OrderDto>>
    {
        private readonly ICobsDbContext _context;
        public GetCurrentUserOrdersQueryHandler(ICobsDbContext context) => _context = context;

        public async Task<List<OrderDto>> Handle(GetCurrentUserOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .Where(o => o.CustomerId == request.CurrentUserId)
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
                throw new KeyNotFoundException($"هیچ سفارشی برای کاربر جاری یافت نشد.");

            return orders;
        }
    }
}

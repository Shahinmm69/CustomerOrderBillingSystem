namespace Cobs.Application.UseCases.Order.Queries.GetAllOrders
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
    {
        private readonly ICobsDbContext _context;
        public GetAllOrdersHandler(ICobsDbContext context) => _context = context;

        public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Orders
                .AsNoTracking()
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
        }
    }
}

namespace Cobs.Application.UseCases.Customer.Queries.GetCurrentUserCustomer
{
    public class GetCurrentUserCustomerQueryHandler : IRequestHandler<GetCurrentUserCustomerQuery, CustomerDto?>
    {
        private readonly ICobsDbContext _context;
        public GetCurrentUserCustomerQueryHandler(ICobsDbContext context) => _context = context;

        public async Task<CustomerDto?> Handle(GetCurrentUserCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers
                .AsNoTracking()
                .Where(c => c.Id == request.CurrentUserId)
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    WalletId = c.WalletId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Role = c.Role
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

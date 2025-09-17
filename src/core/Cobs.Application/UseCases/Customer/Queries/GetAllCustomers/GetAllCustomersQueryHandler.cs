namespace Cobs.Application.UseCases.Customer.Queries.GetAllCustomers
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
    {
        private readonly ICobsDbContext _context;
        public GetAllCustomersHandler(ICobsDbContext context) => _context = context;

        public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers
                .AsNoTracking()
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    WalletId = c.WalletId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Role = c.Role
                })
                .ToListAsync(cancellationToken);
        }
    }
}

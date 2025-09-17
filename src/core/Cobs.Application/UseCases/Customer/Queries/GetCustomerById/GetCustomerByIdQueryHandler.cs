namespace Cobs.Application.UseCases.Customer.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
    {
        private readonly ICobsDbContext _context;
        public GetCustomerByIdQueryHandler(ICobsDbContext context) => _context = context;

        public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.CustomerId, cancellationToken);

            if (customer == null)
                throw new KeyNotFoundException($"مشتری با شناسه {request.CustomerId} یافت نشد.");

            return new CustomerDto
            {
                Id = customer.Id,
                WalletId = customer.WalletId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Role = customer.Role
            };
        }
    }
}

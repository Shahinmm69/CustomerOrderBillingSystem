namespace Cobs.Application.UseCases.Invoice.Queries.GetInvoicesByCustomerId
{
    public record GetInvoicesByCustomerIdHandler
        : IRequestHandler<GetInvoicesByCustomerIdQuery, List<InvoiceDto>>
    {
        private readonly ICobsDbContext _context;
        public GetInvoicesByCustomerIdHandler(ICobsDbContext context) => _context = context;

        public async Task<List<InvoiceDto>> Handle(GetInvoicesByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var customerExists = await _context.Customers
                .AsNoTracking()
                .AnyAsync(c => c.Id == request.CustomerId, cancellationToken);

            if (!customerExists)
                throw new KeyNotFoundException($"مشتری با شناسه {request.CustomerId} یافت نشد.");

            var invoices = await _context.Invoices
                .AsNoTracking()
                .Where(i => i.Order!.CustomerId == request.CustomerId)
                .OrderByDescending(i => i.DueDate)
                .Select(i => new InvoiceDto
                {
                    Id = i.Id,
                    OrderId = i.OrderId,
                    Amount = i.Amount,
                    DueDate = i.DueDate,
                    Status = i.Status
                })
                .ToListAsync(cancellationToken);

            if (!invoices.Any())
                throw new KeyNotFoundException($"هیچ فاکتوری برای مشتری با شناسه {request.CustomerId} یافت نشد.");

            return invoices;
        }
    }

}

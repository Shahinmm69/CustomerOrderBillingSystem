namespace Cobs.Application.UseCases.Invoice.Queries.GetCurrentUserInvoices
{
    public record GetCurrentUserInvoicesQueryHandler
        : IRequestHandler<GetCurrentUserInvoicesQuery, List<InvoiceDto>>
    {
        private readonly ICobsDbContext _context;
        public GetCurrentUserInvoicesQueryHandler(ICobsDbContext context) => _context = context;

        public async Task<List<InvoiceDto>> Handle(GetCurrentUserInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _context.Invoices
                .AsNoTracking()
                .Where(i => i.Order!.CustomerId == request.CurrentUserId)
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
                throw new KeyNotFoundException($"هیچ فاکتوری برای کاربر با شناسه {request.CurrentUserId} یافت نشد.");

            return invoices;
        }
    }

}

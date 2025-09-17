namespace Cobs.Application.UseCases.Invoice.Queries.GetAllInvoices
{
    public record GetAllInvoicesHandler : IRequestHandler<GetAllInvoicesQuery, List<InvoiceDto>>
    {
        private readonly ICobsDbContext _context;
        public GetAllInvoicesHandler(ICobsDbContext context) => _context = context;

        public async Task<List<InvoiceDto>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Invoices
                .AsNoTracking()
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
        }
    }
}

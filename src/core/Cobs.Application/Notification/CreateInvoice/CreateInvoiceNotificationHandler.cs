namespace Cobs.Application.Notification.CreateInvoice
{
    public record CreateInvoiceNotificationHandler : INotificationHandler<CreateInvoiceNotification>
    {
        private readonly ICobsDbContext _context;

        public CreateInvoiceNotificationHandler(ICobsDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateInvoiceNotification notification, CancellationToken cancellationToken)
        {
            var invoice = new Invoice
            {
                OrderId = notification.OrderId,
                Amount = notification.TotalAmount,
                DueDate = DateTime.UtcNow.AddDays(7),
                Status = Status.Pending
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

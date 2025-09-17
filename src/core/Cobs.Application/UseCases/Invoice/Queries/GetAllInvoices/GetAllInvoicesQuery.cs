namespace Cobs.Application.UseCases.Invoice.Queries.GetAllInvoices
{
    public record GetAllInvoicesQuery() : IRequest<List<InvoiceDto>>;
}

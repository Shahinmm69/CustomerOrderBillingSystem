namespace Cobs.Application.UseCases.Invoice.Queries.GetCurrentUserInvoices
{
    public record GetCurrentUserInvoicesQuery(int CurrentUserId) : IRequest<List<InvoiceDto>>;
}

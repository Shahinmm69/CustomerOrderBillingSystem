namespace Cobs.Application.UseCases.Invoice.Queries.GetInvoicesByCustomerId
{
    public record GetInvoicesByCustomerIdQuery(int CustomerId) : IRequest<List<InvoiceDto>>;
}

namespace Cobs.Application.UseCases.Invoice.Commands.PayInvoice
{
    public record PayInvoiceCommand(int InvoiceId, int CurrentUserId) : IRequest<Unit>;
}

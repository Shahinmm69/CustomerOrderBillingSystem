namespace Cobs.Application.UseCases.Invoice.Commands.PayInvoice
{
    public class PayInvoiceCommandValidator : AbstractValidator<PayInvoiceCommand>
    {
        public PayInvoiceCommandValidator()
        {
            RuleFor(x => x.InvoiceId)
                .GreaterThan(0).WithMessage("شناسه فاکتور معتبر نیست.");
        }
    }
}

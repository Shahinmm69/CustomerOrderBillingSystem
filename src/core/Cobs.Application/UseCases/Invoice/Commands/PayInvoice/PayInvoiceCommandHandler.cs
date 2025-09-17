namespace Cobs.Application.UseCases.Invoice.Commands.PayInvoice
{
    public class PayInvoiceCommandHandler : IRequestHandler<PayInvoiceCommand, Unit>
    {
        private readonly IPaymentService _paymentService;

        public PayInvoiceCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<Unit> Handle(PayInvoiceCommand request, CancellationToken cancellationToken)
        {
            await _paymentService.PayAsync(request.InvoiceId, request.CurrentUserId, cancellationToken);
            return Unit.Value;
        }
    }
}

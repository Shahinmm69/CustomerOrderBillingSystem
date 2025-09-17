namespace Cobs.Application.UseCases.Wallet.Commands.Deposit
{
    public class DepositCommandHandler : IRequestHandler<DepositCommand, decimal>
    {
        private readonly IPaymentService _paymentService;

        public DepositCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<decimal> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            var result = await _paymentService.DepositAsync(request.WalletId, request.Amount, cancellationToken);
            return result;
        }
    }
}

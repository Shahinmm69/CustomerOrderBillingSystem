namespace Cobs.Application.UseCases.Wallet.Commands.Deposit
{
    public record DepositCommand(int WalletId, decimal Amount) : IRequest<decimal>;
}

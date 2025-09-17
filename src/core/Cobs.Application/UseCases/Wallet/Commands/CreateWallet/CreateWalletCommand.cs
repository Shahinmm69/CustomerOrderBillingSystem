namespace Cobs.Application.UseCases.Wallet.Commands.CreateWallet
{
    public record CreateWalletCommand(decimal WalletBalance) : IRequest<int>;
}

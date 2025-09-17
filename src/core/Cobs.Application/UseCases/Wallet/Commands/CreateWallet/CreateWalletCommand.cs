namespace Cobs.Application.UseCases.Wallet.Commands.CreateWallet
{
    public record CreateWalletCommand() : IRequest<Domain.Entities.Wallet>;
}

namespace Cobs.Application.UseCases.Wallet.Commands.CreateWallet
{
    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, int>
    {
        private readonly ICobsDbContext _context;

        public CreateWalletCommandHandler(ICobsDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = new Domain.Entities.Wallet
            {
                WalletBalance = request.WalletBalance
            };

            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync(cancellationToken);

            return wallet.Id;
        }
    }
}

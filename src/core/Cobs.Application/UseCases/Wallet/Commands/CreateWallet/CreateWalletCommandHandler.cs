namespace Cobs.Application.UseCases.Wallet.Commands.CreateWallet
{
    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, Domain.Entities.Wallet>
    {
        private readonly ICobsDbContext _context;

        public CreateWalletCommandHandler(ICobsDbContext context)
        {
            _context = context;
        }

        public Task<Domain.Entities.Wallet> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = new Domain.Entities.Wallet
            {
                WalletBalance = 0
            };

            _context.Wallets.Add(wallet);
            return Task.FromResult(wallet);
        }
    }
}

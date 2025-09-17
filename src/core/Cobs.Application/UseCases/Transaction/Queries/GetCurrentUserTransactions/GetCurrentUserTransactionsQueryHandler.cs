namespace Cobs.Application.UseCases.Transaction.Queries.GetCurrentUserTransactions
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<TransactionDto>>
    {
        private readonly ICobsDbContext _context;

        public GetTransactionsQueryHandler(ICobsDbContext context)
        {
            _context = context;
        }

        public async Task<List<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _context.Transactions
                .AsNoTracking()
                .Include(t => t.Wallet)
                .ThenInclude(w => w.Customer)
                .Where(t => t.Wallet.Customer.Id == request.CurrentUserId)
                .OrderByDescending(t => t.TransactionDate)
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    TransactionAmount = t.TransactionAmount,
                    WalletBalance = t.WalletBalance,
                    TransactionDate = t.TransactionDate
                })
                .ToListAsync(cancellationToken);

            return transactions;
        }
    }
}

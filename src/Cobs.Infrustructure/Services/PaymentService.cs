namespace Cobs.Infrustructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IDbContextFactory<CobsDbContext> _contextFactory;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IDbContextFactory<CobsDbContext> contextFactory, ILogger<PaymentService> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task PayAsync(int invoiceId, int currentUserId, CancellationToken cancellationToken)
        {
            const int maxRetry = 3;
            int retryCount = 0;

            while (true)
            {
                await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
                using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var invoice = await context.Invoices
                        .Include(i => i.Order)
                        .ThenInclude(o => o.Customer)
                        .ThenInclude(c => c.Wallet)
                        .FirstOrDefaultAsync(i => i.Id == invoiceId, cancellationToken);

                    if (invoice == null)
                        throw new KeyNotFoundException("فاکتور یافت نشد");

                    if (invoice.Status == Status.Paid)
                        throw new InvalidOperationException("فاکتور قبلا پرداخت شده است");

                    if (invoice.Order.CustomerId != currentUserId)
                        throw new UnauthorizedAccessException("دسترسی ندارید");

                    var wallet = invoice.Order.Customer.Wallet;
                    if (wallet.WalletBalance < invoice.Amount)
                        throw new InvalidOperationException("موجودی کیف پول کافی نیست");

                    wallet.WalletBalance -= invoice.Amount;
                    invoice.Status = Status.Paid;

                    var transactionRecord = new Transaction
                    {
                        WalletId = wallet.Id,
                        TransactionAmount = -invoice.Amount,
                        WalletBalance = wallet.WalletBalance,
                        TransactionDate = DateTime.UtcNow
                    };
                    context.Transactions.Add(transactionRecord);

                    await context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    retryCount++;
                    _logger.LogWarning(ex, "Concurrency conflict on payment. Retry {RetryCount}/{MaxRetry}", retryCount, maxRetry);

                    if (retryCount >= maxRetry)
                        throw new DbUpdateConcurrencyException("پرداخت به دلیل تداخل همزمانی انجام نشد. لطفا دوباره تلاش کنید.", ex);
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }

        public async Task<decimal> DepositAsync(int walletId, decimal amount, CancellationToken cancellationToken)
        {
            const int maxRetry = 3;
            int retryCount = 0;

            while (true)
            {
                await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
                using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var wallet = await context.Wallets
                        .FirstOrDefaultAsync(w => w.Id == walletId, cancellationToken);

                    if (wallet == null)
                        throw new KeyNotFoundException("کیف پول یافت نشد");

                    wallet.WalletBalance += amount;
                    var transactionRecord = new Transaction
                    {
                        WalletId = wallet.Id,
                        TransactionAmount = amount,
                        WalletBalance = wallet.WalletBalance,
                        TransactionDate = DateTime.UtcNow
                    };
                    context.Transactions.Add(transactionRecord);

                    await context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return wallet.WalletBalance;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    retryCount++;
                    _logger.LogWarning(ex, "Concurrency conflict on deposit. Retry {RetryCount}/{MaxRetry}", retryCount, maxRetry);

                    if (retryCount >= maxRetry)
                        throw new DbUpdateConcurrencyException("واریز به دلیل تداخل همزمانی انجام نشد. لطفا دوباره تلاش کنید.", ex);
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}

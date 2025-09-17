namespace Cobs.Application.Contracts.Services
{
    public interface IPaymentService
    {
        Task PayAsync(int invoiceId, int currentUserId, CancellationToken cancellationToken);
        Task<decimal> DepositAsync(int walletId, decimal amount, CancellationToken cancellationToken);
    }
}

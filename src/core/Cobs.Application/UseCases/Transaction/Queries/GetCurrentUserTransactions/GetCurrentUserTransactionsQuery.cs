namespace Cobs.Application.UseCases.Transaction.Queries.GetCurrentUserTransactions
{
    public record GetTransactionsQuery(int CurrentUserId) : IRequest<List<TransactionDto>>;
}

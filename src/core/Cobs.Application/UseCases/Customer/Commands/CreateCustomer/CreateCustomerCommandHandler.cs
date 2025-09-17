using Cobs.Application.UseCases.Customer.Commands.CreateCustomer;
using Cobs.Application.UseCases.Wallet.Commands.CreateWallet;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly ICobsDbContext _context;
    private readonly IMediator _mediator;

    public CreateCustomerCommandHandler(ICobsDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await (_context as DbContext)!.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var walletId = await _mediator.Send(new CreateWalletCommand(0), cancellationToken);
            var customer = new Customer
            {
                WalletId = walletId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Role = request.Role
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(cancellationToken);

            await (_context as DbContext)!.Database.CommitTransactionAsync(cancellationToken);
            return customer.Id;
        }
        catch
        {
            await (_context as DbContext)!.Database.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}

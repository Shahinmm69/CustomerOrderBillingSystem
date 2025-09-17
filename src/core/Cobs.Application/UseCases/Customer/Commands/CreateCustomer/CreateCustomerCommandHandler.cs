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
        var wallet = await _mediator.Send(new CreateWalletCommand(), cancellationToken);
        var customer = new Customer
        {
            Wallet = wallet,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Role = request.Role
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}

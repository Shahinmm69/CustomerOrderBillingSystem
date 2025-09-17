namespace Cobs.Application.UseCases.Customer.Queries.GetAllCustomers
{
    public record GetAllCustomersQuery() : IRequest<List<CustomerDto>>;
}

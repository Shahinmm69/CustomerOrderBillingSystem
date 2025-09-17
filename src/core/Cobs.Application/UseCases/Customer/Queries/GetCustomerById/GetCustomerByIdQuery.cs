namespace Cobs.Application.UseCases.Customer.Queries.GetCustomerById
{
    public record GetCustomerByIdQuery(int CustomerId) : IRequest<CustomerDto>;
}

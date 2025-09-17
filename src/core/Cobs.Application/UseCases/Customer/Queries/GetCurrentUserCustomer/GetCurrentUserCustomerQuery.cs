namespace Cobs.Application.UseCases.Customer.Queries.GetCurrentUserCustomer
{
    public record GetCurrentUserCustomerQuery(int CurrentUserId) : IRequest<CustomerDto>;
}

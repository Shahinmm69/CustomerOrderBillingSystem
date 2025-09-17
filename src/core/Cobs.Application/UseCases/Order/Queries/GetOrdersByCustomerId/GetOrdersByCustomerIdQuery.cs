namespace Cobs.Application.UseCases.Order.Queries.GetOrdersByCustomerId
{
    public record GetOrdersByCustomerIdQuery(int CustomerId) : IRequest<List<OrderDto>>;
}

namespace Cobs.Application.UseCases.Order.Queries.GetAllOrders
{
    public record GetAllOrdersQuery() : IRequest<List<OrderDto>>;
}

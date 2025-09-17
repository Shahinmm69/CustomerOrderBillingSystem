namespace Cobs.Application.UseCases.Order.Queries.GetCurrentUserOrders
{
    public record GetCurrentUserOrdersQuery(int CurrentUserId) : IRequest<List<OrderDto>>;
}

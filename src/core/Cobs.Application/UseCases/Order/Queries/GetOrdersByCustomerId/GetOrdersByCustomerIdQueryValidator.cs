namespace Cobs.Application.UseCases.Order.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdQueryValidator : AbstractValidator<GetOrdersByCustomerIdQuery>
    {
        public GetOrdersByCustomerIdQueryValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("شناسه مشتری معتبر نیست.");
        }
    }
}

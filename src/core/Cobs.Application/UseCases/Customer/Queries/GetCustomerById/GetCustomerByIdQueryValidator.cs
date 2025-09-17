namespace Cobs.Application.UseCases.Customer.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
    {
        public GetCustomerByIdQueryValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("شناسه مشتری معتبر نیست.");
        }
    }
}

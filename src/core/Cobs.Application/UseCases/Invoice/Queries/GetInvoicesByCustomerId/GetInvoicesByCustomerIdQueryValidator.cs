namespace Cobs.Application.UseCases.Invoice.Queries.GetInvoicesByCustomerId
{
    public class GetInvoicesByCustomerIdValidator : AbstractValidator<GetInvoicesByCustomerIdQuery>
    {
        public GetInvoicesByCustomerIdValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("شناسه مشتری معتبر نیست.");
        }
    }
}
